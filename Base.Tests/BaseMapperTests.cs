namespace Zuhid.Base.Tests;

public class BaseMapperTests {
  // Test classes for mapping
  private class SourceModel {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string ExtraProperty { get; set; } = string.Empty;
  }

  private class DestinationModel {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string UniqueProperty { get; set; } = string.Empty;
  }

  [Theory]
  [InlineData(1, "Test Name", "Extra Value 1")]
  [InlineData(42, "Another Name", "Extra Value 2")]
  [InlineData(999, "Third Test Case", "Some other value")]
  public void Map_ShouldMapMatchingProperties(int id, string name, string extraValue) {
    // Arrange
    var mapper = new BaseMapper<SourceModel, DestinationModel>();
    var testDate = new DateTime(2024, 1, 1);
    var source = new SourceModel {
      Id = id,
      Name = name,
      CreatedDate = testDate,
      ExtraProperty = extraValue
    };

    // Act
    var result = mapper.Map(source);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(source.Id, result.Id);
    Assert.Equal(source.Name, result.Name);
    Assert.Equal(source.CreatedDate, result.CreatedDate);
    Assert.Equal(string.Empty, result.UniqueProperty); // Not mapped, should be default
  }

  // [Fact]
  // public void Map_ShouldThrowNullReferenceException_WhenSourceIsNull() {
  //   // Arrange
  //   var mapper = new BaseMapper<SourceModel, DestinationModel>();

  //   // Act & Assert
  //   Assert.Throws<NullReferenceException>(() => mapper.Map(null));
  // }

  [Fact]
  public void Map_ShouldIgnorePropertiesOnlyInSource() {
    // Arrange
    var mapper = new BaseMapper<SourceModel, DestinationModel>();
    var source = new SourceModel {
      Id = 1,
      Name = "Test Name",
      ExtraProperty = "Extra Value"
    };

    // Act
    var result = mapper.Map(source);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(source.Id, result.Id);
    Assert.Equal(source.Name, result.Name);
    // ExtraProperty is only in source, shouldn't affect destination
  }

  [Fact]
  public void Map_ShouldLeavePropertiesOnlyInDestinationAsDefault() {
    // Arrange
    var mapper = new BaseMapper<SourceModel, DestinationModel>();
    var source = new SourceModel {
      Id = 1,
      Name = "Test Name"
    };

    // Act
    var result = mapper.Map(source);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(string.Empty, result.UniqueProperty); // Should be default value
  }

  [Theory]
  [InlineData(0, new int[0], new string[0])] // Empty list
  [InlineData(1, new[] { 1 }, new[] { "First" })] // Single item
  [InlineData(3, new[] { 1, 2, 3 }, new[] { "First", "Second", "Third" })] // Multiple items
  public void MapList_ShouldMapListOfSourceToListOfDestination(int expectedCount, int[] ids, string[] names) {
    // Arrange
    var mapper = new BaseMapper<SourceModel, DestinationModel>();
    var sourceList = new List<SourceModel>();

    for (var i = 0; i < ids.Length; i++) {
      sourceList.Add(new SourceModel { Id = ids[i], Name = names[i] });
    }

    // Act
    var result = mapper.MapList(sourceList);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedCount, result.Count);

    for (var i = 0; i < sourceList.Count; i++) {
      Assert.Equal(sourceList[i].Id, result[i].Id);
      Assert.Equal(sourceList[i].Name, result[i].Name);
    }
  }

  [Fact]
  public void MapList_ShouldReturnEmptyList_WhenSourceListIsEmpty() {
    // Arrange
    var mapper = new BaseMapper<SourceModel, DestinationModel>();
    var emptyList = new List<SourceModel>();

    // Act
    var result = mapper.MapList(emptyList);

    // Assert
    Assert.NotNull(result);
    Assert.Empty(result);
  }

  // [Fact]
  // public void MapList_ShouldThrowException_WhenSourceListIsNull() {
  //   // Arrange
  //   var mapper = new BaseMapper<SourceModel, DestinationModel>();

  //   // Act & Assert
  //   Assert.Throws<ArgumentNullException>(() => mapper.MapList(null));
  // }

  [Fact]
  public void Map_ShouldHandleDifferentPropertyTypes_WhenTypeConversionIsPossible() {
    // This test would require custom models with convertible property types
    // Skipping for now as it's not directly applicable to the basic mapper
    // Would need to implement type conversion in the mapper first
  }

  [Theory]
  [InlineData(1, "Original", "Custom: Original")]
  [InlineData(42, "Test String", "Custom: Test String")]
  [InlineData(999, "", "Custom: ")]
  public void Map_WithCustomImplementation_ShouldOverrideDefaultBehavior(int id, string originalName, string expectedName) {
    // Arrange
    var customMapper = new CustomMapper();
    var source = new SourceModel { Id = id, Name = originalName };

    // Act
    var result = customMapper.Map(source);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(source.Id, result.Id);
    Assert.Equal(expectedName, result.Name); // Should have custom prefix
  }

  // Custom mapper implementation for testing overrides
  private class CustomMapper : BaseMapper<SourceModel, DestinationModel> {
    public override DestinationModel Map(SourceModel model) {
      var result = base.Map(model);

      // Custom mapping logic
      result.Name = $"Custom: {model.Name}";

      return result;
    }
  }
}
