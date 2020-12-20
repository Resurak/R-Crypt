using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.Common.Utils;
using R_Crypt.ViewModels.Base;
using R_Crypt.Views;
using R_Crypt.Models;
using R_Crypt.Models.Serializable;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace R_Crypt.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        public MainWindowVM()
        {
            CryptoFiles = new();
        }

        public ObservableCollection<CryptoFiles> CryptoFiles { get; set; }

        public void AddFilesToList(string[] files)
        {
            foreach (var file in files)
            {
                CryptoFiles.Add(new CryptoFiles(file));
            }
        } 
    }
}
