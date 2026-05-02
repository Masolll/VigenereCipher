using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using VigenereCipher.Views;

namespace VigenereCipher.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentView { get; set; }

        private ViewModelBase _encryptionView { get; init; }
        private ViewModelBase _decryptionView { get; init; }

        public MainWindowViewModel()
        {
            _encryptionView = new EncryptionViewModel(swapView);
            _decryptionView = new DecryptionViewModel(swapView);
            CurrentView = _encryptionView;
        }

        private void swapView()
        {
            CurrentView = CurrentView is EncryptionViewModel ? _decryptionView : _encryptionView;
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
