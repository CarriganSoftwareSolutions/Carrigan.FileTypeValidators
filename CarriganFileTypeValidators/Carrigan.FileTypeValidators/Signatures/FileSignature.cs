using Carrigan.FileTypeValidators.Signatures;
using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

internal class FileSignature
{
    private readonly IEnumerable<ByteSignature> _signatures;
    private readonly IEnumerable<ByteTrailer> _trailers;

    internal IEnumerable<string> FileExtensions { get; }

    internal FileSignature(ByteSignature signature, string fileExtension)
    {
        _signatures = [signature];
        _trailers = [];
        FileExtensions = [fileExtension];
    }
    internal FileSignature(ByteSignature signature, string[] fileExtensions)
    {
        _signatures = [signature];
        _trailers = [];
        FileExtensions = fileExtensions;
    }

    internal FileSignature(ByteSignature signature, ByteTrailer trailer, string fileExtension)
    {
        _signatures = [signature];
        _trailers = [trailer];
        FileExtensions = [fileExtension];
    }

    internal FileSignature(ByteSignature signature, ByteTrailer trailer, string[] fileExtension)
    {
        _signatures = [signature];
        _trailers = [trailer];
        FileExtensions = fileExtension;
    }

    internal bool Validate(IEnumerable<byte> data, string extension) =>
        (_signatures.IsNullOrEmpty() || _signatures.Where(signature => signature.IsMatching(data)).Any()) &&
        (_trailers.IsNullOrEmpty() || _trailers.Where(trailer => trailer.IsMatching(data)).Any()) &&
        (FileExtensions.IsNullOrEmpty() || FileExtensions.Where(fileExtension => fileExtension == extension).Any());
}