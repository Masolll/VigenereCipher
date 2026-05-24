using CommunityToolkit.Mvvm.Input;
using System;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VigenereCipher.ViewModels
{
    public class HackingViewModel : ViewModelBase
    {
        private IKasiskiService _kasiskiService;

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
        public RelayCommand ActivateDecryptionMode { get; init; }
        public RelayCommand RunHackingCommand { get; init; }
        public HackingViewModel(Action swapToEncryption, Action activateDecryptionMode)
        {
            _kasiskiService = new KasiskiService();
            SwapToEncryptionCommand = new RelayCommand(swapToEncryption);
            ActivateDecryptionMode = new RelayCommand(activateDecryptionMode);
            RunHackingCommand = new RelayCommand(runHacking);
        }

        private void runHacking()
        {
            var hackData = _kasiskiService.Hack(_cipher);
            Message = hackData.message;
            Key = hackData.key;
        }
    }
}
