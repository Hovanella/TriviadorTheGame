using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.GameLobbyPage;

namespace TriviadorTheGame.ViewModels
{
    public class GameLobbyViewModel : BaseViewModel.BaseViewModel
    {
        private User _firstFriendUser;
        private User _secondFriendUser;
        private bool _languageChecked;
        private bool _volumeChecked;

        public GameLobbyViewModel()
        {
            ModelViewManager.GameLobbyViewModel = this;
            PacksList = (ObservableCollection<QuestionsPack>)UnitOfWork.QuestionPackRepository.GetAll();
            GameLobbyLoggingWindows = new GameLobbyLoggingWindows();
            GameLobbyLoggingWindows.Hide();
            StartTheGame = new RelayCommand(o =>
            {
                if (FirstFriendUser == null || SecondFriendUser == null)
                {
                    MessageBox.Show((string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                        x => x.Contains("MustSelectTwoFriends"))?["MustSelectTwoFriends"]);
                    return;
                }
                
                var game = new Game(SelectedNumberOfRounds, HostUser, FirstFriendUser,
                    SecondFriendUser, IsRating,
                    UnitOfWork.QuestionPackRepository.GetPackWithQuestionsById(SelectedPack.QUESTIONS_PACK_ID));

                ModelViewManager.GameplayViewModel.VolumeChecked = VolumeChecked;
                ModelViewManager.GameplayViewModel.Game = game;
                ModelViewManager.GameplayViewModel.Game.Start();
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["GameplayPage"];
            }, o => SelectedPack != null && SelectedNumberOfRounds != null && SelectedNumberOfRounds != string.Empty);

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

            FirstPlayerLoggingCommand = new RelayCommand(o =>
            {
                if (GameLobbyLoggingWindows == null) GameLobbyLoggingWindows = new GameLobbyLoggingWindows();

                GameLobbyLoggingWindows.ShowDialog();
                if (GameLobbyLoggingWindows.DialogResult != true)
                    FirstFriendUser = ModelViewManager.CreateLobbyLoggingWindowViewModel.user;
            });

            ChangeLanguage = new RelayCommand(o =>
            {
                ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);
            });

            SecondPlayerLoggingCommand = new RelayCommand(o =>
            {
                if (GameLobbyLoggingWindows == null) GameLobbyLoggingWindows = new GameLobbyLoggingWindows();

                GameLobbyLoggingWindows.ShowDialog();
                if (GameLobbyLoggingWindows.DialogResult != true)
                    SecondFriendUser = ModelViewManager.CreateLobbyLoggingWindowViewModel.user;
            });
            
            OpenMainMenu = new RelayCommand(o =>
                {
                    ModelViewManager.MainMenuViewModel.VolumeChecked = VolumeChecked;
                    ModelViewManager.MainMenuViewModel.LanguageChecked = LanguageChecked;
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];
                }
            );
        }

        public RelayCommand OpenMainMenu { get; set; }

        public RelayCommand SwitchVolume { get; set; }

        public RelayCommand ChangeLanguage { get; set; }


        public User HostUser { get; set; }
        public string[] NumberOfRounds { get; set; } = { "5", "10", "15", "20", "-" };
        public bool IsRating { get; set; } = true;
        public string SelectedNumberOfRounds { get; set; } = string.Empty;

        public User FirstFriendUser
        {
            get => _firstFriendUser;
            set
            {
                _firstFriendUser = value;
                OnPropertyChanged();
            }
        }

        public User SecondFriendUser
        {
            get => _secondFriendUser;
            set
            {
                _secondFriendUser = value;
                OnPropertyChanged();
            }
        }

        public QuestionsPack SelectedPack { get; set; }
        public ObservableCollection<QuestionsPack> PacksList { get; set; } = new();
        public RelayCommand StartTheGame { get; set; }

        public RelayCommand FirstPlayerLoggingCommand { get; set; }

        public RelayCommand SecondPlayerLoggingCommand { get; set; }

        public GameLobbyLoggingWindows GameLobbyLoggingWindows { get; set; }

        public bool LanguageChecked
        {
            get => _languageChecked;
            set { _languageChecked = value; OnPropertyChanged(); }
        }

        public bool VolumeChecked
        {
            get => _volumeChecked;
            set { _volumeChecked = value; OnPropertyChanged(); }
        }

        public void SetHost(User currentUser)
        {
            HostUser = currentUser;
            OnPropertyChanged();
        }
    }
}