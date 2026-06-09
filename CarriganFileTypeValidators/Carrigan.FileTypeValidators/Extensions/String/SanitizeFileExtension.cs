namespace Carrigan.FileTypeValidators.Extensions.String;

internal static class SanitizeFileExtensionExtension
{
    internal static string SanitizeFileExtension(this string fileExtension) =>
        (fileExtension.StartsWith('.') ? fileExtension[1..] : fileExtension).ToLower();
}
