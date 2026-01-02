using System.Windows.Input;
using Bingo.Generators;
using Bingo.Services;
using Bingo.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using log4net;

namespace Bingo.ViewModels;

public partial class BingoCallerViewModel : ObservableObject
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BingoCallerViewModel));
    
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
        Log.Debug("BingoCallerViewModel constructor called");
        
        _bingoNumberGenerator = bingoNumberGenerator;
        _numbersBoard = numbersBoard;
        _dialogService = dialogService;
        _windowService = windowService;
        CallNextNumberCommand = new RelayCommand(CallNextNumber);
        NewGameCommand = new RelayCommand(NewGame);
        ExitCommand = new RelayCommand(Exit);
        
        Log.Info("BingoCallerViewModel initialized successfully");
    }

    [ObservableProperty]
#pragma warning disable IDE0044 // Add readonly modifier
    private string _currentCall = "";

    [ObservableProperty]
    private string _info = "Ready to play";
#pragma warning restore IDE0044 // Add readonly modifier

    public ICommand CallNextNumberCommand { get; }
    public ICommand NewGameCommand { get; }
    public ICommand ExitCommand { get; }

    public CalledNumbersBoardViewModel NumbersBoard => _numbersBoard;

    private void CallNextNumber()
    {
        Log.Debug("CallNextNumber method called");
        
        if (_bingoNumberGenerator.TryGetNextNumber(out var nextNumber))
        {
            Log.Info($"Next number called: {nextNumber}");
            Info = "";
            CurrentCall = $"{nextNumber}";
            NumbersBoard.MarkNumberCalled(nextNumber);
        }
        else
        {
            Log.Warn("All numbers have been called - no more numbers available");
            Info = "All numbers have been called";
            CurrentCall = "";
        }
    }

    private void NewGame()
    {
        Log.Info("Starting a new game");
        
        _bingoNumberGenerator.Reset();
        NumbersBoard.Reset();
        CurrentCall = "";
        Info = "Ready to play";
        
        Log.Debug("New game initialized successfully");
    }

    private void Exit()
    {
        Log.Debug("Exit command initiated");
        
        var confirmed = _dialogService.ShowConfirmation(
            "Are you sure you want to exit the game - all progress will be lost?", 
            "Exit");
        
        if (confirmed)
        {
            Log.Info("User confirmed exit - closing application");
            _windowService.CloseWindow<Views.BingoCallerView>();
        }
        else
        {
            Log.Debug("User cancelled exit");
        }
    }
}
