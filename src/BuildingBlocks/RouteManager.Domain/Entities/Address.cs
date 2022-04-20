namespace RouteManager.Domain.Entities
{

    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public City City { get; set; }
    }
}
