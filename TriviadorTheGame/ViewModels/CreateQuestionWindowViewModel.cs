using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class CreateQuestionWindowViewModel : BaseViewModel.BaseViewModel
    {
        private string _firstWrongAnswer = string.Empty;
        private string _questionText = string.Empty;
        private string _rightAnswer = string.Empty;
        private string _secondWrongAnswer = string.Empty;
        private string _thirdWrongAnswer = string.Empty;


        public CreateQuestionWindowViewModel()
        {
            ModelViewManager.CreateQuestionWindowViewModel = this;


            CloseCreateQuestionWindowCommand = new RelayCommand(o =>
            {
                ModelViewManager.QuestionArchiveViewModel.CloseCreateQuestionWindow();
            });
        }

        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                OnPropertyChanged();
            }
        }

        public string FirstWrongAnswer
        {
            get => _firstWrongAnswer;
            set
            {
                _firstWrongAnswer = value;
                OnPropertyChanged();
            }
        }

        public string SecondWrongAnswer
        {
            get => _secondWrongAnswer;
            set
            {
                _secondWrongAnswer = value;
                OnPropertyChanged();
            }
        }

        public string ThirdWrongAnswer
        {
            get => _thirdWrongAnswer;
            set
            {
                _thirdWrongAnswer = value;
                OnPropertyChanged();
            }
        }

        public string RightAnswer
        {
            get => _rightAnswer;
            set
            {
                _rightAnswer = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseCreateQuestionWindowCommand { get; set; }
  
    }
}