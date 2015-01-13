using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace WpfMultithreading
{
    public class Thumbnail
    {
        private FileInfo _imageFile;
        private double _size;
        private byte[] _dataBuffer;

        public Thumbnail(string imagePath, double newSize)
        {
            _imageFile = new FileInfo(imagePath);
            _size = newSize;
            _dataBuffer = GetDataBuffer();
        }

        private byte[] GetDataBuffer()
        {
            Bitmap myImage = (Bitmap)Image.FromFile(_imageFile.FullName);
            using (myImage = ResizeImage(myImage))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    myImage.Save(ms, ImageFormat.Jpeg);
                    return ms.GetBuffer();
                }
            }
        }

        public Bitmap ResizeImage(Bitmap originalImage)
        {
            return new Bitmap(originalImage, new Size((int)_size, (int)_size));
        }

        public FileInfo ImageFile { get { return _imageFile; } }
        public double Size { get { return _size; } }
        public byte[] DataBuffer { get { return _dataBuffer; } }
    }
}
