using RouteManager.Domain.Core.Entities.Base;

namespace Identity.Domain.Entities.v1;

public sealed class User : EntityBase
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public string? Email { get; set; }
    public bool Active { get; set; } = true;
    public Role? Role { get; set; }
}