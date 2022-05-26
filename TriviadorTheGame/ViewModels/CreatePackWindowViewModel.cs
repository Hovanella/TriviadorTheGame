using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class CreatePackWindowViewModel : BaseViewModel.BaseViewModel
    {
        private string _PackName = string.Empty;
        public bool Editing { get; set; } = false;

        public CreatePackWindowViewModel()
        {
            ModelViewManager.CreatePackWindowViewModel = this;
            

            CloseCreatePackWindowCommand = new RelayCommand(o =>
            {
                ModelViewManager.RedactorViewModel.CloseCreatePackWindow();
            });
        }


        public string PackName
        {
            get => _PackName;
            set
            {
                _PackName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseCreatePackWindowCommand { get; set; }
    }
}