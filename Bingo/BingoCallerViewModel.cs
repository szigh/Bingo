using System;
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

        private string _currentCall = "00!";
        public ICommand CallNextNumberCommand { get; set; }

        public string CurrentCall
        {
            get => _currentCall;
            set => SetProperty(ref _currentCall, value);
        }

        private void CallNextNumber()
        {
            int nextNumber = _bingoNumberGenerator.GetNextNumber();
            CurrentCall = $"{nextNumber}";
        }
    }
}
