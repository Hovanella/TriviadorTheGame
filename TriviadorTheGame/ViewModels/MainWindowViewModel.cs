using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.ViewModels
{
    public class MainWindowViewModel : BaseViewModel.BaseViewModel
    {
        private Page _currentPage;

        public  MediaPlayer _mediaPlayer { get; } = new MediaPlayer();

        public MainWindowViewModel()
        {
            ModelViewManager.MainWindowViewModel = this;
            CurrentPage = Navigation.Pages["LoggingPage"];
            
 

        }

        public void ToggleAppSound()
        {
            _mediaPlayer.IsMuted= !_mediaPlayer.IsMuted;
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
    }
}