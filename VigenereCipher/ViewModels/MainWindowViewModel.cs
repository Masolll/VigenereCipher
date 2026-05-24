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
            _encryptionView = new EncryptionViewModel(SwapView);
            _decryptionView = new DecryptionViewModel(SwapView, ShowHackingView);
            _hackingView = new HackingViewModel(SwapView, ShowDecryptionView);
            CurrentView = _encryptionView;
        }

        private void SwapView()
        {
            CurrentView = CurrentView is EncryptionViewModel ? _decryptionView : _encryptionView;
            OnPropertyChanged(nameof(CurrentView));
        }

        private void ShowDecryptionView()
        {
            CurrentView = _decryptionView;
            OnPropertyChanged(nameof(CurrentView));
        }

        private void ShowHackingView()
        {
            CurrentView = _hackingView;
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
