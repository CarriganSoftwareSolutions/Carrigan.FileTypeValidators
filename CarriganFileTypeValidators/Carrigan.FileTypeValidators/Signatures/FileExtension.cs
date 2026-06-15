using Carrigan.Core.DataTypes;

namespace Carrigan.FileTypeValidators.Signatures;

/// <summary>
/// Represents a file extension signature fragment that checks for a specific file extension in a case-insensitive manner.
/// </summary>
public class FileExtension : StringWrapper
{
    /// <summary>
    /// Initializes a new instance of the FileExtension class with the specified file extension string.
    /// </summary>
    /// <param name="fileExtension">
    /// The file extension string to check for. Cannot be null. The leading dot will be removed if it exists, ensuring that the file
    /// extension is stored in a consistent format for comparison.
    /// </param>
    public FileExtension(string fileExtension) : base(SanitizeFileExtension(fileExtension), StringComparison.OrdinalIgnoreCase)
    { }

    /// <summary>
    /// Removes the leading dot from the file extension if it exists, ensuring that the file extension is stored in a consistent format for comparison.
    /// </summary>
    /// <param name="fileExtension">
    /// The file extension string to sanitize. Cannot be null.
    /// </param>
    /// <returns>
    /// The sanitized file extension string.
    /// </returns>
    private static string SanitizeFileExtension(string fileExtension)
    {
        ArgumentNullException.ThrowIfNull(fileExtension);

        return fileExtension.StartsWith('.') ? fileExtension[1..] : fileExtension;
    }
}
