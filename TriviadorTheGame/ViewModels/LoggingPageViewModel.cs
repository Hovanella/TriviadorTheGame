using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class LoggingPageViewModel : BaseViewModel.BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public bool IsRegistered { get; set; }

        public LoggingPageViewModel()
        {
            IsRegistered = false;
            OnPropertyChanged();

            Login = "Test";
            Password = "Test";
            ModelViewManager.LoggingPageViewModel = this;


            OpenMainMenu = new RelayCommand
            (async () =>
            {
                if (IsRegistered)
                {
                    if (IsLoggingCorrect() == false)
                        return;
                }
                else
                {
                    if (IsRegistrationCorrect() == false)
                        return;
                }

                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                {
                    ModelViewManager.MainMenuViewModel.SetRoleImage(
                        new BitmapImage(new Uri("../../Resources/Images/manager.png", UriKind.Relative)));
                    ModelViewManager.RedactorViewModel.IsNotAdmin = false;
                    ModelViewManager.RedactorViewModel.CanEdit = true;
                }

                else
                {
                    ModelViewManager.RedactorViewModel.IsNotAdmin = true;
                    ModelViewManager.RedactorViewModel.CanEdit = false;
                    ModelViewManager.MainMenuViewModel.SetRoleImage(
                        new BitmapImage(new Uri("../../Resources/Images/user.png", UriKind.Relative)));
                }


                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];
                ModelViewManager.MainWindowViewModel._mediaPlayer.Open(
                    new Uri("../../Resources/Sounds/MORGENSHTERN - Pablo.mp3", UriKind.Relative));
                ModelViewManager.MainWindowViewModel._mediaPlayer.Play();
            });
        }

        private bool IsRegistrationCorrect()
        {
            var userAccordingToLogin = UnitOfWork.UserRepository.FindUserByLogging(Login);

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }

            if (userAccordingToLogin != null)
            {
                MessageBox.Show("User with this login already exists");
                return false;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords are not equal");
                return false;
            }

            var newUser = new User(Login, Password, "C");
            UnitOfWork.UserRepository.AddUser(newUser);
            UnitOfWork.UserRepository.CurrentUser = newUser;
            return true;
        }

        private bool IsLoggingCorrect()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }

            var userAccordingToLogin = UnitOfWork.UserRepository.FindUserByLogging(Login);

            if (userAccordingToLogin == null)
            {
                MessageBox.Show("Логин введён неверно");
                return false;
            }

            if (userAccordingToLogin.USER_PASSWORD != Password)
            {
                MessageBox.Show("Пароль введён неверно");
                return false;
            }

            MessageBox.Show("Вы успешно вошли");

            UnitOfWork.UserRepository.CurrentUser = userAccordingToLogin;
            return true;
        }


        public RelayCommand OpenMainMenu { get; set; }
    }
}