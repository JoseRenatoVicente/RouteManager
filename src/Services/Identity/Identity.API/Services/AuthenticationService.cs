using Identity.API.Certificates;
using Identity.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RouteManager.Domain.Entities.Enums;
using RouteManager.Domain.Entities.Identity;
using RouteManager.Domain.Services;
using RouteManager.Domain.Services.Base;
using RouteManager.WebAPI.Core.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class AuthenticationService : BaseService
    {
        private readonly GatewayService _gatewayService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings, IUserService userService, IRoleService roleService, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
            _roleService = roleService;
            _gatewayService = gatewayService;
        }


        public async Task<UserResponseLogin> LoginAsync(UserLogin userLogin)
        {
            if (!await _userService.PasswordSignInAsync(userLogin))
            {
                Notification("Login and Password incorret");
                return null;
            }

            return await GenerateJwt(userLogin.UserName);
        }


        public async Task<UserResponseLogin> CreateAsync(UserRegister userRegister)
        {
            var user = new User
            {
                Name = userRegister.Name,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                Password = userRegister.Password,
            };

            user.Role = await _roleService.GetRoleByDescriptionAsync("User");

            var userReponse = await _userService.AddUserAsync(user);
            if (userReponse == null) return null;

            await _gatewayService.PostLogAsync(user, null, user, Operation.Create);

            return await GenerateJwt(user.UserName);
        }


        public async Task<UserResponseLogin> GenerateJwt(string login)
        {
            var user = await _userService.GetUserByLoginAsync(login);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new System.Security.Claims.Claim("Email", user.Email));
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, user.Role.Description));

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningAudienceCertificate().GetAudienceSigningKey()
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserResponseLogin
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds
            };
        }
    }
}
