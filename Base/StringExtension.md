# StringExtension

Documentation for `Tools/StringExtension.cs` (namespace `Zuhid.Tools`).

## Overview

`StringExtension` is a static class exposing extension methods for common string transformations:

- `ToCamelCase()` — lowercases the first character.
- `ToTitleCase()` — uppercases the first character.
- `RemoveSpecialCharacters()` — strips non-alphanumeric characters.
- `ToKebabCase()` — converts PascalCase/camelCase to kebab-case.
- `ToSnakeCase()` — converts PascalCase/camelCase to snake_case.

## Usage

Add `using Zuhid.Tools;` to access the extension methods on `string`.

```csharp
using Zuhid.Tools;

var s = "HelloWorld";
var camel = s.ToCamelCase();    // "helloWorld"
var title = s.ToTitleCase();    // "HelloWorld" (no change)
var kebab = s.ToKebabCase();    // "hello-world"
var snake = s.ToSnakeCase();    // "hello_world"

var messy = "A! B@# 123-xyz";
var cleaned = messy.RemoveSpecialCharacters(); // "AB123xyz"
```

## API Reference

### `string ToCamelCase(this string str)`

- Description: Converts only the first character of `str` to lowercase and returns the combined string.
- Parameters: `str` — input string.
- Returns: A new string with the first character lowercased.
- Notes: Implementation uses `str[..1]` and `str[1..]` slicing. Calling on `null` will throw `NullReferenceException`. Calling on an empty string will throw an exception due to the slice.

### `string ToTitleCase(this string str)`

- Description: Converts only the first character of `str` to uppercase and returns the combined string.
- Parameters: `str` — input string.
- Returns: A new string with the first character uppercased.
- Notes: Same null/empty behavior as `ToCamelCase()`.

### `string RemoveSpecialCharacters(this string str)`

- Description: Returns a string containing only alphanumeric characters from `str` (0-9, A-Z, a-z).
- Parameters: `str` — input string.
- Returns: Filtered string containing only letters and digits.
- Notes: Preserves character order. If `str` is `null`, calling the extension will throw `NullReferenceException`. Empty string returns empty string.

### `string ToKebabCase(this string str)`

- Description: Converts a mixed case string into kebab-case using a regular expression, then lowercases the result.
- Parameters: `str` — input string.
- Returns: kebab-cased string (e.g., `HelloWorld` -> `hello-world`).
- Notes: Removes a leading separator if present. If `str` is `null`, calling the extension will throw `NullReferenceException`.

### `string ToSnakeCase(this string str)`

- Description: Converts a mixed case string into snake_case using a regular expression, then lowercases the result.
- Parameters: `str` — input string.
- Returns: snake_cased string (e.g., `HelloWorld` -> `hello_world`).
- Notes: Removes a leading separator if present. If `str` is `null`, calling the extension will throw `NullReferenceException`.

## Edge Cases & Recommendations

- All methods are simple, allocation-light helpers but do not validate `null` or empty inputs. To avoid exceptions, check `string.IsNullOrEmpty(str)` before calling, or use a short guard:

```csharp
string SafeToCamel(string s) => string.IsNullOrEmpty(s) ? s : s.ToCamelCase();
```

- `RemoveSpecialCharacters` keeps only ASCII letters and digits. It does not preserve Unicode letters. If you need Unicode-aware filtering, consider using `char.IsLetterOrDigit` instead.

- The regex used for `ToKebabCase`/`ToSnakeCase` attempts to handle transitions from uppercase to lowercase and digits; test with your specific naming patterns (e.g., acronyms like `HTTPServer` may produce extra separators).

## Example: Safe wrapper

```csharp
public static string SafeToKebab(this string? s) => string.IsNullOrEmpty(s) ? string.Empty : s.ToKebabCase();
```
