using Finance.Core;
using System.Windows.Input;

namespace Finance.Factory.DialogFactory.Dialogs
{
    public abstract class Dialog
    {
        public string CommandParameter { get; set; }

        public ICommand ExecuteYesCommand { get; }
        public ICommand ExecuteNoCommand { get; }

        public Dialog()
        {
            ExecuteYesCommand = new RelayCommand(ExecuteYes, obj => true);
            ExecuteNoCommand = new RelayCommand(ExecuteNo, obj => true);
        }

        public abstract void ExecuteYes(object obj);
        public abstract void ExecuteNo(object obj);
    }
}
