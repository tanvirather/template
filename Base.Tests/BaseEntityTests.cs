namespace Zuhid.Base.Tests;

public class BaseEntityTests {
  [Fact]
  public void Default_Constructor_Should_Set_Default_Values() {
    // Arrange & Act
    var entity = new BaseEntity();

    // Assert
    Assert.Equal(Guid.Empty, entity.Id);
    Assert.Equal(Guid.Empty, entity.UpdatedById);

    // DateTime default should be DateTime.MinValue (0001-01-01)
    Assert.Equal(default, entity.UpdatedDateTime);
  }

  [Fact]
  public void Properties_Should_Be_Assignable_And_Retrievable() {
    // Arrange
    var id = Guid.NewGuid();
    var updatedById = Guid.NewGuid();
    var updatedDateTime = DateTime.UtcNow;

    var entity = new BaseEntity {
      Id = id,
      UpdatedById = updatedById,
      UpdatedDateTime = updatedDateTime
    };

    // Assert
    Assert.Equal(id, entity.Id);
    Assert.Equal(updatedById, entity.UpdatedById);
    Assert.Equal(updatedDateTime, entity.UpdatedDateTime);
  }

  [Fact]
  public void UpdatedDateTime_Should_Handle_Utc_Kind() {
    // Arrange
    var utcNow = DateTime.UtcNow;

    var entity = new BaseEntity {
      UpdatedDateTime = utcNow
    };

    // Assert
    Assert.Equal(DateTimeKind.Utc, entity.UpdatedDateTime.Kind);
    Assert.Equal(utcNow, entity.UpdatedDateTime);
  }

  [Fact]
  public void Can_Update_Properties_After_Initialization() {
    // Arrange
    var entity = new BaseEntity();

    // Act
    var id1 = Guid.NewGuid();
    var id2 = Guid.NewGuid();
    var time1 = DateTime.UtcNow.AddMinutes(-5);
    var time2 = DateTime.UtcNow;

    entity.Id = id1;
    entity.UpdatedById = id1;
    entity.UpdatedDateTime = time1;

    // Assert initial assignments
    Assert.Equal(id1, entity.Id);
    Assert.Equal(id1, entity.UpdatedById);
    Assert.Equal(time1, entity.UpdatedDateTime);

    // Act: reassign
    entity.Id = id2;
    entity.UpdatedById = id2;
    entity.UpdatedDateTime = time2;

    // Assert reassigned values
    Assert.Equal(id2, entity.Id);
    Assert.Equal(id2, entity.UpdatedById);
    Assert.Equal(time2, entity.UpdatedDateTime);
  }
}
