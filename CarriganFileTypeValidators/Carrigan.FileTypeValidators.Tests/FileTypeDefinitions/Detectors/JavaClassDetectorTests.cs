using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class JavaClassDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new JavaClassDetector();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new([0xCA, 0xFE, 0xBA, 0xBE], [], new("class"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application", "x-java"),
        new("application", "java"),
        new("application", "x-java-class"),
        new("application", "x-httpd-java"),
        new("application", "java-vm"),
        new("application", "java-byte-code"),
        new("application", "x-java-vm")
    ];

    [Fact]
    public void WhiteListIgnoresMimeTypeForDetector()
    {
        byte[] data = [0xCA, 0xFE, 0xBA, 0xBE];

        bool result = Validator.IsValid(data, new MimeType("application", "octet-stream"), new FileExtension("class"));

        Assert.True(result);
    }
}
