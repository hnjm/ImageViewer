﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ImageFramework.Annotations;
using ImageFramework.Model.Statistics;
using ImageViewer.Models;

namespace ImageViewer.ViewModels.Statistics
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public enum Types
        {
            Luminance = DefaultStatistics.Types.Luminance,
            Average = DefaultStatistics.Types.Average,
            Luma = DefaultStatistics.Types.Luma,
            Lightness = DefaultStatistics.Types.Lightness,
            Alpha = DefaultStatistics.Types.Alpha,
            SSIM
        }

        private readonly ModelsEx models;
        private readonly StatisticViewModel[] viewModels;
        private static readonly string[] channelDescriptions = new[]
        {
            // luminance
            @"Luminance is the radiant power weighted by a spectral sensitivity function that is characteristic of vision. 
The magnitude of luminance is proportional to physical power. 
But the spectral composition of luminance is related to the brightness sensitivity of human vision.
Luminance is computed in linear color space with: dot(RGB*A, (0.2125, 0.7154, 0.0721)).",
            // average
            @"The average weight computed in linear space: dot(RGB*A, (1/3, 1/3, 1/3)).",
            // luma
            @"Luma is brightness computed in sRGB color space which is often used by video codecs.
The ""NTSC"" luma formula is: dot(sRGB*A, (0.299, 0.587, 0.114)).",
            // lightness
            @"Human vision has a nonlinear perceptual response to brightness: a source having a luminance only 18% of a reference luminance appears about half as bright. 
The perceptual response to luminance Y is called lightness L.
It is computed from the luminance: L = 116 * Y ^ (1/3) - 16.",
            // alpha
            "Alpha channel.",
            // ssim
            @"The Structural Similarity Index (SSIM) is a metric that measures the perceived difference between two images. 
SSIM is based on visible structure differences instead of per-pixel absolute differences (like RMSE or MAE). 
SSIM is computed with the luma grayscale image: dot(sRGB*A, (0.299, 0.587, 0.114)). 
Note that RGB will be clamped between 0.0 and 1.0 which results in an SSIM value between 0.0 and 1.0 (higher is better)."
        };

        public StatisticsViewModel(ModelsEx models)
        {
            this.models = models;
            var settingsValue = (Types)(int)models.Settings.StatisticsChannel;
            selectedChannel = AvailableChannels.Find(v => v.Cargo == settingsValue);
            ChannelDescription = channelDescriptions[(int)settingsValue];

            viewModels = new StatisticViewModel[models.NumPipelines];
            for (int i = 0; i < viewModels.Length; ++i)
            {
                viewModels[i] = new StatisticViewModel(i, models, this);
            }

            models.Window.Window.TabControl.SelectionChanged += TabControlOnSelectionChanged;
            SSIM = new SSIMsViewModel(models);
        }

        private void TabControlOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            IsVisible = ReferenceEquals(models.Window.Window.TabControl.SelectedItem, models.Window.Window.StatisticsTabItem);
        }

        public StatisticViewModel Equation1 => viewModels[0];
        public StatisticViewModel Equation2 => viewModels[1];
        public StatisticViewModel Equation3 => viewModels[2];
        public StatisticViewModel Equation4 => viewModels[3];

        public SSIMsViewModel SSIM { get; }

        public bool ShowSSIM => selectedChannel.Cargo == Types.SSIM;

        private bool isVisible = false;
        public bool IsVisible
        {
            get => isVisible;
            private set
            {
                if (isVisible == value) return;
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public string ChannelDescription { get; private set; } = "";

        public List<ListItemViewModel<Types>> AvailableChannels { get; } = new List<ListItemViewModel<Types>>
        {
            new ListItemViewModel<Types>
            {
                Name = "Luminance",
                Cargo = Types.Luminance
            },
            new ListItemViewModel<Types>
            {
                Name = "SSIM",
                Cargo = Types.SSIM
            },
            new ListItemViewModel<Types>
            {
                Name = "Average",
                Cargo = Types.Average
            },
            new ListItemViewModel<Types>
            {
                Name = "Luma",
                Cargo = Types.Luma
            },
            new ListItemViewModel<Types>
            {
                Name = "Lightness",
                Cargo = Types.Lightness
            },
            new ListItemViewModel<Types>
            {
                Name = "Alpha",
                Cargo = Types.Alpha
            },
        };

        private ListItemViewModel<Types> selectedChannel;
        public ListItemViewModel<Types> SelectedChannel
        {
            get => selectedChannel;
            set
            {
                if (ReferenceEquals(value, selectedChannel)) return;
                selectedChannel = value;
                ChannelDescription = channelDescriptions[(int)value.Cargo];
                OnPropertyChanged(nameof(ShowSSIM));
                OnPropertyChanged(nameof(SelectedChannel));
                OnPropertyChanged(nameof(ChannelDescription));
                if ((int) value.Cargo < (int) DefaultStatistics.Types.Size)
                {
                    models.Settings.StatisticsChannel = (DefaultStatistics.Types)value.Cargo;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
