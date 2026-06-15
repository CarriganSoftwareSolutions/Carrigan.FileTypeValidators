using Carrigan.Core.DataTypes;

namespace Carrigan.FileTypeValidators.Signatures;

public class MimeType : StringWrapper
{
    public MimeType(string type, string subtype) : base(Combine(type, subtype), StringComparison.OrdinalIgnoreCase)
    { }

    public MimeType(string mimeType) : base(Validate(mimeType), StringComparison.OrdinalIgnoreCase)
    { }

    private static string Combine(string type, string subtype)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(subtype);

        return $"{type}/{subtype}";
    }

    private static string Validate(string mimeType)
    {
        ArgumentNullException.ThrowIfNull(mimeType);

        return mimeType;
    }
}
