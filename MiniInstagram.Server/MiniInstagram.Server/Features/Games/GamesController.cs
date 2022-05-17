using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniInstagram.Server.Features.Games.Models;
using MiniInstagram.Server.Infrastructure.Services;

namespace MiniInstagram.Server.Features.Games
{
    public class GamesController : ApiController
    {
        private readonly IGameService gameService;
        private readonly ICurrentUserService currentUserService;

        public GamesController(
            IGameService gameService,
            ICurrentUserService currentUserService)
        {
            this.gameService = gameService;
            this.currentUserService = currentUserService;
        }

        [Authorize]
        [Route(nameof(Mine))]
        [HttpGet]
        public async Task<IEnumerable<GameListServiceModel>> Mine()
        {
            var userId = this.currentUserService.GetId();

            var games = await this.gameService.AllByUserId(userId);

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
            var userId = this.currentUserService.GetId();

            var gameId = await this.gameService.Create(model.Title, model.Description, model.ImageUrl, userId);

            return Created(nameof(this.Create), gameId);
        }

        [Authorize]
        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<ActionResult> Edit(UpdateGameRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var isUpdated = await this.gameService.Update(model.Id, model.Title, model.Description, model.ImageUrl, userId);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var isDeleted = await this.gameService.Delete(id, userId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
