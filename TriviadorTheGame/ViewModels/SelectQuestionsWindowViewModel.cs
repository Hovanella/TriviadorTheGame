using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class SelectQuestionsWindowViewModel : BaseViewModel.BaseViewModel
    {
        private ObservableCollection<Question> _questions;
        private string _searchText = string.Empty;

        private bool _myQuestions;

        public bool MyQuestions
        {
            get => _myQuestions;
            set { _myQuestions = value; OnPropertyChanged(); }
        }

        private Question _selectedQuestion;

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }

        public SelectQuestionsWindowViewModel()
        {
            ModelViewManager.SelectQuestionsWindowViewModel = this;
            
            Questions = new ObservableCollection<Question>( UnitOfWork.QuestionRepository.GetAll());


            Search = new RelayCommand(o =>
            {
                Update();
                if (_myQuestions == false)
                    Questions = new ObservableCollection<Question>(Questions.Where(x =>
                        x.CREATOR_ID == UnitOfWork.UserRepository.CurrentUser.USER_ID));
                if (SearchText == String.Empty)
                    return;


                Questions = new ObservableCollection<Question>(Questions.Where(x =>
                    x.ToString().Contains(SearchText.ToLower())));
            });
            
            ChooseQuestions = new RelayCommand(o =>
            {
                var questions = ModelViewManager.RedactorViewModel.SelectedQuestionPackList.Questions;
                
                if (questions.FirstOrDefault(x =>
                        x.QUESTION_ID == _selectedQuestion.QUESTION_ID) == null)
                {
                    UnitOfWork.QuestionPackRepository.AddQuestionToPack(SelectedQuestion,
                        ModelViewManager.RedactorViewModel.SelectedQuestionPackList);
                    ModelViewManager.RedactorViewModel.UpdateCollection();
                }
            },o=> SelectedQuestion != null);
            CloseSelectQuestionsWindow = new RelayCommand(o =>
            {
              
                ModelViewManager.RedactorViewModel._SelectQuestionsWindow.Hide();
            });
        }

        public RelayCommand CloseSelectQuestionsWindow { get; set; }

        public RelayCommand ChooseQuestions { get; set; }

        public string SearchText { get; set; } = String.Empty;

        private void Update()
        {
            Questions = new ObservableCollection<Question>( UnitOfWork.QuestionRepository.GetAll());
        }

        public RelayCommand Search { get; set; }

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged();
            }
        }
    }
}