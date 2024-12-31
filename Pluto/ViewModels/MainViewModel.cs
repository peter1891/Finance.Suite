using Finance.Core;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Forms;
using Finance.Utilities.FormBuilder.Interface;
using Microsoft.Extensions.DependencyInjection;
using Pluto.Views;

namespace Pluto.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;

        private MenuView _menuView;

        public object CurrentView { get; set; }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _menuView = _serviceProvider.GetRequiredService<MenuView>();

            CurrentView = _menuView;

            IFormBuilder iFormBuilder = new TransactionForm();
            Form form = iFormBuilder.GetForm();
        }
    }
}
