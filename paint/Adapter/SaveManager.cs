using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace paint.Adapter
{
    internal class SaveManager
    {
        private IImageSaver _imageSaver;
        public static IImageSaver GetImageSaver(string format)
        {
            switch (format.ToLower())
            {
                case "png":
                    return new PngImageSaver();
                case "jpeg":
                case "jpg": 
                    return new JpegImageSaver();
                case "bmp":
                    return new BmpImageSaver();
                case "gif":
                    return new GifImageSaver();
                default:
                    throw new NotSupportedException($"Format {format} is not supported.");
            }
        }
        public SaveManager(string format)
        {
            _imageSaver = GetImageSaver(format);
        }

        public void SaveImage(string filePath, Canvas canvas)
        {
            _imageSaver.Save(filePath, canvas);
        }
    }
}
