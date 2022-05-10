using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Infrastructure.Extensions;
using MiniInstagram.Server.Models.Games;

namespace MiniInstagram.Server.Controllers
{
    public class GamesController : ApiController
    {
        private readonly MiniInstagramDbContext db;

        public GamesController(MiniInstagramDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateGamesRequestModel model)
        {
            var userId = this.User.GetId();

            var game = new Game
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                UserId = userId,
            };

            this.db.Add(game);
            await this.db.SaveChangesAsync();

            return Created(nameof(this.Create), game.Id);
        }
    }
}
