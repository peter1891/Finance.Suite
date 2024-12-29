using Finance.Core;

namespace Finance.Services.Navigation.Interface
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        void NavigateTo<T>() where T : ViewModelBase;
    }
}
