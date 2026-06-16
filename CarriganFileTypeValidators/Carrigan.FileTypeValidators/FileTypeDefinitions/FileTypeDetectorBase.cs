namespace Carrigan.FileTypeValidators.FileTypeDefinitions;

/// <summary>
/// Defines the base behavior for detector definitions that are intended for deny-list use.
/// </summary>
public abstract class FileTypeDetectorBase : FileTypeValidatorBase
{
    /// <summary>
    /// Gets a value indicating whether MIME type checks should be included when this detector is used in allow-list validation.
    /// Detectors are intended for deny-list use, so MIME type checks are ignored if a detector is accidentally used in an allow-list.
    /// </summary>
    protected override bool UseMimeTypeInAllowListChecks => false;
}
