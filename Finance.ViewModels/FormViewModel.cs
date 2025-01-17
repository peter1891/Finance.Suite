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

        private IFormBuilder _iFormBuilder;
        public IFormBuilder IFormBuilder
        {
            get { return _iFormBuilder; }
            set
            {
                _iFormBuilder = value;
                OnPropertyChanged(nameof(IFormBuilder));
            }
        }

        private Form _form;

        public string Title { get; set; }

        public Grid Grid { get; set; }
        public Button SubmitButton { get; set; }
        public Button CancelButton { get; set; }

        public ICommand ExecuteSubmitCommand { get; }
        public ICommand ExecuteCancelCommand { get; }

        public FormViewModel(IServiceProvider serviceProvider, INavigationService navigationService) 
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;

            GetFormAsync();

            Title = _form.GetTitle();

            this.Grid = _form.GetGrid();
            SubmitButton = _form.GetSubmitButton();
            CancelButton = _form.GetCancelButton();

            ExecuteSubmitCommand = _form.GetSubmitCommand();
            ExecuteCancelCommand = _form.GetCancelCommand();
        }

        private async void GetFormAsync()
        {
            _form = await _serviceProvider
                .GetKeyedService<IFormBuilder>(_navigationService.FormType)
                .GetFormAsync(_navigationService.EditId);
        }
    }
}
