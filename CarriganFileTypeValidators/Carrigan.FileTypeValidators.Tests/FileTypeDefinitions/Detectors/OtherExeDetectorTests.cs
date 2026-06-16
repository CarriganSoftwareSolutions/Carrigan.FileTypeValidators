using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class OtherExeDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new OtherExeDetector();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new([0xFF], [], new("sys")),
        new([0xE8], [], new("com")),
        new([0xE8], [], new("sys")),
        new([0xE9], [], new("com")),
        new([0xE9], [], new("sys")),
        new([0xEB], [], new("com")),
        new([0xEB], [], new("sys")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application", "octet-stream")
    ];
}
