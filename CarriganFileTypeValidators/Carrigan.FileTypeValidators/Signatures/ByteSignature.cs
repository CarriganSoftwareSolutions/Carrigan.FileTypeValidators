using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

internal class ByteSignature
{
    internal ByteSignature(IEnumerable<byte> signature, int offset)
    {
        Offset = offset;
        Signature= signature;
    }

    internal ByteSignature(IEnumerable<byte> signature)
    {
        Offset = 0;
        Signature = signature;
    }

    private int Offset { get; }
    private IEnumerable<byte> Signature { get; set; }

    internal bool IsMatching(IEnumerable<byte> data) =>
        data.Skip(Offset).StartsWith(Signature);
}
