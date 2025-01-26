using Finance.Core;
using Finance.Factory.DialogFactory.Dialogs;
using Finance.Factory.DialogFactory.Interface;

namespace Finance.ViewModels
{
    public class DialogViewModel : ViewModelBase
    {
        private Dialog _dialog;
        public Dialog Dialog 
        {
            get => _dialog;
            set
            {
                _dialog = value;
                OnPropertyChanged(nameof(Dialog));
            }
        }

        public DialogViewModel(IDialogFactory dialogFactory)
        {
            Dialog = dialogFactory.GetDialog();
        }
    }
}
