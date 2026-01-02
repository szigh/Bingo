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

    /// <summary>
    /// Marks the specified bingo number as called on the board.
    /// </summary>
    /// <param name="number">The bingo number to mark as called (1-90).</param>
    public void MarkNumberCalled(int number)
    {
        if (number is >= 1 and <= 90)
            _cells[number - 1].IsCalled = true;
    }

    /// <summary>
    /// Resets the called numbers board by marking all numbers as not called.
    /// </summary>
    public void Reset()
    {
        foreach (var cell in _cells)
            cell.IsCalled = false;
    }
}