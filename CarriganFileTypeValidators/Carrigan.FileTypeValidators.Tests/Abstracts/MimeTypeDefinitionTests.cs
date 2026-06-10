
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.FileTypeDefinitions;


namespace Carrigan.FileTypeValidators.Tests.Abstracts;

public class FileTypeDefinitionTests
{
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasAtLeastOne()
    {
        FileTypeDefinition baseClass = new JpegValidator();
        Assert.True(baseClass.AllFileExtensions.Any());
    }
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasFour()
    {
        FileTypeDefinition baseClass = new JpegValidator();
        Assert.Equal(4, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestBmpHasOne()
    {
        FileTypeDefinition baseClass = new BitmapValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestGifHasOne()
    {
        FileTypeDefinition baseClass = new GifValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestTiffHasTwo()
    {
        FileTypeDefinition baseClass = new TiffValidator();
        Assert.Equal(2, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestWebpHasOne()
    {
        FileTypeDefinition baseClass = new WebpValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
}
