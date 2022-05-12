namespace MiniInstagram.Server.Features.Games.Models
{
    public class GameDetailsServiceModel : GameListServiceModel
    {
        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
