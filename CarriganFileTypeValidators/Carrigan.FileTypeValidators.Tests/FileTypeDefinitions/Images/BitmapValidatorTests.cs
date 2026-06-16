using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class BitmapValidatorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new BitmapValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([0x42, 0x4D], null, new("bmp")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "bmp")];
}
