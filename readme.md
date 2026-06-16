<a id="carrigan.filetypevalidators"></a>

# Carrigan.FileTypeValidators

Carrigan.FileTypeValidators validates files by comparing the caller-provided file extension and MIME type against known file type definitions, then confirming that the file bytes match the expected signature.

The library is intended for allow-list and deny-list validation. It is not antivirus software, malware scanning, content moderation, or a full file parser.

---

## Table of Contents

- [Installation](#installation)
- [Supported Built-In Validators](#supported-built-in-validators)
- [Basic Usage](#basic-usage)
- [Allow List and Deny List Validation](#allow-list-and-deny-list-validation)
- [Creating Custom File Type Validators](#creating-custom-file-type-validators)
- [ASP.NET Core Upload Example](#aspnet-core-upload-example)
- [Important Behavior Notes](#important-behavior-notes)
- [Signature Sources](#signature-sources)
- [License](#license)

---

## Installation

```powershell
dotnet add package Carrigan.FileTypeValidators
```

[Table of Contents](#table-of-contents)

---

## Supported Built-In Validators

Carrigan.FileTypeValidators currently includes built-in validators for common image file formats:

- BMP
- GIF
- JPEG / JPG / JPE / JFIF
- PNG
- TIFF / TIF, including BigTIFF signatures
- WebP

It also includes built-in validators for legacy document formats:

- Word 97-2003 / DOC / DOT
- Excel 97-2003 / XLS / XLT / XLA
- PowerPoint 97-2003 / PPT / POT / PPS / PPA
- Outlook MSG
- Outlook Express / EML
- Publisher 97 / PUB

It also includes detector definitions intended for deny-list scenarios:

- Windows executable-style files
- ELF files
- Mach-O files
- Java class files
- Additional COM/SYS executable-style byte patterns

Detector definitions inherit from `FileTypeDetectorBase`. They are intended for deny-list use; MIME type checks are ignored if a detector is accidentally used as an allow-list validator.

Note: Do not rely solely on blacklists or deny-lists. Always prioritize allow-lists as the primary validation mechanism, using blacklists only as a fallback or supplemental safeguard. Blacklists are inherently unreliable, but they are included here as an additional layer of defense.

[Table of Contents](#table-of-contents)

---

## Basic Usage

Create a `FileTypeValidator` with the file types that should be allowed. Then pass the file bytes, MIME type, and file extension to `IsValid`.

```csharp
using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

FileTypeValidator validator = new(
    allowed:
    [
        new PngValidator(),
        new JpegValidator(),
        new GifValidator()
    ]);

byte[] data = await File.ReadAllBytesAsync("sample.png");

bool isValid = validator.IsValid(
    data,
    new MimeType("image", "png"),
    new FileExtension(".png"));
```

`FileExtension` accepts extensions with or without the leading period, so `.png` and `png` are treated the same.

[Table of Contents](#table-of-contents)

---

## Allow List and Deny List Validation

The constructor accepts an allow list and an optional deny list.

```csharp
using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

IEnumerable<FileTypeValidatorBase> allowed =
[
    new PngValidator(),
    new JpegValidator(),
    new GifValidator(),
    new WebpValidator()
];

IEnumerable<FileTypeValidatorBase> disallowed =
[
    new WebpValidator()
];

FileTypeValidator validator = new(allowed: allowed, disallowed: disallowed);
```

A file is valid only when it matches an allowed file type and does not match a disallowed file type.

[Table of Contents](#table-of-contents)

---

## Creating Custom File Type Validators

Third-party packages and application projects can add their own validators by inheriting from `FileTypeValidatorBase` and returning the MIME types and file signatures that identify the custom format.

The example below defines a fictional ACME report format. The file is considered a match when the caller-provided MIME type is `application/vnd.acme.report`, the extension is `.acmerpt` or `.arpt`, and the bytes contain `ACME` at offset `0` and `RPT` at offset `8`.

```csharp
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;

public sealed class AcmeReportValidator : FileTypeValidatorBase
{
    private static readonly MimeType[] mimeTypes =
    [
        new("application", "vnd.acme.report")
    ];

    private static readonly FileSignature[] signatures =
    [
        new(
            signatureFragments:
            [
                new ByteSignature("ACME"u8.ToArray(), 0),
                new ByteSignature("RPT"u8.ToArray(), 8)
            ],
            fileExtensions:
            [
                new FileExtension("acmerpt"),
                new FileExtension("arpt")
            ])
    ];

    public override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public override IEnumerable<FileSignature> Signatures => signatures;
}
```

Use the custom definition the same way as the built-in validators:

```csharp
using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.Signatures;

FileTypeValidator validator = new(
    allowed:
    [
        new AcmeReportValidator()
    ]);

byte[] data = await File.ReadAllBytesAsync("report.acmerpt");

bool isValid = validator.IsValid(
    data,
    new MimeType("application", "vnd.acme.report"),
    new FileExtension(".acmerpt"));
```

A single `FileSignature` may contain multiple signature fragments. All fragments in that `FileSignature` must match. Multiple `FileSignature` entries can be returned when a format has alternative valid signatures.

[Table of Contents](#table-of-contents)

---

## ASP.NET Core Upload Example

For uploaded files, read the upload stream into a byte array, parse the browser-provided content type, and validate using the uploaded file name extension.

```csharp
using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;
using Microsoft.AspNetCore.Http;

static async Task<bool> IsAllowedImageUploadAsync(IFormFile file)
{
    FileTypeValidator validator = new(
        allowed:
        [
            new PngValidator(),
            new JpegValidator(),
            new GifValidator()
        ]);

    await using MemoryStream memoryStream = new();
    await file.CopyToAsync(memoryStream);

    byte[] data = memoryStream.ToArray();
    MimeType mimeType = new(file.ContentType);
    FileExtension fileExtension = new(Path.GetExtension(file.FileName));

    return validator.IsValid(data, mimeType, fileExtension);
}
```

Do not rely on this package alone as your only upload security control. Uploaded files should still be handled using normal security practices, such as size limits, safe storage paths, randomized file names, authorization checks, and malware scanning where appropriate.

[Table of Contents](#table-of-contents)

---

## Important Behavior Notes

- The validator requires a matching allow-list entry. If no allowed validators are provided, files are invalid.
- Validation uses three pieces of information: file bytes, MIME type, and file extension.
- The MIME type is not detected by the library. It must be supplied by the caller.
- Browser-provided MIME types and file names can be spoofed, which is why the file signature check is also performed.
- The library checks known signatures for supported file types. It does not fully parse every supported file format.
- The library does not scan for malware or determine whether file content is safe to open.

[Table of Contents](#table-of-contents)

---

## Signature Sources

Carrigan.FileTypeValidators includes a small set of built-in validators for common image and legacy document file formats.

Gary C. Kessler's GCK File Signatures Table and the SEARCH-hosted GCK File Signature Table are acknowledged as useful external research and comparison references. Kessler's table is attributed in the package `NOTICE.txt` and `Acknowledgements.txt` files.

The project does not include, redistribute, scrape, vendor, bulk-import, or mechanically translate those tables. Built-in signature definitions should be sourced from public file-format documentation, independent implementation knowledge, and/or independently verified sample files. See `docs/SOURCES.md` in the package source for the source policy and reference notes.

[Table of Contents](#table-of-contents)

---

## License

Carrigan.FileTypeValidators  
Copyright © 2025 - 2026 Carrigan Software Solutions LLC

Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0

[Table of Contents](#table-of-contents)
