using Finance.Core;
using Finance.Utilities.FormBuilder;

namespace Finance.Services.Navigation.Interface
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        string FormType { get; set; }

        void NavigateTo<T>(String formType = "null") where T : ViewModelBase;
    }
}
