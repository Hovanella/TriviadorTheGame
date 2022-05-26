using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly DataBaseContext _context = new();

        public User CurrentUser { get; set; }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }


        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUserByLogging(string login)
        {
            return _context.Users.FirstOrDefault(x => x.USER_LOGIN == login);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            _context.UserStatistics.Add(new UserStatistic()
                { USER_ID = user.USER_ID, GAMES_NUMBER = 0, SCORE_NUMBER = 0 });
            _context.SaveChanges();
        }

        public UserStatistic FindUserStatistics(int userId)
        {
            return _context.UserStatistics.FirstOrDefault(x => x.USER_ID == userId);
        }

        public void UpdateUserStatistics(int statisticsId, int Score)
        {
            var userStatistic = _context.UserStatistics.FirstOrDefault(x => x.STATISTICS_ID == statisticsId);
            userStatistic.SCORE_NUMBER += Score;
            userStatistic.GAMES_NUMBER++;
            _context.SaveChanges();
        }

        public ObservableCollection<UserWithStatistics> GetUserWithStatistics()
        {
            var users = _context.Users.ToList();
            var userStatistics = _context.UserStatistics.ToList();
            var userWithStatistics = new ObservableCollection<UserWithStatistics>();
            foreach (var user in users)
            {
                var userStatistic = userStatistics.FirstOrDefault(x => x.USER_ID == user.USER_ID);
                userWithStatistics.Add(new UserWithStatistics()
                {
                    USER_ID = user.USER_ID,
                    USER_LOGIN = user.USER_LOGIN,
                    USER_PASSWORD = user.USER_PASSWORD,
                    USER_ROLE = user.USER_ROLE,
                    GAMES_NUMBER = (int)userStatistic.GAMES_NUMBER,
                    SCORE_NUMBER = (int)userStatistic.SCORE_NUMBER
                });
            }

            return userWithStatistics;
        }

        public void DeleteUser(int selectedUserUserId)
        {
            var user = _context.Users.FirstOrDefault(x => x.USER_ID == selectedUserUserId);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User userRepositoryCurrentUser)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            _context.SaveChanges();
        }

        public void UpdateUserInfo(int selectedUserUserId, string login, string password, int score, int games)
        {

            var user = _context.Users.FirstOrDefault(x => x.USER_ID == selectedUserUserId);
            user.USER_LOGIN = login;
            user.USER_PASSWORD = password;
            var userStatistic = _context.UserStatistics.FirstOrDefault(x => x.USER_ID == selectedUserUserId);
            userStatistic.SCORE_NUMBER = score;
            userStatistic.GAMES_NUMBER = games;
            _context.SaveChanges();
        }

        public void AddUserStatistics(UserStatistic newUserStatistics)
        {
            _context.UserStatistics.Add(newUserStatistics);
            _context.SaveChanges();
        }
    }

}
    
