using System.Windows;
using Bingo.Generators;
using Bingo.ViewModels;
using Bingo.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Bingo
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<BinfoCallerView>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Generators
            services.AddSingleton<IBingoNumberGenerator, BingoNumberGenerator>();
            
            // ViewModels
            services.AddTransient<CalledNumbersBoardViewModel>();
            services.AddTransient<BingoCallerViewModel>();
            services.AddTransient<CardGeneratorViewModel>();
            
            // Views
            services.AddTransient<BinfoCallerView>();
            services.AddTransient<BingoCallerView>();
            services.AddTransient<CardGeneratorView>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
