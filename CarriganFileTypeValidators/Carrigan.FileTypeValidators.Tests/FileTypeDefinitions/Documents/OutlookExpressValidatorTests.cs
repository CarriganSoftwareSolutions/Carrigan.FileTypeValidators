using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class OutlookExpressValidatorTests : DocumentValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new OutlookExpressValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(FromReadOnlySpan("From   "u8), [], new("eml")),
        new(FromReadOnlySpan("From ???"u8), [], new("eml")),
        new(FromReadOnlySpan("From: "u8), [], new("eml"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("message/rfc822")
    ];
}
