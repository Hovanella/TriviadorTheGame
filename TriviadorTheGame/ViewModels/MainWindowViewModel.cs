using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TriviadorTheGame.Models;
using TriviadorTheGame.ViewModels.BaseViewModel;

namespace TriviadorTheGame.ViewModels
{
    public class MainWindowViewModel : BaseViewModel.BaseViewModel
    {
        private Page _currentPage;

        public MainWindowViewModel()
        {
            ModelViewManager.MainWindowViewModel = this;
            CurrentPage = Navigation.Pages["LoggingPage"];

        }

        public bool LanguageChecked { get; set; } = false;
        public MediaPlayer _mediaPlayer { get; } = new();

        public void ChangeLanguage(bool languageChecked)
        {
            LanguageChecked = languageChecked;
            if (LanguageChecked == true)
            {
                var russian = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x =>
                    x.Source == new Uri("Resources/Languages/Russian.xaml", UriKind.Relative));
                Application.Current.Resources.MergedDictionaries.Remove(russian);

                var english = new ResourceDictionary()
                    { Source = new Uri("Resources/Languages/English.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(english);
            }
            else
            {
                var english = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x =>
                    x.Source == new Uri("Resources/Languages/English.xaml", UriKind.Relative));
                Application.Current.Resources.MergedDictionaries.Remove(english);

                var russian = new ResourceDictionary()
                    { Source = new Uri("Resources/Languages/Russian.xaml", UriKind.Relative) };
                Application.Current.Resources.MergedDictionaries.Add(russian);

            }

         
        }

        public MediaPlayer _mediaPlayerForArtem { get; } = new();

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        
    }
}