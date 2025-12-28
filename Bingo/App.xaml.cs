using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Bingo
{
    public partial class App : Application
    {
#pragma warning disable CS8618 
        private ServiceProvider _serviceProvider;
#pragma warning restore CS8618 

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBingoNumberGenerator, BingoNumberGenerator>();
            services.AddTransient<BingoCallerViewModel>();
            services.AddTransient<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
