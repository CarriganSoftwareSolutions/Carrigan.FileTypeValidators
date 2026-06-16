using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

public class SingleSample
{
    protected static byte?[]? FromReadOnlySpan(ReadOnlySpan<byte> bytes) => 
        [.. bytes.ToArray().AsEnumerable().Select(aByte => (byte?)aByte)];

    /// <summary>
    /// LeadBytes represent the known bytes in the leading bytes file signature.
    /// Nulls represent wild cards in the signature.
    /// </summary>
    internal byte?[] LeaderBytes { get; init; }
    /// <summary>
    /// TrailerBytes represent the known bytes in the trailing bytes file signature.
    /// Nulls represent wild cards in the signature.
    /// </summary>
    internal byte?[] TrailerBytes { get; init; }
    /// <summary>
    /// File extension associated with the file signature.
    /// </summary>
    internal FileExtension FileExtension { get; init; }


    internal SingleSample(byte?[]? leadBytes, byte?[]? trailerBytes, FileExtension fileExtension)
    {
        LeaderBytes = leadBytes ?? [];
        TrailerBytes = trailerBytes ?? [];
        FileExtension = fileExtension;
    }
}
