namespace PlanthorWebApi.Domain.Shared;

public interface IOwnedEntity
{
    string OwnerId { get; }
}
