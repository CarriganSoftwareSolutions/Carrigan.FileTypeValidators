using Carrigan.Core.DataStructures;
using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators;

/// <summary>
/// Provides functionality to validate file types based on allowed and disallowed file type definitions.
/// </summary>
public class FileTypeValidator
{
    /// <summary>
    /// Gets the collection of allowed file type definitions, indexed by their associated file extensions.
    /// If null or empty, no file types are considered allowed.
    /// </summary>
    private readonly Dictionary<FileExtension, FileTypeDefinition>? Allowed;

    /// <summary>
    /// Gets the collection of disallowed file type definitions. If null or empty, no file types are considered disallowed.
    /// </summary>
    private readonly IEnumerable<FileTypeDefinition>? Disallowed;

    /// <summary>
    /// Initializes a new instance of the FileTypeValidator class with the specified allowed and disallowed file type definitions.
    /// </summary>
    /// <param name="allowed">
    /// The collection of allowed file type definitions. Each file type definition's associated file extensions will be used as keys
    /// in the internal dictionary for quick lookup.
    /// </param>
    /// <param name="disallowed">
    /// The collection of disallowed file type definitions. If null or empty, no file types are considered disallowed.
    /// </param>
    public FileTypeValidator(IEnumerable<FileTypeDefinition>? allowed = null, IEnumerable<FileTypeDefinition>? disallowed = null)
    { 
        if (allowed.IsNotNullOrEmpty())
            Allowed = allowed
                .SelectMany
                (
                    allowable => allowable
                        .AllFileExtensions
                        .Select(fileExtension => new KeyValuePair<FileExtension, FileTypeDefinition>(fileExtension, allowable))
                )
                .ToDictionary();
        else
            Allowed = null;
        Disallowed = disallowed?.ToArray();
    }

    /// <summary>
    /// Determines whether the specified data, MIME type, and file extension combination is valid based on the allowed and disallowed file type definitions.
    /// </summary>
    /// <param name="data">
    /// The byte array representing the file data to be validated. 
    /// </param>
    /// <param name="mimeType">
    /// The MIME type associated with the file data. This can be used in conjunction with the file extension to determine if the file type is valid according to the defined rules.
    /// </param>
    /// <param name="fileExtension">
    /// The file extension associated with the file data.
    /// This will be used to look up the corresponding file type definition in the allowed and disallowed collections.
    /// </param>
    /// <returns>
    /// True if the file type is valid according to the defined rules; otherwise, false.
    /// </returns>
    public bool IsValid(byte[] data, MimeType mimeType, FileExtension fileExtension)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(mimeType);
        ArgumentNullException.ThrowIfNull(fileExtension);

        return IsAllowed(data, mimeType, fileExtension) && IsDisallowed(data, mimeType, fileExtension) == false;
    }

    /// <summary>
    /// Determines whether the specified data, MIME type, and file extension combination is allowed based on the allowed file type definitions.
    /// </summary>
    /// <param name="data">
    /// The byte array representing the file data to be validated.
    /// </param>
    /// <param name="mimeType">
    /// The MIME type associated with the file data. This can be used in conjunction with the file extension to determine if the file type is allowed.
    /// </param>
    /// <param name="fileExtension">
    /// The file extension associated with the file data. This will be used to look up the corresponding file type definition in the allowed collection.
    /// </param>
    /// <returns>
    /// True if the file type is allowed according to the defined rules; otherwise, false.
    /// </returns>
    private bool IsAllowed(byte[] data, MimeType mimeType, FileExtension fileExtension)
    {
        if (Allowed.IsNullOrEmpty())
            return false;
        else if (Allowed.DoesNotContainKey(fileExtension))
            return false;
        else
            return Allowed[fileExtension].WhiteListMatch(data, mimeType, fileExtension);
    }

    /// <summary>
    /// Determines whether the specified data, MIME type, and file extension combination is disallowed based on the disallowed file type definitions.
    /// </summary>
    /// <param name="data">
    /// The byte array representing the file data to be validated.
    /// </param>
    /// <param name="mimeType">
    /// The MIME type associated with the file data. This can be used in conjunction with the file extension to determine if the file type is disallowed.
    /// </param>
    /// <param name="fileExtension">
    /// The file extension associated with the file data. This will be used to look up the corresponding file type definition in the disallowed collection.
    /// </param>
    /// <returns>
    /// True if the file type is disallowed according to the defined rules; otherwise, false.
    /// </returns>
    private bool IsDisallowed(byte[] data, MimeType mimeType, FileExtension fileExtension) =>
        Disallowed.IsNotNullOrEmpty() && Disallowed.Any(testable => testable.BlackListMatch(data, mimeType, fileExtension));
}
