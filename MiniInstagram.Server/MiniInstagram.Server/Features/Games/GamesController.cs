using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniInstagram.Server.Features.Games.Models;
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
        [Route(nameof(Mine))]
        [HttpGet]
        public async Task<IEnumerable<GameListServiceModel>> Mine()
        {
            var userId = this.User.GetId();

            var games = await this.gameService.ByUser(userId);

            return games;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<GameDetailsServiceModel>> GetOne(int id)
        {
            var game = await this.gameService.GetOne(id);
            if (game == null)
            {
                return BadRequest();
            }

            return game;
        }

        [Route(nameof(All))]
        [HttpGet]
        public async Task<IEnumerable<GameListServiceModel>> All()
        {
            return await this.gameService.All();
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreateGameRequestModel model)
        {
            var userId = this.User.GetId();

            var gameId = await this.gameService.Create(model.Title, model.Description, model.ImageUrl, userId);

            return Created(nameof(this.Create), gameId);
        }

        [Authorize]
        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<ActionResult> Edit(UpdateGameRequestModel model)
        {
            var userId = this.User.GetId();

            var isUpdated = await this.gameService.Update(model.Id, model.Title, model.Description, model.ImageUrl, userId);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
