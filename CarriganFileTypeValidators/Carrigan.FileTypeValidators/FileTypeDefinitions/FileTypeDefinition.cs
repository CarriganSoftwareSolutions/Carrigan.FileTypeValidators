
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions;

public abstract class FileTypeDefinition
{
    public abstract IEnumerable<MimeType> MimeTypes { get; }
    public abstract IEnumerable<FileSignature> Signatures { get; }
    public virtual bool WhiteListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension) =>
        MimeTypes.Contains(mimeType) && Signatures
            .Where(signature => signature.WhiteListMatch(data, fileExtension))
            .Any();
    public virtual bool BlackListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension) =>
        MimeTypes.Contains(mimeType) || Signatures
            .Where(signature => signature.BlackListMatch(data, fileExtension))
            .Any();
    public IEnumerable<FileExtension> AllFileExtensions =>
         Signatures
            .SelectMany(fileSignature => fileSignature.FileExtensions)
            .Distinct();
}
