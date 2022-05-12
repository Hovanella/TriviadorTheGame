using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class MainMenuViewModel : BaseViewModel.BaseViewModel
    {
        public MainMenuViewModel()
        {
            ModelViewManager.MainMenuViewModel = this;

            OpenLoggingPage = new RelayCommand
            (
                async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"]; }
            );
            OpenGameLobbyPage = new RelayCommand
                (async () =>
                {
                    ModelViewManager.GameLobbyViewModel.SetHost(UnitOfWork.UserRepository.CurrentUser);
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["GameLobbyPage"];
                });

            OpenRedactorPage = new RelayCommand
                (async () =>
                {
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["RedactorPage"];
                  
                });

            ExitCommand = new RelayCommand
                (async () => { App.Current.Shutdown(); });

            OpenAboutGamePage = new RelayCommand(async () =>
            {
                ModelViewManager.MainWindowViewModel.ToggleAppSound();
            });
        }

        public void SetRoleImage(BitmapImage bitmapImage)
        {
            RoleImage = bitmapImage;
            OnPropertyChanged();
        }

        public RelayCommand OpenLoggingPage { get; set; }
        public RelayCommand OpenGameLobbyPage { get; set; }
        public RelayCommand OpenRedactorPage { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public ImageSource RoleImage { get; set; }
        public RelayCommand OpenAboutGamePage { get; set; }
    }
}