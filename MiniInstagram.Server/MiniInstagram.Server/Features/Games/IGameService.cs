namespace MiniInstagram.Server.Features.Games
{
    public interface IGameService
    {
        public Task<int> Create(string title, string description, string imageUrl, string userId);

        public Task<IEnumerable<GameListResponseModel>> ByUser(string userId);

        public Task<IEnumerable<GameListResponseModel>> All();
    }
}
