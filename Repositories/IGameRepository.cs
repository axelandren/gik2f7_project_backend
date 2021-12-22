using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektWebApi.Models;

namespace ProjektWebApi.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Add(Game game);
        Task<IEnumerable<Game>> Get();
        Task<Game> Get(int Id);
        Task<Game> Update(Game game);
        Task<bool> Delete(int Id);
    }
}
