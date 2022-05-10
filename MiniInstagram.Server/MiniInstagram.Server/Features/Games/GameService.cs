using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;

namespace MiniInstagram.Server.Features.Games
{
    public class GameService : IGameService
    {
        private readonly MiniInstagramDbContext db;

        public GameService(MiniInstagramDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(string title, string description, string imageUrl, string userId)
        {
            var game = new Game
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId,
            };

            this.db.Games.Add(game);
            await this.db.SaveChangesAsync();

            return game.Id;
        }
    }
}
