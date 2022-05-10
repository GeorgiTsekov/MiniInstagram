using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
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
        public async Task<ActionResult> Create(CreateGamesRequestModel model)
        {
            var userId = this.User.GetId();

            var gameId = this.gameService.Create(model.Title, model.Description, model.ImageUrl, userId);

            return Created(nameof(this.Create), gameId);
        }
    }
}
