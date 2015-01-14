using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.IO;

namespace WpfMultithreading
{
    public class TestViewModel
    {
        public ObservableCollection<ImageFrame> ImageCollection { get; private set; }
        private Thread _produceThumbnailsThread;
        private string _directoryPath;

        public TestViewModel(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        internal void Start()
        {
            ImageCollection = new ObservableCollection<ImageFrame>();
            _produceThumbnailsThread = new Thread(new ParameterizedThreadStart(LoadThumbnails));
            _produceThumbnailsThread.Start(_directoryPath);
        }

        private void LoadThumbnails(object dirPath)
        {
            List<FileInfo> files = new DirectoryInfo((string)dirPath)
                    .GetFiles("*.jpg").OrderBy(o => o.Name).ToList<FileInfo>();
            foreach (FileInfo file in files)
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Send,
                    new Action(() =>
                    {
                        ImageFrame temp = new ImageFrame(new Thumbnail(file.FullName, 200));
                        if (temp != null)
                        {
                            ImageCollection.Add(temp);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Thread.Abort();
                        }
                    }));
            }
        }
    }
}
