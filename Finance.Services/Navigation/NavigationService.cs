using Finance.Core;
using Finance.Services.Navigation.Interface;

namespace Finance.Services.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private Func<Type, ViewModelBase> _viewModelFactory;

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private string _formType;
        public string FormType
        {
            get { return _formType; }
            set
            {
                _formType = value;
            }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<T>(String formType = "null") where T : ViewModelBase
        {
            if (formType != "null")
                FormType = formType;

            ViewModelBase viewModelBase = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewModelBase;
        }
    }
}
