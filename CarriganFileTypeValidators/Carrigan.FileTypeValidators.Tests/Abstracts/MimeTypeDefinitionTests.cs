using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;
using Carrigan.FileTypeValidators.MimeTypeDefinitions;


namespace Carrigan.FileTypeValidators.Tests.Abstracts;

public class MimeTypeDefinitionTests
{
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasAtLeastOne()
    {
        MimeTypeDefinition baseClass = new ImageJpegDefinition();
        Assert.True(baseClass.AllFileExtensions.Any());
    }
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasFour()
    {
        MimeTypeDefinition baseClass = new ImageJpegDefinition();
        Assert.Equal(4, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestBmpHasOne()
    {
        MimeTypeDefinition baseClass = new ImageBmpDefinition();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestGifHasOne()
    {
        MimeTypeDefinition baseClass = new ImageGifDefinition();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestTiffHasTwo()
    {
        MimeTypeDefinition baseClass = new ImageTiffDefinition();
        Assert.Equal(2, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestWebpHasOne()
    {
        MimeTypeDefinition baseClass = new ImageWebpDefinition();
        Assert.Single(baseClass.AllFileExtensions);
    }
}
