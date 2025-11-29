namespace PlaceRentalApp.API.Entities;

public class PlaceAmenity : BaseEntity
{
    protected PlaceAmenity() { }

    public PlaceAmenity(int idPlace, string description)
    {
        IdPlace = idPlace;
        Description = description;
    }

    public int IdPlace { get; private set; }
    public string Description { get; private set; }
}
