using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfMultithreading
{
    public class ImageFrame
    {
        private ImageSource _source;
        private string _name;
        private int _size;

        public ImageFrame(Thumbnail thumbnail)
        {
            _source = GetImageSourceThumbnail(thumbnail);
            _name = thumbnail.ImageFile.Name;
            _size = (int)thumbnail.Size;
        }

        private BitmapImage GetImageSourceThumbnail(Thumbnail thumbnail)
        {
            BitmapImage myImageSource = new BitmapImage();
            myImageSource.BeginInit();
            myImageSource.StreamSource = new MemoryStream(thumbnail.DataBuffer);
            myImageSource.EndInit();
            return myImageSource;
        }

        public ImageSource Source { get { return _source; } }
        public string Name { get { return _name; } }
        public int Size { get { return _size; } }
    }
}
