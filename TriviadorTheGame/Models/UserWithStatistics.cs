namespace TriviadorTheGame.Models
{
    public class UserWithStatistics
    {
        public int USER_ID { get; set; }
        public string USER_LOGIN { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_ROLE { get; set; }
        public int SCORE_NUMBER { get; set; }
        public int GAMES_NUMBER { get; set; }
    }
}