using CommunityToolkit.Mvvm.Input;
using System;
using VigenereCipher.Interfaces.Services;
using VigenereCipher.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VigenereCipher.ViewModels
{
    public partial class HackingViewModel : ViewModelBase
    {
        private IKasiskiService _kasiskiService;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunHackingCommand))]
        private string _cipher;

        public RelayCommand SwapToEncryptionCommand { get; init; }
        public RelayCommand ActivateDecryptionMode { get; init; }
        public HackingViewModel(Action swapToEncryption, Action activateDecryptionMode)
        {
            _kasiskiService = new KasiskiService();
            SwapToEncryptionCommand = new RelayCommand(swapToEncryption);
            ActivateDecryptionMode = new RelayCommand(activateDecryptionMode);
        }

        [RelayCommand(CanExecute = nameof(CanHacking))]
        private void RunHacking()
        {
            var hackData = _kasiskiService.Hack(_cipher);
            Message = hackData.message;
            Key = hackData.key;
        }

        private bool CanHacking()
        {
            return !string.IsNullOrEmpty(_cipher);
        }
    }
}
