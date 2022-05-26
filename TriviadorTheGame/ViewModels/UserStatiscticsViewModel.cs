using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using MaterialDesignThemes.Wpf;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.UserStatistics;

namespace TriviadorTheGame.ViewModels
{
    public class UserStatiscticsViewModel : BaseViewModel.BaseViewModel
    {
        private ObservableCollection<UserWithStatistics> _users;
        private UserWithStatistics _selectedUser;
        private string _searchText=String.Empty;
        private bool _volumeChecked;
        private bool _languageChecked;

        public ChangePasswordAndLoginWindow _ChangePasswordAndLoginWindow { get; set; } = new();
        public ChangeUserInfoWindow _ChangeUserInfoWindow { get; set; } = new();


        public bool IsAdmin { get; set; } = false;

        public ObservableCollection<UserWithStatistics> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }

        }
        
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }
        
        public UserWithStatistics SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public bool LanguageChecked
        {
            get => _languageChecked;
            set { _languageChecked = value; OnPropertyChanged(); }
        }

        public void Update()
        {
            Users=UnitOfWork.UserRepository.GetUserWithStatistics();
        }

        public UserStatiscticsViewModel()
        {
            ModelViewManager.UserStatisticsViewModel = this;
            
            Users=UnitOfWork.UserRepository.GetUserWithStatistics();
            
            ChangeLanguage = new RelayCommand((o)=>
                {
                    ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);
                }
            );

            DeleteSelectedUser = new RelayCommand(o =>
            {
              var allUserQuestions = UnitOfWork.QuestionPackRepository.GetAllQuestionsOfUser(SelectedUser.USER_ID);
              
              foreach (var question in allUserQuestions)
              {
                  UnitOfWork.QuestionPackRepository.DeleteQuestionsFromAllPacks(question);
              }
              
              UnitOfWork.QuestionRepository.DeleteAllQuestionsOfUser(SelectedUser.USER_ID);
              
              var allUserPacks = UnitOfWork.QuestionPackRepository.GetAllPacksOfUser(SelectedUser.USER_ID);
              
              foreach (var pack in allUserPacks)
              {
                  UnitOfWork.QuestionPackRepository.DeleteQuestionsFromPackById(pack.QUESTIONS_PACK_ID);
              }
              
              UnitOfWork.QuestionRepository.DeleteAllPacksOfUser(SelectedUser.USER_ID);

              UnitOfWork.UserRepository.DeleteUser(SelectedUser.USER_ID);

              Update();

            }, o =>
            {
                if (UnitOfWork.UserRepository.CurrentUser is { USER_ROLE: "A" } && SelectedUser != null && SelectedUser.USER_ROLE != "A")
                    return true;
                return false;
            });
            
            OpenMainMenuPage = new RelayCommand(o =>
            {
                ModelViewManager.MainMenuViewModel.LanguageChecked = LanguageChecked;
                ModelViewManager.MainMenuViewModel.VolumeChecked= VolumeChecked;
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];
            });

            ChangeOwnPasswordAndLogin = new RelayCommand(o =>
            {
                var password = UnitOfWork.UserRepository.CurrentUser.USER_PASSWORD;
                var login = UnitOfWork.UserRepository.CurrentUser.USER_LOGIN;
                
                _ChangePasswordAndLoginWindow.LoginTextBox.Text = login;
                _ChangePasswordAndLoginWindow.PasswordTextBox.Text = password;
                

                _ChangePasswordAndLoginWindow.ShowDialog();
            });

            EditSelectedUser = new RelayCommand(o =>
            {
                
                _ChangeUserInfoWindow.ShowDialog();
            }, o =>
            {
                if (UnitOfWork.UserRepository.CurrentUser is { USER_ROLE: "A" } && SelectedUser != null && SelectedUser.USER_ROLE != "A")
                    return true;
                return false;
            });
            
            Search = new RelayCommand(o=>
            {
                Update();
                if (SearchText==String.Empty) return; 
                
                Users = new ObservableCollection<UserWithStatistics>( Users.Where(x => x.USER_LOGIN.ToLower().Contains(SearchText.ToLower())));
            });

            SwitchVolume = new RelayCommand(o =>
            {
                if (VolumeChecked==true)
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0;
                }
                else
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0.5;
                }
            });

        }

        public RelayCommand SwitchVolume { get; set; }

        public RelayCommand OpenMainMenuPage { get; set; }

        public RelayCommand ChangeLanguage { get; set; }

        public RelayCommand Search { get; set; }

        public RelayCommand EditSelectedUser { get; set; }

        public RelayCommand ChangeOwnPasswordAndLogin { get; set; }

        public RelayCommand DeleteSelectedUser { get; set; }

        public bool VolumeChecked
        {
            get => _volumeChecked;
            set { _volumeChecked = value; OnPropertyChanged(); }
        }

        public void ComitChanges()
        {
            UnitOfWork.UserRepository.CurrentUser.USER_LOGIN = _ChangePasswordAndLoginWindow.LoginTextBox.Text;
            UnitOfWork.UserRepository.CurrentUser.USER_PASSWORD = _ChangePasswordAndLoginWindow.PasswordTextBox.Text;
            UnitOfWork.UserRepository.Update();
            _ChangePasswordAndLoginWindow.Hide();
            
        }

        public void ComitUserInfoChanges()
        {
            var login = _ChangeUserInfoWindow.LoginTextBox.Text;
            var password = _ChangeUserInfoWindow.PasswordTextBox.Text;
            var scoreNumber = _ChangeUserInfoWindow.ScoreNumber.Text;
            var gamesNumber = _ChangeUserInfoWindow.GamesNumber.Text;
            
            if (int.TryParse(scoreNumber, out int score) && int.TryParse(gamesNumber, out int games))
            {
                UnitOfWork.UserRepository.UpdateUserInfo(SelectedUser.USER_ID, login, password, score, games);
                Update();
                _ChangeUserInfoWindow.Hide();
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Contains("IncorrectNumberData"))?["IncorrectNumberData"]);
            }
            
        }
    }
}