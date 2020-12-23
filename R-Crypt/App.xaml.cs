using R_Crypt.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace R_Crypt
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            foreach (var arg in e.Args)
            {
                if (arg == "-debug") MessageBox.Show("debug");
            }

            R_Crypt.Views.SplashScreen sc = new();
            Application.Current.MainWindow = sc;
            sc.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
