using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public interface  IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        
        Task<bool> Remove(int id);
    
    }
}