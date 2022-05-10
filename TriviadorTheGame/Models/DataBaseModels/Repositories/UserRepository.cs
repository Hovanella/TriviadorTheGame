using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public class UserRepository : IUsersRepository
    {
        
        private readonly DataBaseContext _context = new DataBaseContext();
        
        public  User CurrentUser { get; set; }
        
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
        }
    }
}