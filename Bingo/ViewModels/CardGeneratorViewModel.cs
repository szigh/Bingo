using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bingo.ViewModels
{
    public partial class CardGeneratorViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _numberOfCards = 6;

        [ObservableProperty]
        private string _statusMessage = "Ready to generate cards";

        public ICommand GenerateCardsCommand { get; }
        public ICommand PrintCardsCommand { get; }

        public CardGeneratorViewModel()
        {
            GenerateCardsCommand = new RelayCommand(GenerateCards);
            PrintCardsCommand = new RelayCommand(PrintCards, CanPrintCards);
        }

        private void GenerateCards()
        {
            if (NumberOfCards < 1 || NumberOfCards > 100)
            {
                StatusMessage = "Please enter a number between 1 and 100";
                return;
            }

            StatusMessage = $"Generating {NumberOfCards} bingo cards...";
            
            // TODO: Implement card generation logic
            // For now, just a placeholder
            MessageBox.Show($"Would generate {NumberOfCards} bingo cards", 
                "Card Generation", MessageBoxButton.OK, MessageBoxImage.Information);
            
            StatusMessage = $"Generated {NumberOfCards} cards successfully!";
        }

        private void PrintCards()
        {
            MessageBox.Show("Print functionality coming soon!", 
                "Print Cards", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanPrintCards()
        {
            return NumberOfCards > 0;
        }
    }
}
