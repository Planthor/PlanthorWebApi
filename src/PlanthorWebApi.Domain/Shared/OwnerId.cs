using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

public sealed class OwnerId : ValueObject
{
    public string Value { get; }

    public OwnerId()
    {
        Value = Guid.Empty.ToString();
    }

    public OwnerId(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value;
    }

    protected override IEnumerable<object> EqualityComponents
    {
        get { yield return Value; }
    }

    public override string ToString() => Value;
}