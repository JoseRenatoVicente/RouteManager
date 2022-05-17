namespace Identity.API.Models;

public record ChangePasswordCurrentUserViewModel
{
    public string UserId { get; set; }
    public string CurrentPassword { get; init; }
    public string Password { get; init; }
}

public record ChangePasswordUserViewModel
{
    public string UserId { get; init; }
    public string Password { get; init; }
}