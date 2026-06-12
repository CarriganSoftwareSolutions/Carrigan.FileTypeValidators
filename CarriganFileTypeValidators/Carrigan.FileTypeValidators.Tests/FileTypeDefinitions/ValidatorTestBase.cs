using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

public abstract class ValidatorTestBase
{
    protected abstract FileTypeDefinition Validator { get; }
    internal  abstract IEnumerable<SampleData> SampleData { get; }

    protected abstract IEnumerable<MimeType> MimeTypes { get; }

    protected FileTypeValidator validator;

    protected ValidatorTestBase() => 
        validator = new FileTypeValidator([Validator]);

    private bool IsValid(SampleFileModel sampleFileModel) => 
        validator.IsValid([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

    [Fact]
    public void ExactTest()
    {
        SampleFileModel sampleFileModel;
        foreach (MimeType mime in MimeTypes)
        {
            foreach (SampleData sampleData in SampleData)
            {
                sampleFileModel = new()
                {
                    Bytes = sampleData.ToExact(),
                    MimeType = mime,
                    FileExtension = sampleData.FileExtension
                };
                Assert.True(IsValid(sampleFileModel));
            }
        }
    }

    [Fact]
    public void Valid()
    {
        SampleFileModel sampleFileModel;
        foreach (MimeType mime in MimeTypes)
        {
            foreach (SampleData sampleData in SampleData)
            {
                sampleFileModel = new()
                {
                    Bytes = sampleData.ToValid(),
                    MimeType = mime,
                    FileExtension = sampleData.FileExtension
                };
                Assert.True(IsValid(sampleFileModel));
            }
        }
    }

    [Fact]
    public void InvalidLeader()
    {
        SampleFileModel sampleFileModel;
        foreach (MimeType mime in MimeTypes)
        {
            foreach (SampleData sampleData in SampleData)
            {
                if (sampleData.HasLeaderBytes())
                {
                    IEnumerable<byte[]> allInvalids = sampleData.ToInvalidBecauseOfLeaders();
                    foreach (byte[] bytes in sampleData.ToInvalidBecauseOfLeaders())
                    {
                        sampleFileModel = new()
                        {
                            Bytes = bytes,
                            MimeType = mime,
                            FileExtension = sampleData.FileExtension
                        };
                        Assert.False(IsValid(sampleFileModel));
                    }
                }
            }
        }
    }

    [Fact]
    public void InvalidTrailer()
    {
        SampleFileModel sampleFileModel;
        foreach (MimeType mime in MimeTypes)
        {
            foreach (SampleData sampleData in SampleData)
            {
                if (sampleData.HasTrailerBytes())
                {
                    foreach (byte[] bytes in sampleData.ToInvalidBecauseOfTrailer())
                    {
                        sampleFileModel = new()
                        {
                            Bytes = bytes,
                            MimeType = mime,
                            FileExtension = sampleData.FileExtension
                        };
                        Assert.False(IsValid(sampleFileModel));
                    }
                }
            }
        }
    }

    //[Fact]
    //public void Exact_Plus_Extra_True()
    //{
    //    JpegValidator definition = new();
    //    Assert.True(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
    //}

    //[Fact]
    //public void Exact_Plus_Extra_At_End_False()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE8, 0xFF, 0xD9, 0x00], new("image", "jpeg"), new FileExtension("jpg")));
    //}

    //[Fact]
    //public void Exact_Plus_Extra_At_Start_False()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0x00, 0xFF, 0xD8, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpe")));
    //}

    //[Fact]
    //public void To_Small_Sig()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jfif")));
    //}

    //[Fact]
    //public void To_Small_Footer()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xD9], new("image", "jpeg"), new FileExtension("jfif")));
    //}


    //[Fact]
    //public void Wrong_Extension()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jtt")));
    //}
    //[Fact]
    //public void No_Extension()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("")));
    //}
    //[Fact]
    //public void Empty()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Empty_Header()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Empty_Footer()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE8], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Bad_Value_Header()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xF0, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Bad_Value_Header2()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0x08, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Bad_Value_Footer1()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new("image", "jpeg"), new FileExtension("jpg")));
    //}
    //[Fact]
    //public void Bad_Value_Footer2()
    //{
    //    JpegValidator definition = new();
    //    Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new("image", "jpeg"), new FileExtension("jpg")));
    //}
}
