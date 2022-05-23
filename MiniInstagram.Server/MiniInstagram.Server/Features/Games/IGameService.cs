using MiniInstagram.Server.Features.Games.Models;
using MiniInstagram.Server.Infrastructure.Services;

namespace MiniInstagram.Server.Features.Games
{
    public interface IGameService
    {
        Task<int> Create(string title, string description, string imageUrl, string userId);

        Task<IEnumerable<GameListServiceModel>> AllByUserId(string userId);

        Task<IEnumerable<GameListServiceModel>> All();

        Task<GameDetailsServiceModel> GetOne(int gameId);

        Task<Result> Update(int gameId, string title, string description, string imageUrl, string userId);

        Task<Result> Delete(int gameId, string userId);

        Task<IEnumerable<GameListServiceModel>> SearchByTitle(string query);
    }
}
