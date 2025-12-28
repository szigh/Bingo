using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bingo
{
    public class BingoCallerViewModel : ObservableObject
    {
        private readonly IBingoNumberGenerator _bingoNumberGenerator;

        public BingoCallerViewModel(IBingoNumberGenerator bingoNumberGenerator)
        {
            _bingoNumberGenerator = bingoNumberGenerator;
            CallNextNumberCommand = new RelayCommand(CallNextNumber);
        }

        private string _currentCall = "";
        private string _info = "Ready to play";

        public ICommand CallNextNumberCommand { get; set; }

        public string CurrentCall
        {
            get => _currentCall;
            set => SetProperty(ref _currentCall, value);
        }

        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private void CallNextNumber()
        {
            if (_bingoNumberGenerator.TryGetNextNumber(out var nextNumber))
            {
                Info = "";
                CurrentCall = $"{nextNumber}";
            }
            else
            {
                Info = "All numbers have been called";
                CurrentCall = "";
            }
        }
    }
}
