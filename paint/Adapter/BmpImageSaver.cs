﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace paint.Adapter
{
    internal class BmpImageSaver : IImageSaver
    {
        public void Save(string filePath, Canvas canvas)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
                canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
                canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));
                bmp.Render(canvas);
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fileStream);
            }
        }
    }
}
