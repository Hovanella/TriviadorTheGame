using System.Collections.Generic;
using System.Collections.ObjectModel;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.Views.RedactorPage
{
    public class QuestionPack
    {
        public string Name { get; set; }
        
        public int Id{ get; set; }
   

        public ObservableCollection<Question> Questions { get; set; }


       
        public QuestionPack(int id, string name, ObservableCollection<Question> questions)
        {
            Id = id;
            
            Name = name;
         
            Questions = questions;
        }
    }
}