using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VigenereCipher.Service;

namespace VigenereCipher.ViewModels
{
    public class DecryptionViewModel : ViewModelBase
    {
        private ICipherService _cipherService;
        private string _message;
        private string _key;
        private string _cipher;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public string Cipher 
        {
            get => _cipher;
            set
            {
                _cipher = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand SwapToEncryptionCommand { get; init; }
        public RelayCommand RunDecryptCommand { get; init; }

        public DecryptionViewModel(Action swapToEncryption)
        {
            _cipherService = new CipherService();
            SwapToEncryptionCommand = new RelayCommand(swapToEncryption);
            RunDecryptCommand = new RelayCommand(runDecrypt);
        }

        private void runDecrypt()
        {
            var decryption = _cipherService.Decrypt(_cipher);
            Message = decryption.message;
            Key = decryption.key;
        }
    }
}
