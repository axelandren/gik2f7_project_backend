using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektWebApi.Repositories;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// GameController handles Http-operations
namespace ProjektWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository gr;
        private readonly IWebHostEnvironment env;
        public GameController(IGameRepository repo, IWebHostEnvironment env)
        {
            this.env = env;
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
            return await gr.Delete(id);
        }

        [HttpPost("img")]
        public async Task<Game> AddGameImage([FromForm] PostGameImage gameImage)
        {
            Game exGame = await gr.Get(gameImage.Id);

            var file = gameImage.postImage;
            var ext = Path.GetExtension(file.FileName).Replace(".", string.Empty);
            string fName = exGame.Id + "." + ext;

            string path = Path.Combine(env.ContentRootPath, "Uploads\\" + fName);
            using(var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(stream);
            }

            string url = "Uploads\\" + fName;
            exGame.Image = url;
            await gr.Update(exGame);
            return exGame;
        }

        [HttpGet("img/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            Game game = await gr.Get(id);
            if(game == null)
            {
                throw new ArgumentException("Felaktigt id");
            }
            if(game.Image == null)
            {
                throw new ArgumentException("Spelet har ingen bild");
            }
            var imgSrc = Path.Combine(env.ContentRootPath, game.Image);
            if(System.IO.File.Exists(imgSrc))
            {
                return PhysicalFile(imgSrc, "image/png");
            }
            else
            {
                throw new ArgumentException("Fil ej funnen, eller fel filtyp (krävs .png)");
            }
        }
    }
}
