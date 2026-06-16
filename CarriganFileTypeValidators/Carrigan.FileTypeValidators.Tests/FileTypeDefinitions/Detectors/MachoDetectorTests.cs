using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class MachoDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new MachoDetector();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new([0xFE, 0xED, 0xFA], [], new("dylib")),
        new([0xFE, 0xED, 0xFA], [], new("macho")),
        new([0xFA, 0xED, 0xFE], [], new("dylib")),
        new([0xFE, 0xED, 0xFA], [], new("macho")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application", "octet-stream")
    ];
}
