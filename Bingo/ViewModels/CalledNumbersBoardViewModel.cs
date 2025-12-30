using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bingo.ViewModels;

public partial class CalledNumbersBoardViewModel : ObservableObject
{
    private readonly BingoNumberCell[] _cells;

    public CalledNumbersBoardViewModel()
    {
        _cells = new BingoNumberCell[90];
        
        for (int i = 0; i < 90; i++)
            _cells[i] = new BingoNumberCell(i + 1);
        
        Numbers = new ObservableCollection<BingoNumberCell>(_cells);
    }

    public ObservableCollection<BingoNumberCell> Numbers { get; }

    public void MarkNumberCalled(int number)
    {
        if (number is >= 1 and <= 90)
            _cells[number - 1].IsCalled = true;
    }
}