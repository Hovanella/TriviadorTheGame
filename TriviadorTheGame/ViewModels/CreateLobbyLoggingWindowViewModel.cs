using System.Linq;
using System.Windows;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class CreateLobbyLoggingWindowViewModel : BaseViewModel.BaseViewModel
    {
        public CreateLobbyLoggingWindowViewModel()
        {
            ModelViewManager.CreateLobbyLoggingWindowViewModel = this;

            LoggingCommand = new RelayCommand(_ =>
            {
                user = null;
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                        x => x.Contains("FillFieldsMessage"))?["FillFieldsMessage"]);
                    return;
                }

                var userAccordingToLogin = UnitOfWork.UserRepository.FindUserByLogging(Login);

                if (userAccordingToLogin == null)
                {
                    MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                        x => x.Contains("IncorrectLoginMessage"))?["IncorrectLoginMessage"]);
                    return;
                }

                if (userAccordingToLogin.USER_PASSWORD != Password)
                {
                    MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                        x => x.Contains("IncorrectPasswordMessage"))?["IncorrectPasswordMessage"]);
                    return;
                }

                if (userAccordingToLogin == ModelViewManager.GameLobbyViewModel.HostUser ||
                    userAccordingToLogin == ModelViewManager.GameLobbyViewModel.FirstFriendUser ||
                    userAccordingToLogin == ModelViewManager.GameLobbyViewModel.SecondFriendUser)
                {
                    MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                        x => x.Contains("PlayerAlreadyInLobby"))?["PlayerAlreadyInLobby"]);
                    return;
                }

                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("UserJoin"))?["UserJoin"]);
                user = userAccordingToLogin;
                ModelViewManager.GameLobbyViewModel.GameLobbyLoggingWindows.Hide();
            });
        }

        public User user { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RelayCommand LoggingCommand { get; set; }
    }
}