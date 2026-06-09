# File Signature Source Policy

Carrigan.FileTypeValidators includes a small set of built-in file type validators for common file formats.

The byte patterns used by those validators are treated as factual format identifiers. New built-in signatures should be added from public file-format documentation, independent implementation knowledge, and/or independently verified sample files.

## SEARCH/GCK and Gary Kessler file signature references

The SEARCH/GCK File Signature Table and Gary Kessler's historical file signature table are useful external references for researching file signatures:

- GCK File Signature Table, powered by SEARCH: https://filesig.search.org/
- Gary Kessler's File Signatures Table: https://www.garykessler.net/library/file_sigs.html

This project does not include, redistribute, scrape, vendor, or mechanically translate either table. Do not bulk-import rows from those tables into this repository without a license or written permission that allows redistribution under terms compatible with this project.

## Maintainer rules for built-in definitions

When adding or changing a built-in file type validator:

1. Prefer official or public file-format documentation.
2. Verify the bytes against independently obtained sample files when practical.
3. Do not copy third-party table descriptions, notes, row ordering, category structure, or bulk selections.
4. Keep the implementation limited to the validators actually supported by this package.
5. Add source notes here when a non-obvious signature, offset, trailer, or special case is introduced.

## Current built-in definitions

The current built-in definitions cover common image formats only: BMP, GIF, JPEG/JFIF/PJPEG, PNG, TIFF, and WebP. These definitions use common file header and, where implemented, trailer checks for those formats.
