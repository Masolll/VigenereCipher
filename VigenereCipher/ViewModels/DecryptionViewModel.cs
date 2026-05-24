using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VigenereCipher.ViewModels
{
    public partial class DecryptionViewModel : ViewModelBase
    {
        private ICipherService _cipherService = new CipherService();
        private ITextFormatterService _textFormatterService = new TextFormatterService();

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunDecryptCommand))]
        private string _key;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunDecryptCommand))]
        private string _cipher;

        public RelayCommand SwapToEncryptionCommand { get; init; }
        public RelayCommand ActivateHackingMode { get; init; }

        public DecryptionViewModel(Action swapToEncryption, Action activateHackingMode)
        {
            SwapToEncryptionCommand = new RelayCommand(swapToEncryption);
            ActivateHackingMode = new RelayCommand(activateHackingMode);
        }

        [RelayCommand(CanExecute = nameof(CanDecrypt))]
        private void RunDecrypt()
        {
            var decryption = _cipherService.Decrypt(_cipher, _key);
            Message = decryption;
        }
        
        private bool CanDecrypt()
        {
            return !string.IsNullOrEmpty(_cipher) && !string.IsNullOrEmpty(_key)
                && !string.IsNullOrEmpty(_textFormatterService.ClearText(_cipher))
                && _textFormatterService.ClearText(_key).Length == _key.Length;
        }
    }
}
