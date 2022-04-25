namespace RouteManager.Domain.Entities.Identity
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
