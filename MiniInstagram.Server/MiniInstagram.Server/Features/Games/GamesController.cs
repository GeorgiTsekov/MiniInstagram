using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniInstagram.Server.Infrastructure.Extensions;

namespace MiniInstagram.Server.Features.Games
{
    public class GamesController : ApiController
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreateGamesRequestModel model)
        {
            var userId = this.User.GetId();

            var gameId = await this.gameService.Create(model.Title, model.Description, model.ImageUrl, userId);

            return Created(nameof(this.Create), gameId);
        }

        [Authorize]
        [Route(nameof(Mine))]
        public async Task<IEnumerable<GameListResponseModel>> Mine()
        {
            var userId = this.User.GetId();

            return await this.gameService.ByUser(userId);
        }

        [Route(nameof(All))]
        public async Task<IEnumerable<GameListResponseModel>> All()
        {
            return await this.gameService.All();
        }
    }
}
