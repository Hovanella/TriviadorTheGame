using TriviadorTheGame.Models.DataBaseModels.Repositories;
using TriviadorTheGame.ViewModels;

namespace TriviadorTheGame.Models.DataBaseModels
{
    public static class UnitOfWork
    {
        private static DataBaseContext context = new();
        public static UserRepository UserRepository { get; set; } = new();

        public static QuestionPackRepository QuestionPackRepository { get; set; } = new();

        public static QuestionRepository QuestionRepository { get; set; } = new();
    }
}