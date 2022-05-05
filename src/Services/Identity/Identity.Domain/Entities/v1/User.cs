using RouteManager.Domain.Core.Entities.Base;

namespace Identity.Domain.Entities.v1;

public class User : EntityBase
{
    public string? Name { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public string? Email { get; init; }
    public bool Active { get; set; } = true;
    public Role? Role { get; set; }
}