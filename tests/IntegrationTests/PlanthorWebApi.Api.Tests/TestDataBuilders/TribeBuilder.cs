using System;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Api.Tests.TestDataBuilders;

public class TribeBuilder
{
    private string _name;
    private string _description;
    private readonly string? _slogan;
    private readonly string _ownerId;

    public TribeBuilder()
    {
        _name = "Tribe 1";
        _description = "Test Tribe 1";
        _slogan = null;
        _ownerId = Guid.Empty.ToString();
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
        return new Tribe(
            _name,
            _slogan,
            _description,
            _ownerId
        );
    }
}
