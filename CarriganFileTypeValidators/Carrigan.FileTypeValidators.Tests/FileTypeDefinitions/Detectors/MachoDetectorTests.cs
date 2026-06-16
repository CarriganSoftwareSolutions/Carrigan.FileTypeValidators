using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Detectors;

public class MachoDetectorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new MachoDetector();
    private IEnumerable<FileExtension> FileExtensions = [new("dylib"), new("bundle"), new("o")];

    protected override IEnumerable<SingleSample> Samples =>
    [
        //https://en.wikipedia.org/wiki/Mach-O#Multi-architecture_binaries
        .. FileExtensions.Select(fileExt => new SingleSample([0xFE, 0xED, 0xFA, 0xCE], [], fileExt)),
        .. FileExtensions.Select(fileExt => new SingleSample([0xCE, 0xFA, 0xED, 0xFE], [], fileExt)),
        .. FileExtensions.Select(fileExt => new SingleSample([0xFE, 0xED, 0xFA, 0xCF], [], fileExt)),
        .. FileExtensions.Select(fileExt => new SingleSample([0xCF, 0xFA, 0xED, 0xFE], [], fileExt))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/x-mach-binary")
    ];
}
