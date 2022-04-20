using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities.Identity;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService, INotifier notifier) : base(notifier)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
            var Role = await _roleService.GetRoleByIdAsync(id);

            if (Role == null)
            {
                return NotFound();
            }

            return Role;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, Role Role)
        {
            if (id != Role.Id)
            {
                return BadRequest();
            }

            await _roleService.UpdateRoleAsync(Role);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role Role)
        {
            return await _roleService.AddRoleAsync(Role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            await _roleService.RemoveRoleAsync(id);

            return NoContent();
        }
    }
}
