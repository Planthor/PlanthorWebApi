using System.Collections.Generic;
using System.Linq;

namespace PlanthorWebApi.Domain;

/// <summary>
/// Represents a base class for value objects.
/// Value objects are immutable objects that encapsulate a set of attributes or properties.
/// They are used to model concepts that have no identity and are solely defined by their attributes.
/// Value objects promote immutability, encapsulation, and strong typing.
/// They are an essential building block in DDD and Clean Architecture to ensure domain integrity and separation of concerns.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Gets the equality components of the value object.
    /// </summary>
    protected abstract IEnumerable<object> EqualityComponents { get; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return EqualityComponents.SequenceEqual(other.EqualityComponents);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return EqualityComponents
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}
