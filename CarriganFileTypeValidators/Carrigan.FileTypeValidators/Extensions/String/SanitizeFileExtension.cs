using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Extensions.String;

internal static class SanitizeFileExtensionExtension
{
    internal static FileExtension SanitizeFileExtension(this string fileExtension) =>
        new ((fileExtension.StartsWith('.') ? fileExtension[1..] : fileExtension));
}
