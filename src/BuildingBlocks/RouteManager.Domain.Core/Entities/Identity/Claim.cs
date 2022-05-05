namespace RouteManager.Domain.Core.Entities.Identity;

public class Claim
{
    public Claim(string description)
    {
        Description = description;
    }

    private string Description { get; }
}
