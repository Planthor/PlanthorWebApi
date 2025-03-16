using System;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Api.Tests.TestDataBuilders;

public class TribeBuilder
{
    private string _name;
    private string _description;
    private string? _slogan;
    private string? _pathAvatar;
    private string _nationality;
    private string _ownerId;

    public TribeBuilder()
    {
        _name = "Tribe 1";
        _description = "Test Tribe 1";
        _nationality = "VN";
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
            _pathAvatar,
            _nationality,
            _ownerId
        );
    }
}
