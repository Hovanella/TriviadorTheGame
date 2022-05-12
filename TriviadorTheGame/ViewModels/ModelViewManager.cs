namespace TriviadorTheGame.ViewModels
{
    public static class ModelViewManager
    {
        public static RedactorViewModel RedactorViewModel { get; set; }
        public static MainMenuViewModel MainMenuViewModel { get; set; }
        
        public static CreatePackWindowViewModel CreatePackWindowViewModel { get; set; }
        
        public static GameLobbyViewModel GameLobbyViewModel { get; set; }

        public static MainWindowViewModel MainWindowViewModel { get; set; }
        public static LoggingPageViewModel LoggingPageViewModel { get; set; }
    }
}