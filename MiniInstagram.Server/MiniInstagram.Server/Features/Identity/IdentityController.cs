using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Features.Identity.Models;
using MiniInstagram.Server.Infrastructure.Services;

namespace MiniInstagram.Server.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUserService;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<AppSettings> appSettings,
            ICurrentUserService currentUserService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.currentUserService = currentUserService;
            this.appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route(nameof(MyProfile))]
        [Authorize]
        public async Task<ActionResult<ProfileServiceModel>> MyProfile()
        {
            var userId = currentUserService.GetId();
            return await this.identityService.GetOne(userId);
        }

        [HttpGet]
        [Route(nameof(Search))]
        public async Task<IEnumerable<SearchUsersServiceModel>> Search(string searchTerm)
        {
            return await this.identityService.SearchByUserName(searchTerm);
        }

        [Authorize]
        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<ActionResult> Edit(UpdateProfileRequestModel model)
        {
            var userId = currentUserService.GetId();

            var updated = await this.identityService.Update(
                userId, 
                model.ProfileUrl, 
                model.Gender, 
                model.WebSite, 
                model.Biography, 
                model.IsPrivate);

            if (updated.Failure)
            {
                return BadRequest(updated.Error);
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var usedEmail = await this.identityService.IsEmailUnique(model.Email);

            if (usedEmail.Failure)
            {
                return BadRequest(usedEmail.Error);
            }

            var user = new User 
            { 
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var token = this.identityService.GenerateJwtToken(user.Id, user.UserName, this.appSettings.Secret);

            return new LoginResponseModel
            {
                Token = token
            };
        }
    }
}
