namespace Identity.Domain.Entities.v1
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
