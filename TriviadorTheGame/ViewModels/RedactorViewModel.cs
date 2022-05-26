using System;
using System.Collections.ObjectModel;
using System.Linq;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class RedactorViewModel : BaseViewModel.BaseViewModel
    {
        private bool _canEdit;
        private CreatePackWindow _createPackWindow = new();
    
        private bool _isNotAdmin;
        private ObservableCollection<QuestionPackList> _questionPackLists = new();
        private Question _selectedQuestion;
        private QuestionPackList _selectedQuestionPackList;
        private bool _languageChecked;
        private bool _byPackName = true;
        private bool _byQuestion;
        private bool _notMyPacks;
        private string _searchText = String.Empty;
        private bool _volumeChecked;

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        public bool ByPackName
        {
            get => _byPackName;
            set { _byPackName = value; OnPropertyChanged(); }
        }

        public bool ByQuestion
        {
            get => _byQuestion;
            set { _byQuestion = value; OnPropertyChanged(); }
        }


        public SelectQuestionsWindow _SelectQuestionsWindow { get; set; } = new();

        public RedactorViewModel()
        {
            ModelViewManager.RedactorViewModel = this;
            
            QuestionPackLists =
                new ObservableCollection<QuestionPackList>(UnitOfWork.QuestionPackRepository
                    .GetAllPacksWithQuestions());
            OnPropertyChanged();
            
            ChangeLanguage = new RelayCommand((o)=>
            {
                ModelViewManager.MainWindowViewModel.ChangeLanguage(LanguageChecked);
            }
            );
            OpenMainMenuPage = new RelayCommand(o =>
            {
                ModelViewManager.MainMenuViewModel.VolumeChecked = VolumeChecked;
                ModelViewManager.MainMenuViewModel.LanguageChecked = LanguageChecked;
                ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["MainMenuPage"];
            });
            
            OpenSelectQuestionsWindow = new RelayCommand(o =>
            {
                _SelectQuestionsWindow.Show();
            }, o =>
            {
                if (_selectedQuestionPackList == null)
                    return false;
                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;
                
                return UnitOfWork.QuestionPackRepository.GetById(_selectedQuestionPackList.QuestionsPackId).CREATOR_ID==UnitOfWork.UserRepository.CurrentUser.USER_ID;
            });

            DeleteSelectedQuestion = new RelayCommand(o =>
            {
                if (SelectedQuestion == null || SelectedQuestionPackList == null)
                    return;

                var questionId = _selectedQuestion.QUESTION_ID;
                UnitOfWork.QuestionPackRepository.DeleteQuestionFromPackById(questionId,
                    SelectedQuestionPackList.QuestionsPackId);

                var questionToDelete =
                    SelectedQuestionPackList.Questions.FirstOrDefault(x => x.QUESTION_ID == questionId);
                SelectedQuestionPackList.Questions.Remove(questionToDelete);
            }, o =>
            {
                if (UnitOfWork.UserRepository.CurrentUser == null || SelectedQuestion == null ||
                    SelectedQuestionPackList == null)
                    return false;
                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;

                var questionPack = UnitOfWork.QuestionPackRepository.GetById(SelectedQuestionPackList.QuestionsPackId);

                if (questionPack.CREATOR_ID == UnitOfWork.UserRepository.CurrentUser.USER_ID)
                    return true;

                return false;

            });

            DeleteSelectedPack = new RelayCommand(o =>
            {
                var packId = SelectedQuestionPackList.QuestionsPackId;
                QuestionPackLists.Remove(SelectedQuestionPackList);
                UnitOfWork.QuestionPackRepository.DeleteQuestionsFromPackById(packId);
                UnitOfWork.QuestionPackRepository.DeletePackById(packId);

            }, o =>
            {
                if (_selectedQuestionPackList == null)
                    return false;

                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;

                var questionPack = UnitOfWork.QuestionPackRepository.GetById(SelectedQuestionPackList.QuestionsPackId);

                if (questionPack.CREATOR_ID == UnitOfWork.UserRepository.CurrentUser.USER_ID)
                    return true;

                return false;
            });

         

            EditSelectedPack = new RelayCommand(o =>
            {
                _createPackWindow ??= new CreatePackWindow();

                _createPackWindow.CreatePackWindowTextBox.Text = SelectedQuestionPackList.QuestionPackName;

                ModelViewManager.CreatePackWindowViewModel.Editing = true;
                _createPackWindow.ShowDialog();
                if (_createPackWindow.DialogResult != true) return;
            }, o =>
            {
                if (UnitOfWork.UserRepository.CurrentUser == null || SelectedQuestionPackList == null)
                    return false;

                if (UnitOfWork.UserRepository.CurrentUser.USER_ROLE == "A")
                    return true;

                var questionPack = UnitOfWork.QuestionPackRepository.GetById(SelectedQuestionPackList.QuestionsPackId);

                if (questionPack.CREATOR_ID == UnitOfWork.UserRepository.CurrentUser.USER_ID)
                    return true;

                return false;
            });
            
            Search = new RelayCommand(o =>
            {
                UpdateCollection();
                if (NotMyPacks == false && _byPackName==true )
                {
                    var questionPackListsTemp = new ObservableCollection<QuestionPackList>();
                    
                    foreach (var questionPackList in _questionPackLists)
                    {
                        var questionpack = UnitOfWork.QuestionPackRepository.GetById(questionPackList.QuestionsPackId);
                        if(questionpack.CREATOR_ID==UnitOfWork.UserRepository.CurrentUser.USER_ID)
                            questionPackListsTemp.Add(questionPackList);
                    }
                    QuestionPackLists = questionPackListsTemp;
                }
                if(SearchText==String.Empty)
                    return;

                if (_byPackName == true)
                {
                    QuestionPackLists=new ObservableCollection<QuestionPackList>( QuestionPackLists.Where(x=>x.QuestionPackName.ToLower().Contains(SearchText.ToLower())).ToList());
                }
                else
                {
                    SelectedQuestionPackList.Questions=new ObservableCollection<Question>( SelectedQuestionPackList.Questions.Where(x=>x.ToString().Contains(SearchText.ToLower())).ToList());
                    var id = SelectedQuestionPackList.QuestionsPackId;
                    SelectedQuestionPackList = null;
                    SelectedQuestionPackList = QuestionPackLists.FirstOrDefault(x=>x.QuestionsPackId==id);
            
                }
                
               
            }, o =>
            {
                if (ByQuestion == true)
                    return SelectedQuestionPackList != null;
                return true;
            });

    

            OpenCreatePackWindowCommand = new RelayCommand(o =>
            {
                _createPackWindow ??= new CreatePackWindow();

                ModelViewManager.CreatePackWindowViewModel.Editing = false;
                _createPackWindow.ShowDialog();
                if (_createPackWindow.DialogResult != true) return;
            });
            
            SwitchVolume = new RelayCommand(o =>
            {
                if (VolumeChecked==true)
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0;
                }
                else
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0.5;
                }
            });
        }

        public RelayCommand SwitchVolume { get; set; }

        public bool NotMyPacks
        {
            get => _notMyPacks;
            set { _notMyPacks = value; OnPropertyChanged(); }
        }

        public RelayCommand Search { get; set; }

        public RelayCommand ChangeLanguage { get; set; }

        public RelayCommand OpenSelectQuestionsWindow { get; set; }

        public RelayCommand EditSelectedQuestion { get; set; }

        public RelayCommand EditSelectedPack { get; set; }

        public RelayCommand DeleteSelectedPack { get; set; }

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

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }

        public QuestionPackList SelectedQuestionPackList
        {
            get => _selectedQuestionPackList;
            set
            {
                _selectedQuestionPackList = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<QuestionPackList> QuestionPackLists
        {
            get => _questionPackLists;
            set
            {
                _questionPackLists = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenCreateQuestionWindowCommand { get; set; }

        public RelayCommand OpenMainMenuPage { get; set; }

        public RelayCommand DeleteSelectedQuestion { get; set; }

        public RelayCommand OpenCreatePackWindowCommand { get; set; }

        public bool LanguageChecked
        {
            get => _languageChecked;
            set { _languageChecked = value; OnPropertyChanged(); }
        }

        public bool VolumeChecked
        {
            get => _volumeChecked;
            set { _volumeChecked = value; OnPropertyChanged(); }
        }


        public void CloseCreatePackWindow()
        {
            if (ModelViewManager.CreatePackWindowViewModel.Editing == true)
            {
                UnitOfWork.QuestionPackRepository.GetById(_selectedQuestionPackList.QuestionsPackId)
                    .QUESTIONS_PACK_NAME = _createPackWindow.CreatePackWindowTextBox.Text;
                UnitOfWork.QuestionPackRepository.Update();
            }
            else
            {
                var newPack = new QuestionsPack
                {
                    QUESTIONS_PACK_NAME = _createPackWindow.CreatePackWindowTextBox.Text,
                    CREATOR_ID = UnitOfWork.UserRepository.CurrentUser.USER_ID
                };
                UnitOfWork.QuestionPackRepository.AddPack(newPack);
            }

            
            QuestionPackLists =
                new ObservableCollection<QuestionPackList>(UnitOfWork.QuestionPackRepository
                    .GetAllPacksWithQuestions());
            OnPropertyChanged();
            _createPackWindow.Hide();
        }

        public void UpdateCollection()
        {
            int selectedPackId = -1;
            if (_selectedQuestionPackList!=null) 
             selectedPackId = _selectedQuestionPackList.QuestionsPackId;  
            
            QuestionPackLists =
                new ObservableCollection<QuestionPackList>(UnitOfWork.QuestionPackRepository
                    .GetAllPacksWithQuestions());

            if (selectedPackId!=-1 && QuestionPackLists.FirstOrDefault(x => x.QuestionsPackId == selectedPackId) != null)
            {
                SelectedQuestionPackList = QuestionPackLists.FirstOrDefault(x => x.QuestionsPackId == selectedPackId);
            }
        }
    }
}