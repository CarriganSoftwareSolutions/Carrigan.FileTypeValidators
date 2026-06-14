
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class TiffValidatorTests : ValidatorTestBase
{
    protected override FileTypeDefinition ValidatorDefinition =>
        new TiffValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([0x49, 0x49, 0x2A, 0x00], null, new("tif")),
        new ([0x49, 0x49, 0x2A, 0x00], null, new("tiff")),

        new ([0x4D, 0x4D, 0x00, 0x2A], null, new("tif")),
        new ([0x4D, 0x4D, 0x00, 0x2A], null, new("tiff")),

        new ([0x49, 0x49, 0x2B, 0x00], null, new("tif")),
        new ([0x49, 0x49, 0x2B, 0x00], null, new("tiff")),

        new ([0x4D, 0x4D, 0x00, 0x2B], null, new("tif")),
        new ([0x4D, 0x4D, 0x00, 0x2B], null, new("tiff")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "tiff")];
}
