namespace Zuhid.Base.Tests;

public class ComplexMappingTests {
  // Models with more diverse property types for testing conversion
  public class ComplexSource {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public List<string> Tags { get; set; } = [];
  }

  public class ComplexDestination {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public List<string> Tags { get; set; } = [];
  }

  // Testing value types (numbers, booleans, dates)
  [Theory]
  [InlineData(1, 9.99, true)]
  [InlineData(2, 0, false)]
  [InlineData(int.MaxValue, 999.99, true)]
  [InlineData(int.MinValue, -100.50, false)]
  public void Map_ShouldHandleVariousValueTypes(
      int id,
      decimal price,
      bool isActive) {
    // Arrange
    var mapper = new BaseMapper<ComplexSource, ComplexDestination>();
    var testDate = new DateTime(2024, 3, 15);
    var source = new ComplexSource {
      Id = id,
      Name = "Test Product",
      CreatedDate = testDate,
      Price = price,
      IsActive = isActive
    };

    // Act
    var result = mapper.GetEntity(source);

    // Assert
    Assert.Equal(id, result.Id);
    Assert.Equal(price, result.Price);
    Assert.Equal(isActive, result.IsActive);
    Assert.Equal(testDate, result.CreatedDate);
  }

  // Testing reference types (strings, complex objects)
  [Theory]
  [InlineData("Regular String")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("Very long string that exceeds typical size limits and might cause issues in some mappers Very long string that exceeds typical size limits and might cause issues in some mappers Very long string that exceeds typical size limits and might cause issues in some mappers")]
  public void Map_ShouldHandleVariousStringValues(string? name) {
    // Arrange
    var mapper = new BaseMapper<ComplexSource, ComplexDestination>();
    var source = new ComplexSource {
      Id = 1,
      Name = name
    };

    // Act
    var result = mapper.GetEntity(source);

    // Assert
    Assert.Equal(name, result.Name);
  }

  // Testing collections mapping
  [Theory]
  [MemberData(nameof(CollectionTestData))]
  public void Map_ShouldHandleCollections(List<string> tags) {
    // Arrange
    var mapper = new BaseMapper<ComplexSource, ComplexDestination>();
    var source = new ComplexSource {
      Id = 1,
      Tags = tags
    };

    // Act
    var result = mapper.GetEntity(source);

    // Assert
    Assert.Equal(tags, result.Tags);
  }

  // Testing DateTimes
  [Theory]
  [MemberData(nameof(DateTimeTestData))]
  public void Map_ShouldHandleDateTimeValues(DateTime dateTime) {
    // Arrange
    var mapper = new BaseMapper<ComplexSource, ComplexDestination>();
    var source = new ComplexSource {
      Id = 1,
      CreatedDate = dateTime
    };

    // Act
    var result = mapper.GetEntity(source);

    // Assert
    Assert.Equal(dateTime, result.CreatedDate);
  }

  // Member data for collections
  public static IEnumerable<object[]> CollectionTestData() {
    yield return new object[] { new List<string>() }; // Empty list
    yield return new object[] { new List<string> { "tag1" } }; // Single item
    yield return new object[] { new List<string> { "tag1", "tag2", "tag3" } }; // Multiple items
    yield return new object[] { null }; // Null list
  }

  // Member data for DateTimes
  public static IEnumerable<object[]> DateTimeTestData() {
    yield return new object[] { DateTime.MinValue };
    yield return new object[] { DateTime.MaxValue };
    yield return new object[] { DateTime.UtcNow };
    yield return new object[] { new DateTime(2000, 1, 1) };
    yield return new object[] { new DateTime(2024, 12, 31, 23, 59, 59, 999) };
  }
}
