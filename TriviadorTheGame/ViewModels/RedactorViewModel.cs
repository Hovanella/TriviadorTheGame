using System.Collections.Generic;
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
        public bool IsNotAdmin { get; set; } = true;

        public Window CreatePackWindow { get; set; } = new CreatePackWindow();
        public ObservableCollection<QuestionPack> QuestionPacks { get; set; } = new ObservableCollection<QuestionPack>();

        public RelayCommand OpenCreatePackWindowCommand { get; set; }


        public RedactorViewModel()
        {
            ModelViewManager.RedactorViewModel = this;

            QuestionPacks =  new ObservableCollection<QuestionPack>(UnitOfWork.QuestionPackRepository.GetAllPacksWithQuestions()) ;
            OnPropertyChanged();
            
            OpenCreatePackWindowCommand = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"]; });

            
            


        }
    }
}