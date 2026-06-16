using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class GifValidatorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new GifValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new([0x47, 0x49, 0x46, 0x38, 0x37, 0x61],  [0x00, 0x3B], new ("gif")),
        new([0x47, 0x49, 0x46, 0x38, 0x39, 0x61],  [0x00, 0x3B], new ("gif"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "gif")];

}
