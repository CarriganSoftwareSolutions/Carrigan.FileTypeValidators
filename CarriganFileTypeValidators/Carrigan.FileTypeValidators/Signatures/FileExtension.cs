using Carrigan.Core.DataTypes;
namespace Carrigan.FileTypeValidators.Signatures;

public class FileExtension : StringWrapper
{
    public FileExtension(string fileExtension) : base(fileExtension, StringComparison.OrdinalIgnoreCase)
    { }
}
