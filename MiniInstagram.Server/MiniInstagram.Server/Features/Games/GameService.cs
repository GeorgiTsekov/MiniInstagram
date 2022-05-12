using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Features.Games.Models;

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

        public async Task<IEnumerable<GameListServiceModel>> ByUser(string userId)
        {
            return await this.db
                .Games
                .Where(g => g.UserId == userId)
                .Select(g => new GameListServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GameListServiceModel>> All()
        {
            return await this.db
                .Games
                .Select(g => new GameListServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<GameDetailsServiceModel> GetOne(int gameId)
        {
            var game = await this.db.Games
                .Where(g => g.Id == gameId)
                .Select(g => new GameDetailsServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    UserId = g.UserId,
                    UserName = g.User.UserName,
                    ImageUrl = g.ImageUrl

                }).FirstOrDefaultAsync();

            return game;
        }
    }
}
