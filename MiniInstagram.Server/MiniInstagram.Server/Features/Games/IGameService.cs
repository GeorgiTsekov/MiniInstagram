using MiniInstagram.Server.Features.Games.Models;

namespace MiniInstagram.Server.Features.Games
{
    public interface IGameService
    {
        public Task<int> Create(string title, string description, string imageUrl, string userId);

        public Task<IEnumerable<GameListServiceModel>> ByUser(string userId);

        public Task<IEnumerable<GameListServiceModel>> All();

        public Task<GameDetailsServiceModel> GetOne(int gameId);

        public Task<bool> Update(int id, string title, string description, string imageUrl, string userId);

    }
}
