using System.Windows;
using System.Windows.Controls;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.Views.RedactorPage
{
    public partial class RedactorPage : Page
    {
        public RedactorPage()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem;

            if (item != null)
                MessageBox.Show((item as Question).QUESTION_TEXT);
        }
    }
}