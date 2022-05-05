namespace Routes.Domain.Entities.v1;

public class Address
{

    public override string ToString()
    {
        return $"{Street}, {Number} - {City?.Name}, {CEP}";
    }

    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? Complement { get; set; }
    public string? District { get; init; }
    public string? CEP { get; init; }
    public City? City { get; init; }
}