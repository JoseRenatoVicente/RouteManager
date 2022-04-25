using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities.Identity;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, INotifier notifier) : base(notifier)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [Authorize(Roles = "Usuários")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            return await CustomResponseAsync(await _userService.UpdateUserAsync(user));
        }

        [Authorize(Roles = "Usuários")]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            return await CustomResponseAsync(await _userService.AddUserAsync(user));
        }

        [Authorize(Roles = "Usuários")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableUser(string id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DisableUserAsync(id);

            return NoContent();
        }
    }
}
