using Domain.Primitives;

namespace Tests.UnitTests;

public class EntityTests
{
    public class TestEntity : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [Fact]
    public void ToString_WhenCalled_ReturnsCorrectStringRepresentation()
    {
        // Arrange
        var entity = new TestEntity { Name = "Test", Age = 25 };

        // Act
        var result = entity.ToString();

        // Assert
        var expected = $"Name: Test{Environment.NewLine}Age: 25{Environment.NewLine}";
        Assert.Equal(expected, result);
    }
}