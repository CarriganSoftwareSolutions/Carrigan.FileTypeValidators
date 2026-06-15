using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

/// <summary>
/// Represents a signature fragment that checks for a specific sequence of bytes at the end of the data, with an optional offset from the end.
/// </summary>
public class ByteTrailer : ISignatureFragment
{
    /// <summary>
    /// Gets the offset from the end of the data at which to check for the byte sequence.
    /// A value of 0 means the byte sequence must be at the very end of the data.
    /// </summary>
    private int Offset { get; }

    /// <summary>
    /// Gets the sequence of bytes to check for at the end of the data.
    /// </summary>
    private IReadOnlyCollection<byte> Trailer { get; }

    /// <summary>
    /// Initializes a new instance of the ByteTrailer class with the specified byte sequence and offset from the end.
    /// </summary>
    /// <param name="trailer">
    /// The sequence of bytes to check for at the end of the data. Cannot be null or empty.
    /// </param>
    /// <param name="offset">
    /// The offset from the end of the data at which to check for the byte sequence. Must be non-negative.
    /// A value of 0 means the byte sequence must be at the very end of the data.
    /// </param>
    public ByteTrailer(IEnumerable<byte> trailer, int offset)
    {
        ArgumentNullException.ThrowIfNull(trailer);
        ArgumentOutOfRangeException.ThrowIfNegative(offset);

        byte[] trailerArray = [.. trailer];
        if (trailerArray.Length == 0)
            throw new ArgumentException("The trailer must contain at least one byte.", nameof(trailer));

        Offset = offset;
        Trailer = trailerArray;
    }

    /// <summary>
    /// Initializes a new instance of the ByteTrailer class with the specified byte sequence and an offset of 0 from the end.
    /// </summary>
    /// <param name="trailer">
    /// The sequence of bytes to check for at the end of the data. Cannot be null or empty.
    /// </param>
    public ByteTrailer(IEnumerable<byte> trailer) : this(trailer, 0)
    { }

    /// <summary>
    /// Determines whether the specified data matches the byte trailer at the defined offset from the end.
    /// </summary>
    /// <param name="data">The data to check.</param>
    /// <returns>true if the data matches the trailer; otherwise, false.</returns>
    public bool IsMatch(IEnumerable<byte> data)
    {
        ArgumentNullException.ThrowIfNull(data);

        return data.SkipLast(Offset).EndsWith(Trailer);
    }
}
