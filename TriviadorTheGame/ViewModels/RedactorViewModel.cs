using System.Collections.Generic;
using System.Windows;
using TriviadorTheGame.Models;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class RedactorViewModel : BaseViewModel.BaseViewModel
    {
        public bool IsNotAdmin { get; set; } = true;

        public Window CreatePackWindow { get; set; } = new CreatePackWindow();
        public List<QuestionPack> QuestionPacks { get; set; } = new List<QuestionPack>();

        public RelayCommand OpenCreatePackWindowCommand { get; set; }


        public RedactorViewModel()
        {
            ModelViewManager.RedactorViewModel = this;


            OpenCreatePackWindowCommand = new RelayCommand
                (async () => { ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["LoggingPage"]; });

            List<Question> questions = new List<Question>();
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));
            questions.Add(new Question("Какой город находится в Северной Европе?", "Минск", "Москва", "Вильнюс",
                "Брест"));


            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
            QuestionPacks.Add(new QuestionPack("TestPack", questions));
        }
    }
}