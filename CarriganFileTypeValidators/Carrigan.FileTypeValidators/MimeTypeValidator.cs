using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Extensions.String;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators;

public class MimeTypeValidator
{
    private BlackWhiteList<string> _blackWhiteList;

    public MimeTypeValidator(params FileType[] allowedFileTypes) =>
        _blackWhiteList = new BlackWhiteList<string>([.. allowedFileTypes.SelectMany(allowed => GetFileExtensions(allowed)).Distinct()]);

    public MimeTypeValidator(params MimeType[] allowedMimeTypes)
    {
        string[] fileTypes = [.. allowedMimeTypes.SelectMany(allowedType => MimeTypeDefinitionFactory.GetDefinition(allowedType).AllFileExtensions).Distinct()];
        _blackWhiteList = new BlackWhiteList<string>(fileTypes);
    }

    public MimeTypeValidator AddToBlackList(params FileType[] blockedFileTypes)
    {
        _blackWhiteList.AddBlackListValues([.. blockedFileTypes.SelectMany(blocked => GetFileExtensions(blocked)).Distinct()]);
        return this;
    }

    public MimeTypeValidator AddToBlackList(params MimeType[] blockedMimeTypes)
    {
        string[] fileTypes = [.. blockedMimeTypes.SelectMany(blockedType => MimeTypeDefinitionFactory.GetDefinition(blockedType).AllFileExtensions).Distinct()];
        _blackWhiteList.AddBlackListValues(fileTypes);
        return this;
    }

    public bool IsValid(byte[] data, string mimeType, string fileExtension) =>
        _blackWhiteList.IsAllowed(fileExtension.SanitizeFileExtension()) && MimeTypeDefinitionFactory.GetDefinition(mimeType).IsValid(data, fileExtension.SanitizeFileExtension());

    private static IEnumerable<string> GetFileExtensions(FileType fileType) =>
        fileType switch
        {
            FileType.Tiff => [new FileExtension("tif"), new FileExtension("tiff")],
            _ => [fileType.ToString().ToLowerInvariant()]
        };
}
