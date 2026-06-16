# File Signature Source Policy

Carrigan.FileTypeValidators includes a small set of built-in file type validators for common file formats and detector definitions intended for deny-list scenarios.

The byte patterns used by those validators and detectors are treated as factual format identifiers. New built-in signatures should be added from public file-format documentation, independent implementation knowledge, and/or independently verified sample files.

## Kessler and SEARCH/GCK file signature references

Gary C. Kessler's GCK File Signatures Table and the SEARCH-hosted GCK File Signature Table are useful external references for researching and comparing file signatures:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- GCK File Signature Table, powered by SEARCH: https://filesig.search.org/
- Library of Congress, BigTIFF File Format Description: https://www.loc.gov/preservation/digital/formats/fdd/fdd000328.shtml

Gary C. Kessler's page states that it is copyright © 2002-2026 Gary C. Kessler and permits visitor use with appropriate attribution, provided the information is not altered without express written permission.

This project does not include, redistribute, scrape, vendor, bulk-import, or mechanically translate either table. Do not bulk-import rows from those tables into this repository without a license or written permission that allows redistribution under terms compatible with this project.


## Document validators

The legacy Microsoft Office document validators identify OLE Compound File Binary containers with a leading `D0 CF 11 E0 A1 B1 1A E1` header and an additional subheader at byte offset `0x200` where applicable. The `0x200` value is hexadecimal and corresponds to decimal offset `512`.

General container references:

- Microsoft, Compound File Binary File Format: https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-cfb/
- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html

### Word 97-2003 validator

`Word97Validator` identifies legacy Word binary documents/templates using the OLE compound-file header and Word subheader implemented in the validator.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html

MIME type references:

- `application/msword`: https://www.iana.org/assignments/media-types/application/msword

### Excel 97-2003 validator

`Excel97Validator` identifies legacy Excel binary workbooks/templates/add-ins using the OLE compound-file header and the BIFF8 workbook subheader implemented in the validator.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Library of Congress, Microsoft Excel 97-2003 Workbook (xls), BIFF8: https://www.loc.gov/preservation/digital/formats/fdd/fdd000510.shtml

MIME type references:

- `application/vnd.ms-excel`: https://www.iana.org/assignments/media-types/application/vnd.ms-excel

### PowerPoint 97-2003 validator

`PowerPoint97Validator` identifies legacy PowerPoint binary presentations/templates/shows/add-ins using the OLE compound-file header and PowerPoint subheaders implemented in the validator.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Microsoft, PowerPoint (.ppt) Binary File Format: https://learn.microsoft.com/en-us/openspecs/office_file_formats/ms-ppt/

MIME type references:

- `application/vnd.ms-powerpoint`: https://www.iana.org/assignments/media-types/application/vnd.ms-powerpoint

### Outlook MSG validator

`OutlookValidator` identifies Outlook MSG files using the OLE compound-file header and the CFB root-entry subheader implemented in the validator. The root-entry marker is not unique to MSG files, so this validator should be understood as an extension/MIME/signature validator rather than a full MSG parser.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Microsoft, Outlook Item (.msg) File Format: https://learn.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxmsg/
- Library of Congress, Microsoft Outlook Item (MSG): https://www.loc.gov/preservation/digital/formats/fdd/fdd000379.shtml

MIME type references:

- `application/vnd.ms-outlook`: https://www.loc.gov/preservation/digital/formats/fdd/fdd000379.shtml

### Outlook Express EML validator

`OutlookExpressValidator` identifies EML-style message files using the leading message bytes implemented in the validator.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html

MIME type references:

- `message/rfc822`: https://www.w3.org/Protocols/rfc1341/7_3_Message.html

### Publisher 97 validator

`Publisher97Validator` identifies legacy Publisher files using the OLE compound-file header and Publisher subheader implemented in the validator.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html

MIME type references:

- `application/x-mspublisher`: https://www.digipres.org/formats/sources/tika/formats/#application/x-mspublisher

## Detector definitions

Detector definitions are intended for deny-list use. They may be broader than normal validators because they are meant to identify dangerous executable or executable-adjacent content. `FileTypeDetectorBase` disables MIME type checks during allow-list checks so that MIME-only matches do not accidentally make detector definitions useful as normal allow-list validators.

### Windows executable detector

`WindowsExeDetector` identifies DOS MZ / Windows executable-style content using the leading `MZ` byte signature and the executable-related extensions implemented in the detector.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Microsoft, PE Format: https://learn.microsoft.com/en-us/windows/win32/debug/pe-format

MIME type references:

- `application/vnd.microsoft.portable-executable`: https://www.iana.org/assignments/media-types/application/vnd.microsoft.portable-executable
- `application/x-msdownload`: https://www.digipres.org/formats/mime-types/

### ELF detector

`ElfDetector` identifies ELF files using the leading `7F 45 4C 46` byte signature.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Linux manual page, `elf(5)`: https://man7.org/linux/man-pages/man5/elf.5.html

MIME type references:

- `application/x-elf`: https://www.digipres.org/formats/mime-types/

### Mach-O detector

`MachoDetector` identifies Mach-O files using the shared byte sequences from Apple's `MH_MAGIC` / `MH_MAGIC_64` and `MH_CIGAM` / `MH_CIGAM_64` constants. The detector intentionally does not define MIME types because it is intended to use signature and extension checks only.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Apple Open Source, XNU `mach-o/loader.h`: https://github.com/apple-oss-distributions/xnu/blob/main/EXTERNAL_HEADERS/mach-o/loader.h

### Java class detector

`JavaClassDetector` identifies Java class files using the leading `CA FE BA BE` byte signature.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- Oracle, Java Virtual Machine Specification, Chapter 4, The `class` File Format: https://docs.oracle.com/javase/specs/jvms/se7/html/jvms-4.html

MIME type references:

- `application/java`: https://www.digipres.org/formats/mime-types/
- `application/java-byte-code`: https://www.digipres.org/formats/mime-types/
- `application/java-vm`: https://www.digipres.org/formats/mime-types/
- `application/x-java`: https://www.digipres.org/formats/mime-types/
- `application/x-java-class`: https://www.digipres.org/formats/mime-types/
- `application/x-httpd-java`: https://www.digipres.org/formats/mime-types/
- `application/x-java-vm`: https://www.digipres.org/formats/mime-types/

### Other executable detector

`OtherExeDetector` identifies additional COM/SYS executable-style byte patterns using leading `E8`, `E9`, and `EB` bytes. This detector intentionally does not define MIME types because it is intended to use signature and extension checks only.

Signature references:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html

## Maintainer rules for built-in definitions

When adding or changing a built-in file type validator or detector:

1. Prefer official or public file-format documentation.
2. Verify the bytes against independently obtained sample files when practical.
3. Do not copy third-party table descriptions, notes, row ordering, category structure, or bulk selections.
4. Keep the implementation limited to the validators and detectors actually supported by this package.
5. Add source notes here when a non-obvious signature, offset, trailer, MIME type, or special case is introduced.
