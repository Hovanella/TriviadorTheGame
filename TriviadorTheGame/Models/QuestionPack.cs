using System.Collections.Generic;
using System.Collections.ObjectModel;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.Views.RedactorPage
{
    public class QuestionPack
    {
        public string Name { get; set; }
   

        public ObservableCollection<Question> Questions { get; set; }

       
       
        public QuestionPack(string name, ObservableCollection<Question> questions)
        {
            Name = name;
         
            Questions = questions;
        }
    }
}