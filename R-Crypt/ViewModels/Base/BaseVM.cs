using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace R_Crypt.ViewModels.Base
{
    [Serializable]
    public class RelayCommand : ICommand
    {
        public RelayCommand() { }

        public event EventHandler CanExecuteChanged;

        private Action<object> executeMethod;
        private Predicate<object> CanExecuteMethod;

        public RelayCommand(Action<object> Execute, Predicate<object> CanExecute)
        {
            executeMethod = Execute;
            CanExecuteMethod = CanExecute;
        }

        public RelayCommand(Action<object> Execute) : this(Execute, null)
        {

        }

        public bool CanExecute(object parameter)
        {
            return (CanExecuteMethod == null) ? true : CanExecuteMethod.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [Serializable]
    public class BaseVM : RelayCommand, INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
