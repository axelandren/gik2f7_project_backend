using Microsoft.AspNetCore.Http;

namespace ProjektWebApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        public string Image { get; set; }
    }

    public class AddGame : Game
    {
        private new int Id { get; set; }
    }
    // public class PutGameImage
    // {
    //     public int Id { get; set; }
    //     public IFormFile postImage { get; set; }
    // }
}
