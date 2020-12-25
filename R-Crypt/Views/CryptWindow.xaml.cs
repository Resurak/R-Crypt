using R_Crypt.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace R_Crypt.Views
{
    /// <summary>
    /// Logica di interazione per CryptProcessWindow.xaml
    /// </summary>
    public partial class CryptWindow : Window
    {
        public CryptWindow()
        {
            InitializeComponent();
        }

        private void FileList_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                (DataContext as CryptWindowVM).FilesHandler.GetCryptoFiles((string[])e.Data.GetData(DataFormats.FileDrop), true);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
