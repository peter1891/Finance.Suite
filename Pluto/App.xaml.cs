﻿using Finance.Core;
using Finance.Services.Navigation;
using Finance.Services.Navigation.Interface;
using Finance.Forms;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Pluto.Views;
using System.Windows;
using Finance.Repository.Interface.Models;
using Finance.Repository.Repository.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=PlutoDB.db;"));

            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddTransient<FormViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<AccountsViewModel>();
            services.AddSingleton<TransactionsViewModel>();

            services.AddKeyedTransient<IFormBuilder, AccountFormBuilder>("account");
            services.AddKeyedTransient<IFormBuilder, TransactionFormBuilder>("transaction");

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

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
