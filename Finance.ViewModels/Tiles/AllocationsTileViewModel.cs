using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;

namespace Finance.ViewModels.Tiles
{
    public class AllocationsTileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IAllocationRepository _allocationRepository;

        public List<AllocationModel> AllocationModels { get; set; }

        public AllocationsTileViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IAllocationRepository allocationRepository)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _allocationRepository = allocationRepository;

            GetAllocationsAsync();
        }

        private async void GetAllocationsAsync()
        {
            AllocationModels = new List<AllocationModel>(await _allocationRepository.GetAllocationsByAuthenticatedIdAsync(_authenticationService.UserId));
        }
    }
}
