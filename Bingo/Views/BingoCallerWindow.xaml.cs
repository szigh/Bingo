using System.Windows;
using Bingo.ViewModels;

namespace Bingo.Views
{
    public partial class BinfoCallerView : Window
    {
        public BinfoCallerView(BingoCallerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
