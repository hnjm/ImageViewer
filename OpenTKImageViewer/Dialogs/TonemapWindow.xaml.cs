﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using OpenTKImageViewer.ImageContext;
using OpenTKImageViewer.Tonemapping;
using Xceed.Wpf.Toolkit;

namespace OpenTKImageViewer.Dialogs
{
    /// <summary>
    /// Interaction logic for TonemapWindow.xaml
    /// </summary>
    public partial class TonemapWindow : Window
    {
        private readonly MainWindow parent;
        private readonly TonemapperManager.Settings toneSettings;
        private ListBoxItem draggedItem = null;
        private ToneParameter previousDisplayedItem = null;

        public TonemapWindow(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            
            // clone current tonemapping settings
            toneSettings = parent.Context.Tonemapper.GetSettings();

            UpdateList();

            ListBoxMapper.PreviewMouseLeftButtonUp += OnListMouseUp;
        }

        private void OnListMouseUp(object sender, MouseButtonEventArgs e)
        {
            // release item that was dragged
            draggedItem = null;
        }

        private void ItemOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            // determine if an elemt is being dragged
            draggedItem = sender as ListBoxItem;
        }

        private void ItemOnMouseEnter(object sender, MouseEventArgs args)
        {
            if (draggedItem == null)
                return;
            var target = sender as ListBoxItem;
            if(ReferenceEquals(draggedItem, target) || target == null)
                return;
            
            // swap both elements
            var idx1 = ListBoxMapper.Items.IndexOf(target);
            var idx2 = ListBoxMapper.Items.IndexOf(draggedItem);
            if (idx2 == -1 || idx2 == -1)
                return; // not in list?

            SwapItems(idx1, idx2);
            draggedItem.IsSelected = true;
        }

        public TonemapperManager.Settings GetCurrentSettings()
        {
            return toneSettings;
        }

        private void TonemapWindow_OnClosing(object sender, CancelEventArgs e)
        {
            // set last relative coordinates
            parent.ParentApp.GetConfig().LastTonemapDialogX = (int)(Left - parent.Left);
            parent.ParentApp.GetConfig().LastTonemapDialogY = (int)(Top - parent.Top);

            parent.TonemapDialog = null;
            parent.Context.Tonemapper.RemoveUnusedShader();
        }

        /// <summary>
        /// adding a tonemapping shader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.InitialDirectory = parent.ParentApp.GetShaderPath(ofd);

            if (ofd.ShowDialog() != true) return;
            parent.ParentApp.SetShaderPath(ofd);

            LoadTonemapper(ofd.FileName);
        }

        /// <summary>
        /// attempts to load a tonemapper
        /// </summary>
        /// <param name="filename">path + filename of the tonemapper</param>
        /// <returns>true if tonemapper was succesfully loaded</returns>
        public bool LoadTonemapper(string filename)
        {
            // load shader
            bool success = true;
            parent.EnableOpenGl();
            try
            {
                var param = parent.Context.Tonemapper.LoadShader(filename);

                toneSettings.ToneParameters.Add(param);
                ListBoxMapper.Items.Add(new ToneParameterItem(param, this));
                ListBoxMapper.SelectedIndex = ListBoxMapper.Items.Count - 1;
            }
            catch (Exception exception)
            {
                App.ShowErrorDialog(this, exception.Message);
                success = false;
            }
            parent.DisableOpenGl();
            return success;
        }

        /// <summary>
        /// Updates the list of used tonemappers
        /// </summary>
        private void UpdateList()
        {
            ListBoxMapper.Items.Clear();
            foreach (var toneParameter in toneSettings.ToneParameters)
            {
                var item = new ToneParameterItem(toneParameter, this);
                ListBoxMapper.Items.Add(item);
            }
            if (ListBoxMapper.Items.Count > 0)
                ListBoxMapper.SelectedIndex = 0;

            ListBoxMapper.Items.Insert(toneSettings.ExportPosition, GenerateExportPositionItem());
        }

        /// <summary>
        /// displays the shader dialog on the right side (title, parameters etc.)
        /// </summary>
        /// <param name="parameters">Shader with parameters</param>
        private void DisplayItem(ToneParameter parameters)
        {
            if (ReferenceEquals(parameters, previousDisplayedItem))
                return;
            previousDisplayedItem = parameters;

            // clear previous stack pannel
            var list = StackPanelMapper.Children;
            list.Clear();

            var margin = new Thickness(0.0, 0.0, 0.0, 2.0);

            // add title and description
            list.Add(new TextBlock {Text = parameters.Shader.Name, Margin = margin, TextWrapping = TextWrapping.Wrap, FontSize = 18.0});
            if(parameters.Shader.Description.Length > 0)
                list.Add(new TextBlock { Text = parameters.Shader.Description, Margin = new Thickness(0.0, 0.0, 0.0, 10.0), TextWrapping = TextWrapping.Wrap});

            // Display all settings
            foreach (var para in parameters.Parameters)
            {
                // parameter name
                list.Add(new TextBlock {Text = para.Name + ":", Margin = margin, TextWrapping = TextWrapping.Wrap});

                // parameter input (check box ord number box)
                switch (para.Type)
                {
                    case ShaderLoader.ParameterType.Bool:
                    {
                        // check Box
                        var box = new CheckBox
                        {
                            IsChecked = GetBoolValue(para),
                            Margin = margin
                        };
                        box.Checked += (sender, args) => para.CurrentValue = BoolToDecimal(box.IsChecked);
                        box.Unchecked += (sender, args) => para.CurrentValue = BoolToDecimal(box.IsChecked);
                        para.ValueChanged += (sender, args) => box.IsChecked = para.CurrentValue != (decimal)0.0;
                        list.Add(box);
                    }
                        break;
                    case ShaderLoader.ParameterType.Int:
                    {
                        // use num up down
                        var numBox = new IntegerUpDown
                        {
                            Value = (int) para.CurrentValue,
                            Margin = margin,
                            CultureInfo = App.GetCulture(),
                            Increment = 0
                        };

                        // custom handler for up down button
                        numBox.Spinned += (sender, args) =>
                        {
                            if (args.Direction == SpinDirection.Increase)
                            {
                                para.InvokeAction(ShaderLoader.ActionType.OnAdd);
                            }
                            else if (args.Direction == SpinDirection.Decrease)
                            {
                                para.InvokeAction(ShaderLoader.ActionType.OnSubtract);
                            }
                        };

                        // default value change handler
                        numBox.ValueChanged += (sender, args) =>
                        {
                            if (numBox.Value != null) para.CurrentValue = (decimal)numBox.Value;
                        };
                        para.ValueChanged += (sender, args) => numBox.Value = (int)para.CurrentValue;
                        list.Add(numBox);
                    }
                        break;
                    case ShaderLoader.ParameterType.Float:
                    {
                        var numBox = new DecimalUpDown
                        {
                            Value = para.CurrentValue,
                            Margin = margin,
                            CultureInfo = App.GetCulture(),
                            Increment = 0
                        };

                        // custom handler for up down button
                        numBox.Spinned += (sender, args) =>
                        {
                            if (args.Direction == SpinDirection.Increase)
                            {
                                para.InvokeAction(ShaderLoader.ActionType.OnAdd);
                            }
                            else if(args.Direction == SpinDirection.Decrease)
                            {
                                para.InvokeAction(ShaderLoader.ActionType.OnSubtract);
                            }
                        };

                        // default value change handler
                        numBox.ValueChanged += (sender, args) =>
                        {
                            if (numBox.Value != null) para.CurrentValue =(decimal)numBox.Value;
                        };
                        para.ValueChanged += (sender, args) =>
                        {
                            numBox.Value = para.CurrentValue;
                        };
                        list.Add(numBox);
                    }
                        break;
                }
            }

            // restore default button
            var btn = new Button
            {
                Content = "Restore Defaults",
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0.0, 8.0, 0.0, 0.0),
                Padding = new Thickness(2)
            };
            btn.Click += (sender, args) => parameters.RestoreDefaults();
            list.Add(btn);
        }

        private static bool GetBoolValue(ShaderLoader.Parameter p)
        {
            Debug.Assert(p.Type == ShaderLoader.ParameterType.Bool);
            return p.CurrentValue != (decimal) 0.0;
        }

        private decimal BoolToDecimal(bool? b)
        {
            if (b == null)
                return 0;
            return (bool)b ? 1 : 0;
        }

        class ToneParameterItem : ListBoxItem
        {
            public ToneParameter Parameter { get; private set; }

            public ToneParameterItem(ToneParameter parameter, TonemapWindow parent)
            {
                Parameter = parameter;
                // load images
                var imgDelete = new Image
                {
                    Source = new BitmapImage(new Uri($@"pack://application:,,,/{App.AppName};component/Icons/cancel.png", UriKind.Absolute))
                };

                var imgArrow = new Image
                {
                    Source = new BitmapImage(new Uri($@"pack://application:,,,/{App.AppName};component/Icons/list_move.png", UriKind.Absolute))
                };
                imgArrow.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);

                var btnDelete = new Button
                {
                    Height = 16,
                    Width = 16,
                    Content = imgDelete
                };

                var text = new TextBlock { Text = parameter.Shader.Name };

                // grid with name, remove, up/down
                var grid = new Grid { Width = 210.0 };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });

                Grid.SetColumn(imgArrow, 0);
                grid.Children.Add(imgArrow);
                Grid.SetColumn(text, 1);
                grid.Children.Add(text);
                Grid.SetColumn(btnDelete, 2);
                grid.Children.Add(btnDelete);

                // add callbacks
                Content = grid;
                ToolTip = parameter.Shader.Description;

                btnDelete.Click += (sender, args) => parent.ItemDelete(this);
            }
        }

        private ListBoxItem GenerateExportPositionItem()
        {
            var imgArrow = new Image
            {
                Source = new BitmapImage(new Uri($@"pack://application:,,,/{App.AppName};component/Icons/list_move.png", UriKind.Absolute))
            };
            imgArrow.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);

            var text = new TextBlock { Text = "Export Position" };

            // grid with name, remove, up/down
            var grid = new Grid { Width = 210.0 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });

            Grid.SetColumn(imgArrow, 0);
            grid.Children.Add(imgArrow);
            Grid.SetColumn(text, 1);
            grid.Children.Add(text);

            return new ListBoxItem { Content = grid, ToolTip = "The exported image will inlcude all tonemappers until this point." };
        }

        /// <summary>
        /// tries to match the item object with an object in the left list
        /// </summary>
        /// <param name="item">list box item</param>
        /// <returns>item index or -1 if not found</returns>
        private int GetItemIndex(ListBoxItem item)
        {
            for (int i = 0; i < ListBoxMapper.Items.Count; ++i)
            {
                if (ReferenceEquals(ListBoxMapper.Items[i], item))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// swaps two items in the left list
        /// </summary>
        /// <param name="idx1">index of first item</param>
        /// <param name="idx2">index of second item</param>
        private void SwapItems(int idx1, int idx2)
        {
            if (idx1 > idx2)
            {
                var tmp = idx1;
                idx1 = idx2;
                idx2 = tmp;
            }

            // remember values
            var box1  = ListBoxMapper.Items[idx1];
            var box2 = ListBoxMapper.Items[idx2];

            // detach
            ListBoxMapper.Items.RemoveAt(idx2);
            ListBoxMapper.Items.RemoveAt(idx1);

            // attach
            ListBoxMapper.Items.Insert(idx1, box2);
            ListBoxMapper.Items.Insert(idx2, box1);

            // adjust settings
            UpdateSettingsFromItemList();
        }

        private void UpdateSettingsFromItemList()
        {
            toneSettings.ToneParameters.Clear();
            toneSettings.ExportPosition = 0;
            for (var i = 0; i < ListBoxMapper.Items.Count; ++i)
            {
                var tone = ListBoxMapper.Items[i] as ToneParameterItem;
                if (tone != null)
                {
                    toneSettings.ToneParameters.Add(tone.Parameter);
                }
                else
                {
                    toneSettings.ExportPosition = i;
                }
            }
        }

        /// <summary>
        /// deletes an item in the left list
        /// </summary>
        /// <param name="item">item object</param>
        private void ItemDelete(ListBoxItem item)
        {
            var idx = GetItemIndex(item);
            if (idx < 0) return;
            ListBoxMapper.Items.RemoveAt(idx);
            UpdateSettingsFromItemList();
        }

        /// <summary>
        /// applies the new tonemapper configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
        {
            ApplyTonemapper();
        }

        /// <summary>
        /// applies the new tonemapper configuration
        /// </summary>
        public void ApplyTonemapper()
        {
            // aplly current set of settings
            parent.EnableOpenGl();
            try
            {
                parent.Context.Tonemapper.Apply(toneSettings);
            }
            catch (Exception exception)
            {
                App.ShowErrorDialog(parent, exception.Message);
            }
            parent.DisableOpenGl();
        }

        /// <summary>
        /// display the new selected item on the right side or nothing if nothing is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxMapper_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxMapper.SelectedIndex >= 0)
            {
                var tone = ListBoxMapper.Items[ListBoxMapper.SelectedIndex] as ToneParameterItem;
                if (tone != null)
                {
                    DisplayItem(tone.Parameter);
                }
                else
                {
                    // display nothing or info box?
                    // TODO make prettier

                    // clear previous stack pannel
                    StackPanelMapper.Children.Clear();
                }

            } else StackPanelMapper.Children.Clear();
        }
    }
}
