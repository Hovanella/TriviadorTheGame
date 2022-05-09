using System.Windows.Input;
using System.Windows.Navigation;
using TriviadorTheGame.Models;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views;

namespace TriviadorTheGame.ViewModels
{

    public class MainMenuViewModel : BaseViewModel.BaseViewModel
    {
        public MainMenuViewModel()
        {
            ModelViewManager.MainMenuViewModel = this;
            OpenLoggingPage = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"]; });
            OpenGameLobbyPage = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["GameLobbyPage"]; });

            OpenRedactorPage = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["RedactorPage"]; });
        }

        public RelayCommand OpenLoggingPage { get; set; }
        public RelayCommand OpenGameLobbyPage { get; set; }
        public RelayCommand OpenRedactorPage { get; set; }
    }
}