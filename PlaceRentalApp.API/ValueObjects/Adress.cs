namespace PlaceRentalApp.API.ValueObjects;

public record Adress(string Street, string Number, string ZipCode, string District, string City, string State, string Country)
{
    public string GetFullAddress()
        => $"{Street}, {Number}, {District}, {City}, {State}, {Country}, {ZipCode}";

}
