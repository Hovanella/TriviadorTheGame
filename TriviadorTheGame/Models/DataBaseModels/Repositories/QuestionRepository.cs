namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public class QuestionRepository
    {
        private DataBaseContext  _context = new DataBaseContext();
        public void Update()
        {
   
        }

        public void AddQuestion(Question newQuestion)
        {
            _context.Questions.Add(newQuestion);
            _context.SaveChanges();
        }
    }
}