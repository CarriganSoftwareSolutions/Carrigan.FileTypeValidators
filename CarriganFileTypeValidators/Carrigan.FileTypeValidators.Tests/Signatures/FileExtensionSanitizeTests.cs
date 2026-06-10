using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.Signatures;


public class SanitizeFileExtensionTests
{
    [Fact]
    public void SanitizeFileExtension_RemovesLeadingDotToLower()
    {
        // Arrange
        string extension = ".JpG";

        // Act
        FileExtension result = new (extension);

        // Assert
        Assert.Equal("JpG", result);
    }
    [Fact]
    public void SanitizeFileExtension_RemovesLeadingDot()
    {
        // Arrange
        string extension = ".jpg";

        // Act
        FileExtension result = new(extension);

        // Assert
        Assert.Equal("jpg", result);
    }

    [Fact]
    public void SanitizeFileExtension_NoLeadingDot_NoChange()
    {
        // Arrange
        string extension = "png";

        // Act
        FileExtension result = new(extension);

        // Assert
        Assert.Equal("png", result);
    }

    [Fact]
    public void SanitizeFileExtension_ToLower()
    {
        // Arrange
        string extension = "PnG";

        // Act
        FileExtension result = new(extension);

        // Assert
        Assert.Equal("PnG", result);
    }

    [Fact]
    public void SanitizeFileExtension_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        string extension = "";

        // Act
        FileExtension result = new(extension);

        // Assert
        Assert.Equal("", result);
    }
}
