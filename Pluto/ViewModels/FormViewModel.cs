using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Pluto.ViewModels
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

        public FormViewModel(IServiceProvider serviceProvider, INavigationService navigationService) 
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;

            _form = _serviceProvider.GetKeyedService<IFormBuilder>(_navigationService.FormType).GetForm();

            SubmitButton = _form.GetSubmit();
        }
    }
}
