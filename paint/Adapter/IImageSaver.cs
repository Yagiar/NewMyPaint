﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace paint
{
    public interface IImageSaver
    {
        void Save(string filePath, Canvas canvas);
    }
}
