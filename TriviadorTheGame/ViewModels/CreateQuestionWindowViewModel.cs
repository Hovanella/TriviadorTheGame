using System;
using System.Net.Mime;
using System.Windows;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class CreatePackWindowViewModel : BaseViewModel.BaseViewModel
    {
        private string _questionText = String.Empty;
        private string _rightAnswer = String.Empty;
        private string _firstWrongAnswer = String.Empty;
        private string _secondWrongAnswer = String.Empty;
        private string _thirdWrongAnswer = String.Empty;

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
        


        public CreatePackWindowViewModel()
        {
            
            ModelViewManager.CreatePackWindowViewModel = this;
            
           
            CloseCreatePackWindowCommand = new RelayCommand(() =>
            {
                ModelViewManager.RedactorViewModel.CloseCreateQuestionWindow();
            });
            
        }

        public RelayCommand CloseCreatePackWindowCommand { get; set; }
        

    }
}