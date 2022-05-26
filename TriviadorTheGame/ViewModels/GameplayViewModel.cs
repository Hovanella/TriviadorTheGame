using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;
using TriviadorTheGame.ViewModels.BaseViewModel;
using TriviadorTheGame.Views.GameplayPage;

namespace TriviadorTheGame.ViewModels
{
    public class GameplayViewModel : BaseViewModel.BaseViewModel
    {
        private ObservableCollection<Cell> _cellsMatrix = new();
        private string _logText;
        private double _pageOpacity = 1;
        private int _greenUserScore = 0;
        private int _blueUserScore = 0;
        private int _redUserScore = 0;
        private int _currentRound=0;
        private bool _volumeChecked;

        public int CurrentRound
        {
            get => _currentRound;
            set { _currentRound = value; OnPropertyChanged(); }
        }


        public double PageOpacity
        {
            get => _pageOpacity;
            set
            {
                _pageOpacity = value;
                OnPropertyChanged();
            }
        }

        public GameplayViewModel()
        {
            ModelViewManager.GameplayViewModel = this;
            LogText += (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                x => x.Contains("GameStarted"))?["GameStarted"]+"\n-----------------------------------------------------\n";

            SwitchVolume = new RelayCommand(o =>
            {
                if (VolumeChecked==true)
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0;
                }
                else
                {
                    ModelViewManager.MainWindowViewModel._mediaPlayer.Volume = 0.5;
                }
            });


            CaptureTheCell = new RelayCommand(obj =>
            {
                var name = (string)obj;

                var Cells = new ObservableCollection<Cell>(ModelViewManager.GameplayViewModel.CellsMatrix);
                var cell = Cells.FirstOrDefault(x => x.RowAndColumn == name);

                if (Game.PossibleCells.Contains(name) == false) return;

                if (Game.GreenPlayerDontMakeMove)
                {
                    if (ShowQuestions(Game.GreenUser, cell.Lord, cell.RowAndColumn))
                    {
                        cell.Brush = Game.GreenUserBrush;
                        cell.Lord = Game.GreenUser;
                    }


                    Game.GreenPlayerDontMakeMove = false;
                }
                else if (Game.RedPlayerDontMakeMove)
                {
                   if ( ShowQuestions(Game.RedUser, cell.Lord, cell.RowAndColumn))
                    {
                        cell.Brush = Game.RedUserBrush;
                        cell.Lord = Game.RedUser;
                    }
                    Game.RedPlayerDontMakeMove = false;
                }
                else
                {
                    if (ShowQuestions(Game.BlueUser, cell.Lord, cell.RowAndColumn))
                    {
                        cell.Brush = Game.BlueUserBrush;
                        cell.Lord = Game.BlueUser;
                    }

                    Game.BluePlayerDontMakeMove = false;
                }

                CellsMatrix = Cells;
            });
        }

        public RelayCommand SwitchVolume { get; set; }

        private bool ShowQuestions(User Attacker, User Defender, string cellString)
        {
            QuestionWindow = new QuestionWindow();

            IsDefenderAnswerCorrect = null;
            IsAttackerAnswerCorrect = null;
            var questionPosition = Game.QuestionsSubsequence[Game.CurrentQuestionPosition];

            Question firstQuestion = null;
       

            firstQuestion = Game.QuestionPackList.Questions[questionPosition];

            Game.CurrentQuestionPosition++;

            if (Game.CurrentQuestionPosition == Game.QuestionsSubsequence.Count)
            {
                Game.Finish();
                return false;
            }

            questionPosition = Game.QuestionsSubsequence[Game.CurrentQuestionPosition];
            
            Question secondQuestion = null;
            if (Defender != null)
                secondQuestion =
                    Game.QuestionPackList.Questions[questionPosition];

            ModelViewManager.QuestionWindowViewModel.Question = firstQuestion;


            PageOpacity = 0.4;
            ModelViewManager.QuestionWindowViewModel.PrepareQuestion();
            QuestionWindow.ShowDialog();

            if (secondQuestion != null)
            {
                ModelViewManager.QuestionWindowViewModel.Question = secondQuestion;
                ModelViewManager.QuestionWindowViewModel.PrepareQuestion();
                QuestionWindow.ShowDialog();
                Game.CurrentQuestionPosition++;
            }

            PageOpacity = 1;


            if (IsAttackerAnswerCorrect == true && (IsDefenderAnswerCorrect == false || Defender == null))
            {
                var message = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                 x => x.Contains("Player"))?["Player"] + " " + Attacker.USER_LOGIN + " " +
                             (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                 x => x.Contains("CapturedCell"))?["CapturedCell"] + " " + cellString + " "  + (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                 x => x.Contains("Andget"))?["Andget"] + " 150 " +(string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                 x => x.Contains("Points"))?["Points"] ;
                
                MakeLogNote(message+"\n");
                /*MakeLogNote($@"Player {Attacker.USER_LOGIN} captured cell {cellString} and get 150 points " + "\n");*/
                if (Attacker.USER_ID == Game.GreenUser.USER_ID)
                {
                    GreenUserScore+= 150;
                }
                else if (Attacker.USER_ID == Game.RedUser.USER_ID)
                {
                    RedUserScore += 150;
                }
                else
                {
                    BlueUserScore += 150;
                }
                return true;
            }

            if (IsAttackerAnswerCorrect == false && IsDefenderAnswerCorrect == true)
            {
                var message = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Player"))?["Player"] + " " + Attacker.USER_LOGIN + " " +
                              (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("DefendedCell"))?["DefendedCell"] + " " + cellString + " "  + (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Andget"))?["Andget"] + " 150" +(string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Points"))?["Points"] ;
                
                /*MakeLogNote($@"Player {Defender.USER_LOGIN} defended cell {cellString} and get 150 points"+"\n");*/
                MakeLogNote(message+"\n");
                if (Defender.USER_ID == Game.GreenUser.USER_ID)
                {
                    GreenUserScore += 150;
                }
                else if (Defender.USER_ID == Game.RedUser.USER_ID)
                {
                    RedUserScore += 150;
                }
                else
                {
                    BlueUserScore += 150;
                }
                return false;
            }

            if (IsAttackerAnswerCorrect == true && IsDefenderAnswerCorrect == true)
            {
                
                var message = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Players"))?["Players"] + " " +
                              (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Didntpickawinner"))?["Didntpickawinner"] + " "  + (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Andget"))?["Andget"] + " 50" +(string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Points"))?["Points"] + " " +(string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Foreveryone"))?["Foreveryone"] ;
                
                
                /*MakeLogNote($@"Players didn't pick a winner and get 50 points for everyone " +"\n");*/
                MakeLogNote(message+"\n");
                if (Attacker.USER_ID == Game.GreenUser.USER_ID)
                {
                    GreenUserScore += 50;
                }
                else if (Attacker.USER_ID == Game.RedUser.USER_ID)
                {
                    RedUserScore += 50;
                }
                else
                {
                    BlueUserScore += 50;
                }
                if (Defender.USER_ID == Game.GreenUser.USER_ID)
                {
                    GreenUserScore += 50;
                }
                else if (Defender.USER_ID == Game.RedUser.USER_ID)
                {
                    RedUserScore += 50;
                }
                else
                {
                    BlueUserScore += 50;
                }
                return false;
            }

            if (IsAttackerAnswerCorrect == false && (IsDefenderAnswerCorrect == false || IsDefenderAnswerCorrect==null))
            {
                /*MakeLogNote($@"The battle for cell {cellString} was bad " + "\n");*/
                var message = (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("Battleforcell"))?["Battleforcell"] + " " + cellString+" "+ 
                              (string)Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                  x => x.Contains("WasBad"))?["WasBad"];

                MakeLogNote(message+"\n");
                return false;
            }

            return false;
        }

        public bool? IsDefenderAnswerCorrect { get; set; } = null;
        public bool? IsAttackerAnswerCorrect { get; set; } = null;

        public int GreenUserScore
        {
            get => _greenUserScore;
            set { _greenUserScore = value; OnPropertyChanged(); }
        }

        public int BlueUserScore
        {
            get => _blueUserScore;
            set { _blueUserScore = value; OnPropertyChanged();}
        }

        public int RedUserScore
        {
            get => _redUserScore;
            set { _redUserScore = value; OnPropertyChanged(); }
        }


        public Game Game { get; set; }

        public string LogText
        {
            get => _logText;
            set
            {
                _logText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cell> CellsMatrix
        {
            get => _cellsMatrix;
            set
            {
                _cellsMatrix = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CaptureTheCell { get; set; }

        public QuestionWindow QuestionWindow { get; set; } = new();

        public bool VolumeChecked
        {
            get => _volumeChecked;
            set { _volumeChecked = value; OnPropertyChanged(); }
        }


        public void MakeLogNote(string logeNote)
        {
            LogText += logeNote;
        }

        public void CloseQuestionWindow()
        {
            QuestionWindow.Hide();
        }

        public void CreateResults()
        {
            if (IsAttackerAnswerCorrect is null)
                IsAttackerAnswerCorrect = ModelViewManager.QuestionWindowViewModel.isAnswerCorrect;
            else
                IsDefenderAnswerCorrect = ModelViewManager.QuestionWindowViewModel.isAnswerCorrect;
        }

        public void CreateField()
        {
            LogText="";
            BlueUserScore = 0;
            GreenUserScore = 0;
            RedUserScore = 0;
            CellsMatrix.Clear();
            CellsMatrix.Add(new Cell("01"));

            CellsMatrix.Add(new Cell("03"));
            CellsMatrix.Add(new Cell("05"));
            CellsMatrix.Add(new Cell("10"));
            CellsMatrix.Add(new Cell("11"));
            CellsMatrix.Add(new Cell("12"));
            CellsMatrix.Add(new Cell("13"));
            CellsMatrix.Add(new Cell("14"));
            CellsMatrix.Add(new Cell("15"));
            CellsMatrix.Add(new Cell("16"));
            CellsMatrix.Add(new Cell("20"));
            CellsMatrix.Add(new Cell("21"));
            CellsMatrix.Add(new Cell("22"));
            CellsMatrix.Add(new Cell("23"));
            CellsMatrix.Add(new Cell("24"));
            CellsMatrix.Add(new Cell("25"));
            CellsMatrix.Add(new Cell("26"));
            CellsMatrix.Add(new Cell("30"));
            CellsMatrix.Add(new Cell("31"));
            CellsMatrix.Add(new Cell("32"));
            CellsMatrix.Add(new Cell("33"));
            CellsMatrix.Add(new Cell("34"));
            CellsMatrix.Add(new Cell("35"));
            CellsMatrix.Add(new Cell("36"));
            CellsMatrix.Add(new Cell("40"));
            CellsMatrix.Add(new Cell("41"));
            CellsMatrix.Add(new Cell("42"));
            CellsMatrix.Add(new Cell("43"));
            CellsMatrix.Add(new Cell("44"));
            CellsMatrix.Add(new Cell("45"));
            CellsMatrix.Add(new Cell("46"));
            CellsMatrix.Add(new Cell("01"));
        }
    }
}