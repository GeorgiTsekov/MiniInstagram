using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Features.Games.Models;
using MiniInstagram.Server.Infrastructure.Services;

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

        public async Task<IEnumerable<GameListServiceModel>> AllByUserId(string userId)
        {
            return await this.db
                .Games
                .OrderByDescending(g => g.CreatedOn)
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
                .OrderByDescending(g => g.CreatedOn)
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
            var game = await this.db
                .Games
                .Where(g => g.Id == gameId)
                .Select(g => new GameDetailsServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    UserId = g.UserId,
                    UserName = g.User.UserName,
                    ImageUrl = g.ImageUrl

                })
                .FirstOrDefaultAsync();

            return game;
        }

        public  async Task<Result> Update(int gameId, string title, string description, string imageUrl, string userId)
        {
            var game = await this.GetGameByIdAndByUserId(gameId, userId);

            if (game == null)
            {
                return "You are not authorized to update this game, or game doesn't exist already";
            }

            game.Title = title;
            game.Description = description;
            game.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<Result> Delete(int gameId, string userId)
        {
            var game = await GetGameByIdAndByUserId(gameId, userId);

            if (game == null)
            {
                return "You are not authorized to delete this game, or game doesn't exist already";
            }

            this.db.Games.Remove(game);

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<Game> GetGameByIdAndByUserId(int gameId, string userId)
        {
            return await this.db.Games.FirstOrDefaultAsync(g => g.Id == gameId && g.UserId == userId);
        }

        public async Task<IEnumerable<GameListServiceModel>> SearchByTitle(string query)
        {
            return await this.db.Games
                .Where(x => x.Title.Contains(query))
                .Select(g => new GameListServiceModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                })
                .ToListAsync();
        }
    }
}
