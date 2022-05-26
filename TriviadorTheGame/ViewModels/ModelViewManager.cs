using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public static class ModelViewManager
    {
        public static RedactorViewModel RedactorViewModel { get; set; }
        public static MainMenuViewModel MainMenuViewModel { get; set; }
        public static CreateQuestionWindowViewModel CreateQuestionWindowViewModel { get; set; }
        public static GameLobbyViewModel GameLobbyViewModel { get; set; }
        public static MainWindowViewModel MainWindowViewModel { get; set; }
        public static LoggingPageViewModel LoggingPageViewModel { get; set; }
        public static CreatePackWindowViewModel CreatePackWindowViewModel { get; set; }
        public static GameplayViewModel GameplayViewModel { get; set; }
        public static CreateLobbyLoggingWindowViewModel CreateLobbyLoggingWindowViewModel { get; set; }
        public static QuestionWindowViewModel QuestionWindowViewModel { get; set; }
        internal static QuestionArchiveViewModel QuestionArchiveViewModel { get; set; }
        public static SelectQuestionsWindowViewModel SelectQuestionsWindowViewModel { get; set; }
        public static UserStatiscticsViewModel UserStatisticsViewModel { get; set; }
        public static ChangePasswordAndLoginWindowViewModel ChangePasswordAndLoginWindowViewModel { get; set; }
        public static ChangeUserInfoWindowViewModel ChangeUserInfoWindowViewModel { get; set; }
    }
}