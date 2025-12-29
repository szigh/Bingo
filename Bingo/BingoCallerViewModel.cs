using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bingo;

public partial class BingoCallerViewModel : ObservableObject
{
    private readonly IBingoNumberGenerator _bingoNumberGenerator;

    public BingoCallerViewModel(IBingoNumberGenerator bingoNumberGenerator)
    {
        _bingoNumberGenerator = bingoNumberGenerator;
        CallNextNumberCommand = new RelayCommand(CallNextNumber);
        InitializeNumbersBoard();
    }

    [ObservableProperty]
    private string _currentCall = "";
    
    [ObservableProperty]
    private string _info = "Ready to play";

    public ICommand CallNextNumberCommand { get; }

    public ObservableCollection<BingoNumberCell> NumbersBoard { get; } = [];

    private void InitializeNumbersBoard()
    {
        for (int i = 1; i <= 90; i++)
            NumbersBoard.Add(new BingoNumberCell(i));
    }

    private void CallNextNumber()
    {
        if (_bingoNumberGenerator.TryGetNextNumber(out var nextNumber))
        {
            Info = "";
            CurrentCall = $"{nextNumber}";
            
            var cell = NumbersBoard.FirstOrDefault(c => c.Number == nextNumber);
            cell?.IsCalled = true;
        }
        else
        {
            Info = "All numbers have been called";
            CurrentCall = "";
        }
    }
}
