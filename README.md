# String Enum .NET
A C# enumeration type defined by a set of named string constants that supports all common features of the native enum type.

[![build](https://github.com/kyrylomyr/StringEnumNet/workflows/build/badge.svg?branch=master)](https://github.com/kyrylomyr/StringEnumNet/actions?query=workflow%3Abuild)
[![coverage](https://coveralls.io/repos/github/kyrylomyr/StringEnumNet/badge.svg)](https://coveralls.io/github/kyrylomyr/StringEnumNet?branch=master)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/kyrylomyr/StringEnumNet/blob/master/LICENSE)

# Usage

Define a string enum with the syntax similar to the native C# enums:

```csharp
public class Orientation : StringEnum<Orientation>
{
    public static readonly Orientation North = Define("North");
    public static readonly Orientation South = Define("South");
    public static readonly Orientation East  = Define("East");
    public static readonly Orientation West  = Define("West");
}
```

It is not possible to define duplicate or empty string values.

Use a defined enum as usual:

```csharp
// Get a concrete enum member
// Possible values are shown by IDE because they are statically declared
Orientation north = Orientation.North;

// Implicit conversion to the string
string title = north;

// Explicit conversion from the string
Orientation north = (Orientation)"North";

// Compare an enum value with a string
if ("South" == Orientation.South)
{
    ...
}

// Check if a string value is defined in the enum
var isDefined = Orientation.IsDefined("Nowhere");

// Parse a string
Orientation somewhere = Orientation.Parse("West");

// Try to parse a string
if (Orientation.TryParse("Somewhere", out var orientation))
{
    ...
}
```

Parsing and explicit conversion expect **exact** string as an enum member was defined. They don't allow whitespace aroung the actual string value and are sensitive to the case.

C# supports only constants in the `switch` cases. Because of this the `StringEnum` can be used only in the `switch expression` within the `when` clause:

```csharp
var direction = orientation switch
                {
                    _ when orientation == Orientation.North => "Up",
                    _ when orientation == Orientation.South => "Down",
                    _ when orientation == Orientation.East  => "Left",
                    _ when orientation == Orientation.West  => "Right",
                    _ => throw new ArgumentOutOfRangeException(nameof(orientation))
                };
```

If some actions must be performed, `if-else` statements should be used instead:

```csharp
if (orientation == Orientation.North)
    MoveUp();
else if (orientation == Orientation.South)
    MoveDown();
else if (orientation == Orientation.East)
    MoveLeft();
else if (orientation == Orientation.West)
    MoveRight();
else
    throw new ArgumentOutOfRangeException(nameof(orientation));
```
