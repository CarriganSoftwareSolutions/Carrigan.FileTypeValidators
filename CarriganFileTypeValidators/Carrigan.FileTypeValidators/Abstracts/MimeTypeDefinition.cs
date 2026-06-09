using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Abstracts;

public abstract class MimeTypeDefinition
{
    protected abstract string Type { get; }
    protected abstract string Subtype { get; }
    internal abstract MimeType MimeTypeEnum { get; }
    internal abstract IEnumerable<FileType> FileTypeEnums { get; }
    public string MimeType => $"{ Type }/{ Subtype }".ToLower(); 
    internal abstract IEnumerable<FileSignature> Signatures { get; }
    internal virtual bool IsValid(IEnumerable<byte> data, string fileExtension) =>
        Signatures.Where(signature => signature.Validate(data, fileExtension)).Any();
    internal IEnumerable<string> AllFileExtensions =>
         Signatures
            .SelectMany(fileSignature => fileSignature.FileExtensions)
            .Distinct();
}
