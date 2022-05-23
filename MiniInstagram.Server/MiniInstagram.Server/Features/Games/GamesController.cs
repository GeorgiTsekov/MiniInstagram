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
        [Route(nameof(MyGames))]
        [HttpGet]
        public async Task<IEnumerable<GameListServiceModel>> MyGames()
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

        [HttpGet]
        [Route(nameof(Search))]
        public async Task<IEnumerable<GameListServiceModel>> Search(string searchTerm)
        {
            return await this.gameService.SearchByTitle(searchTerm);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreateUpdateGameRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var gameId = await this.gameService.Create(model.Title, model.Description, model.ImageUrl, userId);

            return Created(nameof(this.Create), gameId);
        }

        [Authorize]
        [HttpPut]
        [Route($"id/{nameof(Edit)}")]
        public async Task<ActionResult> Edit(int id, CreateUpdateGameRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var updated = await this.gameService.Update(id, model.Title, model.Description, model.ImageUrl, userId);

            if (updated.Failure)
            {
                return BadRequest(updated.Error);
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route($"id/{nameof(Delete)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var deleted = await this.gameService.Delete(id, userId);

            if (deleted.Failure)
            {
                return BadRequest(deleted.Error);
            }

            return Ok();
        }
    }
}
