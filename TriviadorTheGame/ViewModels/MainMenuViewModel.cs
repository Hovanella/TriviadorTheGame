using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class MainMenuViewModel : BaseViewModel.BaseViewModel
    {
        private bool _languageChecked = false;
        private bool _volumeChecked = false;

        public MainMenuViewModel()
        {
            ModelViewManager.MainMenuViewModel = this;

            OpenLoggingPage = new RelayCommand
            (o =>
                {
                    ModelViewManager.LoggingPageViewModel.LanguageChecked = LanguageChecked;
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Stop();
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"];
                }
            );
            OpenGameLobbyPage = new RelayCommand
            (o =>
            {
                ModelViewManager.GameLobbyViewModel.VolumeChecked = VolumeChecked;
                ModelViewManager.GameLobbyViewModel.LanguageChecked = LanguageChecked;
                ModelViewManager.GameLobbyViewModel.SetHost(UnitOfWork.UserRepository.CurrentUser);
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["GameLobbyPage"];
            });

            OpenUserStatisticsPage = new RelayCommand(o =>
            {
                ModelViewManager.UserStatisticsViewModel.VolumeChecked = VolumeChecked;
                ModelViewManager.UserStatisticsViewModel.LanguageChecked = LanguageChecked;
                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    ModelViewManager.UserStatisticsViewModel.IsAdmin = true;
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["UserStatisticsPage"];
            });

            OpenRedactorPage = new RelayCommand
                (o =>

                {
                    ModelViewManager.RedactorViewModel.VolumeChecked = VolumeChecked;
                    ModelViewManager.RedactorViewModel.LanguageChecked = LanguageChecked;
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["RedactorPage"];
                    ModelViewManager.RedactorViewModel.UpdateCollection();
                });

            ExitCommand = new RelayCommand
                (o => { Application.Current.Shutdown(); });

            OpenQuestionArchive = new RelayCommand(o =>
                {
                    ModelViewManager.QuestionArchiveViewModel.VolumeChecked = VolumeChecked;
                    ModelViewManager.QuestionArchiveViewModel.LanguageChecked = LanguageChecked;
                    ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["QuestionArchivePage"];
                }
            );

           
            
            

            ChangeLanguage = new RelayCommand(o =>
            {
                ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);
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

        public RelayCommand OpenUserStatisticsPage { get; set; }

        public RelayCommand OpenQuestionArchive { get; set; }

        public RelayCommand SwitchVolume { get; set; }

        public ToolTip ToolTip { get; set; } = new ToolTip();

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

        public RelayCommand OpenLoggingPage { get; set; }
        public RelayCommand OpenGameLobbyPage { get; set; }
        public RelayCommand OpenRedactorPage { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public ImageSource RoleImage { get; set; }
        public RelayCommand OpenAboutGamePage { get; set; }
        public RelayCommand ChangeLanguage { get; set; }

        public void SetRoleImage(BitmapImage bitmapImage)
        {
            RoleImage = bitmapImage;
            OnPropertyChanged();
        }
    }
}