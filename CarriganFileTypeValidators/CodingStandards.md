# Carrigan.SqlTools Coding & Documentation Standards

## General Coding Standards

- Use **target-typed `new(...)`** without the constructor name when possible.
- Use **expression-bodied members** when possible.
- **Never use `var`** unless absolutely necessary.
- Use **file-scoped namespaces**.
- Never use **block (“flower boxing”) comments**. If you must, use regions instead.
- Use **variables with full, unambiguous names**.
  - **Exception:** Data types may be used as variable names when referencing the datatype itself  
    *Example: `GetInt`*.
- Allow abbreviations **only when the variable name is identical to the datatype name**.  
    *Valid Example: EncodingEnum encodingEnum;*
- **Never use `continue`** statements.
- Use **collection expressions** when possible.
- Prefer params IEnumerable<T> over params T[], and yes,params as an IEnumerable is supported in the version of c# used by the SqlTools project.
- You may assume lines of code up to 180 characters are allowed for optimal viewing on a 1080p at 100% magnification with a reasonable allowance for a Solution Explorer pane. 180 characters is a suggested guideline, not a hard and fast rule.
- Avoid leaving warnings in the codebase. If a warning cannot be resolved, it must be suppressed with a justification comment. Warnings have been tuned in .editorconfig to enforce additional coding standards, not addressed in this document. 
- Especially avoid warnings related to nullable reference types. Use nullable reference types properly and avoid suppressing warnings related to them. If you find yourself needing to suppress a nullable reference type warning, consider whether the code can be refactored to avoid the need for suppression.
- The project has "Ignore Spelling" comments, these are intentional and should not be removed. They are used to prevent false positives from the EWS Software's "Spell Check My Code" extension on technical terms, class names, method names, and other identifiers that may not be recognized as standard words but are correct in the context of the codebase. You may remove words from the Ignore Spelling comments if they also appear in the IgnoredWords.dic file.
- When dividing parenthesis or square brackets out over multiple lines they should be lined up in the same manner that curly braces divided out over multiple lines.
  For example:
```csharp
void Foo
(
    int argumentOne,
    .
    .
    .
    int argumentN
)

int[] GetArray() =>
[
    1,
    2,
    3,
    .
    .
    .
    N
]
```
---

## Assumed Global Usings

The following namespaces are assumed to be globally imported in this project:

```csharp
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;
```

---

# Unit Test Standards

- Assume **internals are exposed** to the unit testing project  
  (e.g., using `InternalsVisibleTo`).  
  Unit tests should take advantage of this and **test the code as thoroughly as possible**.

- For xUnit tests, always prefer **`[InlineData]`** over `MemberData`.

- Test method names must be:
  - Clear  
  - Descriptive  
  - Written in standard xUnit naming conventions  

- When testing constructors expected to throw exceptions, always use:

```csharp
Assert.Throws<SomeException>(() => new SomeType(...));
```

- Unit tests assume the additional global using:

```csharp
global using global::Xunit;
```

---

## Unit Test Method Naming Rules

When naming unit test methods using `_` separators:

- Keep the number of sections **as small as needed** to differentiate between similarly named tests.
- Use the pattern:

```
BasicName
BasicName_UniqueAspect
BasicName_UniqueAspect_Exception
```

- Only include a **result section** (e.g., `Exception`, `Error`) when the test expects an exception or error.
- **Normal tests** should contain **no more than two sections** (one `_`).
- **Exception tests** should contain **no more than three sections** (two `_`).

### Examples

```
Constructor
Constructor_NoArguments
Constructor_WithArguments
Constructor_WithArguments_Exception
```

---