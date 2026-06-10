using Carrigan.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrigan.FileTypeValidators.Signatures;

public class MimeType : StringWrapper
{
    public MimeType(string type, string subtype) : base($"{type}/{subtype}", StringComparison.OrdinalIgnoreCase)
    { }
}
