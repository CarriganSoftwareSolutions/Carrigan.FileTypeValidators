using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrigan.FileTypeValidators.Signatures;

public interface ISignatureFragment
{
    bool IsMatch(IEnumerable<byte> data);
}
