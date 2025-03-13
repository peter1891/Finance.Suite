using Finance.Services.Navigation;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder.Interface;
using Microsoft.Extensions.DependencyInjection;
using Pluto.Views;
using System.Windows;
using Finance.Repository.Interface.Models;
using Finance.Repository.Repository.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;
using Finance.Core;
using Finance.Forms.FormBuilders;
using Finance.ViewModels;
using Finance.Services.Authentication.Interface;
using Finance.Services.Authentication;
using Finance.Utilities.Encoder.Interface;
using Finance.Utilities.Encoder;
using Finance.Strategy.TransactionStrategy;
using Finance.Strategy.TransactionStrategy.Transactions;
using Finance.Factory.DialogFactory;
using Finance.Factory.DialogFactory.Dialogs;
using Finance.Factory.DialogFactory.Interface;
using Finance.ViewModels.Tiles;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;

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
            services.AddSingleton<IConfiguration>(LoadConfiguration());
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=PlutoDB.db;"));

            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddTransient<DialogViewModel>();
            services.AddTransient<FormViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddTransient<AccountsViewModel>();
            services.AddTransient<AllocationsViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddTransient<TransactionsViewModel>();

            services.AddTransient<AllocationsTileViewModel>();
            services.AddTransient<ExpensesTileViewModel>();
            services.AddTransient<IncomeTileViewModel>();
            services.AddTransient<TotalBalanceTileViewModel>();
            services.AddTransient<TransactionsTileViewModel>();

            services.AddSingleton<TransactionContext>();
            services.AddSingleton<IngTransaction>();

            services.AddSingleton<IPasswordEncoder, PasswordEncoder>();

            services.AddKeyedTransient<IFormBuilder, AccountFormBuilder>("account");
            services.AddKeyedTransient<IFormBuilder, AllocationFormBuilder>("allocation");
            services.AddKeyedTransient<IFormBuilder, ImportTransactionFormBuilder>("importTransaction");
            services.AddKeyedTransient<IFormBuilder, RegisterFormBuilder>("register");
            services.AddKeyedTransient<IFormBuilder, TransactionFormBuilder>("transaction");

            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IAllocationRepository, AllocationRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IDialogFactory, DialogFactory>();

            services.AddTransient<DeleteAccountDialog<AccountsViewModel>>();
            services.AddTransient<DeleteAllocationDialog<AllocationsViewModel>>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<FirebaseAuthClient>(Provider =>
            {
                var config = Provider.GetRequiredService<IConfiguration>();
                return new FirebaseAuthClient(new FirebaseAuthConfig()
                {
                    ApiKey = config["ApiKey:Firebase"],
                    AuthDomain = "pluto-finance-fd5f6.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
                        new EmailProvider()
                    }
                });
            });
        }

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .Build();
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
