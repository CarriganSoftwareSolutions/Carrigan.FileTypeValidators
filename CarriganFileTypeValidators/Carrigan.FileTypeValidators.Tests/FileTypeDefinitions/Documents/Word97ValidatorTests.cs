using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class Word97ValidatorTests : OleCompoundDocumentValidatorTestBase
{
    private static readonly FileExtension[] FileExtensions =
    [
        new("doc"),
        new("dot")
    ];

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new Word97Validator();

    protected override IEnumerable<SingleSample> Samples =>
        FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0xEC, 0xA5, 0xC1, 0x00]),
            [],
            fileExtension));

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/msword")
    ];
}
