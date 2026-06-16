using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class WebpValidatorTests : ValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new WebpValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new ([.. FromReadOnlySpan("RIFF"u8), .. XNulls(4), .. FromReadOnlySpan("WEBP"u8)], null, new("webp")),
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
         [new("image", "webp")];

}
