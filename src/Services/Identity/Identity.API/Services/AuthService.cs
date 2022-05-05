using Identity.API.Certificates;
using Identity.API.Models;
using Identity.Domain.Entities.v1;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RouteManager.Domain.Core.Services.Base;
using RouteManager.WebAPI.Core.Identity;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Services;

public class AuthService : BaseService
{
    private readonly IUserService _userService;
    private readonly AppSettings _appSettings;

    public AuthService(IOptions<AppSettings> appSettings, IUserService userService, IRoleService roleService, INotifier notifier) : base(notifier)
    {
        _appSettings = appSettings.Value;
        _userService = userService;
    }


    public async Task<UserResponseLogin> LoginAsync(UserLogin userLogin)
    {
        if (!await _userService.PasswordSignInAsync(userLogin))
        {
            Notification("Nome de usuário ou senha incorreta");
            return null;
        }

        return await GenerateJwt(userLogin.UserName);
    }


    private async Task<UserResponseLogin> GenerateJwt(string login)
    {
        var user = await _userService.GetUserByLoginAsync(login);

        var identityClaims = await GetClaimsUser(user);
        var encodedToken = EncodeToken(identityClaims);

        //var refreshToken = await GerarRefreshToken(email);

        return GetResponseToken(encodedToken, user);
    }

    private Task<ClaimsIdentity> GetClaimsUser(User user)
    {
        ICollection<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();

        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id));

        foreach (var userRole in user.Role!.Claims!)
        {
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, userRole.Description));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return Task.Run(() => identityClaims);
    }

    private string EncodeToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningAudienceCertificate().GetAudienceSigningKey()
        });

        return tokenHandler.WriteToken(token);
    }

    private UserResponseLogin GetResponseToken(string encodedToken, User user)
    {
        return new UserResponseLogin
        {
            AccessToken = encodedToken,
            //RefreshToken = refreshToken.Token,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Name = user.Name
            }
        };
    }


    /*public async Task<UserResponseLogin> GenerateJwt(string login)
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

        return await ObterRespostaToken(tokenHandler.WriteToken(token), )
    }*/
}