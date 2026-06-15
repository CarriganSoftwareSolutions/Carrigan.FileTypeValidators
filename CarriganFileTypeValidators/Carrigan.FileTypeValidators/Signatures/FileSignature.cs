namespace Carrigan.FileTypeValidators.Signatures;

public class FileSignature
{
    internal readonly IEnumerable<ISignatureFragment> _signatureFragments;

    internal IEnumerable<FileExtension> FileExtensions { get; }

    public FileSignature(ISignatureFragment signatureFragments, FileExtension fileExtension)
    {
        ArgumentNullException.ThrowIfNull(signatureFragments);
        ArgumentNullException.ThrowIfNull(fileExtension);

        _signatureFragments = [signatureFragments];
        FileExtensions = [fileExtension];
    }

    public FileSignature(ISignatureFragment signatureFragments, IEnumerable<FileExtension> fileExtensions)
    {
        ArgumentNullException.ThrowIfNull(signatureFragments);
        ArgumentNullException.ThrowIfNull(fileExtensions);

        FileExtension[] fileExtensionArray = [.. fileExtensions];
        if (fileExtensionArray.Length == 0)
            throw new ArgumentException("At least one file extension must be provided.", nameof(fileExtensions));

        _signatureFragments = [signatureFragments];
        FileExtensions = fileExtensionArray;
    }

    public FileSignature(IEnumerable<ISignatureFragment> signatureFragments, FileExtension fileExtension)
    {
        ArgumentNullException.ThrowIfNull(signatureFragments);
        ArgumentNullException.ThrowIfNull(fileExtension);

        ISignatureFragment[] signatureFragmentArray = [.. signatureFragments];
        if (signatureFragmentArray.Length == 0)
            throw new ArgumentException("At least one signature fragment must be provided.", nameof(signatureFragments));

        _signatureFragments = signatureFragmentArray;
        FileExtensions = [fileExtension];
    }

    public FileSignature(IEnumerable<ISignatureFragment> signatureFragments, IEnumerable<FileExtension> fileExtensions)
    {
        ArgumentNullException.ThrowIfNull(signatureFragments);
        ArgumentNullException.ThrowIfNull(fileExtensions);

        ISignatureFragment[] signatureFragmentArray = [.. signatureFragments];
        if (signatureFragmentArray.Length == 0)
            throw new ArgumentException("At least one signature fragment must be provided.", nameof(signatureFragments));

        FileExtension[] fileExtensionArray = [.. fileExtensions];
        if (fileExtensionArray.Length == 0)
            throw new ArgumentException("At least one file extension must be provided.", nameof(fileExtensions));

        _signatureFragments = signatureFragmentArray;
        FileExtensions = fileExtensionArray;
    }

    public bool WhiteListMatch(IEnumerable<byte> data, FileExtension fileExtension) =>
        _signatureFragments.All(signature => signature.IsMatch(data)) &&
        FileExtensions.Contains(fileExtension);

    public bool BlackListMatch(IEnumerable<byte> data, FileExtension fileExtension) =>
        _signatureFragments.All(signature => signature.IsMatch(data)) ||
        FileExtensions.Contains(fileExtension);
}
