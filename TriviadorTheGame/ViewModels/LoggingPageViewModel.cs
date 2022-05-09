using TriviadorTheGame.Models;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class LoggingPageViewModel : BaseViewModel.BaseViewModel
    {
        public LoggingPageViewModel()
        {
            ModelViewManager.LoggingPageViewModel = this;
            OpenMainMenu = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"]; });
        }

        public RelayCommand OpenMainMenu { get; set; }
    }
}