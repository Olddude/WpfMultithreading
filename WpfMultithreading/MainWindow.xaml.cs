using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public ObservableCollection<ImageFrame> MyImageCollection = new ObservableCollection<ImageFrame>();

        public MainWindow()
        {
            InitializeComponent();
            tb_DirectoryPath.Text = @"C:\Users\Dude\Pictures";
            lv_MyImageList.ItemsSource = MyImageCollection;
        }

        private void LoadImages(string directoryPath)
        {
            IOrderedEnumerable<FileInfo> imageFiles = new DirectoryInfo(directoryPath).GetFiles("*.jpg").OrderBy(i => i.Name);
            foreach (FileInfo fileInfo in imageFiles)
            {
                try
                {
                    MyImageCollection.Add(new ImageFrame(new Thumbnail(fileInfo.FullName, 100)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load an image file!\n\n" + ex.Message);
                }
            }
        }

        private void bu_LoadThumbnails_Click(object sender, RoutedEventArgs e)
        {
            //Also hier soll dann ein Seperater Thread gestartet werden der 'MyImageCollection' befüllt oder so
            //Die Gui Oberfläche muss dabei responsive bleiben
            LoadImages(tb_DirectoryPath.Text);
        }
    }
}
