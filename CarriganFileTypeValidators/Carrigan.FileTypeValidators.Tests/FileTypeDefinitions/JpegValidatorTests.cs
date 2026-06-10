//using Carrigan.FileTypeValidators.FileTypeDefinitions;
//using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
//using Carrigan.FileTypeValidators.Signatures;



//namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

////IGNORE SPELLING: Jpeg Jfif Jpg jtt jpe

//public abstract class FileValidatorBaseTest
//{
//    public abstract FileTypeDefinition Validator { get; }
//    public abstract IEnumerable<FileSignature> FileSignatures { get; }
//    public abstract IEnumerable<MimeType> MimeTypes { get; }
//    public static JpegValidator validator = new ();

//    [Fact]
//    public void ExactTest()
//    {
//        SampleFileModel sampleFileModel
//        foreach (FileSignature fileSignature in FileSignatures)
//        {
//            sampleFileModel = new(fileSignature.si)
//        }
//    }
//    [Fact]
//    public void ExactTest2()
//    {
//        Assert.True(validator.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jfif")));
//    }
//    [Fact]
//    public void ExactTest3()
//    {
//        JpegValidator definition = new();
//        Assert.True(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE8, 0xFF, 0xD9], new("IMAGE", "JPEG"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void ExactTest4()
//    {
//        JpegValidator definition = new();
//        Assert.True(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void ExactTest5()
//    {
//        JpegValidator definition = new();
//        Assert.True(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpe")));
//    }

//    [Fact]
//    public void Exact_Plus_Extra_True()
//    {
//        JpegValidator definition = new();
//        Assert.True(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
//    }

//    [Fact]
//    public void Exact_Plus_Extra_At_End_False()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE8, 0xFF, 0xD9, 0x00], new("image", "jpeg"), new FileExtension("jpg")));
//    }

//    [Fact]
//    public void Exact_Plus_Extra_At_Start_False()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0x00, 0xFF, 0xD8, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpe")));
//    }

//    [Fact]
//    public void To_Small_Sig()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jfif")));
//    }

//    [Fact]
//    public void To_Small_Footer()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xD9], new("image", "jpeg"), new FileExtension("jfif")));
//    }


//    [Fact]
//    public void Wrong_Extension()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jtt")));
//    }
//    [Fact]
//    public void No_Extension()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("")));
//    }
//    [Fact]
//    public void Empty()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Empty_Header()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Empty_Footer()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE8], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Bad_Value_Header()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xF0, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Bad_Value_Header2()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0x08, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Bad_Value_Footer1()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//    [Fact]
//    public void Bad_Value_Footer2()
//    {
//        JpegValidator definition = new();
//        Assert.False(definition.WhiteListMatch([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new("image", "jpeg"), new FileExtension("jpg")));
//    }
//}
