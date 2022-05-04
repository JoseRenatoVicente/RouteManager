namespace RouteManager.Domain.Core.Entities.Identity
{
    public class Claim
    {
        public Claim(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
