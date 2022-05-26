using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class QuestionWindowViewModel : BaseViewModel.BaseViewModel
    {
        private string _questionText = "";
        private string _fourthAnswer = "";
        private string _firstAnswer = "";
        private string _secondAnswer = "";
        private string _thirdAnswer = "";
        private bool toogle = false;
        private bool toggleForButtons = false;
        private SolidColorBrush _firstAnswerBrush = new(Colors.AntiqueWhite);
        private SolidColorBrush _secondAnswerBrush = new(Colors.AntiqueWhite);
        private SolidColorBrush _thirdAnswerBrush = new(Colors.AntiqueWhite);
        private SolidColorBrush _fourthAnswerBrush = new(Colors.AntiqueWhite);

        public QuestionWindowViewModel()
        {
            ModelViewManager.QuestionWindowViewModel = this;



            CloseCommand = new RelayCommand((obj) =>
            {
                ModelViewManager.GameplayViewModel.QuestionWindow.Hide();
                toogle = false;
            });

            SubmitChooseCommand = new RelayCommand((obj) =>
            {
                
                var answer = obj as string;
                isAnswerCorrect = answer == Question.RIGHT_ANSWER;
                ChangeColorsOfAnswers(answer, isAnswerCorrect);


                ModelViewManager.GameplayViewModel.CreateResults();

                ModelViewManager.GameplayViewModel.QuestionWindow.Hide();
            });
        }

        public RelayCommand CloseCommand { get; set; }

        private void ChangeColorsOfAnswers(string answer, bool isAnswerCorrect)
        {
            if (Question.RIGHT_ANSWER == _firstAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.FirstAnswerButton.Background =
                    new SolidColorBrush(Colors.GreenYellow);
            else if (Question.RIGHT_ANSWER == _secondAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.SecondAnswerButton.Background =
                    new SolidColorBrush(Colors.GreenYellow);
            else if (Question.RIGHT_ANSWER == _thirdAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.ThirdAnswerButton.Background =
                    new SolidColorBrush(Colors.GreenYellow);
            else if (Question.RIGHT_ANSWER == _fourthAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.FourthAnswerButton.Background =
                    new SolidColorBrush(Colors.GreenYellow);

            if (isAnswerCorrect) return;
            if (answer == _firstAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.FirstAnswerButton.Background =
                    new SolidColorBrush(Colors.Red);
            else if (answer == _secondAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.SecondAnswerButton.Background =
                    new SolidColorBrush(Colors.Red);
            else if (answer == _thirdAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.ThirdAnswerButton.Background =
                    new SolidColorBrush(Colors.Red);
            else if (answer == _fourthAnswer)
                ModelViewManager.GameplayViewModel.QuestionWindow.FourthAnswerButton.Background =
                    new SolidColorBrush(Colors.Red);
        }

        public Question Question { get; set; } = null;
        public bool isAnswerCorrect { get; set; } = false;

        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                OnPropertyChanged();
            }
        }

        public string FourthAnswer
        {
            get => _fourthAnswer;
            set
            {
                _fourthAnswer = value;
                OnPropertyChanged();
            }
        }

        public string FirstAnswer
        {
            get => _firstAnswer;
            set
            {
                _firstAnswer = value;
                OnPropertyChanged();
            }
        }

        public string SecondAnswer
        {
            get => _secondAnswer;
            set
            {
                _secondAnswer = value;
                OnPropertyChanged();
            }
        }

        public string ThirdAnswer
        {
            get => _thirdAnswer;
            set
            {
                _thirdAnswer = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush FirstAnswerBrush
        {
            get => _firstAnswerBrush;
            set
            {
                _firstAnswerBrush = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush SecondAnswerBrush
        {
            get => _secondAnswerBrush;
            set
            {
                _secondAnswerBrush = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush ThirdAnswerBrush
        {
            get => _thirdAnswerBrush;
            set
            {
                _thirdAnswerBrush = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush FourthAnswerBrush
        {
            get => _fourthAnswerBrush;
            set
            {
                _fourthAnswerBrush = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand SubmitChooseCommand { get; set; }

        public void PrepareQuestion()
        {
            FirstAnswerBrush = new SolidColorBrush(Colors.AntiqueWhite);
            SecondAnswerBrush = new SolidColorBrush(Colors.AntiqueWhite);
            ThirdAnswerBrush = new SolidColorBrush(Colors.AntiqueWhite);
            FourthAnswerBrush = new SolidColorBrush(Colors.AntiqueWhite);
            
            ModelViewManager.GameplayViewModel.QuestionWindow.CloseWindow.Visibility = Visibility.Collapsed;

      
  


            int random = new Random().Next(0, 3);

            QuestionText = Question.QUESTION_TEXT;
            switch (random)
            {
                case 0:
                    FirstAnswer = Question.RIGHT_ANSWER;
                    SecondAnswer = Question.FIRST_WRONG_ANSWER;
                    ThirdAnswer = Question.SECOND_WRONG_ANSWER;
                    FourthAnswer = Question.THIRD_WRONG_ANSWER;
                    break;
                case 1:
                    FirstAnswer = Question.FIRST_WRONG_ANSWER;
                    SecondAnswer = Question.RIGHT_ANSWER;
                    ThirdAnswer = Question.SECOND_WRONG_ANSWER;
                    FourthAnswer = Question.THIRD_WRONG_ANSWER;
                    break;
                case 2:
                    FirstAnswer = Question.FIRST_WRONG_ANSWER;
                    SecondAnswer = Question.SECOND_WRONG_ANSWER;
                    ThirdAnswer = Question.RIGHT_ANSWER;
                    FourthAnswer = Question.THIRD_WRONG_ANSWER;
                    break;
            }
        }

        public void SubmitFunction(string answer)
        {
            ModelViewManager.GameplayViewModel.QuestionWindow.FirstAnswerButton.IsEnabled = false;
            ModelViewManager.GameplayViewModel.QuestionWindow.SecondAnswerButton.IsEnabled = false;
                       ModelViewManager.GameplayViewModel.QuestionWindow.ThirdAnswerButton.IsEnabled = false;
            ModelViewManager.GameplayViewModel.QuestionWindow.FourthAnswerButton.IsEnabled = false;
            if(toogle==true) return;
            toogle = true;

            isAnswerCorrect = answer == Question.RIGHT_ANSWER;
            ChangeColorsOfAnswers(answer, isAnswerCorrect);
            

            ModelViewManager.GameplayViewModel.CreateResults();

            ModelViewManager.GameplayViewModel.QuestionWindow.CloseWindow.Visibility = Visibility.Visible;
            
          
        }

        public void CloseSelf()
        {
            ModelViewManager.GameplayViewModel.QuestionWindow.FirstAnswerButton.IsEnabled = true;
            ModelViewManager.GameplayViewModel.QuestionWindow.SecondAnswerButton.IsEnabled = true;
            ModelViewManager.GameplayViewModel.QuestionWindow.ThirdAnswerButton.IsEnabled = true;
            ModelViewManager.GameplayViewModel.QuestionWindow.FourthAnswerButton.IsEnabled = true;
            ModelViewManager.GameplayViewModel.QuestionWindow.Hide();
            toogle = false;
        }

       
    }
}