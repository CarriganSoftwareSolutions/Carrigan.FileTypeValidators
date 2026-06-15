# File Signature Source Policy

Carrigan.FileTypeValidators includes a small set of built-in file type validators for common file formats.

The byte patterns used by those validators are treated as factual format identifiers. New built-in signatures should be added from public file-format documentation, independent implementation knowledge, and/or independently verified sample files.

## Kessler and SEARCH/GCK file signature references

Gary C. Kessler's GCK File Signatures Table and the SEARCH-hosted GCK File Signature Table are useful external references for researching and comparing file signatures:

- Gary C. Kessler, GCK's File Signatures Table: https://www.garykessler.net/library/file_sigs_GCK_latest.html
- GCK File Signature Table, powered by SEARCH: https://filesig.search.org/
- https://www.loc.gov/preservation/digital/formats/fdd/fdd000328.shtml

Gary C. Kessler's page states that it is copyright © 2002-2026 Gary C. Kessler and permits visitor use with appropriate attribution, provided the information is not altered without express written permission.

This project does not include, redistribute, scrape, vendor, bulk-import, or mechanically translate either table. Do not bulk-import rows from those tables into this repository without a license or written permission that allows redistribution under terms compatible with this project.

## Maintainer rules for built-in definitions

When adding or changing a built-in file type validator:

1. Prefer official or public file-format documentation.
2. Verify the bytes against independently obtained sample files when practical.
3. Do not copy third-party table descriptions, notes, row ordering, category structure, or bulk selections.
4. Keep the implementation limited to the validators actually supported by this package.
5. Add source notes here when a non-obvious signature, offset, trailer, or special case is introduced.
