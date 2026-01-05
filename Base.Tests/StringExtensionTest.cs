namespace Zuhid.Base.Tests;

public class StringExtensionTest {
  [Theory]
  [InlineData("helloWorld", "HelloWorld")] // simple
  public void ToCamelCase(string expected, string input) {
    Assert.Equal(expected, input.ToCamelCase());
  }

  [Theory]
  [InlineData("HelloWorld", "helloWorld")] // simple
  public void ToTitleCase(string expected, string input) {
    Assert.Equal(expected, input.ToTitleCase());
  }

  [Theory]
  [InlineData("first-name", "FirstName")] // simple
  [InlineData("first-name", "firstName")] // when the first letter is lowercase
  [InlineData("first-500-name", "First500Name")] // convert number as one word
  [InlineData("first-acronym-last", "FirstACRONYMLast")] // convert acronym as one word
  public void ToKebabCase_test(string expected, string input) {
    Assert.Equal(expected, input.ToKebabCase());
  }

  [Theory]
  [InlineData("first_name", "FirstName")] // simple
  [InlineData("first_name", "firstName")] // when the first letter is lowercase
  [InlineData("first_500_name", "First500Name")] // convert number as one word
  [InlineData("first_acronym_last", "FirstACRONYMLast")] // convert acronym as one word
  public void ToSnakeCase_Test(string expected, string input) {
    Assert.Equal(expected, input.ToSnakeCase());
  }

  [Theory]
  [InlineData("HelloWorld", "~`!@#$%^&*()_+{}|:\"<>?-=[]\\;',./Hello   World")]
  [InlineData("Caft", "Café-été")]
  [InlineData("", "")]
  public void RemoveSpecialCharacters_Test(string expected, string input) {
    Assert.Equal(expected, input.RemoveSpecialCharacters());
  }
}
