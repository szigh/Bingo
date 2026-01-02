using System.Windows;
using Bingo.Generators;
using Bingo.Services;
using Bingo.ViewModels;
using Bingo.Views;
using Microsoft.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using log4net.Core;
using System.IO;
using System.Reflection;

namespace Bingo
{
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize log4net
            ConfigureLogging();
            
            Log.Info("Application starting...");
            
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<BinfoCallerView>();
            mainWindow.Show();
            
            Log.Info("Application started successfully");
        }

        private static void ConfigureLogging()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var configFile = new FileInfo("log4net.config");
            
            if (configFile.Exists)
            {
                XmlConfigurator.Configure(logRepository, configFile);
            }
            else
            {
                // Fallback to basic configuration
                BasicConfigurator.Configure(logRepository);
            }

            // Set log level based on build configuration
#if DEBUG
            ((log4net.Repository.Hierarchy.Hierarchy)logRepository).Root.Level = Level.Debug;
            Log.Debug("Logging configured for DEBUG build");
#else
            ((log4net.Repository.Hierarchy.Hierarchy)logRepository).Root.Level = Level.Info;
            Log.Info("Logging configured for RELEASE build");
#endif
            ((log4net.Repository.Hierarchy.Hierarchy)logRepository).RaiseConfigurationChanged(EventArgs.Empty);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IWindowService, WindowService>();
            
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
            Log.Info("Application exiting...");
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
