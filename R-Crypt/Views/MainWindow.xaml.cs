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
using System.Windows.Navigation;
using System.Windows.Shapes;
using R_Crypt.Models.Serializable;
using R_Crypt.ViewModels;

namespace R_Crypt.Views
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                (DataContext as MainWindowVM).AddFilesToList((string[])e.Data.GetData(DataFormats.FileDrop));
            }
        }

        private void DataGrid_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (FileList.HasItems && FileList.SelectedItems.Count > 0)
            {
                if (e.Key == Key.Delete)
                {
                    List<CryptoFile> files = new();

                    foreach (var file in FileList.SelectedItems)
                        files.Add((CryptoFile)file);

                    (DataContext as MainWindowVM).RemoveFileFromList(files);
                }
            }
        }
    }
}
