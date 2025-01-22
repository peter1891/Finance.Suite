using Finance.Core;

namespace Finance.Services.Navigation.Interface
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        string Key { get; set; }
        int EditId { get; set; }

        void NavigateTo<T>(string key = "null", int editId = 0) where T : ViewModelBase;
    }
}
