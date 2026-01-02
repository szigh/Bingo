using CommunityToolkit.Mvvm.ComponentModel;

namespace Bingo.ViewModels;

public partial class BingoNumberCell : ObservableObject
{
    public BingoNumberCell(int number)
    {
        Number = number;
        IsCalled = false;
    }

    public int Number { get; }

    [ObservableProperty]
    private bool _isCalled;
}