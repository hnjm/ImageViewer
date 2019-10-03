﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFramework.ImageLoader
{
    public class Resource : IDisposable
    {
        public int Id { get; private set; }

        public Resource(string file)
        {
            Id = Dll.image_open(file);
            if (Id == 0)
                throw new Exception("error in " + file + ": " + Dll.GetError());
        }

        public Resource(uint format, int width, int height, int layer, int mipmaps)
        {
            Id = Dll.image_allocate(format, width, height, layer, mipmaps);
            if(Id == 0)
                throw new Exception("error allocating image: " + Dll.GetError());
        }

        ~Resource()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Id != 0)
            {
                Dll.image_release(Id);
                Id = 0;
            }
        }
    }
}