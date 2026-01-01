using System.Windows;
using Bingo.ViewModels;

namespace Bingo.Views
{
    public partial class CardGeneratorView : Window
    {
        public CardGeneratorView(CardGeneratorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
