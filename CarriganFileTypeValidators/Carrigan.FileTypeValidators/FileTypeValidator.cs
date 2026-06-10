using Carrigan.Core.DataStructures;
using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators;

public class FileTypeValidator
{
    Dictionary<FileExtension, FileTypeDefinition>? Allowed;
    IEnumerable<FileTypeDefinition>? Disallowed;

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
        Disallowed = disallowed;                    
    }

    public bool IsValid(byte[] data, MimeType mimeType, FileExtension fileExtension) =>
        IsAllowed(data, mimeType, fileExtension) && IsDisallowed(data, mimeType, fileExtension) == false;

    private bool IsAllowed(byte[] data, MimeType mimeType, FileExtension fileExtension)
    {
        if (Allowed.IsNullOrEmpty())
            return false;
        else if (Allowed.DoesNotContainKey(fileExtension))
            return false;
        else
            return Allowed[fileExtension].WhiteListMatch(data, mimeType, fileExtension);
    }
    private bool IsDisallowed(byte[] data, MimeType mimeType, FileExtension fileExtension) =>
        Disallowed.IsNotNullOrEmpty() && Disallowed.Any(testable => testable.BlackListMatch(data, mimeType, fileExtension));
}
