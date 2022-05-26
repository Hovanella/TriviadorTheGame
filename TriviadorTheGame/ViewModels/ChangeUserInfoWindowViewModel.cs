using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class ChangeUserInfoWindowViewModel : BaseViewModel.BaseViewModel{

        public ChangeUserInfoWindowViewModel()
        {
            ModelViewManager.ChangeUserInfoWindowViewModel = this;

            Confirm = new RelayCommand(o =>
            {
                ModelViewManager.UserStatisticsViewModel.ComitUserInfoChanges();
            });
        }

        public RelayCommand Confirm { get; set; }
    }
}