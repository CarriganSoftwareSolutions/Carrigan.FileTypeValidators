namespace Carrigan.FileTypeValidators.Signatures;

public class FileSignature
{
    private readonly IEnumerable<ISignatureFragment> _signatureFragments;

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

    public bool Validate(IEnumerable<byte> data, FileExtension fileExtension) =>
        _signatureFragments.All(signature => signature.IsMatching(data)) &&
        FileExtensions.Contains(fileExtension);
}
