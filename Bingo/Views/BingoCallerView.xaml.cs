using System.Windows;

namespace Bingo.Views
{
    public partial class BingoCallerView : Window
    {
        public BingoCallerView(BingoCallerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
