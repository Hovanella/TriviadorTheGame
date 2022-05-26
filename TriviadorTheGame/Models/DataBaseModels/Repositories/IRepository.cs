using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviadorTheGame.Models.DataBaseModels.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        IEnumerable<T> GetAll();

        Task<bool> Remove(int id);
    }
}