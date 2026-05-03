using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VigenereCipher.Service;

namespace VigenereCipher.ViewModels
{
    public class EncryptionViewModel : ViewModelBase
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
        public RelayCommand SwapToDecryptionCommand { get; init; }
        public RelayCommand RunEncryptCommand { get; init; }

        public EncryptionViewModel(Action swapToDecryption)
        {
            _cipherService = new CipherService();
            SwapToDecryptionCommand = new RelayCommand(swapToDecryption);
            RunEncryptCommand = new RelayCommand(runEncrypt);
        }

        private void runEncrypt()
        {
            Cipher = _cipherService.Encrypt(_message, _key);
        }
    }
}
