using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace R_Crypt.Models
{
    public class FilesToCrypt
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public double Value { get; set; }
        public Visibility TextBlockVisibility { get => Visibility.Visible; }
        public Visibility ProgressBarVisibility { get => Visibility.Hidden; }

    }
}
