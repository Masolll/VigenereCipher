using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Services;

namespace VigenereCipher.ViewModels
{
    public partial class EncryptionViewModel : ViewModelBase
    {
        private IVigenereService _cipherService = new VigenereService();
        private ITextFormatterService _textFormatterService = new TextFormatterService();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunEncryptCommand))]
        private string _message;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunEncryptCommand))]
        private string _key;

        [ObservableProperty]
        private string _cipher;

        public RelayCommand SwapToDecryptionCommand { get; init; }

        public EncryptionViewModel(Action swapToDecryption)
        {
            SwapToDecryptionCommand = new RelayCommand(swapToDecryption);
        }

        [RelayCommand(CanExecute = nameof(CanEncrypt))]
        private void RunEncrypt()
        {
            Cipher = _cipherService.Encrypt(_message, _key);
        }

        private bool CanEncrypt()
        {
            return !string.IsNullOrEmpty(_message) && !string.IsNullOrEmpty(_key)
                && !string.IsNullOrEmpty(_textFormatterService.ClearText(_message))
                && _textFormatterService.ClearText(_key).Length == _key.Length;
        }
    }
}
