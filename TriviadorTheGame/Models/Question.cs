namespace TriviadorTheGame.Views.RedactorPage
{



    public class Question
    {
        public Question(string questionText, string rightAnswer, string firstWrongAnswer, string secondWrongAnswer,
            string thirdWrongAnswer)
        {
            QuestionText = questionText;
            RightAnswer = rightAnswer;
            FirstWrongAnswer = firstWrongAnswer;
            SecondWrongAnswer = secondWrongAnswer;
            ThirdWrongAnswer = thirdWrongAnswer;
        }

        public string QuestionText { get; set; }
        public string RightAnswer { get; set; }
        public string FirstWrongAnswer { get; set; }
        public string SecondWrongAnswer { get; set; }
        public string ThirdWrongAnswer { get; set; }



    }


}
