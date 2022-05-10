using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.ViewModels
{
    public class GameLobbyViewModel:BaseViewModel.BaseViewModel
    {
        public GameLobbyViewModel()
        {
            ModelViewManager.GameLobbyViewModel = this;

        }

        public User HostUser { get; set; }

        public void SetHost(User currentUser)
        {
            HostUser = currentUser;
            OnPropertyChanged();
        }
    }
}