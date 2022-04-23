using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RouteManager.Domain.Identity.Extensions;
using RouteManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace RouteManagerMVC.Services
{
    public interface IAuthService
    {
        Task<UserResponseLogin> LoginAsync(UserLogin userLogin);
        Task LogoutAsync();
        Task<UserResponseLogin> RegisterAsync(UserRegister userForRegisterDto);
        Task SaveTokenAsync(UserResponseLogin responseLogin);
    }

    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAspNetUser _aspNetUser;

        public AuthService(HttpClient httpClient, IAuthenticationService authenticationService, IAspNetUser aspNetUser)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7114");
            _authenticationService = authenticationService;
            _aspNetUser = aspNetUser;
        }

        public async Task<UserResponseLogin> LoginAsync(UserLogin userLogin)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("Identity/api/Auth/login", userLogin);

            if (responseMessage.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<UserResponseLogin>(await responseMessage.Content.ReadAsStreamAsync(), options);

                await SaveTokenAsync(result);

                return result;
            }
            else
            {
                //errors
            }
            return null;
        }





        public async Task<UserResponseLogin> RegisterAsync(UserRegister userForRegisterDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("Identity/api/Auth/register", userForRegisterDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await JsonSerializer.DeserializeAsync<UserResponseLogin>(await responseMessage.Content.ReadAsStreamAsync(), options);

                await SaveTokenAsync(result);
            }
            else
            {
                //errors
            }
            return null;
        }

        public async Task SaveTokenAsync(UserResponseLogin responseLogin)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", responseLogin.AccessToken));
            claims.Add(new Claim("RefreshToken", responseLogin.RefreshToken.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(
                    _aspNetUser.GetHttpContext(),
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
        }


        public async Task LogoutAsync()
        {
            await _authenticationService.SignOutAsync(
                _aspNetUser.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                null);
        }

    }
}
