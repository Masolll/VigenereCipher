namespace VigenereCipher.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentView { get; set; }

        private ViewModelBase _encryptionView { get; init; }
        private ViewModelBase _decryptionView { get; init; }
        private ViewModelBase _hackingView { get; init; }

        public MainWindowViewModel()
        {
            _encryptionView = new EncryptionViewModel(swapView);
            _decryptionView = new DecryptionViewModel(swapView, showHackingView);
            _hackingView = new HackingViewModel(swapView, showDecryptionView);
            CurrentView = _encryptionView;
        }

        private void swapView()
        {
            CurrentView = CurrentView is EncryptionViewModel ? _decryptionView : _encryptionView;
            OnPropertyChanged(nameof(CurrentView));
        }

        private void showDecryptionView()
        {
            CurrentView = _decryptionView;
            OnPropertyChanged(nameof(CurrentView));
        }

        private void showHackingView()
        {
            CurrentView = _hackingView;
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
