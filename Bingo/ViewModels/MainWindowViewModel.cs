using System.Windows;
using System.Windows.Input;
using Bingo.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Bingo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            StartCallerCommand = new RelayCommand(StartCaller);
            GenerateCardsCommand = new RelayCommand(GenerateCards);
            ExitCommand = new RelayCommand(Exit);
        }

        public ICommand StartCallerCommand { get; }
        public ICommand GenerateCardsCommand { get; }
        public ICommand ExitCommand { get; }

        private void StartCaller()
        {
            var callerView = _serviceProvider.GetRequiredService<BingoCallerView>();
            callerView.Show();
        }

        private void GenerateCards()
        {
            var cardGeneratorView = _serviceProvider.GetRequiredService<CardGeneratorView>();
            cardGeneratorView.Show();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
