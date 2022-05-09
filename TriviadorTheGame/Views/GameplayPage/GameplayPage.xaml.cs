using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TriviadorTheGame.Views.GameplayPage
{
    /// <summary>
    /// Interaction logic for GameplayPage.xaml
    /// </summary>
    public partial class GameplayPage : Page
    {
        public GameplayPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;
            a.Background = a.Background != Brushes.Red ? Brushes.Red : Brushes.White;

            SystemSounds.Hand.Play();
        }

        private void ButtonMainMenu_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu.MainMenu());
        }
    }
}
