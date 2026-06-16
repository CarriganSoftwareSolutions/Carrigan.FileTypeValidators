
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
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

    [Fact]
    public void AllFileExtensionsLogicTestWord97HasTwo()
    {
        FileTypeValidatorBase baseClass = new Word97Validator();
        Assert.Equal(2, baseClass.AllFileExtensions.Count());
    }

    [Fact]
    public void AllFileExtensionsLogicTestExcel97HasThree()
    {
        FileTypeValidatorBase baseClass = new Excel97Validator();
        Assert.Equal(3, baseClass.AllFileExtensions.Count());
    }

    [Fact]
    public void AllFileExtensionsLogicTestPowerPoint97HasFour()
    {
        FileTypeValidatorBase baseClass = new PowerPoint97Validator();
        Assert.Equal(4, baseClass.AllFileExtensions.Count());
    }

    [Fact]
    public void AllFileExtensionsLogicTestOutlookExpressHasOne()
    {
        FileTypeValidatorBase baseClass = new OutlookExpressValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }

    [Fact]
    public void AllFileExtensionsLogicTestOutlookHasOne()
    {
        FileTypeValidatorBase baseClass = new OutlookValidator();
        Assert.Single(baseClass.AllFileExtensions);
    }

    [Fact]
    public void AllFileExtensionsLogicTestPublisher97HasOne()
    {
        FileTypeValidatorBase baseClass = new Publisher97Validator();
        Assert.Single(baseClass.AllFileExtensions);
    }
}
