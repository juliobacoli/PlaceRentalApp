using System.Text.Json.Serialization;

namespace PlaceRentalApp.Core.Entities;

public class PlaceComment : BaseEntity
{
    public PlaceComment(int idUser, int idPlace, string comment)
        : base()
    {
        IdUser = idUser;
        IdPlace = idPlace;
        Comment = comment;
    }

    public int IdUser { get; private set; }

    public User User { get; private set; }

    public int IdPlace { get; private set; }

    [JsonIgnore]
    public Place Place { get; private set; }

    public string Comment { get; private set; }

    public void UpdateComment(string newComment)
    {
        Comment = newComment;
    }
}
