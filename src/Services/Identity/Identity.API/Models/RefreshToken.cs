using System;

namespace Identity.API.Models;

public record RefreshToken
{
    public RefreshToken()
    {
        Id = Guid.NewGuid();
        Token = Guid.NewGuid();
    }

    private Guid Id { get; }
    public string Username { get; set; }
    private Guid Token { get; }
    public DateTime ExpirationDate { get; set; }
}