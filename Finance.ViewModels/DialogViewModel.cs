using Finance.Core;
using Finance.Factory.DialogFactory;
using Finance.Factory.DialogFactory.Dialogs;

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

        public DialogViewModel(DialogFactory dialogFactory)
        {
            Dialog = dialogFactory.GetDialog();
        }
    }
}
