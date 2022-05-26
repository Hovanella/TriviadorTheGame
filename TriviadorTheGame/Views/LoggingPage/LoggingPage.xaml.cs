using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TriviadorTheGame.Views
{
    /// <summary>
    ///     Interaction logic for LoggingPage.xaml
    /// </summary>
    public partial class LoggingPage : Page
    {
        public LoggingPage()
        {
            InitializeComponent();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button && button.IsChecked.Value)
            {
                ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                ConfirmPasswordTextBox.Visibility = Visibility.Visible;
                ConfirmPasswordTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}