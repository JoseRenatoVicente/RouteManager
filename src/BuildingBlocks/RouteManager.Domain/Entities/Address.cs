namespace RouteManager.Domain.Entities
{

    public class Address
    {

        public override string ToString()
        {
            return $"{Street}, {Number} - {City.Name}, {CEP}";
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public City City { get; set; }
    }
}
