namespace Carrigan.FileTypeValidators.Abstracts;

public abstract class MimeImageTypeDefinition : MimeTypeDefinition
{
    protected sealed override string Type { get => "Image"; }
}
