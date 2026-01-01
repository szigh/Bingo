using System.Windows;
using System.Windows.Input;
using Bingo.Generators;
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
        NewGameCommand = new RelayCommand(NewGame);
        ExitCommand = new RelayCommand(Exit);
    }

    [ObservableProperty]
    private string _currentCall = "";
    
    [ObservableProperty]
    private string _info = "Ready to play";

    public ICommand CallNextNumberCommand { get; }
    public ICommand NewGameCommand { get; }
    public ICommand ExitCommand { get; }

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

    private void NewGame()
    {
        _bingoNumberGenerator.Reset();
        NumbersBoard.Reset();
        CurrentCall = "";
        Info = "Ready to play";
    }

    private void Exit()
    {
        var result = MessageBox.Show("Are you sure you want to exit the game - all progress " +
            "will be lost?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
        if (result == MessageBoxResult.Yes)
        {
            Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.GetType() == typeof(Views.BingoCallerView))?.Close();
        }
    }
}
