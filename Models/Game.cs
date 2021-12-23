using Microsoft.AspNetCore.Http;

namespace ProjektWebApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // public int Grade { get; set; }
        // public image?
    }

    public class AddGame : Game
    {
        private new int Id { get; set; }
    }
    public class PostGameImage
    {
        public int Id { get; set; }
        public IFormFile postImage { get; set; }
    }
}
