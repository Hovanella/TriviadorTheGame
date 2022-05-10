using TriviadorTheGame.Models.DataBaseModels.Repositories;
using TriviadorTheGame.ViewModels;

namespace TriviadorTheGame.Models.DataBaseModels
{
    public static class UnitOfWork
    {
        private static DataBaseContext context = new DataBaseContext();
        public static  UserRepository UserRepository { get; set; } = new UserRepository();
        
        public static QuestionPackRepository QuestionPackRepository { get;set;} = new QuestionPackRepository();
        
        public static QuestionRepository QuestionRepository { get; set; } = new QuestionRepository();

    }
}