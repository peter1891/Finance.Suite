using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class FormViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private Form _form;

        private Button _submitButton;
        public Button SubmitButton
        { 
            get { return _submitButton; } 
            set
            {
                _submitButton = value;
                OnPropertyChanged(nameof(SubmitButton));
            }
        }

        private Button _cancelButton;
        public Button CancelButton
        {
            get { return _cancelButton; }
            set
            {
                _cancelButton = value;
                OnPropertyChanged(nameof(CancelButton));
            }
        }

        public ICommand ExecuteSubmitCommand { get; set; }
        public ICommand ExecuteCancelCommand { get; set; }

        public FormViewModel(IServiceProvider serviceProvider, INavigationService navigationService) 
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;

            _form = _serviceProvider.GetKeyedService<IFormBuilder>(_navigationService.FormType).GetForm();

            SubmitButton = _form.GetSubmitButton();
            CancelButton = _form.GetCancelButton();

            ExecuteSubmitCommand = _form.GetSubmitCommand();
            ExecuteCancelCommand = _form.GetCancelCommand();
        }
    }
}
