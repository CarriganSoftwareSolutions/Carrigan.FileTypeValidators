using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

internal class SampleData
{
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


    internal SampleData(byte?[] leadBytes, byte?[] trailerBytes, FileExtension fileExtension)
    {
        LeaderBytes = leadBytes;
        TrailerBytes = trailerBytes;
        FileExtension = fileExtension;
    }

    internal bool HasLeaderBytes() =>
        LeaderBytes.Length > 0;
    internal bool HasTrailerBytes() =>
        TrailerBytes.Length > 0;

    /// <summary>
    /// Generates a byte array that matches the known bytes in the lead and trailer byte arrays, and fills the wild cards with byte values not in signatures.
    /// </summary>
    /// <returns>
    /// A byte array that matches the known bytes in the lead and trailer byte arrays, and fills the wild cards with byte values not in signatures.
    /// </returns>
    internal byte[] ToExact() =>
        [
            .. LeaderBytes.FillNullsWithValues(BytesNotInSignatures()),
            .. TrailerBytes.FillNullsWithValues(BytesNotInSignatures()),
        ];

    /// <summary>
    /// Generates a byte array that matches the known bytes in the lead and trailer byte arrays, and fills the wild cards with byte values not in signatures, 
    /// and includes additional random bytes not in signatures between the lead and trailer bytes.
    /// </summary>
    /// <returns>
    /// A byte array that matches the known bytes in the lead and trailer byte arrays, and fills the wild cards with byte values not in signatures,
    /// and includes additional random bytes not in signatures between the lead and trailer bytes.
    /// </returns>
    internal byte[] ToValid() =>
        [
            .. LeaderBytes.FillNullsWithValues(BytesNotInSignatures()),
            .. CreateFillerBytes().AsEnumerable(),
            .. TrailerBytes.FillNullsWithValues(BytesNotInSignatures()),
        ];

    internal IEnumerable<byte[]> ToInvalidBecauseOfLeaders() =>
        GetInvalidPartials(LeaderBytes)
            .Select
            (
                leader => (byte[])
                [
                    .. leader.FillNullsWithValues(BytesNotInSignatures()),
                    .. TrailerBytes.FillNullsWithValues(BytesNotInSignatures()),
                ]
            );

    internal IEnumerable<byte[]> ToInvalidBecauseOfTrailer() =>
        GetInvalidPartials(TrailerBytes)
            .Select
            (
                trailer => (byte[])
                [
                    .. LeaderBytes.FillNullsWithValues(BytesNotInSignatures()),
                    .. trailer.FillNullsWithValues(BytesNotInSignatures()),
                ]
            );

    internal IEnumerable<byte?[]> GetInvalidPartials(byte?[] signaturePartial)
    {
        Func<byte> cycler = BytesNotInSignatures().CreateCycler();
        IEnumerable<byte?[]> returnValue = [];
        for(int i = 0; i < signaturePartial.Length; i++)
        {
            if (signaturePartial[i] is not null)
            {
                returnValue = returnValue.Append([.. signaturePartial.SwapAt(i, cycler())]);
            }
        }
        return returnValue;
    }

    /// <summary>
    /// Generates a byte array of random length between 10 and 20 bytes inclusive, filled with byte values not in signatures.
    /// </summary>
    /// <returns>
    /// A byte array of random length between 10 and 20 bytes inclusive, filled with byte values not in signatures.
    /// </returns>
    private IEnumerable<byte> CreateFillerBytes()
    {
        IEnumerable<byte> returnValue = [];
        Func<byte> cycler = BytesNotInSignatures().CreateCycler();
        int length = RandomNumberGenerator.GetInt32(10, 21);
        for (int i = 0; i < length; i++)
            returnValue = returnValue.Append(cycler());
        return returnValue;
    }


    /// <summary>
    /// All possible byte values from 0 to 255 inclusive that are not in the sample data signatures.
    /// Used to generate additional test data bytes that does not contain any of the bytes in the signature.
    /// </summary>
    internal byte[] BytesNotInSignatures() =>
        [..AllBytes
                .Where(aByte => DistinctBytes.DoesNotContain(aByte))];

    /// <summary>
    /// All possible byte values that are in the sample data signatures.
    /// </summary>
    internal IEnumerable<byte> DistinctBytes =>
        LeaderBytes
            .AsEnumerable()
            .Concat(TrailerBytes.AsEnumerable())
            .OfType<byte>()
            .Distinct();

    /// <summary>
    /// All possible byte values from 0 to 255 inclusive. Used to generate test data that does not contain any of the bytes in the signature.
    /// </summary>
    private static readonly IEnumerable<byte> AllBytes =
    [.. Enumerable
            .Range(byte.MinValue, byte.MaxValue + 1)
            .Select(value => (byte)value)
    ];

}
