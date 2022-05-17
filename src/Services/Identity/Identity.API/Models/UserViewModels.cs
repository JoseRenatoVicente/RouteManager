using System;
using System.Collections.Generic;

namespace Identity.API.Models;

public record UserRegister
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}


public record UserLogin
{
    public string UserName { get; init; }

    public string Password { get; init; }
}

public record UserResponseLogin
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
}

public record UserToken
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaim> Claims { get; set; }
}

public record UserClaim
{
    public string Value { get; set; }
    public string Type { get; set; }
}