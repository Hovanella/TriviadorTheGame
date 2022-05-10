using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignColors.Recommended;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.ViewModels
{
    public class QuestionPackRepository : IQuestionPackRepository
    {
        private readonly DataBaseContext _context = new DataBaseContext();

        public Task<QuestionsPack> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<QuestionsPack>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<QuestionsPack> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<QuestionPack> GetAllPacksWithQuestions()
        {
            
            var packs =
                (from p in _context.QuestionsPacks
                    join s in _context.QuestionsToPacks on p.QUESTIONS_PACK_ID equals s.QUESTION_PACK_ID
                    join q in _context.Questions on s.QUESTION_ID equals q.QUESTION_ID
                    orderby p.QUESTIONS_PACK_ID
                    select new { p.QUESTIONS_PACK_ID, q.QUESTION_ID }).ToList();


            var result = new List<QuestionPack>();

            if (packs.Count == 0) return result;

            var questionPackId = packs[0].QUESTIONS_PACK_ID;
            var questionPackName = _context.QuestionsPacks
                .FirstOrDefault(x => x.QUESTIONS_PACK_ID == questionPackId)
                ?.QUESTIONS_PACK_NAME;


            var pack = new QuestionPack(questionPackName,
                new ObservableCollection<Question>()
                    { _context.Questions.FirstOrDefault(x => x.QUESTION_ID == questionPackId) });

            var previousQuestionPackId = packs[0].QUESTIONS_PACK_ID;

            for (var i = 1; i < packs.Count(); i++)
            {
                var currentQuestionPackId = packs[i].QUESTIONS_PACK_ID;
                var currentQuestionId = packs[i].QUESTION_ID;
                if (currentQuestionPackId == previousQuestionPackId)
                {
                    pack.Questions.Add(_context.Questions.FirstOrDefault(x => x.QUESTION_ID == currentQuestionId));
                }
                else
                {
                    result.Add(pack);
                    questionPackName = _context.QuestionsPacks
                        .FirstOrDefault(x => x.QUESTIONS_PACK_ID == currentQuestionPackId)?.QUESTIONS_PACK_NAME;
                    pack = new QuestionPack(questionPackName,
                        new ObservableCollection<Question>()
                            { _context.Questions.FirstOrDefault(x => x.QUESTION_ID == currentQuestionId) });
                    previousQuestionPackId = currentQuestionPackId;
                }
            }

            result.Add(pack);

            return result;
        }
    }
}