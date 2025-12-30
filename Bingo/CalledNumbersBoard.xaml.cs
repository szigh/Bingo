using System.Windows.Controls;

namespace Bingo;

public partial class CalledNumbersBoard : UserControl
{
    public CalledNumbersBoard()
    {
        InitializeComponent();
        DataContext = new CalledNumbersBoardViewModel();
    }

    public CalledNumbersBoardViewModel ViewModel => (CalledNumbersBoardViewModel)DataContext;
}