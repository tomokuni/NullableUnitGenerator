This document is a work in progress. (���̃h�L�������g�͏��������ł��B)
---

NullableUnitGenerator
===
[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/tomokuni/NullableUnitGenerator/build.yml?label=UnitTest%20.NET6%20C%23)](https://github.com/tomokuni/NullableUnitGenerator/actions/workflows/build.yml)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/tomokuni/NullableUnitGenerator?label=GitHub%20release)](https://github.com/tomokuni/NullableUnitGenerator/releases)
[![Nuget](https://img.shields.io/nuget/v/NullableUnitGenerator?label=Nuget%20release)](https://www.nuget.org/packages/NullableUnitGenerator/)

C# Source Generator to create [Value object](https://en.wikipedia.org/wiki/Value_object) pattern to support arithmetic operators and serialization and Null and Undefined value.  

�Z�p���Z�q�A�V���A�����A����� Null�l��Undefined�l���T�|�[�g���� [Value object](https://en.wikipedia.org/wiki/Value_object) �p�^�[�����쐬���� C# �\�[�X�W�F�l���[�^�[�ł��B  


GitHub: [NullableUnitGenerator](https://github.com/tomokuni/NullableUnitGenerator/)
---

NuGet: [NullableUnitGenerator](https://www.nuget.org/packages/NullableUnitGenerator/)
---
```
Install-Package NullableUnitGenerator
```


Thanks 
---
forked from [UnitGenerator](https://github.com/Cysharp/UnitGenerator) to support Null and Undefined values.  Thanks to the author of UnitGenerator.  

Null�l �� Undefined�l ���T�|�[�g���邽�߂� [UnitGenerator](https://github.com/Cysharp/UnitGenerator) ���番�򂵂܂����B  UnitGenerator �̍�җl�Ɋ��ӂ��܂��B  


MessagePackFormatter, EntityFrameworkValueConverter, and Unity are not yet supported.
---
MessagePackFormatter, EntityFrameworkValueConverter, and Unity are supported by UnitGenerator.  
However, NullableUnitGenerator does not yet support them.  
They are listed in this document, but cannot be used with NullableUnitGenerator.  

MessagePackFormatter�AEntityFrameworkValueConverter�AUnity �́AUnitGenerator �ŃT�|�[�g����Ă��܂��B  
�������ANullableUnitGenerator �͂܂��������T�|�[�g���Ă��܂���B  
�����͂��̕����ɋL�ڂ��Ă��邪�ANullableUnitGenerator �Ŏg�p���邱�Ƃ͂ł��܂���B  


## Introduction

For example, Identifier, UserId is comparable only to UserId, and cannot be assigned to any other type. Also, arithmetic operations are not allowed.  

�Ⴆ�΁A���ʎq UserId �� UserId �Ƃ̂ݔ�r�\�ł���A���̃^�C�v�Ɋ��蓖�Ă邱�Ƃ͂ł��܂���B �܂��A�Z�p���Z�͎g�p�ł��܂���B  

```csharp
using NullableUnitGenerator;

[UnitOf(typeof(int))]
public readonly partial struct UserId { }
```

will generates  

�ȉ��̂悤�ȃR�[�h��������������܂��B  

```csharp
[System.ComponentModel.TypeConverter(typeof(UserIdTypeConverter))]
public readonly partial struct UserId : IEquatable<UserId>, IEqualityComparer<UserId> 
{
    readonly int m_value = default;
    readonly TernaryState m_state = TernaryState.Undef;
    
    public UserId(in UserId value) { (m_state, m_value) = (value.m_state, value.m_value); }
    public UserId(in TernaryState state, in int value = default) { ... }
    public UserId(in int value) { (m_state, m_value) = (TernaryState.Value, value); }
    public UserId(in int? value) { ... }

    public bool IsUndef       => m_state == TernaryState.Undef;
    public bool IsNull        => m_state == TernaryState.Null;
    public bool IsUndefOrNull => m_state != TernaryState.Value;
    public bool HasValue      => m_state == TernaryState.Value;
    public TernaryState State => m_state;

    public int  Value         => GetOrThrow();
    public int  AsPrimitive() => Value;
    public int  GetRawValue() => m_value;
    public int  GetOr(in int defaultValue)  => HasValue ? m_value : defaultValue;
    public int? GetOr(in int? defaultValue) => HasValue ? m_value : defaultValue;
    public int  GetOrDefault() => GetOr(default);
    public int? GetOrNull()    => GetOr(null);
    public int  GetOrThrow()   => ...;
    public bool TryGet(out int value, in int defaultValue = default) => ...;

    public override int GetHashCode()  => (m_state, m_value).GetHashCode();
    public int GetHashCode(UserId obj) => (obj.m_state, obj.m_value).GetHashCode();
    public override string ToString()  => ValueString;

    public static explicit operator int(in UserId value)  => (int)value.GetOrThrow();
    public static explicit operator UserId(in int value)  => new(value);
    public static explicit operator int?(in UserId value) => (int?)value.GetOrNull();
    public static explicit operator UserId(in int? value) => new(value);

    public bool Equals(UserId other)         => m_state.Equals(other.m_state) && m_value.Equals(other.m_value);
    public bool Equals(UserId x, UserId y)   => x.Equals(y);
    public override bool Equals(object? obj) => obj is UserId ts && Equals(ts);

    public static bool operator ==(in UserId x, in UserId y) => x.Equals(y);
    public static bool operator !=(in UserId x, in UserId y) => !(x == y);

    private class UserIdTypeConverter : System.ComponentModel.TypeConverter
    { ... }
}
```

However, Hp in games, should not be allowed to be assigned to other types, but should support arithmetic operations with int. For example double heal = `target.Hp = Hp.Min(target.Hp * 2, target.MaxHp)`.  

�������A�Q�[���ɂ����� Hp �́A���̌^�ɑ�����邱�Ƃ͋����ꂸ�Aint���g�����Z�p���Z���T�|�[�g����K�v������܂��B�Ⴆ�΁Adouble heal = `target.Hp = Hp.Min(target.Hp * 2, target.MaxHp)`.

```csharp
[UnitOf(typeof(int), UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.Comparable | UnitGenerateOptions.MinMaxMethod)]
public readonly partial struct Hp { }

// -- generates

[System.ComponentModel.TypeConverter(typeof(HpTypeConverter))]
public readonly partial struct Hp : IEquatable<Hp> , IComparable<Hp>
{
    readonly int value;

    public Hp(int value)
    {
        this.value = value;
    }

    public int AsPrimitive() => value;
    public static explicit operator int(Hp value) => value.value;
    public static explicit operator Hp(int value) => new Hp(value);
    public bool Equals(Hp other) => value.Equals(other.value);
    public override bool Equals(object? obj) => snip...;
    public override int GetHashCode() => value.GetHashCode();
    public override string ToString() => value.ToString();
    public static bool operator ==(in Hp x, in Hp y) => x.value.Equals(y.value);
    public static bool operator !=(in Hp x, in Hp y) => !x.value.Equals(y.value);
    private class HpTypeConverter : System.ComponentModel.TypeConverter { /* snip... */ }

    // UnitGenerateOptions.ArithmeticOperator
    public static Hp operator +(in Hp x, in Hp y) => new Hp(checked((int)(x.value + y.value)));
    public static Hp operator -(in Hp x, in Hp y) => new Hp(checked((int)(x.value - y.value)));
    public static Hp operator *(in Hp x, in Hp y) => new Hp(checked((int)(x.value * y.value)));
    public static Hp operator /(in Hp x, in Hp y) => new Hp(checked((int)(x.value / y.value)));

    // UnitGenerateOptions.ValueArithmeticOperator
    public static Hp operator ++(in Hp x) => new Hp(checked((int)(x.value + 1)));
    public static Hp operator --(in Hp x) => new Hp(checked((int)(x.value - 1)));
    public static Hp operator +(in Hp x, in int y) => new Hp(checked((int)(x.value + y)));
    public static Hp operator -(in Hp x, in int y) => new Hp(checked((int)(x.value - y)));
    public static Hp operator *(in Hp x, in int y) => new Hp(checked((int)(x.value * y)));
    public static Hp operator /(in Hp x, in int y) => new Hp(checked((int)(x.value / y)));

    // UnitGenerateOptions.Comparable
    public int CompareTo(Hp other) => value.CompareTo(other.value);
    public static bool operator >(in Hp x, in Hp y) => x.value > y.value;
    public static bool operator <(in Hp x, in Hp y) => x.value < y.value;
    public static bool operator >=(in Hp x, in Hp y) => x.value >= y.value;
    public static bool operator <=(in Hp x, in Hp y) => x.value <= y.value;

    // UnitGenerateOptions.MinMaxMethod
    public static Hp Min(Hp x, Hp y) => new Hp(Math.Min(x.value, y.value));
    public static Hp Max(Hp x, Hp y) => new Hp(Math.Max(x.value, y.value));
}
```

You can configure with `UnitGenerateOptions`, which method to implement.  

�ǂ̃��\�b�h���������邩�� `UnitGenerateOptions` �Őݒ肷�邱�Ƃ��ł��܂��B  

```csharp
[Flags]
enum UnitGenerateOptions
{
    None = 0,
    ImplicitOperator = 1,
    ParseMethod = 2,
    MinMaxMethod = 4,
    ArithmeticOperator = 8,
    ValueArithmeticOperator = 16,
    Comparable = 32,
    Validate = 64,
    JsonConverter = 128,
    MessagePackFormatter = 256,  // Unsupported
    DapperTypeHandler = 512,
    EntityFrameworkValueConverter = 1024,  // Unsupported
    WithoutComparisonOperator = 2048,
    JsonConverterDictionaryKeySupport = 4096
}
```

UnitGenerateOptions has some serializer support. For example, a result like `Serialize(userId) => { Value = 1111 }` is awful. The value-object should be serialized natively, i.e. `Serialize(useId) => 1111`, and should be able to be added directly to a database, etc.  
Currently UnitGenerator supports System.Text.Json(JsonSerializer), [Dapper](https://github.com/StackExchange/Dapper), ~~[MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp) and EntityFrameworkCore~~.  

UnitGenerateOptions�ɂ́A�������̃V���A���C�U�[�T�|�[�g������܂��B�Ⴆ�΁A`Serialize(userId) => { Value = 1111 }`�̂悤�Ȍ��ʂ͂Ђǂ����̂ł��B�l�I�u�W�F�N�g�̓l�C�e�B�u�ɃV���A���C�Y�����ׂ��ŁA���Ȃ킿 `Serialize(useId) => 1111` �ƂȂ�A�f�[�^�x�[�X�Ȃǂɒ��ڒǉ��ł���悤�ɂ��ׂ��ł��B  
���݁AUnitGenerator�́ASystem.Text.Json(JsonSerializer)�A[Dapper](https://github.com/StackExchange/Dapper)�A~~[MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp)�AEntityFrameworkCore~~ ���T�|�[�g���Ă��܂��B�B  

```csharp
[UnitOf(typeof(int), UnitGenerateOptions.MessagePackFormatter)]
public readonly partial struct UserId { }

// -- generates

[MessagePackFormatter(typeof(UserIdMessagePackFormatter))]
public readonly partial struct UserId 
{
    class UserIdMessagePackFormatter : IMessagePackFormatter<UserId>
    {
        public void Serialize(ref MessagePackWriter writer, UserId value, MessagePackSerializerOptions options)
        {
            options.Resolver.GetFormatterWithVerify<int>().Serialize(ref writer, value.value, options);
        }

        public UserId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return new UserId(options.Resolver.GetFormatterWithVerify<int>().Deserialize(ref reader, options));
        }
    }
}
```

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [UnitOfAttribute](#unitofattribute)
- [UnitGenerateOptions](#unitgenerateoptions)
  - [ImplicitOperator](#implicitoperator)
  - [ParseMethod](#parsemethod)
  - [MinMaxMethod](#minmaxmethod)
  - [ArithmeticOperator](#arithmeticoperator)
  - [ValueArithmeticOperator](#valuearithmeticoperator)
  - [Comparable](#comparable)
  - [WithoutComparisonOperator](#withoutcomparisonoperator)
  - [Validate](#validate)
  - [JsonConverter](#jsonconverter)
  - [JsonConverterDictionaryKeySupport](#jsonconverterdictionarykeysupport)
  - [DapperTypeHandler](#dappertypehandler)
  - ~~[MessagePackFormatter](#messagepackformatter)~~
  - ~~[EntityFrameworkValueConverter](#entityframeworkvalueconverter)~~
- [Use for Unity](#use-for-unity)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->


## UnitOfAttribute
When referring to the UnitGenerator, it generates a internal `UnitOfAttribute`.  

UnitGenerator���Q�Ƃ���ƁA������`UnitOfAttribute`�𐶐����܂��B  

```csharp
namespace UnitGenerator
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    internal class UnitOfAttribute : Attribute
    {
        public UnitOfAttribute(Type type, UnitGenerateOptions options = UnitGenerateOptions.None, string toStringFormat = null)
    }
}
```

You can attach this attribute with any specified underlying type to `readonly partial struct`.  

���̑����́A`readonly partial struct`�Ɏw�肳�ꂽ��{�^�ƈꏏ�ɕt���邱�Ƃ��ł��܂��B  

```csharp
[UnitOf(typeof(Guid))]
public readonly partial struct GroupId { }

[UnitOf(typeof(string))]
public readonly partial struct Message { }

[UnitOf(typeof(long))]
public readonly partial struct Power { }

[UnitOf(typeof(byte[]))]
public readonly partial struct Image { }

[UnitOf(typeof(DateTime))]
public readonly partial struct StartDate { }

[UnitOf(typeof((string street, string city)))]
public readonly partial struct StreetAddress { }
```

Standard UnitOf(`UnitGenerateOptions.None`) generates value constructor, `explicit operator`, `implement IEquatable<T>`, `override GetHashCode`, `override ToString`, `==` and `!=` operator, `TypeConverter` for ASP.NET Core binding, `AsPrimitive` method.  
If you want to retrieve primitive value, use `AsPrimitive()` instead of `.Value`. This is intended to avoid casual getting of primitive values (using the arithmetic operator option if available).  
> When type is bool, also implements `true`, `false`, `!` operators.  

�W���� UnitOf(`UnitGenerateOptions.None`) �́A�l�̃R���X�g���N�^�A`explicit operator` �A`implement IEquatable<T>` �A`override GetHashCode` �A `override ToString` �A`==` �� `!=` �I�y���[�^�A`TypeConverter` for ASP.NET Core binding �A`AsPrimitive` ���\�b�h�𐶐����܂��B  
�v���~�e�B�u�Ȓl���擾�������ꍇ�́A`.Value`�̑���� `AsPrimitive()` ���g�p���܂��B����́A�v���~�e�B�u�Ȓl�̃J�W���A���Ȏ擾������邽�߂̂��̂ł��i�Z�p���Z�q�I�v�V����������ꍇ�͂�����g�p���܂��j�B  
> �^�C�v��bool�̏ꍇ�A`true`, `false`, `!` ���Z�q���������܂��B  

```csharp 
public static bool operator true(Foo x) => x.value;
public static bool operator false(Foo x) => !x.value;
public static bool operator !(Foo x) => !x.value;
```

> When type is Guid or [Ulid](https://github.com/Cysharp/Ulid), also implements `New()` and `New***()` static operator.  

> �^��Guid�܂���[Ulid](https://github.com/Cysharp/Ulid)�̏ꍇ�A `New()` ����� `New***()` �̐ÓI���Z�q���������Ă��܂��B  

```csharp
public static GroupId New();
public static GroupId NewGroupId();
```

Second parameter `UnitGenerateOptions options` can configure which method to implement, default is `None`.  
Third parameter `strign toStringFormat` can configure `ToString` format. Default is null and output as $`{0}`.  

2�Ԗڂ̃p�����[�^ `UnitGenerateOptions options` �́A�ǂ̃��\�b�h���������邩��ݒ肷�邱�Ƃ��ł���i�f�t�H���g�� `None` �j�B  
��3�p�����[�^ `strign toStringFormat` �� `ToString` �̃t�H�[�}�b�g��ݒ肷�邱�Ƃ��ł���B�f�t�H���g��NULL�ŁA$`{0}`�Ƃ��ďo�͂����B  


## UnitGenerateOptions

When referring to the UnitGenerator, it generates a internal `UnitGenerateOptions` that is bit flag of which method to implement.  

UnitGenerator���Q�Ƃ���ꍇ�A�ǂ̃��\�b�h���������邩�̃r�b�g�t���O�ł������`UnitGenerateOptions`�𐶐����܂��B  

```csharp
[Flags]
internal enum UnitGenerateOptions
{
    None = 0,
    ImplicitOperator = 1,
    ParseMethod = 2,
    MinMaxMethod = 4,
    ArithmeticOperator = 8,
    ValueArithmeticOperator = 16,
    Comparable = 32,
    Validate = 64,
    JsonConverter = 128,
    MessagePackFormatter = 256,
    DapperTypeHandler = 512,
    EntityFrameworkValueConverter = 1024,
}
```

You can use this with `[UnitOf]`.  

`[UnitOf]` �����̈����Ɏw��ł��܂��B  

```csharp
[UnitOf(typeof(int), UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.Comparable | UnitGenerateOptions.MinMaxMethod)]
public readonly partial struct Strength { }

[UnitOf(typeof(DateTime), UnitGenerateOptions.Validate | UnitGenerateOptions.ParseMethod | UnitGenerateOptions.Comparable)]
public readonly partial struct EndDate { }

[UnitOf(typeof(double), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.MinMaxMethod | UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.Comparable | UnitGenerateOptions.Validate | UnitGenerateOptions.JsonConverter | UnitGenerateOptions.MessagePackFormatter | UnitGenerateOptions.DapperTypeHandler | UnitGenerateOptions.EntityFrameworkValueConverter)]
public readonly partial struct AllOptionsStruct { }
```

You can setup project default options like this.  

���̂悤�ɁA�v���W�F�N�g�̃f�t�H���g�I�v�V������ݒ肷�邱�Ƃ��ł��܂��B  

```csharp
internal static class UnitOfOptions
{
    public const UnitGenerateOptions Default = UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.Comparable | UnitGenerateOptions.MinMaxMethod;
}

[UnitOf(typeof(int), UnitOfOptions.Default)]
public readonly partial struct Hp { }
```


### ImplicitOperator

```csharp
// Default
public static explicit operator U(T value) => value.value;
public static explicit operator T(U value) => new T(value);

// UnitGenerateOptions.ImplicitOperator
public static implicit operator U(T value) => value.value;
public static implicit operator T(U value) => new T(value);
```


### ParseMethod 

```csharp
public static T Parse(string s)
public static bool TryParse(string s, out T result)
```


### MinMaxMethod

```csharp
public static T Min(T x, T y)
public static T Max(T x, T y)
```


### ArithmeticOperator

```csharp
public static T operator +(in T x, in T y) => new T(checked((U)(x.value + y.value)));
public static T operator -(in T x, in T y) => new T(checked((U)(x.value - y.value)));
public static T operator *(in T x, in T y) => new T(checked((U)(x.value * y.value)));
public static T operator /(in T x, in T y) => new T(checked((U)(x.value / y.value)));
```


### ValueArithmeticOperator

```csharp
public static T operator ++(in T x) => new T(checked((U)(x.value + 1)));
public static T operator --(in T x) => new T(checked((U)(x.value - 1)));
public static T operator +(in T x, in U y) => new T(checked((U)(x.value + y)));
public static T operator -(in T x, in U y) => new T(checked((U)(x.value - y)));
public static T operator *(in T x, in U y) => new T(checked((U)(x.value * y)));
public static T operator /(in T x, in U y) => new T(checked((U)(x.value / y)));
```


### Comparable

Implements `IComparable<T>` and `>`, `<`, `>=`, `<=` operators. 

`IComparable<T>`�� `>`, `<`, `>=`, `<=` ���Z�q���������Ă��܂��B 

```csharp
public U CompareTo(T other) => value.CompareTo(other.value);
public static bool operator >(in T x, in T y) => x.value > y.value;
public static bool operator <(in T x, in T y) => x.value < y.value;
public static bool operator >=(in T x, in T y) => x.value >= y.value;
public static bool operator <=(in T x, in T y) => x.value <= y.value;
```


### WithoutComparisonOperator

Without implements `>`, `<`, `>=`, `<=` operators. For example, useful for Guid.  

�Ⴆ�΁AGuid �́A `>`, `<`, `>=`, `<=` ���Z�q �� ���������܂���B  

```csharp
[UnitOf(typeof(Guid), UnitGenerateOptions.Comparable | UnitGenerateOptions.WithoutComparisonOperator)]
public readonly partial struct FooId { }
```


### Validate

Implements `partial void Validate()` method that is called on constructor.  

�R���X�g���N�^�ŌĂяo����� `partial void Validate()` ���\�b�h���������Ă��܂��B  

```csharp
// You can implement this custom validate method.
[UnitOf(typeof(int), UnitGenerateOptions.Validate)]
public readonly partial struct SampleValidate
{
    // impl here.
    private partial void Validate()
    {
        if (value > 9999) throw new Exception("Invalid value range: " + value);
    }
}

// Source generator generate this codes.
public T(int value)
{
    this.value = value;
    this.Validate();
}
 
private partial void Validate();
```


### JsonConverter

Implements `System.Text.Json`'s `JsonConverter`. It will be used `JsonSerializer` automatically.  

`System.Text.Json`�� `JsonConverter` ���������Ă��܂��B�����I�� `JsonSerializer` ���g�p����܂��B  

```csharp
[JsonConverter(typeof(UserIdJsonConverter))]
public readonly partial struct UserId
{
    class UserIdJsonConverter : JsonConverter<UserId>
}
```


### JsonConverterDictionaryKeySupport

Implements `JsonConverter`'s `WriteAsPropertyName/ReadAsPropertyName`. It supports from .NET 6, supports Dictionary's Key.  

`JsonConverter` �� `WriteAsPropertyName/ReadAsPropertyName` ���������Ă��܂��B.NET 6����T�|�[�g����ADictionary��Key���T�|�[�g���Ă��܂��B  

```csharp
var dict = Dictionary<UserId, int>
JsonSerializer.Serialize(dict);
````


### DapperTypeHandler

Implements Dapper's TypeHandler by public accessibility. TypeHandler is automatically registered at the time of Module initialization.  

Dapper��TypeHandler���p�u���b�N�A�N�Z�V�r���e�B�Ŏ������Ă��܂��BTypeHandler�́AModule�̏��������Ɏ����I�ɓo�^����܂��B  

```csharp
public readonly partial struct UserId
{
    public class UserIdTypeHandler : Dapper.SqlMapper.TypeHandler<UserId>
}

[ModuleInitializer]
public static void AddTypeHandler()
{
    Dapper.SqlMapper.AddTypeHandler(new A.ATypeHandler());
}
```


### ~~MessagePackFormatter~~
<details><summary>Explanation in UnitGenerager (UnitGenerager�ł̐���)</summary>

Implements MessagePack for C#'s `MessagePackFormatter`. It will be used `MessagePackSerializer` automatically. 

C#�� `MessagePackFormatter` �p��MessagePack���������Ă��܂��B�����I�� `MessagePackSerializer` ���g�p����܂��B 

```csharp
[MessagePackFormatter(typeof(UserIdMessagePackFormatter))]
public readonly partial struct UserId
{
    class UserIdMessagePackFormatter : IMessagePackFormatter<UserId>
}
```
</details>


### ~~EntityFrameworkValueConverter~~
<details><summary>Explanation in UnitGenerager (UnitGenerager�ł̐���)</summary>  

Implements EntityFrameworkCore's ValueConverter by public accessibility. It is not registered automatically so you need to register manually.  

EntityFrameworkCore��ValueConverter���p�u���b�N�A�N�Z�V�r���e�B�Ŏ������܂��B�����I�ɂ͓o�^����Ȃ��̂ŁA�蓮�œo�^����K�v������B   

```csharp
public readonly partial struct UserId
{
    public class UserIdValueConverter : ValueConverter<UserId, int>
}

// setup handler manually
builder.HasConversion(new UserId.UserIdValueConverter());
```
</details>


### ~~Use for Unity~~
<details><summary>Explanation in UnitGenerager (UnitGenerager�ł̐���)</summary>

C# Source Generator feature is rely on C# 9.0. If you are using Unity 2021.2, that supports [Source Generators](https://docs.unity3d.com/2021.2/Documentation/Manual/roslyn-analyzers.html). Add the `UnitGenerator.dll` from the [releases page](https://github.com/Cysharp/UnitGenerator/releases), disable Any Platform, disable Include all platforms and set label as `RoslynAnalyzer`.  
It works in Unity Editor however does not work on IDE because Unity does not generate analyzer reference to `.csproj`. We provides [CsprojModifer](https://github.com/Cysharp/CsprojModifier) to analyzer support, uses `Add analyzer references to generated .csproj` supports both IDE and Unity Editor.  
Unity(2020) does not support C# 9.0 so can not use directly. However, C# Source Genertor supports output source as file.  

C#�\�[�X�W�F�l���[�^�@�\�́AC#9.0�Ɉˑ����Ă��܂��BUnity 2021.2���g�p���Ă���ꍇ�A[Source Generators](https://docs.unity3d.com/2021.2/Documentation/Manual/roslyn-analyzers.html)���T�|�[�g���Ă��܂��B�����[�X�y�[�W](https://github.com/Cysharp/UnitGenerator/releases)����`UnitGenerator.dll`��ǉ����AAny Platform�𖳌��ɂ��AInclude all platforms�𖳌��ɂ��A���x����`RoslynAnalyzer`�Ɛݒ肵�܂��B  
Unity Editor�ł͓��삵�܂����AIDE�ł�Unity���A�i���C�U�[�Q�Ƃ𐶐����Ȃ����߁A���삵�܂���BCsprojModifer](https://github.com/Cysharp/CsprojModifier)��񋟂��A�������ꂽ.csproj�ɃA�i���C�U�Q�Ƃ�ǉ����邱�ƂŁAIDE��Unity Editor�̗������T�|�[�g���܂��B  
Unity(2020)��C# 9.0���T�|�[�g���Ă��Ȃ����߁A���ڎg�p���邱�Ƃ͂ł��܂���B�������AC# Source Genertor�̓\�[�X���t�@�C���Ƃ��ďo�͂��邱�Ƃ��\�ł��B  


1. Create `UnitSourceGen.csproj`.

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>net5.0</TargetFramework>

        <!-- add this two lines and configure output path -->
        <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
        <CompilerGeneratedFilesOutputPath>$(ProjectDir)..\Generated</CompilerGeneratedFilesOutputPath>
    </PropertyGroup>

    <ItemGroup>
        <!-- reference UnitGenerator -->
        <PackageReference Include="UnitGenerator" Version="1.0.0" />

        <!-- add target sources path from Unity -->
        <Compile Include="..\MyUnity\Assets\Scripts\Models\**\*.cs" />
    </ItemGroup>
</Project>
```

2. install [.NET SDK](https://dotnet.microsoft.com/download) and run this command.
</details>


generated file and folder
---

```
dotnet build UnitSourceGen.csproj
```

File will be generated under `NullableUnitGenerator\NullableUnitGenerator.SourceGenerator\*.g.cs`. `UnitOfAttribute` is also included in generated folder, so at first, run build command and get attribute to configure.  

�t�@�C���� `NullableUnitGenerator\NullableUnitGenerator.SourceGenerator\*.g.cs` �Ƃ��Đ�������܂��B�������ꂽ�t�H���_�ɂ�`UnitOfAttribute`���܂܂�Ă���̂ŁA�܂���build�R�}���h�����s���đ������擾���A�ݒ肵�܂��B  


License
---
This library is under the MIT License.  

�{���C�u�����́AMIT���C�Z���X�̂��ƂŒ񋟂���Ă��܂��B   
