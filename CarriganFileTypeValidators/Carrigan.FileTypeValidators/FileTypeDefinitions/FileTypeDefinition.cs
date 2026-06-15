
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions;

/// <summary>
/// The FileTypeDefinition class serves as an abstract base class for defining specific file types based on their associated MIME types and file signatures.
/// </summary>
public abstract class FileTypeDefinition
{
    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with the specific file type definition.
    /// </summary>
    public abstract IEnumerable<MimeType> MimeTypes { get; }
    /// <summary>
    /// The Signatures property returns the collection of file signatures that are used to identify files of the specific type based on their byte patterns.
    /// </summary>
    public abstract IEnumerable<FileSignature> Signatures { get; }
    /// <summary>
    /// The WhiteListMatch method checks if the provided file data, MIME type, and file extension match any of the defined MIME types and file
    /// signatures for the specific file type definition.
    /// </summary>
    /// <param name="data">
    /// The data parameter represents the byte array of the file being validated. 
    /// It is used to compare against the defined file signatures to determine if there is a match.
    /// </param>
    /// <param name="mimeType">
    /// The mimeType parameter represents the MIME type of the file being validated.
    /// </param>
    /// <param name="fileExtension">
    /// The fileExtension parameter represents the file extension of the file being validated.
    /// </param>
    /// <returns>
    /// The method returns a boolean value indicating whether the provided data, MIME type, and file extension match any of the defined MIME types 
    /// and file signatures for the specific file type definition.
    /// </returns>
    public virtual bool WhiteListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension) =>
        MimeTypes.Contains(mimeType) && Signatures
            .Where(signature => signature.WhiteListMatch(data, fileExtension))
            .Any();

    /// <summary>
    /// The BlackListMatch method checks if the provided file data, MIME type, and file extension match any of the defined MIME types and file
    /// signatures for the specific file type definition, but in a way that indicates a potential mismatch or invalid file type.
    /// </summary>
    /// <param name="data">
    /// The data parameter represents the byte array of the file being validated.
    /// </param>
    /// <param name="mimeType">
    /// The mimeType parameter represents the MIME type of the file being validated.
    /// </param>
    /// <param name="fileExtension">
    /// The fileExtension parameter represents the file extension of the file being validated.
    /// </param>
    /// <returns>
    /// The method returns a boolean value indicating whether the provided data, MIME type, and file extension match any of the defined MIME types 
    /// and file signatures for the specific file type definition.
    /// </returns>
    public virtual bool BlackListMatch(IEnumerable<byte> data, MimeType mimeType, FileExtension fileExtension) =>
        MimeTypes.Contains(mimeType) || Signatures
            .Where(signature => signature.BlackListMatch(data, fileExtension))
            .Any();

    /// <summary>
    /// The AllFileExtensions property returns a distinct collection of all file extensions associated with the file signatures defined in the Signatures property.
    /// </summary>
    public IEnumerable<FileExtension> AllFileExtensions =>
         Signatures
            .SelectMany(fileSignature => fileSignature.FileExtensions)
            .Distinct();
}
