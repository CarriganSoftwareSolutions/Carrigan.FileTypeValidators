using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions;

/// <summary>
/// Defines the base behavior for file type validators that can be used in allow-list and deny-list checks.
/// </summary>
public abstract class FileTypeValidatorBase
{
    /// <summary>
    /// Gets the MIME types associated with this file type definition.
    /// </summary>
    public abstract IEnumerable<MimeType> MimeTypes { get; }

    /// <summary>
    /// Gets the file signatures associated with this file type definition.
    /// </summary>
    public abstract IEnumerable<FileSignature> Signatures { get; }

    /// <summary>
    /// Gets all file extensions associated with this file type definition.
    /// </summary>
    public IEnumerable<FileExtension> AllFileExtensions =>
        Signatures.SelectMany(signature => signature.FileExtensions).Distinct();

    /// <summary>
    /// Gets a value indicating whether MIME type checks should be included when this definition is used in allow-list validation.
    /// </summary>
    protected virtual bool UseMimeTypeInAllowListChecks => true;

    /// <summary>
    /// Determines whether the supplied file data, MIME type, and extension match this definition for allow-list validation.
    /// </summary>
    /// <param name="data">
    /// The file bytes to validate.
    /// </param>
    /// <param name="mimeType">
    /// The caller-provided MIME type.
    /// </param>
    /// <param name="fileExtension">
    /// The caller-provided file extension.
    /// </param>
    /// <returns>
    /// True if this definition allows the supplied file; otherwise, false.
    /// </returns>
    public bool WhiteListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(mimeType);
        ArgumentNullException.ThrowIfNull(fileExtension);

        if (MimeTypes.IsNotNullOrEmpty() && UseMimeTypeInAllowListChecks)
            return Signatures.Any(signature => signature.WhiteListMatch(data, fileExtension)) &&
                MimeTypes.Contains(mimeType);
        else
            return Signatures.Any(signature => signature.WhiteListMatch(data, fileExtension));
    }

    /// <summary>
    /// Determines whether the supplied file data, MIME type, or extension match this definition for deny-list validation.
    /// </summary>
    /// <param name="data">
    /// The file bytes to validate.
    /// </param>
    /// <param name="mimeType">
    /// The caller-provided MIME type.
    /// </param>
    /// <param name="fileExtension">
    /// The caller-provided file extension.
    /// </param>
    /// <returns>
    /// True if this definition denies the supplied file; otherwise, false.
    /// </returns>
    public bool BlackListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(mimeType);
        ArgumentNullException.ThrowIfNull(fileExtension);

        if (MimeTypes.IsNotNullOrEmpty())
            return MimeTypes.Contains(mimeType) ||
                Signatures.Any(signature => signature.BlackListMatch(data, fileExtension));
        else
            return Signatures.Any(signature => signature.BlackListMatch(data, fileExtension));
    }
}
