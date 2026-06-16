using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators.Signatures;

/// <summary>
/// Represents a signature fragment that checks for a specific sequence of bytes at a given offset in the data.
/// </summary>
public class ByteSignature : ISignatureFragment
{
    /// <summary>
    /// Gets the offset at which to check for the byte sequence.
    /// </summary>
    private int Offset { get; }

    /// <summary>
    /// Gets the sequence of bytes to check for.
    /// </summary>
    private IReadOnlyCollection<byte> Signature { get; }

    /// <summary>
    /// Initializes a new instance of the ByteSignature class with the specified byte sequence and offset.
    /// </summary>
    /// <param name="signature">
    /// The sequence of bytes to check for. Cannot be null or empty.
    /// </param>
    /// <param name="offset">
    /// The offset at which to check for the byte sequence. Must be non-negative.
    /// </param>
    public ByteSignature(IEnumerable<byte> signature, int offset = 0)
    {
        ArgumentNullException.ThrowIfNull(signature);
        ArgumentOutOfRangeException.ThrowIfNegative(offset);

        byte[] signatureArray = [.. signature];
        if (signatureArray.Length == 0)
            throw new ArgumentException("The signature must contain at least one byte.", nameof(signature));

        Offset = offset;
        Signature = signatureArray;
    }

    /// <summary>
    /// Initializes a new instance of the ByteSignature class with the specified byte sequence and offset.
    /// </summary>
    /// <param name="signature">
    /// The sequence of bytes to check for. Cannot be null or empty.
    /// </param>
    /// <param name="offset">
    /// The offset at which to check for the byte sequence. Must be non-negative.
    /// </param>
    public ByteSignature(ReadOnlySpan<byte> signature, int offset = 0) : this(signature.ToArray().AsEnumerable(), offset)
    { }

    /// <summary>
    /// Determines whether the specified data matches the byte signature at the defined offset.
    /// </summary>
    /// <param name="data">The data to check.</param>
    /// <returns>true if the data matches the signature; otherwise, false.</returns>
    public bool IsMatch(IEnumerable<byte> data)
    {
        ArgumentNullException.ThrowIfNull(data);

        return data.Skip(Offset).StartsWith(Signature);
    }
}
