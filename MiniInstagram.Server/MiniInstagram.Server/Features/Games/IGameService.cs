namespace MiniInstagram.Server.Features.Games
{
    public interface IGameService
    {
        public Task<int> Create(string title, string description, string imageUrl, string userId);
    }
}
