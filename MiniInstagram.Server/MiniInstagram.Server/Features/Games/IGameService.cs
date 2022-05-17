using MiniInstagram.Server.Features.Games.Models;

namespace MiniInstagram.Server.Features.Games
{
    public interface IGameService
    {
        Task<int> Create(string title, string description, string imageUrl, string userId);

        Task<IEnumerable<GameListServiceModel>> AllByUserId(string userId);

        Task<IEnumerable<GameListServiceModel>> All();

        Task<GameDetailsServiceModel> GetOne(int gameId);

        Task<bool> Update(int gameId, string title, string description, string imageUrl, string userId);

        Task<bool> Delete(int gameId, string userId);

    }
}
