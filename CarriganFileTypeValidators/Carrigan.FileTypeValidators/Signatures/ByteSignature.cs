using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

public class ByteSignature : ISignatureFragment
{
    public ByteSignature(IEnumerable<byte> signature, int offset)
    {
        Offset = offset;
        Signature= signature;
    }

    public ByteSignature(IEnumerable<byte> signature)
    {
        Offset = 0;
        Signature = signature;
    }

    private int Offset { get; }
    private IEnumerable<byte> Signature { get; }

    public bool IsMatch(IEnumerable<byte> data) =>
        data.Skip(Offset).StartsWith(Signature);
}
