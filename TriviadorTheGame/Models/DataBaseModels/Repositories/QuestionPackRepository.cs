using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.ViewModels
{
    public class QuestionPackRepository : IQuestionPackRepository
    {
        private readonly DataBaseContext _context = new();

        public Task<QuestionsPack> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionsPack>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionsPack> GetAll()
        {
            return _context.QuestionsPacks.Local;
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionPackList> GetAllPacksWithQuestions()
        {
            var packs =
                (from p in _context.QuestionsPacks
                    join s in _context.QuestionsToPacks on p.QUESTIONS_PACK_ID equals s.QUESTION_PACK_ID
                    join q in _context.Questions on s.QUESTION_ID equals q.QUESTION_ID
                    orderby p.QUESTIONS_PACK_ID
                    select new { p.QUESTIONS_PACK_ID, q.QUESTION_ID }).ToList();


            var result = new List<QuestionPackList>();

            if (packs.Count != 0)
            {

                var questionPackId = packs[0].QUESTIONS_PACK_ID;
                var questionPackName = _context.QuestionsPacks
                    .FirstOrDefault(x => x.QUESTIONS_PACK_ID == questionPackId)
                    ?.QUESTIONS_PACK_NAME;

                var questionId = packs[0].QUESTION_ID;
                var firstQuestion = _context.Questions.FirstOrDefault(x => x.QUESTION_ID == questionId);
                var pack = new QuestionPackList(questionPackId, questionPackName,
                    new ObservableCollection<Question>
                    {
                        firstQuestion
                    });

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
                        pack = new QuestionPackList(currentQuestionPackId, questionPackName,
                            new ObservableCollection<Question>
                                { _context.Questions.FirstOrDefault(x => x.QUESTION_ID == currentQuestionId) });
                        previousQuestionPackId = currentQuestionPackId;
                    }
                }

                result.Add(pack);
            }

            var allpacksId = packs.Select(x => x.QUESTIONS_PACK_ID).ToList();
            var emptypacks = _context.QuestionsPacks.Where(x => !allpacksId.Contains(x.QUESTIONS_PACK_ID)).ToList();

            result.AddRange(emptypacks.Select(emptyPack => new QuestionPackList(emptyPack.QUESTIONS_PACK_ID,
                emptyPack.QUESTIONS_PACK_NAME, new ObservableCollection<Question>())));

            return result;
        }

        public void Update()
        {
            _context.SaveChanges();
        }

        public void DeleteQuestionFromPackById(int questionQuestionId, int questionPackId)
        {
            var QuestionToPack = _context.QuestionsToPacks.FirstOrDefault(x =>
                x.QUESTION_ID == questionQuestionId && x.QUESTION_PACK_ID == questionPackId);
            if (QuestionToPack == null) return;
            _context.QuestionsToPacks.Remove(QuestionToPack);
            _context.SaveChanges();
        }

        /*public void AddQuestionToPack(Question newQuestion, QuestionPack questionPack)
        {
            var questionPackId = _context.QuestionsPacks.FirstOrDefault(x => x.QUESTIONS_PACK_ID == questionPack.Id).QUESTIONS_PACK_ID;
            
            _context.QuestionsToPacks.Add(new QuestionsToPack()
            {
                QUESTION_ID = newQuestion.QUESTION_ID,
                QUESTION_PACK_ID = questionPackId
               
            });
        }*/


        public QuestionsPack GetById(int id)
        {
            return _context.QuestionsPacks.FirstOrDefault(x => x.QUESTIONS_PACK_ID == id);
        }

        public void AddQuestionToPack(Question newQuestion, QuestionPackList selectedQuestionPackList)
        {
            _context.QuestionsToPacks.Add(new QuestionsToPack
            {
                QUESTION_ID = newQuestion.QUESTION_ID,
                QUESTION_PACK_ID = selectedQuestionPackList.QuestionsPackId
            });
            _context.SaveChanges();
        }

        public ObservableCollection<Question> GetAllQuestionsFromPack(int questionsPackId)
        {
            var questions = new ObservableCollection<Question>();
            var questionsToPacks = _context.QuestionsToPacks.Where(x => x.QUESTION_PACK_ID == questionsPackId);
            foreach (var questionToPack in questionsToPacks)
                questions.Add(_context.Questions.FirstOrDefault(x => x.QUESTION_ID == questionToPack.QUESTION_ID));

            return questions;
        }

        public void AddPack(QuestionsPack newPack)
        {
            _context.QuestionsPacks.Add(newPack);
            _context.SaveChanges();
        }

        public QuestionPackList GetPackWithQuestionsById(int selectedPackQuestionsPackId)
        {
            var pack = _context.QuestionsPacks.FirstOrDefault(x => x.QUESTIONS_PACK_ID == selectedPackQuestionsPackId);
            var questions = GetAllQuestionsFromPack(selectedPackQuestionsPackId);
            return new QuestionPackList(pack.QUESTIONS_PACK_ID, pack.QUESTIONS_PACK_NAME, questions);
        }

        public void DeleteQuestionsFromPackById(int packId)
        {
            var questionsToPacks = _context.QuestionsToPacks.Where(x => x.QUESTION_PACK_ID == packId);
            foreach (var questionToPack in questionsToPacks)
            {
                _context.QuestionsToPacks.Remove(questionToPack);
            }
            _context.SaveChanges();
           
        }

        public void DeletePackById(int packId)
        {
            var pack = _context.QuestionsPacks.FirstOrDefault(x => x.QUESTIONS_PACK_ID == packId);
            if (pack == null) return;
            _context.QuestionsPacks.Remove(pack);
            _context.SaveChanges();
        }

        public void DeleteQuestionsFromAllPacks(Question question)
        { 
            var questionsToPacks = _context.QuestionsToPacks.Where(x => x.QUESTION_ID == question.QUESTION_ID);
            foreach (var questionToPack in questionsToPacks)
            {
                _context.QuestionsToPacks.Remove(questionToPack);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Question> GetAllQuestionsOfUser(int selectedUserUserId)
        {
           return _context.Questions.Where(x => x.CREATOR_ID == selectedUserUserId);
           
        }

        public IEnumerable<QuestionsPack> GetAllPacksOfUser(int selectedUserUserId)
        {
           return _context.QuestionsPacks.Where(x=>x.CREATOR_ID == selectedUserUserId);
        }
    }
}