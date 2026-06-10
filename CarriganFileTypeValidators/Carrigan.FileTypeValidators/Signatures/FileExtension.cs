using Carrigan.Core.DataTypes;
namespace Carrigan.FileTypeValidators.Signatures;

public class FileExtension : StringWrapper
{
    public FileExtension(string fileExtension) : base(SanitizeFileExtension(fileExtension), StringComparison.OrdinalIgnoreCase)
    { }
    private static string SanitizeFileExtension(string fileExtension) =>
        fileExtension.StartsWith('.') ? fileExtension[1..] : fileExtension;
}
