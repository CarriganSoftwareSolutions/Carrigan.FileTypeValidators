using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

public class ByteTrailer : ISignatureFragment
{
    public ByteTrailer(IEnumerable<byte> trailer, int offset)
    {
        Offset = offset;
        Trailer = trailer;
    }
    public ByteTrailer(IEnumerable<byte> trailer)
    {
        Offset = 0;
        Trailer = trailer;
    }
    private int Offset { get; }
    private IEnumerable<byte> Trailer { get; set; }

    public bool IsMatching(IEnumerable<byte> data) =>
        data.SkipLast(Offset).EndsWith(Trailer);
}
