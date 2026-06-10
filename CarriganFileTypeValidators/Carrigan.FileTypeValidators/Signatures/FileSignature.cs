namespace Carrigan.FileTypeValidators.Signatures;

public class FileSignature
{
    internal readonly IEnumerable<ISignatureFragment> _signatureFragments;

    internal IEnumerable<FileExtension> FileExtensions { get; }

    public FileSignature(ISignatureFragment signatureFragments, FileExtension fileExtension)
    {
        _signatureFragments = [signatureFragments];
        FileExtensions = [fileExtension];
    }
    public FileSignature(ISignatureFragment signatureFragments, IEnumerable<FileExtension> fileExtensions)
    {
        _signatureFragments = [signatureFragments];
        FileExtensions = fileExtensions;
    }

    public FileSignature(IEnumerable<ISignatureFragment> signatureFragments, FileExtension fileExtension)
    {
        _signatureFragments = signatureFragments;
        FileExtensions = [fileExtension];
    }

    public FileSignature(IEnumerable<ISignatureFragment> signatureFragments, IEnumerable<FileExtension> fileExtensions)
    {
        _signatureFragments = signatureFragments;
        FileExtensions = fileExtensions;
    }

    public bool WhiteListMatch(IEnumerable<byte> data, FileExtension fileExtension) =>
        _signatureFragments.All(signature => signature.IsMatch(data)) &&
        FileExtensions.Contains(fileExtension);

    public bool BlackListMatch(IEnumerable<byte> data, FileExtension fileExtension) =>
        _signatureFragments
            .Any(signature => signature.IsMatch(data)) || FileExtensions.Contains(fileExtension);
}
