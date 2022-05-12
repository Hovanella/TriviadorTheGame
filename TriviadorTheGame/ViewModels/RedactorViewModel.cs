using System.Collections.ObjectModel;
using System.Windows;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class RedactorViewModel : BaseViewModel.BaseViewModel
    {
        private object _selectedItem;
        private bool _isNotAdmin;
        private bool _canEdit;
        private ObservableCollection<QuestionPack> _questionPacks = new ObservableCollection<QuestionPack>();
        private object _selectedQuestionPack;
        private CreatePackWindow _createPackWindow = new CreatePackWindow();

        public bool IsNotAdmin
        {
            get => _isNotAdmin;
            set
            {
                _isNotAdmin = value;
                OnPropertyChanged();
            }
        }

        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                _canEdit = value;
                OnPropertyChanged();
            }
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public object SelectedQuestionPack
        {
            get => _selectedQuestionPack;
            set
            {
                _selectedQuestionPack = value;
                OnPropertyChanged();
            }
        }

        public CreatePackWindow CreatePackWindow { get; set; } = new CreatePackWindow();

        public RelayCommand CloseCreatePackWindowCommand { get; set; }

        public ObservableCollection<QuestionPack> QuestionPacks
        {
            get => _questionPacks;
            set
            {
                _questionPacks = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenCreatePackWindowCommand { get; set; }

        public RelayCommand TestSelectedElement { get; set; }

        public RelayCommand OpenMainMenuPage { get; set; }

        public RelayCommand DeleteSelectedQuestion { get; set; }

        public RelayCommand CreateNewPack { get; set; }

        public RedactorViewModel()
        {
            ModelViewManager.RedactorViewModel = this;
            _createPackWindow.Hide();

            QuestionPacks =
                new ObservableCollection<QuestionPack>(UnitOfWork.QuestionPackRepository.GetAllPacksWithQuestions());
            OnPropertyChanged();


            OpenCreatePackWindowCommand = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"]; });

            TestSelectedElement = new RelayCommand(async () => { MessageBox.Show(SelectedItem.ToString()); });

            OpenMainMenuPage = new RelayCommand(async () =>
            {
                UnitOfWork.QuestionPackRepository.Update();
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];
            });

            DeleteSelectedQuestion = new RelayCommand(async () =>
            {
                if (_selectedItem is not Question question ||
                    _selectedQuestionPack is not QuestionPack questionPack) return;

                questionPack.Questions.Remove(question);
                UnitOfWork.QuestionPackRepository.DeleteQuestionFromPackById(question.QUESTION_ID, questionPack.Id);
            });

            CreateNewPack = new RelayCommand(async () =>
            {
                if (CreatePackWindow == null)
                {
                    _createPackWindow = new CreatePackWindow();
                }

                _createPackWindow.ShowDialog();
                if (_createPackWindow.DialogResult != true) return;
            });

            CloseCreatePackWindowCommand = new RelayCommand(async () => { _createPackWindow.Hide(); });
        }

        public void CloseCreatePackWindow()
        {
            var newQuestion = new Question()
            {
                QUESTION_TEXT = ModelViewManager.CreatePackWindowViewModel.QuestionText,
                RIGHT_ANSWER = ModelViewManager.CreatePackWindowViewModel.RightAnswer,
                FIRST_WRONG_ANSWER = ModelViewManager.CreatePackWindowViewModel.FirstWrongAnswer,
                FIRST_SECOND_ANSWER = ModelViewManager.CreatePackWindowViewModel.SecondWrongAnswer,
                FIRST_THIRD_ANSWER = ModelViewManager.CreatePackWindowViewModel.ThirdWrongAnswer,
                CREATOR_ID = UnitOfWork.UserRepository.CurrentUser.USER_ID
            };
            UnitOfWork.QuestionRepository.AddQuestion(newQuestion);
            (SelectedQuestionPack as QuestionPack)?.Questions.Add(newQuestion);


            UnitOfWork.QuestionPackRepository.AddQuestionToPack(newQuestion, SelectedQuestionPack as QuestionPack);
            _createPackWindow.Hide();
        }   
    }
}