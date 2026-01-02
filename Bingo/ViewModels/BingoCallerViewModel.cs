using System.Windows.Input;
using Bingo.Generators;
using Bingo.Services;
using Bingo.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bingo;

public partial class BingoCallerViewModel : ObservableObject
{
    private readonly IBingoNumberGenerator _bingoNumberGenerator;
    private readonly CalledNumbersBoardViewModel _numbersBoard;
    private readonly IDialogService _dialogService;
    private readonly IWindowService _windowService;

    public BingoCallerViewModel(
        IBingoNumberGenerator bingoNumberGenerator, 
        CalledNumbersBoardViewModel numbersBoard,
        IDialogService dialogService,
        IWindowService windowService)
    {
        _bingoNumberGenerator = bingoNumberGenerator;
        _numbersBoard = numbersBoard;
        _dialogService = dialogService;
        _windowService = windowService;
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
        var confirmed = _dialogService.ShowConfirmation(
            "Are you sure you want to exit the game - all progress will be lost?", 
            "Exit");
        
        if (confirmed)
        {
            _windowService.CloseWindow<Views.BingoCallerView>();
        }
    }
}
