using Finance.Core;
using Finance.Enums;
using Finance.Factory.DialogFactory.Interface;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AllocationsViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private readonly IDialogFactory _dialogFactory;

        private readonly IAllocationRepository _allocationRepository;

        public ObservableCollection<AllocationModel> AllocationModels { get; set; }

        public ICommand NavigateAddCommand { get; }
        public ICommand NavigateDeleteCommand { get; }
        public ICommand NavigateEditCommand { get; }

        public AllocationsViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService,
            IDialogFactory dialogFactory, 
            IAllocationRepository allocationRepository
            )
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _dialogFactory = dialogFactory;
            _allocationRepository = allocationRepository;

            GetAllocationsAsync();

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("allocation"); }, obj => true);
            NavigateDeleteCommand = new RelayCommand(NavigateDelete, obj => true);
            NavigateEditCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("allocation", (int)obj); }, obj => true);
        }

        private void NavigateDelete(object obj)
        {
            _dialogFactory.SetDialog<AllocationsViewModel>(DialogType.DeleteAllocation, obj);

            _navigationService.NavigateTo<DialogViewModel>();
        }

        private async void GetAllocationsAsync()
        {
            AllocationModels = new ObservableCollection<AllocationModel>(await _allocationRepository.GetAllocationsByAuthenticatedIdAsync(_authenticationService.UserId));
        }
    }
}
