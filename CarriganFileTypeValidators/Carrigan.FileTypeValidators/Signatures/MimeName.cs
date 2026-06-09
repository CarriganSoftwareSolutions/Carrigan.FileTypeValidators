using Carrigan.Core.DataTypes;
using System.Data;
namespace Carrigan.FileTypeValidators.Signatures;

public class MimeName : StringWrapper
{
    public MimeName(string type, string subtype) : base($"{type}/{subtype}".ToLower(), StringComparison.OrdinalIgnoreCase)
    { }
}
