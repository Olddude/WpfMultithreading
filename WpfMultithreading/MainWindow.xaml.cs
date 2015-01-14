using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfMultithreading
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ImageFrame> ImageCollection = new ObservableCollection<ImageFrame>();
        
        public MainWindow()
        {
            InitializeComponent();
            tb_DirectoryPath.Text = @"C:\Users\Dude\Pictures";
            lv_MyImageList.ItemsSource = ImageCollection;
        }

        private void bu_LoadThumbnails_Click(object sender, RoutedEventArgs e)
        {
            Thread produceThumbnailsThread = new Thread(new ParameterizedThreadStart(LoadThumbnails));
            produceThumbnailsThread.Start((string)tb_DirectoryPath.Text);
        }

        private void LoadThumbnails(object dirPath)
        {
            List<FileInfo> files = new DirectoryInfo((string)dirPath)
                    .GetFiles("*.jpg").OrderBy(o => o.Name).ToList<FileInfo>();
            foreach (FileInfo file in files)
            {
                Thumbnail temp = new Thumbnail(file.FullName, 120);
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Send,
                    new Action(() =>
                    {
                        ImageCollection.Add(new ImageFrame(temp));
                    }));
            }
        }
    }
}
