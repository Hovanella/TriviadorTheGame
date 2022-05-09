using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using TriviadorTheGame.Models;
using TriviadorTheGame.Models.DataBaseModels;

namespace TriviadorTheGame.ViewModels
{

    public class MainWindowViewModel : BaseViewModel.BaseViewModel
    {
        private Page _currentPage;

        public MainWindowViewModel()
        {
            ModelViewManager.MainWindowViewModel = this;
            CurrentPage = Navigation.Pages["RedactorPage"];
д
           using(var Context = new DataBaseContext())
            {
                var users = Context.Users;

               foreach(var user in users)
                {
                    Console.WriteLine($"{user.USER_ID}  {user.USER_LOGIN}  {user.USER_PASSWORD} {user.USER_ROLE}  ");
                }
            }

        }

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