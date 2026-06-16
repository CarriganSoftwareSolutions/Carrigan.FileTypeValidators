
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.FileTypeDefinitions;


namespace Carrigan.FileTypeValidators.Tests.Abstracts;

public class FileTypeDefinitionTests
{
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasAtLeastOne()
    {
        FileTypeValidatorBase baseClass = new JpegValidator();
        Assert.True(baseClass.AllFileExtensions.Any());
    }
    [Fact]
    public void AllFileExtensionsLogicTestJpegHasFour()
    {
        FileTypeValidatorBase baseClass = new JpegValidator();
        Assert.Equal(4, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestBmpHasOne()
    {
        FileTypeValidatorBase baseClass = new BitmapValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestGifHasOne()
    {
        FileTypeValidatorBase baseClass = new GifValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
    [Fact]
    public void AllFileExtensionsLogicTestTiffHasTwo()
    {
        FileTypeValidatorBase baseClass = new TiffValidator();
        Assert.Equal(2, baseClass.AllFileExtensions.Count());
    }
    [Fact]
    public void AllFileExtensionsLogicTestWebpHasOne()
    {
        FileTypeValidatorBase baseClass = new WebpValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }
}
