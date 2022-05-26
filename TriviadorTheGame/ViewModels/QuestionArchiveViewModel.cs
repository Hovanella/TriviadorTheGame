using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    internal class QuestionArchiveViewModel : BaseViewModel.BaseViewModel
    {
        private Question _selectedQuestion;
        private ObservableCollection<Question> _questions;
        private CreateQuestionWindow _createQuestionWindow = new();
        private string _searchText = string.Empty;
        private bool _languageChecked;
        private bool _myQuestions;
        private bool _volumeChecked;
        public bool IsEditing { get; set; } = false;

        public bool MyQuestions
        {
            get => _myQuestions;
            set { _myQuestions = value; OnPropertyChanged(); }
        }


        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set { _selectedQuestion = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set { _questions = value; OnPropertyChanged(); }
        }

        public void Update()
        {
            Questions = new ObservableCollection<Question>( UnitOfWork.QuestionRepository.GetAll());
        }
        public QuestionArchiveViewModel()
        {
            ModelViewManager.QuestionArchiveViewModel = this;
            Questions = new ObservableCollection<Question>( UnitOfWork.QuestionRepository.GetAll());


            ChangeLanguage = new RelayCommand((o) =>
            {   
                ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);

            });
            
            DeleteSelectedQuestion = new RelayCommand(o =>
            {
                var questionId = _selectedQuestion.QUESTION_ID;
                UnitOfWork.QuestionRepository.RemoveFromPacks(questionId);
                UnitOfWork.QuestionRepository.RemovebyId(questionId);
                Update();
            }, o =>
            {
                if (UnitOfWork.UserRepository.CurrentUser == null || SelectedQuestion == null)
                    return false;
                
                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;
                
                if (UnitOfWork.UserRepository.CurrentUser.USER_ID==SelectedQuestion.CREATOR_ID)
                    return true;
                

                return false;

            });
            
            OpenCreateQuestionWindowCommand = new RelayCommand(o =>
            {
                if (_createQuestionWindow == null) _createQuestionWindow = new CreateQuestionWindow();

                IsEditing = false;
                _createQuestionWindow.QuestionTextBox.Text = String.Empty;
                _createQuestionWindow.RightAnswerTextBox.Text =  String.Empty;
                _createQuestionWindow.FirstAnswerTextBox.Text =  String.Empty;
                _createQuestionWindow.SecondAnswerTextBox.Text =  String.Empty;
                _createQuestionWindow.ThirdAnswerTextBox.Text =  String.Empty;
           
                
                _createQuestionWindow.ShowDialog();
                if (_createQuestionWindow.DialogResult != true) return;
            });
            
            EditSelectedQuestion = new RelayCommand(o =>
            {
                IsEditing = true;
                if (_createQuestionWindow == null) _createQuestionWindow = new CreateQuestionWindow();
                _createQuestionWindow.QuestionTextBox.Text = SelectedQuestion.QUESTION_TEXT;
                _createQuestionWindow.RightAnswerTextBox.Text = SelectedQuestion.RIGHT_ANSWER;
                _createQuestionWindow.FirstAnswerTextBox.Text = SelectedQuestion.FIRST_WRONG_ANSWER;
                _createQuestionWindow.SecondAnswerTextBox.Text = SelectedQuestion.SECOND_WRONG_ANSWER;
                _createQuestionWindow.ThirdAnswerTextBox.Text = SelectedQuestion.THIRD_WRONG_ANSWER;
             

                _createQuestionWindow.ShowDialog();
                if (_createQuestionWindow.DialogResult != true) return;
            }, o =>
            {
                if (_selectedQuestion == null)
                    return false;

                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;
                
                if (UnitOfWork.UserRepository.CurrentUser.USER_ID==SelectedQuestion.CREATOR_ID)
                    return true;

                return false;
            });

            Search = new RelayCommand(o =>
            {
                Update();
                if (_myQuestions==false)
                    Questions=new ObservableCollection<Question>( Questions.Where(x=>x.CREATOR_ID==UnitOfWork.UserRepository.CurrentUser.USER_ID));
                if(SearchText==String.Empty)
                    return;
                
            
                
                Questions = new ObservableCollection<Question>( Questions.Where(x => x.ToString().Contains(SearchText.ToLower())));
            });
            OpenMainMenu = new RelayCommand(o =>
            {
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];

                ModelViewManager.MainMenuViewModel.LanguageChecked = LanguageChecked;
                ModelViewManager.MainMenuViewModel.VolumeChecked= VolumeChecked;
            });
        }

        public RelayCommand ChangeLanguage { get; set; }

        public bool LanguageChecked
        {
            get => _languageChecked;
            set { _languageChecked = value; OnPropertyChanged(); }
        }

        public RelayCommand OpenMainMenu { get; set; }

        public RelayCommand Search { get; set; }

        public RelayCommand EditSelectedQuestion { get; set; }

        public RelayCommand OpenCreateQuestionWindowCommand { get; set; }

        public RelayCommand DeleteSelectedQuestion { get; set; }

        public bool VolumeChecked
        {
            get => _volumeChecked;
            set { _volumeChecked = value; OnPropertyChanged(); }
        }

        public void CloseCreateQuestionWindow()
        {
            if (IsEditing == true)
            {
                _selectedQuestion.RIGHT_ANSWER = _createQuestionWindow.RightAnswerTextBox.Text;
                _selectedQuestion.QUESTION_TEXT = _createQuestionWindow.QuestionTextBox.Text;
                _selectedQuestion.FIRST_WRONG_ANSWER = _createQuestionWindow.FirstAnswerTextBox.Text;
                _selectedQuestion.SECOND_WRONG_ANSWER = _createQuestionWindow.SecondAnswerTextBox.Text;
                _selectedQuestion.THIRD_WRONG_ANSWER = _createQuestionWindow.ThirdAnswerTextBox.Text;
                UnitOfWork.QuestionRepository.Update();
                UnitOfWork.QuestionPackRepository.Update();
            }
            else
            {
                var newQuestion = new Question
                {
                    QUESTION_TEXT = _createQuestionWindow.QuestionTextBox.Text,
                    RIGHT_ANSWER = _createQuestionWindow.RightAnswerTextBox.Text,
                    FIRST_WRONG_ANSWER = _createQuestionWindow.FirstAnswerTextBox.Text,
                    SECOND_WRONG_ANSWER = _createQuestionWindow.SecondAnswerTextBox.Text,
                    THIRD_WRONG_ANSWER = _createQuestionWindow.ThirdAnswerTextBox.Text,
                    CREATOR_ID = UnitOfWork.UserRepository.CurrentUser.USER_ID
                };
                UnitOfWork.QuestionRepository.AddQuestion(newQuestion);
                
            }
          
            Update();
         

            _createQuestionWindow.Hide();
        }
    }
}
