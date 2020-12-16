using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.ViewModels.Base;
using R_Crypt.Views;
using R_Crypt.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace R_Crypt.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        public MainWindowVM()
        {
            FilesToCrypt = new();
            FilesToCrypt.Add(new Models.FilesToCrypt { Name = "provwadasdawdsadawdsadawdedada1", Size = "dwawds", ProgressBar = new ProgressBar { Width = 200 } });
            FilesToCrypt.Add(new Models.FilesToCrypt { Name = "awdsavggh", Size = "awda", ProgressBar = new ProgressBar { Width = 200, Value = 20 } });
            FilesToCrypt.Add(new Models.FilesToCrypt { Name = "provwadasdawdsadawdsadawdedada1provwadasdawdsadawdsadawdedada1", Size = "awdsadaw", ProgressBar = new ProgressBar { Width = 200, Value = 35 } });
            FilesToCrypt.Add(new Models.FilesToCrypt { Name = "provwadasdawdsadawdsadawdedada1", Size = "shwadwaddupt", ProgressBar = new ProgressBar { Width = 200, Value = 84 } });
        }

        public ObservableCollection<FilesToCrypt> FilesToCrypt { get; set; }
    }
}
