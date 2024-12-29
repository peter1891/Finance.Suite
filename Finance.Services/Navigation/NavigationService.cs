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

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            ViewModelBase viewModelBase = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewModelBase;
        }
    }
}
