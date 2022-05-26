using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels;

namespace TriviadorTheGame.Models
{
    public class Game
    {
        public Game(string numberOfRounds, User greenUser, User redUser, User blueUser, bool isRating,
            QuestionPackList questionPackList)
        {
            if (numberOfRounds == "-")
                IsNumberOfRoundsLimited = false;
            else
                NumberOfRounds = int.Parse(numberOfRounds);


            GreenUser = greenUser;
            RedUser = redUser;
            BlueUser = blueUser;
            this.isRating = isRating;
            QuestionPackList = questionPackList;
        }

        public int NumberOfRounds { get; set; }
        public bool IsNumberOfRoundsLimited { get; set; } = true;
        public User GreenUser { get; set; }
        public User RedUser { get; set; }
        public User BlueUser { get; set; }
        public bool isRating { get; set; }
        public QuestionPackList QuestionPackList { get; set; }
        public int CurrentRound { get; set; }
        public List<int> QuestionsSubsequence { get; set; }


        public Brush GreenUserBrush { get; set; } = new SolidColorBrush(Colors.Green);
        public Brush RedUserBrush { get; set; } = new SolidColorBrush(Colors.Red);
        public Brush BlueUserBrush { get; set; } = new SolidColorBrush(Colors.Blue);

        public Brush DefaultCellBrush { get; set; } = new SolidColorBrush(Colors.Black);
        public Brush PossibleCellBrush { get; set; } = new SolidColorBrush(Colors.Bisque);


        public List<string> PossibleCells { get; set; } = new();

        public bool BluePlayerDontMakeMove { get; set; }

        public bool RedPlayerDontMakeMove { get; set; }

        public bool GreenPlayerDontMakeMove { get; set; }

        public int CurrentQuestionPosition { get; set; } = 0;

        public void Start()
        {
            CurrentRound = 0;
            QuestionsSubsequence = CreateQuestionsSubsequence(0, QuestionPackList.Questions.Count);


            string Subsequence = "";

            foreach (var item in QuestionsSubsequence)
            {
                Subsequence += item + " ";
            }


            ModelViewManager.GameplayViewModel.CreateField();


            var Cells = new ObservableCollection<Cell>(ModelViewManager.GameplayViewModel.CellsMatrix);
            Cells[1].Brush = RedUserBrush;
            Cells[1].Lord = RedUser;
            Cells[24].Brush = BlueUserBrush;
            Cells[24].Lord = BlueUser;
            Cells[30].Brush = GreenUserBrush;
            Cells[30].Lord = GreenUser;
            ModelViewManager.GameplayViewModel.CellsMatrix = Cells;

            if (ReadyToFinish())
                Finish();

            new Task(Gameplay).Start();
        }

        private void Gameplay()
        {
            while (ReadyToFinish() == false)
            {
                Round();
                CurrentRound++;
                ModelViewManager.GameplayViewModel.CurrentRound = CurrentRound;
            }

            Finish();
        }

        private void Round()
        {
            GreenPlayerDontMakeMove = true;
            RedPlayerDontMakeMove = true;
            BluePlayerDontMakeMove = true;

            var round = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("Round"))?["Round"];
            ModelViewManager.GameplayViewModel.MakeLogNote(round + CurrentRound + " \n");
            PossibleCells = GetPossibleCells(GreenUser);
            if (PossibleCells.Count != 0)
                while (GreenPlayerDontMakeMove)
                    ;
            PossibleCells = GetPossibleCells(RedUser);
            if (PossibleCells.Count != 0)
                while (RedPlayerDontMakeMove)
                    ;
            PossibleCells = GetPossibleCells(BlueUser);
            if (PossibleCells.Count != 0) ;
            while (BluePlayerDontMakeMove) ;
        }

        private List<string> GetPossibleCells(User User)
        {
            var allCells = new ObservableCollection<Cell>(ModelViewManager.GameplayViewModel.CellsMatrix);

            foreach (var t in allCells) t.ForegroundBrush = DefaultCellBrush;

            var userCells = ModelViewManager.GameplayViewModel.CellsMatrix.Where(x => x.Lord == User).ToList();

            var PossibleCells = new List<string>();

            foreach (var cell in userCells)
            {
                var rowAndColumn = cell.RowAndColumn;
                var row = int.Parse(rowAndColumn.Substring(0, 1));
                var column = int.Parse(rowAndColumn.Substring(1, 1));

                Cell upCell;
                Cell downCell;
                Cell upAndRightCell;
                Cell upAndLeftCell;
                Cell downAndRightCell;
                Cell downAndLeftCell;


                if (column % 2 == 0)
                {
                    upCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row - 1 + column.ToString());
                    upAndRightCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row - 1 + (column + 1).ToString());
                    upAndLeftCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row - 1 + (column - 1).ToString());
                    downAndRightCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + (column + 1).ToString());
                    downAndLeftCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + (column - 1).ToString());
                    downCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + 1 + column.ToString());
                }
                else
                {
                    upCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row - 1 + column.ToString());
                    downCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + 1 + column.ToString());
                    upAndLeftCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + (column - 1).ToString());
                    upAndRightCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + (column + 1).ToString());
                    downAndRightCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + 1 + (column + 1).ToString());
                    downAndLeftCell =
                        allCells.FirstOrDefault(x => x.RowAndColumn == row + 1 + (column - 1).ToString());
                }

                if (upCell != null && upCell.Lord != User)
                {
                    PossibleCells.Add(upCell.RowAndColumn);
                    allCells[allCells.IndexOf(upCell)].ForegroundBrush = PossibleCellBrush;
                }

                if (upAndRightCell != null && upAndRightCell.Lord != User)
                {
                    PossibleCells.Add(upAndRightCell.RowAndColumn);
                    allCells[allCells.IndexOf(upAndRightCell)].ForegroundBrush = PossibleCellBrush;
                }

                if (upAndLeftCell != null && upAndLeftCell.Lord != User)
                {
                    PossibleCells.Add(upAndLeftCell.RowAndColumn);
                    allCells[allCells.IndexOf(upAndLeftCell)].ForegroundBrush = PossibleCellBrush;
                }

                if (downAndRightCell != null && downAndRightCell.Lord != User)
                {
                    PossibleCells.Add(downAndRightCell.RowAndColumn);
                    allCells[allCells.IndexOf(downAndRightCell)].ForegroundBrush = PossibleCellBrush;
                }

                if (downAndLeftCell != null && downAndLeftCell.Lord != User)
                {
                    PossibleCells.Add(downAndLeftCell.RowAndColumn);
                    allCells[allCells.IndexOf(downAndLeftCell)].ForegroundBrush = PossibleCellBrush;
                }

                if (downCell != null && downCell.Lord != User)
                {
                    PossibleCells.Add(downCell.RowAndColumn);
                    allCells[allCells.IndexOf(downCell)].ForegroundBrush = PossibleCellBrush;
                }
            }

            ModelViewManager.GameplayViewModel.CellsMatrix = allCells;
            return PossibleCells;
        }

        public void Finish()
        {
            var blueUserCells = ModelViewManager.GameplayViewModel.CellsMatrix.Where(x => x.Lord == BlueUser).ToList();
            var redUserCells = ModelViewManager.GameplayViewModel.CellsMatrix.Where(x => x.Lord == RedUser).ToList();
            var greenUserCells =
                ModelViewManager.GameplayViewModel.CellsMatrix.Where(x => x.Lord == GreenUser).ToList();

            var blueScore = ModelViewManager.GameplayViewModel.BlueUserScore;
            var redScore = ModelViewManager.GameplayViewModel.RedUserScore;
            var greenScore = ModelViewManager.GameplayViewModel.GreenUserScore;

            var gameFinished = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("GameFinished"))?["GameFinished"];
            var captureAllTheCells = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("GameFinished"))?["GameFinished"];
            var red = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("Red"))?["Red"];
            var blue = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("Blue"))?["Blue"];
            var green = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("Green"))?["Green"];
            var wonByPoints = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("WonByPoints"))?["WonByPoints"];
            var noWinner = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("NoWinner"))?["NoWinner"];


            if (blueUserCells.Count == 0 && greenUserCells.Count == 0 && redUserCells.Count != 0)
            {
                var message = gameFinished + " " + red + " " + captureAllTheCells;
                MessageBox.Show(message);
            }
            else if (blueUserCells.Count == 0 && greenUserCells.Count != 0 && redUserCells.Count == 0)
            {
                var message = gameFinished + " " + green + " " + captureAllTheCells;
                MessageBox.Show(message);
            }
            else if (blueUserCells.Count != 0 && greenUserCells.Count == 0 && redUserCells.Count == 0)
            {
                var message = gameFinished + " " + red + " " + captureAllTheCells;
                MessageBox.Show(message);
            }
            else
            {
                if (blueScore > redScore && blueScore > greenScore)
                {
                    var message = gameFinished + " " + blue + " " + wonByPoints;
                    MessageBox.Show(message);
                }
                else if (redScore > blueScore && redScore > greenScore)
                {
                    var message = gameFinished + " " + red + " " + wonByPoints;
                    MessageBox.Show(message);
                }
                else if (greenScore > blueScore && greenScore > redScore)
                {
                    var message = gameFinished + " " + green + " " + wonByPoints;
                    MessageBox.Show(message);
                }
                else
                {
                    var message = gameFinished + " " + noWinner;
                    MessageBox.Show(message);
                }
            }

            UpdateTheStatistics();
            ModelViewManager.MainWindowViewModel.CurrentPage = Navigation.Pages["GameLobbyPage"];
        }

        private void UpdateTheStatistics()
        {
            var blueUserStatistics = UnitOfWork.UserRepository.FindUserStatistics(BlueUser.USER_ID);
            var redUserStatistics = UnitOfWork.UserRepository.FindUserStatistics(RedUser.USER_ID);
            var greenUserStatistics = UnitOfWork.UserRepository.FindUserStatistics(GreenUser.USER_ID);

            UnitOfWork.UserRepository.UpdateUserStatistics(blueUserStatistics.STATISTICS_ID,
                ModelViewManager.GameplayViewModel.BlueUserScore);

            UnitOfWork.UserRepository.UpdateUserStatistics(redUserStatistics.STATISTICS_ID,
                ModelViewManager.GameplayViewModel.RedUserScore);

            UnitOfWork.UserRepository.UpdateUserStatistics(greenUserStatistics.STATISTICS_ID,
                ModelViewManager.GameplayViewModel.GreenUserScore);
        }

        private bool ReadyToFinish()
        {
            if (CurrentQuestionPosition > QuestionsSubsequence.Count)
                return true;
            if (CurrentRound >= NumberOfRounds && IsNumberOfRoundsLimited)
                return true;
            return false;
        }


        public List<int> CreateQuestionsSubsequence(int minInclusive, int maxInclusive)
        {
            var result = new List<int>();

            var allNumber = Enumerable.Range(minInclusive, maxInclusive).ToList();

            var numberOfQuestions = allNumber.Count;

            var random = new Random();

            for (var i = 0; i < numberOfQuestions; i++)
            {
                var randomIndex = random.Next(0, allNumber.Count);
                result.Add(allNumber[randomIndex]);
                allNumber.Remove(allNumber[randomIndex]);
            }

            return result;
        }
    }

    public class Cell
    {
        public Cell(string rowAndColumn)
        {
            RowAndColumn = rowAndColumn;
        }

        public string RowAndColumn { get; set; }


        public Brush Brush { get; set; } = new SolidColorBrush(Colors.SaddleBrown);
        public Brush ForegroundBrush { get; set; } = new SolidColorBrush(Colors.Black);
        public User Lord { get; set; }
    }
}