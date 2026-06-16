using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class WindowsExeDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new WindowsExeDetector();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(FromReadOnlySpan("MZ"u8), [], new("exe"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application", "vnd.microsoft.portable-executable"),
        new("application", "x-msdownload")
    ];

    [Fact]
    public void WhiteListIgnoresMimeTypeForDetector()
    {
        bool result = Validator.IsValid("MZ"u8.ToArray(), new MimeType("application", "octet-stream"), new FileExtension("exe"));

        Assert.True(result);
    }
}
