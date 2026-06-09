using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

internal class ByteTrailer
{
    internal ByteTrailer(IEnumerable<byte> trailer, int offset)
    {
        Offset = offset;
        Trailer = trailer;
    }
    internal ByteTrailer(IEnumerable<byte> trailer)
    {
        Offset = 0;
        Trailer = trailer;
    }
    private int Offset { get; }
    private IEnumerable<byte> Trailer { get; set; }

    internal bool IsMatching(IEnumerable<byte> data) =>
        data.SkipLast(Offset).EndsWith(Trailer);
}
