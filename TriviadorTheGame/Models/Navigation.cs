using System.Collections.Generic;
using System.Windows.Controls;
using TriviadorTheGame.Views;
using TriviadorTheGame.Views.GameLobbyPage;
using TriviadorTheGame.Views.GameplayPage;
using TriviadorTheGame.Views.MainMenu;
using TriviadorTheGame.Views.RedactorPage;

namespace TriviadorTheGame.Models
{

    public static class Navigation
    {
        private static RedactorPage _redactorPage = new RedactorPage();
        private static MainMenu _mainMenuPage = new MainMenu();
        private static GameLobbyPage _gameLobbyPage = new GameLobbyPage();
        private static GameplayPage _gameplayPage = new GameplayPage();
        private static LoggingPage _loggingPage = new LoggingPage();

        public static Dictionary<string, Page> Pages { get; set; } = new Dictionary<string, Page>()
        {
            { "RedactorPage", _redactorPage },
            { "GameplayPage", _gameplayPage },
            { "LoggingPage", _loggingPage },
            { "MainMenuPage", _mainMenuPage },
            { "GameLobbyPage", _gameLobbyPage }
        };



    }
}