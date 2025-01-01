using Finance.Core;
using Finance.Services.Navigation;
using Finance.Services.Navigation.Interface;
using Finance.Forms;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Pluto.Views;
using System.Windows;

namespace Pluto
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<FormViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TransactionsViewModel>();

            services.AddKeyedScoped<IFormBuilder, TransactionFormBuilder>("transaction");

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainView mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainView.Show();
            base.OnStartup(e);
        }
    }

}
