using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using ProjektWebApi.Database;
using ProjektWebApi.Models;

namespace ProjektWebApi.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DatabaseConfig databaseConfig;
        private List<Game> games;
        public GameRepository(DatabaseConfig dbConfig)
        {
            databaseConfig = dbConfig;
        }
        public async Task<Game> Add(Game game)
        {
            using(var connection = new SqliteConnection(databaseConfig.Name))
            {
                var res = await connection.ExecuteAsync("INSERT INTO Games (Name, Description) VALUES (@Name, @Description)", game);
                var lastInsert = await connection.QueryAsync<Game>("SELECT Id FROM Games ORDER BY Id DESC");
                return new Game()
                {
                    Name = game.Name,
                    Description = game.Description,
                    Id = lastInsert.FirstOrDefault<Game>().Id
                };
            }
        }

        public async Task<bool> Delete(int Id)
        {
            using(var connection = new SqliteConnection(databaseConfig.Name))
            {
                var res = await connection.ExecuteAsync("DELETE FROM Games WHERE Id=@Id", new { Id });
                if(res > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Game>> Get()
        {
            using(var con = new SqliteConnection(databaseConfig.Name))
            {
                var res = await con.QueryAsync<Game>("SELECT Id, Name, Description, Image FROM Games ORDER BY Name ASC");
                return res;
            }
        }

        public async Task<Game> Get(int Id)
        {
            using(var con = new SqliteConnection(databaseConfig.Name))
            {
                var res = await con.QueryAsync<Game>("SELECT Id, Name, Description, Image FROM Games WHERE Id=@Id", new { Id });
                return res.FirstOrDefault<Game>();
            }
        }

        public Task<Game> Update(Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}
 