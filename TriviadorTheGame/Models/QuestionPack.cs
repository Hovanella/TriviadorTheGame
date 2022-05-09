using System.Collections.Generic;

namespace TriviadorTheGame.Views.RedactorPage
{
    public class QuestionPack
    {
        public string Name { get; set; }
   

        public List<Question> Questions { get; set; }

       
       
        public QuestionPack(string name, List<Question> questions)
        {
            Name = name;
         
            Questions = questions;
        }
    }
}