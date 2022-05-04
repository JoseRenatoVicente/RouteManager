using Identity.API.Services;
using Identity.Domain.Entities.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService, INotifier notifier) : base(notifier)
        {
            _roleService = roleService;
        }

        [HttpGet("Claims")]
        public async Task<ActionResult<IEnumerable<Role>>> GetCurrentClaims()
        {
            return Ok(await _roleService.GetCurrentClaimsAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        [Authorize(Roles = "Funções")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            return await CustomResponseAsync(await _roleService.UpdateRoleAsync(role));
        }

        [Authorize(Roles = "Funções")]
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            return await CustomResponseAsync(await _roleService.AddRoleAsync(role));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            return await CustomResponseAsync(await _roleService.RemoveRoleAsync(id));
        }
    }
}
