using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;
using Xunit;

namespace PlanthorWebApi.Domain.Tests.Shared;

public class ValueObjectTests
{
    [Fact]
    public void WithSameValuesHaveSameHashCode()
    {
        // Arrange
        var valueObject1 = new TestValueObject
        {
            Value = 1
        };

        var valueObject2 = new TestValueObject
        {
            Value = 1
        };

        // Act & Assert
        Assert.True(valueObject1.Equals(valueObject2));
        Assert.Equal(valueObject1.GetHashCode(), valueObject2.GetHashCode());
    }

    private class TestValueObject : ValueObject
    {
        public int Value { get; init; }

        protected override IEnumerable<object> EqualityComponents => [Value];
    }
}
