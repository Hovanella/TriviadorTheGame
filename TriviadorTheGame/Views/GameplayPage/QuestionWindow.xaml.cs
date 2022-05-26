using System.Threading.Tasks;
using System.Windows;
using TriviadorTheGame.ViewModels;

namespace TriviadorTheGame.Views.GameplayPage
{
    public partial class QuestionWindow : Window
    {
        public QuestionWindow()
        {
            InitializeComponent();
        }

        private void FirstAnswerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ModelViewManager.QuestionWindowViewModel.SubmitFunction(FirstAnswer.Text);
        }

        private void SecondAnswerButton_OnClick(object sender, RoutedEventArgs e)
        {
           ModelViewManager.QuestionWindowViewModel.SubmitFunction(SecondAnswer.Text);
        }

        private void ThirdAnswerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ModelViewManager.QuestionWindowViewModel.SubmitFunction(ThirdAnswer.Text);
        }

        private void FourthAnswerButton_OnClick(object sender, RoutedEventArgs e)
        {
            ModelViewManager.QuestionWindowViewModel.SubmitFunction(FourthAnswer.Text);
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            ModelViewManager.QuestionWindowViewModel.CloseSelf();
        }
    }
}