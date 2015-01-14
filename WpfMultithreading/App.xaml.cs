using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMultithreading
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            TestViewModel test = new TestViewModel(@"C:\Users\Dude\Pictures");
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = test;

            test.Start();

            mainWindow.Show();
        }
    }
}
