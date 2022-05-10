namespace Identity.Domain.Entities.v1;

public sealed class Claim
{
    public Claim(string description)
    {
        Description = description;
    }

    public string Description { get; }
}