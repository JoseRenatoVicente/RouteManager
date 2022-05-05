namespace Identity.API.Models;

public record ChangePasswordCurrentUserViewModel
{
    public string UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
}

public record ChangePasswordUserViewModel
{
    public string UserId { get; set; }
    public string Password { get; set; }
}