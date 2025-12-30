using System.Windows.Input;
using Bingo.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bingo;

public partial class BingoCallerViewModel : ObservableObject
{
    private readonly IBingoNumberGenerator _bingoNumberGenerator;
    private readonly CalledNumbersBoardViewModel _numbersBoard;

    public BingoCallerViewModel(
        IBingoNumberGenerator bingoNumberGenerator, 
        CalledNumbersBoardViewModel numbersBoard)
    {
        _bingoNumberGenerator = bingoNumberGenerator;
        _numbersBoard = numbersBoard;
        CallNextNumberCommand = new RelayCommand(CallNextNumber);
    }

    [ObservableProperty]
    private string _currentCall = "";
    
    [ObservableProperty]
    private string _info = "Ready to play";

    public ICommand CallNextNumberCommand { get; }

    public CalledNumbersBoardViewModel NumbersBoard => _numbersBoard;

    private void CallNextNumber()
    {
        if (_bingoNumberGenerator.TryGetNextNumber(out var nextNumber))
        {
            Info = "";
            CurrentCall = $"{nextNumber}";
            NumbersBoard.MarkNumberCalled(nextNumber);
        }
        else
        {
            Info = "All numbers have been called";
            CurrentCall = "";
        }
    }
}
