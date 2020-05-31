# String Enum .NET
A C# enumeration type defined by a set of named string constants that supports all common features of the native enum type.

![CI](https://github.com/kyrylomyr/StringEnumNet/workflows/CI/badge.svg)

# Definition

```csharp
public class Orientation : StringEnum<Orientation>
{
    public static readonly Orientation North = Define("North orientation");
    public static readonly Orientation South = Define("South orientation");
    public static readonly Orientation East  = Define("East orientation");
    public static readonly Orientation West  = Define("West orientation");
}
```

# Usage

## Switch statement

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

If some actions must be performed, `if-else` statements should be used:

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