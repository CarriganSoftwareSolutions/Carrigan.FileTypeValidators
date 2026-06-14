using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class JpegValidatorTests : ValidatorTestBase
{
    protected override FileTypeDefinition ValidatorDefinition => 
        new JpegValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([0xFF, 0xD8], [0xFF, 0xD9], new("jpe")),
        new ([0xFF, 0xD8], [0xFF, 0xD9], new("jpeg")),
        new ([0xFF, 0xD8], [0xFF, 0xD9], new("jpg")),
        new ([0xFF, 0xD8, 0xFF, 0xE0], [0xFF, 0xD9], new("jfif")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "jpeg"), new("image", "pjpeg")];
}
