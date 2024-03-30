using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Api.Tests.TestDataBuilders;

public class TribeBuilder
{
    private string _name;
    private string _description;

    public TribeBuilder()
    {
        _name = "Tribe 1";
        _description = "Test Tribe 1";
    }

    public TribeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TribeBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Tribe Build()
    {
        return new Tribe
        {
            Name = _name,
            Description = _description
        };
    }
}
