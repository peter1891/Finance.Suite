using Finance.Core;
using Finance.Repository.Interface.Models;
using Finance.Repository.Repository.Models;
using Finance.Services.Navigation.Interface;

namespace Finance.Factory.DialogFactory.Dialogs
{
    public class DeleteAllocationDialog<T> : Dialog where T : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IAllocationRepository _allocationRepository;

        public DeleteAllocationDialog(
            INavigationService navigationService, 
            IAllocationRepository allocationRepository
            )
        {
            _navigationService = navigationService;
            _allocationRepository = allocationRepository;
        }

        public override void ExecuteNo(object obj)
        {
            _navigationService.NavigateTo<T>();
        }

        public override void ExecuteYes(object obj)
        {
            if (obj != null)
                _allocationRepository.DeleteAsync(int.Parse(obj.ToString()));

            _navigationService.NavigateTo<T>();
        }
    }
}
