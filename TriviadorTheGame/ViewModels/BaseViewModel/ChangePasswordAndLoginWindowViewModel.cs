namespace TriviadorTheGame.ViewModels.BaseViewModel
{
    public class ChangePasswordAndLoginWindowViewModel:BaseViewModel
    {
        public ChangePasswordAndLoginWindowViewModel()
        {
            ModelViewManager.ChangePasswordAndLoginWindowViewModel = this;

            ConfirmCommand = new RelayCommand((o) =>
            {
                ModelViewManager.UserStatisticsViewModel.ComitChanges();
            });
        }

        public RelayCommand ConfirmCommand { get; set; }
    }
}