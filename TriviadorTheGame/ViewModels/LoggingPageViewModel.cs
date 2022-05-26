using System;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class LoggingPageViewModel : BaseViewModel.BaseViewModel
    {
        public LoggingPageViewModel()
        {
            IsRegistered = false;
            OnPropertyChanged();

            Login = "Test";
            Password = "Test";
            ModelViewManager.LoggingPageViewModel = this;



            OpenMainMenu = new RelayCommand
            (o =>
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
                    ModelViewManager.MainMenuViewModel.ToolTip.SetResourceReference(ToolTip.ContentProperty,"AdminTooltip");

                    ;

                    ModelViewManager.MainMenuViewModel.SetRoleImage(
                        new BitmapImage(new Uri("../../Resources/Images/manager.png", UriKind.Relative)));
                    ModelViewManager.RedactorViewModel.IsNotAdmin = false;
                    ModelViewManager.RedactorViewModel.CanEdit = true;
                }

                else
                {
                    ModelViewManager.MainMenuViewModel.ToolTip.SetResourceReference(ToolTip.ContentProperty,"ClientTooltip");
                    
                        /*ModelViewManager.MainMenuViewModel.ToolTip.Content =
                        Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                            x => x.Contains("ClientTooltip"))?["ClientTooltip"];*/
                        
                    ModelViewManager.RedactorViewModel.IsNotAdmin = true;

                    ModelViewManager.RedactorViewModel.CanEdit = false;
                    ModelViewManager.MainMenuViewModel.SetRoleImage(
                        new BitmapImage(new Uri("../../Resources/Images/user.png", UriKind.Relative)));
                }
            


                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];

                ModelViewManager.MainMenuViewModel.LanguageChecked = LanguageChecked;

            
                ModelViewManager.MainWindowViewModel._mediaPlayer.Open(
                    new Uri("../../Resources/Sounds/Ведьмак 3 — Silver for Monsters (Essenthy Arrange) (www.lightaudio.ru).mp3", UriKind.Relative));
                ModelViewManager.MainWindowViewModel._mediaPlayer.Play();
            

            });
            ChangeLanguage = new RelayCommand((o) =>
            {
                ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);
            });
        }

        public bool LanguageChecked { get; set; } = false;
        public RelayCommand ChangeLanguage { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public bool IsRegistered { get; set; }


        public RelayCommand OpenMainMenu { get; set; }

        private bool IsRegistrationCorrect()
        {
            var userAccordingToLogin = UnitOfWork.UserRepository.FindUserByLogging(Login);

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("FillFieldsMessage"))?["FillFieldsMessage"]);
                return false;
            }

            if (userAccordingToLogin != null)
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("UserExistsMessage"))?["UserExistsMessage"]);
                return false;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("PasswordsNotEqualMessage"))?["PasswordsNotEqualMessage"]);
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
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("FillFieldsMessage"))?["FillFieldsMessage"]);
                return false;
            }

            var userAccordingToLogin = UnitOfWork.UserRepository.FindUserByLogging(Login);

            if (userAccordingToLogin == null)
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("IncorrectLoginMessage"))?["IncorrectLoginMessage"]);
                return false;
            }

            if (userAccordingToLogin.USER_PASSWORD != Password)
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("IncorrectPasswordMessage"))?["IncorrectPasswordMessage"]);
                return false;
            }

            
            

            UnitOfWork.UserRepository.CurrentUser = userAccordingToLogin;
            return true;
        }
    }
}