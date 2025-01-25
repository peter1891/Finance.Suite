using Finance.Core;
using Finance.Enums;
using Finance.Factory.DialogFactory.Dialogs;
using Finance.Factory.DialogFactory.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Factory.DialogFactory
{
    public class DialogFactory : IDialogFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private Dialog _dialog;

        public DialogFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Dialog GetDialog()
        {
            return _dialog;
        }

        public void SetDialog<T>(DialogType dialogStyle, object commandParameter = null) where T : ViewModelBase
        {
            switch(dialogStyle)
            {
                case DialogType.DeleteAccount:
                    _dialog = _serviceProvider.GetRequiredService<DeleteAccountDialog<T>>();

                    if (commandParameter != null)
                        _dialog.CommandParameter = commandParameter.ToString();
                    break;
                case DialogType.DeleteAllocation:
                    _dialog = _serviceProvider.GetRequiredService<DeleteAllocationDialog<T>>();

                    if (commandParameter != null)
                        _dialog.CommandParameter = commandParameter.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
