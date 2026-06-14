using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

internal class SampleData
{
    private const string AllowedFileExtensionCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

    private static readonly IEnumerable<byte> AllBytes;

    private static byte[] GetInvalidBytes(IEnumerable<SingleSample> samples)
    {
        ArgumentNullException.ThrowIfNull(samples);

        HashSet<byte> usedBytes =
            [.. samples
                .SelectMany(sample => sample.LeaderBytes.Concat(sample.TrailerBytes))
                .OfType<byte>()];

        return [.. AllBytes.Except(usedBytes)];
    }
    private static char[] GetInvalidFileExtensionChars(IEnumerable<SingleSample> samples)
    {
        ArgumentNullException.ThrowIfNull(samples);

        HashSet<char> usedExtensionCharacters =
            [.. samples.SelectMany(sample => sample.FileExtension.ToString().ToLowerInvariant())];

        return [.. AllowedFileExtensionCharacters.Except(usedExtensionCharacters)];
    }

    static SampleData() => 
        AllBytes = Enumerable
            .Range(byte.MinValue, byte.MaxValue + 1)
            .Select(value => (byte)value);

    internal SampleData(IEnumerable<SingleSample> samples, IEnumerable<MimeType> mimeTypes)
    {
        byte[] invalidBytes = GetInvalidBytes(samples);
        char[] invalidChars = GetInvalidFileExtensionChars(samples);

        Samples = samples.Select(sample => new PrivateSample(sample, invalidBytes, invalidChars));
        MimeTypes = mimeTypes;
    }

    private IEnumerable<PrivateSample> Samples { get; }

    private IEnumerable<MimeType> MimeTypes { get; }

    internal IEnumerable<SampleFileModel> GetExactSamples()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                sampleFiles.Add
                (
                    new()
                    {
                        Bytes = sampleData.ToExact(),
                        MimeType = mime,
                        FileExtension = sampleData.FileExtension
                    }
                );
            };
        }
        return sampleFiles;
    }

    internal IEnumerable<SampleFileModel> GetValidExamples()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                sampleFiles.Add
                (
                    new()
                    {
                        Bytes = sampleData.ToValid(),
                        MimeType = mime,
                        FileExtension = sampleData.FileExtension
                    }
                );
            }
        }
        return sampleFiles;
    }


    internal IEnumerable<SampleFileModel> GetInvalidDueToFileExtension()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sample in Samples)
            {
                sampleFiles.Add
                (
                    new()
                    {
                        Bytes = sample.ToValid(),
                        MimeType = mime,
                        FileExtension = sample.GetInvalidFileExtension()
                    }
                );
                sampleFiles.Add
                (
                    new()
                    {
                        Bytes = sample.ToValid(),
                        MimeType = mime,
                        FileExtension = new(string.Empty)
                    }
                );
            }
            
        }
        return sampleFiles;
    }

    internal IEnumerable<SampleFileModel> GetInvalidsDueToLeader()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                if (sampleData.HasLeaderBytes())
                {
                    foreach (byte[] invalidFileBytes in sampleData.ToInvalidBecauseOfLeaders())
                    {
                        sampleFiles.Add
                        (
                            new()
                            {
                                Bytes = invalidFileBytes,
                                MimeType = mime,
                                FileExtension = sampleData.FileExtension
                            }
                        );
                    }
                }
            }
        }
        return sampleFiles;
    }

    internal IEnumerable<SampleFileModel> GetInvalidsDueToTrailer()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                if (sampleData.HasTrailerBytes())
                {
                    foreach (byte[] invalidFileBytes in sampleData.ToInvalidBecauseOfTrailer())
                    {
                        sampleFiles.Add
                        (
                            new()
                            {
                                Bytes = invalidFileBytes,
                                MimeType = mime,
                                FileExtension = sampleData.FileExtension
                            }
                        );
                    }
                }
            }
        }
        return sampleFiles;
    }



    internal IEnumerable<SampleFileModel> GetInvalidLeaderDueToOffset()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                if (sampleData.HasLeaderBytes())
                {
                    sampleFiles.Add
                    (
                        new()
                        {
                            Bytes = sampleData.ToInvalidDueToLeaderOffset(),
                            MimeType = mime,
                            FileExtension = sampleData.FileExtension
                        }
                    );
                }
            }
        }
        return sampleFiles;
    }

    internal IEnumerable<SampleFileModel> GetInvalidTrailerDueToOffset()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (PrivateSample sampleData in Samples)
            {
                if (sampleData.HasTrailerBytes())
                {
                    sampleFiles.Add
                    (
                        new()
                        {
                            Bytes = sampleData.ToInvalidDueToTrailerOffset(),
                            MimeType = mime,
                            FileExtension = sampleData.FileExtension
                        }
                    );
                }
            }
        }
        return sampleFiles;
    }

    internal IEnumerable<SampleFileModel> GetInvalidDueToEmptyBytes()
    {
        List<SampleFileModel> sampleFiles = [];
        foreach (MimeType mime in MimeTypes)
        {
            foreach (FileExtension fileExtension in Samples.Select(sample => sample.FileExtension).Distinct())
            {
                sampleFiles.Add
                (
                    new()
                    {
                        Bytes = [],
                        MimeType = mime,
                        FileExtension = fileExtension
                    }
                );
            }
        }
        return sampleFiles;
    }

    private class PrivateSample
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

        internal byte[] InvalidingBytes { get; init; }

        /// <summary>
        /// File extension associated with the file signature.
        /// </summary>
        internal FileExtension FileExtension { get; init; }

        internal char[] InvalidatingFileExtensionChars { get; set; }

        internal PrivateSample(SingleSample singleSample, byte[] invalidatingBytes, char[] invalidatingFileExtensionChars
            )
        {
            LeaderBytes = singleSample.LeaderBytes;
            TrailerBytes = singleSample.TrailerBytes;
            FileExtension = singleSample.FileExtension;
            InvalidingBytes = invalidatingBytes;
            InvalidatingFileExtensionChars = invalidatingFileExtensionChars;
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
            .. LeaderBytes.FillNullsWithValues(InvalidingBytes),
            .. TrailerBytes.FillNullsWithValues(InvalidingBytes),
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
            .. LeaderBytes.FillNullsWithValues(InvalidingBytes),
            .. CreateFillerBytes(),
            .. TrailerBytes.FillNullsWithValues(InvalidingBytes),
        ];

        internal byte[] ToInvalidDueToLeaderOffset() =>
        [
            .. LeaderBytes.Prepend(InvalidingBytes.ElementAt(0)).ToArray().FillNullsWithValues(InvalidingBytes),
            .. CreateFillerBytes(),
            .. TrailerBytes.FillNullsWithValues(InvalidingBytes),
        ];

        internal byte[] ToInvalidDueToTrailerOffset() =>
        [
            .. LeaderBytes.FillNullsWithValues(InvalidingBytes),
            .. CreateFillerBytes(),
            .. TrailerBytes.Append(InvalidingBytes.ElementAt(0)).ToArray().FillNullsWithValues(InvalidingBytes),
        ];

        /// <summary>
        /// Generates byte arrays that do not match the known bytes in the lead byte arrays, 
        /// by swapping one known byte in the lead byte arrays with a byte value not in signatures.
        /// This process is done for each known byte in the lead byte array, to generate several invalid datasets that are off by one.
        /// </summary>
        /// <returns>
        /// Byte arrays that do not match the known bytes in the lead byte arrays.
        /// </returns>
        internal IEnumerable<byte[]> ToInvalidBecauseOfLeaders() =>
            GetInvalidPartials(LeaderBytes)
                .Select
                (
                    leader => (byte[])
                    [
                        .. leader.FillNullsWithValues(InvalidingBytes),
                        .. TrailerBytes.FillNullsWithValues(InvalidingBytes),
                    ]
                );

        /// <summary>
        /// Generates byte arrays that do not match the known bytes in the trailer byte arrays, 
        /// by swapping one known byte in the trailer byte arrays with a byte value not in signatures.
        /// This process is done for each known byte in the trailer byte array, to generate several invalid datasets that are off by one.
        /// </summary>
        /// <returns>
        /// Byte arrays that do not match the known bytes in the trailer byte arrays.
        /// </returns>
        internal IEnumerable<byte[]> ToInvalidBecauseOfTrailer() =>
            GetInvalidPartials(TrailerBytes)
                .Select
                (
                    trailer => (byte[])
                    [
                        .. LeaderBytes.FillNullsWithValues(InvalidingBytes),
                        .. trailer.FillNullsWithValues(InvalidingBytes),
                    ]
                );

        /// <summary>
        /// Generates byte arrays that do not match the known bytes in the provided byte array,
        /// by swapping one known byte in the provided byte array with a byte value not in signatures.
        /// This process is done for each known byte in the provided byte array, to generate several invalid datasets that are off by one.
        /// </summary>
        /// <param name="signaturePartial">
        /// A byte array representing either the lead or trailer bytes of the file signature, where nulls represent wild cards.
        /// </param>
        /// <returns>
        /// Byte arrays that do not match the known bytes in the provided byte array.
        /// </returns>
        internal IEnumerable<byte?[]> GetInvalidPartials(byte?[] signaturePartial)
        {
            Func<byte> cycler = InvalidingBytes.CreateCycler();
            IEnumerable<byte?[]> returnValue = [];
            for (int i = 0; i < signaturePartial.Length; i++)
            {
                if (signaturePartial[i] is not null)
                {
                    returnValue = returnValue.Append([.. signaturePartial.ReplaceAt(i, cycler())]);
                }
            }
            return returnValue;
        }

        internal FileExtension GetInvalidFileExtension() =>
            new($"{InvalidatingFileExtensionChars.First()}{FileExtension}{InvalidatingFileExtensionChars.Last()}");


        /// <summary>
        /// Generates a byte array of random length between 10 and 20 bytes inclusive, filled with byte values not in signatures.
        /// </summary>
        /// <returns>
        /// A byte array of random length between 10 and 20 bytes inclusive, filled with byte values not in signatures.
        /// </returns>
        private IEnumerable<byte> CreateFillerBytes()
        {
            IEnumerable<byte> returnValue = [];
            Func<byte> cycler = InvalidingBytes.CreateCycler();
            int length = RandomNumberGenerator.GetInt32(10, 21);
            for (int i = 0; i < length; i++)
                returnValue = returnValue.Append(cycler());
            return returnValue;
        }
    }
}
