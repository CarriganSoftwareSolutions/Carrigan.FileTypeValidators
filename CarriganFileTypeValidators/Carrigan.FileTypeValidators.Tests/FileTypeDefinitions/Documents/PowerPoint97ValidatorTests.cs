using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class PowerPoint97ValidatorTests : OleCompoundDocumentValidatorTestBase
{
    private static readonly FileExtension[] FileExtensions =
    [
        new("ppt"),
        new("pot"),
        new("pps"),
        new("ppa")
    ];

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new PowerPoint97Validator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        .. FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0xFD, 0xFF, 0xFF, 0xFF, null, null, 0x00, 0x00]),
            [],
            fileExtension)),
        .. FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0x00, 0x6E, 0x1E, 0xF0]),
            [],
            fileExtension)),
        .. FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0x0F, 0x00, 0xE8, 0x03]),
            [],
            fileExtension)),
        .. FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0xA0, 0x46, 0x1D, 0xF0]),
            [],
            fileExtension))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.ms-powerpoint")
    ];
}
