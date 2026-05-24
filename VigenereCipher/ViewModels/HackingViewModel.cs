using CommunityToolkit.Mvvm.Input;
using System;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VigenereCipher.ViewModels
{
    public partial class HackingViewModel : ViewModelBase
    {
        private IKasiskiService _kasiskiService = new KasiskiService();
        private TextFormatterService _textFormatterService = new TextFormatterService();

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunHackingCommand))]
        private string _cipher;

        [ObservableProperty]
        private bool _isError;

        public RelayCommand SwapToEncryptionCommand { get; init; }
        public RelayCommand ActivateDecryptionMode { get; init; }
        public HackingViewModel(Action swapToEncryption, Action activateDecryptionMode)
        {
            SwapToEncryptionCommand = new RelayCommand(swapToEncryption);
            ActivateDecryptionMode = new RelayCommand(activateDecryptionMode);
        }

        [RelayCommand(CanExecute = nameof(CanHacking))]
        private void RunHacking()
        {
            if (!_kasiskiService.TryHack(_cipher, out var vigenereHackData))
            {
                Message = vigenereHackData.ErrorMessage;
                Key = string.Empty;
                IsError = true;
                return;
            }
            Message = vigenereHackData.Message;
            Key = vigenereHackData.Key;
            IsError = false;
        }

        private bool CanHacking()
        {
            return !string.IsNullOrEmpty(_cipher)
                && !string.IsNullOrEmpty(_textFormatterService.ClearText(_cipher));
        }
    }
}
