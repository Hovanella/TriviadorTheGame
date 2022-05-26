using System.Collections.ObjectModel;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.ViewModels
{
    public class QuestionPackList
    {
        public QuestionPackList(int currentQuestionPackId, string questionPackName,
            ObservableCollection<Question> observableCollection)
        {
            QuestionsPackId = currentQuestionPackId;
            QuestionPackName = questionPackName;
            Questions = observableCollection;
        }

        public override string ToString()
        {
            return $"{QuestionPackName} ({Questions.Count})";
        }

        public ObservableCollection<Question> Questions { get; set; }

        public string QuestionPackName { get; set; }

        public int QuestionsPackId { get; set; }
    }
}