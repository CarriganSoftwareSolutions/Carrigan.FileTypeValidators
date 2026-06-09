using Carrigan.FileTypeValidators.Extensions.String;

namespace Carrigan.FileTypeValidators.Tests.Extensions.String;


public class SanitizeFileExtensionTests
{
    [Fact]
    public void SanitizeFileExtension_RemovesLeadingDotToLower()
    {
        // Arrange
        string extension = ".JpG";

        // Act
        string result = extension.SanitizeFileExtension();

        // Assert
        Assert.Equal("JpG", result);
    }
    [Fact]
    public void SanitizeFileExtension_RemovesLeadingDot()
    {
        // Arrange
        string extension = ".jpg";

        // Act
        string result = extension.SanitizeFileExtension();

        // Assert
        Assert.Equal("jpg", result);
    }

    [Fact]
    public void SanitizeFileExtension_NoLeadingDot_NoChange()
    {
        // Arrange
        string extension = "png";

        // Act
        string result = extension.SanitizeFileExtension();

        // Assert
        Assert.Equal("png", result);
    }

    [Fact]
    public void SanitizeFileExtension_ToLower()
    {
        // Arrange
        string extension = "PnG";

        // Act
        string result = extension.SanitizeFileExtension();

        // Assert
        Assert.Equal("PnG", result);
    }

    [Fact]
    public void SanitizeFileExtension_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        string extension = "";

        // Act
        string result = extension.SanitizeFileExtension();

        // Assert
        Assert.Equal("", result);
    }
}
