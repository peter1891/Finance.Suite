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
            get => _formType;
            set => _formType = value;
        }

        private int _editId;
        public int EditId
        {
            get => _editId;
            set => _editId = value;
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<T>(string formType = "null", int editId = 0) where T : ViewModelBase
        {
            if (formType != "null")
                FormType = formType;

            EditId = editId;

            ViewModelBase viewModelBase = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewModelBase;
        }
    }
}
