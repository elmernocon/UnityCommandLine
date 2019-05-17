# Coding Standards

* Braces go on a newline.

* Use `4` spaces exclusively instead of tabs.

* Use the `C#` `XML`-style summary comment.

* Use `var` instead of declaring variable types. (more readable)

```csharp
// Use
var myVariable = new Dictionary<MyImmutableDataKey, MyImmutableDataValue>();

// Instead of
Dictionary<MyImmutableDataKey, MyImmutableDataValue> myVariable = new Dictionary<MyImmutableDataKey, MyImmutableDataValue>();
```

* Use the `public` access modifier for `enums` inside a class. (easier sharing of `enums` between types)

```csharp
// Use
var myEnum = MyEnum.Default;

// Instead of
var myEnum = MyOtherClass.MyEnum.Default;
```

* Use `string.Format` or `StringBuilder` whenever possible.

* Use `Mathf.Approximately` when comparing `float` variables to constant values.

* Limit the inheritance depth to `3`. Prioritize composition over inheritance.

* Limit the number of parameters in a method to `8`.

* Limit the block nesting depth to `4`.

```csharp
for (;;)
{                                                               // 1
    for (;;)
    {                                                           // 2
        for (;;)
        {                                                       // 3
            if (condition)
            {                                                   // 4
            }
            else
            {                                                   // 4
            }
        }
    }
}
```

* Limit the number control flow branches in a single code block to `20`.
These includes `if`, `else if`, `else`, `while`, `do-while`, `for`, `foreach`, each `case` in a `switch`, `try`, `catch`, and `finally`. 
Also included are the AND (`&&`), OR (`||`), Conditional (`?:`), Null-Propagation (`??`), and Null-Conditional (`?.` or `?[]`) operators.

```csharp
// Method 'DestroyIfTaggedWith' has cyclomatic complexity of 4 (20% of threshold)
private void DestroyIfTaggedWith(string tag, GameObject gameObject)
{
    if (string.IsNullOrWhiteSpace(tag))                         // 1
        throw new ArgumentNullException(nameof(tag));

    if (gameObject == null)                                     // 2
        throw new ArgumentNullException(nameof(gameObject));

    if (tag.Equals(gameObject.tag))                             // 3
        Object.Destroy(gameObject);

                                                                // +1 Always
}
```

* Treat `compiler warnings` as `errors`. There should be `zero warnings` at build or runtime in normal operation.

* All `using` declarations go together at the top of the file.

* All `using` aliases should be placed at the bottom of all the other `using` declarations.

* All `public` `classes`, `interfaces`, `structs`, `enums`, and `members` should be documented using `XML`-style `summary` comments.

* All `private` and `protected` members' `XML`-style comments are **optional**, but **all** `public` members must have a `XML`-style `summary` comment.

* All `Parameter` and `Return` descriptions are **optional**, add them if you feel they need a non-trivial explanation.

* All `SerializeField` fields should **never** be public. Use a `public` accessor property to access the field if external access is required.

* All `SerializeField` fields should **always** have a `Tooltip` attribute. This doubles as code documentation for the field.

```csharp
[SerializeField] [Tooltip("Tooltip comment displayed in the editor.")]
private float _myFloat;
...
/// <summary>
/// MyFloat is a property that exposes
/// the value of the _myFloat field to other types.
/// </summary>
public float MyFloat
{
    get { return _myFloat; }
}
```

* When creating an Editor GUI use `GUIContent` to add tooltips on UI elements instead of plain text. This doubles as code documentation for the UI element.

```csharp
// Use
if (GUILayout.Button("Recenter"))
{
    ...
}

// Instead of
if (GUILayout.Button(new GUIContent("Recenter", "Moves the selected object into the World Coordinates. (0, 0, 0)."))
{
    ...
}
```

## File Layout

```csharp
using MyOtherNamespace;
using Foobar = MyOtherOtherNamespace;

namespace MyNameSpace
{
    public class MyClass<TParam0, TParam1> : MonoBehaviour,     // <summary>
        IMyInterface
        where TParam0 : MyOtherClass
        where TParam1 : IMyOtherInterface
    {
        #region Nested Types
        ..enum                                                  // <summary> if public or protected access
        ..delegate                                              // <summary> if public or protected access
        ..structs                                               // <summary> if public or protected access
        ..classes                                               // <summary> if public or protected access
        ..other types                                           // <summary> if public or protected access

        #region Constants
        ..public                                                // <summary>
        ..protected                                             // <summary>
        ..private

        #region Statics
            #region Fields
                ..public                                        // <summary>
                ..protected                                     // <summary>
                ..private
            #region Properties
                ..public                                        // <summary>
                ..protected                                     // <summary>
                ..private
            #region Methods
                ..public                                        // <summary>
                ..protected                                     // <summary>
                ..private

        #region Operators

        #region Fields
        ..private (SerializeFieldAttribute & TooltipAttribute)  // private access only, make sure to also use TooltipAttribute
        ..private (InjectAttribute)                             // private access only
        ..public                                                // do not use
        ..protected                                             // <summary>
        ..private

        #region Constructors & Destructors

        #region Indexers & Events

        #region Properties
        ..interface                                             // <summary> if public or protected access
        ..public                                                // <summary>
        ..protected                                             // <summary>
        ..private
        ..internal

        #region Methods
        ..interface                                             // <summary> if public or protected access
        ..public                                                // <summary>
        ..protected                                             // <summary>
        ..private
        ..internal
    }
}
```

## Naming Style

```csharp
// Types and Namespaces
public class Foobar { ... }

// Interfaces
public interace IFoobar { ... }

// Type Parameters
public class Foobar<TFoobar> { ... }

// Enums
enum MyEnum
{
    Fee,
    Fi,
    Fo,
    Fum
}

// Constants
public const float PI_VALUE = 3.1415926f;
private const string PASSPHRASE = "Hello World!";

// Static Fields
private static int _fee;
private static readonly int Fi;
protected static int Fo;
public static int Fum;

// Fields
private string _quux;
protected string Quuz;
public string Corge;

// Events, Properties, Methods
public event Bar;
protected int Foo { get; set; }
private void Baz() { ... }

// Parameters
private void Baz(int qux) { ... }

// Local Variables
int quux;

// All Other Entities
// UpperCamelCase
```