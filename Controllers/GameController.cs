using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektWebApi.Repositories;

namespace ProjektWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository gr;
        public GameController(IGameRepository repo)
        {
            gr = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            return await gr.Get();
        }
        
        [HttpGet("{id}")]
        public async Task<Game> GetGame(int id)
        {
            return await gr.Get(id);
        }

        [HttpPost]
        public async Task<Game> Add(AddGame newGame)
        {
            return await gr.Add(newGame);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await gr.Delete(id); // 100?
        }

        [HttpPost("img")]
        public async Task<Game> AddGameImage([FromForm] PostGameImage gameImage)
        {
            Game exGame = await gr.Get(gameImage.Id);

            

            return exGame;
        }
    }
}
