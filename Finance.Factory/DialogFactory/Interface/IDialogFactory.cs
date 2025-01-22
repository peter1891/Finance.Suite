using Finance.Core;
using Finance.Enums;
using Finance.Factory.DialogFactory.Dialogs;

namespace Finance.Factory.DialogFactory.Interface
{
    public interface IDialogFactory
    {
        void SetDialog<T>(DialogType dialogStyle, object commandParameter = null) where T : ViewModelBase;
        Dialog GetDialog();
    }
}
