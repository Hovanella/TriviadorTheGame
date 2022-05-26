using System.Collections.Generic;
using System.Windows.Controls;
using TriviadorTheGame.Views;
using TriviadorTheGame.Views.GameLobbyPage;
using TriviadorTheGame.Views.GameplayPage;
using TriviadorTheGame.Views.MainMenu;
using TriviadorTheGame.Views.QuestionArchive;
using TriviadorTheGame.Views.RedactorPage;
using TriviadorTheGame.Views.UserStatistics;

namespace TriviadorTheGame.Models
{
    public static class Navigation
    {
        private static readonly RedactorPage _redactorPage = new();
        private static readonly MainMenu _mainMenuPage = new();
        private static readonly GameLobbyPage _gameLobbyPage = new();
        private static readonly GameplayPage _gameplayPage = new();
        private static readonly LoggingPage _loggingPage = new();
        private static readonly QuestionArchivePage _questionArchivePage = new();
        private static readonly UserStatisticsPage _userStatisticsPage = new();

        public static Dictionary<string, Page> Pages { get; set; } = new()
        {
            { "RedactorPage", _redactorPage },
            { "GameplayPage", _gameplayPage },
            { "LoggingPage", _loggingPage },
            { "MainMenuPage", _mainMenuPage },
            { "GameLobbyPage", _gameLobbyPage },
            { "QuestionArchivePage", _questionArchivePage },
            { "UserStatisticsPage", _userStatisticsPage }
        };
    }
}