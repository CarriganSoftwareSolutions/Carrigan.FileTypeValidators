using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class PngValidatorTests : ValidatorTestBase
{
    protected override FileTypeDefinition ValidatorDefinition =>
        new PngValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A], [0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("png")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "png")];
}
