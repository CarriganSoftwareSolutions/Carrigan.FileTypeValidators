using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class ElfDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new ElfDetector();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([0x7F, 0x45, 0x4C, 0x46], [], new("elf"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application", "x-elf")
    ];

    [Fact]
    public void WhiteListIgnoresMimeTypeForDetector()
    {
        byte[] data = [0x7F, 0x45, 0x4C, 0x46];

        bool result = Validator.IsValid(data, new MimeType("application", "octet-stream"), new FileExtension("elf"));

        Assert.True(result);
    }
}
