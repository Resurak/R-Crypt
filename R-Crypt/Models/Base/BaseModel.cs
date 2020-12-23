using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Models.Base
{
    [Serializable]
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Notify(List<string> properties)
        {
            foreach (var property in properties)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
