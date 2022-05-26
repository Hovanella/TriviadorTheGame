using System.Data.Entity;

namespace TriviadorTheGame.Models.DataBaseModels
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=TriviadorTheGameDBEntities")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionsPack> QuestionsPacks { get; set; }
        public DbSet<QuestionsToPack> QuestionsToPacks { get; set; }
        public DbSet<UserStatistic> UserStatistics { get; set; }
    }
}