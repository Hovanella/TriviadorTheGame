using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public class QuestionRepository: IRepository<Question>

    {
        private readonly DataBaseContext _context = new();

        public void Update()
        {
            _context.SaveChanges();
        }

        public void AddQuestion(Question newQuestion)
        {
            _context.Questions.Add(newQuestion);
            _context.SaveChanges();
        }

        public Task<Question> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions;
        }

        public Task<bool> Remove(int id)
        {
            throw new System.NotImplementedException();
        }


        public void RemovebyId(int questionId)
        {
            var question = _context.Questions.Find(questionId);
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public void RemoveFromPacks(int questionId)
        {
            _context.QuestionsToPacks.RemoveRange(_context.QuestionsToPacks.Where(x => x.QUESTION_ID == questionId));
        }

        public void DeleteAllQuestionsOfUser(int selectedUserUserId)
        {
            var questions = _context.Questions.Where(x => x.CREATOR_ID == selectedUserUserId);
            _context.Questions.RemoveRange(questions);
            _context.SaveChanges();
        }

        public void DeleteAllPacksOfUser(int selectedUserUserId)
        {
            var packs = _context.QuestionsPacks.Where(x => x.CREATOR_ID == selectedUserUserId);
            _context.QuestionsPacks.RemoveRange(packs);
            _context.SaveChanges();
        }
    }
}