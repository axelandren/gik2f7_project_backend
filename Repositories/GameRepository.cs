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

            games = new List<Game> {
                new Game() { Name = "League" , Id = 1},
                new Game() { Name = "Hearts of Iron", Id = 2 },
                new Game() { Name = "Counter-Strike", Id = 3 }
            };
        }
        public async Task<Game> Add(Game game)
        {
            using(var connection = new SqliteConnection(databaseConfig.Name))
            {
                var res = await connection.ExecuteAsync("INSERT INTO Games (Name, Description) VALUES (@Name, @Description)", game);
                var lastInsert = await connection.QueryAsync<Game>("SELECT Id FROM Games ORDER BY Id DESC");
                game.Id = lastInsert.FirstOrDefault<Game>().Id;
                return game;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            using(var connection = new SqliteConnection(databaseConfig.Name))
            {
                var res = await connection.ExecuteAsync("DELETE FROM Games WHERE Id=@Id,", new { Id });
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

        public Task<IEnumerable<Game>> Get()
        {
            var task = Task.Run<IEnumerable<Game>>(() => {
                return games;
            });
            return task;
        }

        public Task<Game> Get(int Id)
        {
            var task = Task.Run<Game>(() => {
                return games.FirstOrDefault(e => e.Id == Id);
            });
            return task;
        }

        public Task<Game> Update(Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}
