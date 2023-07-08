using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace NullableUnitGeneratorSample.Sandbox.ConsoleApp;


[UnitOf(typeof(Guid), UGO.IComparable)]
public readonly partial struct FooId { }


[UnitOf(typeof(Ulid), UGO.IComparable | UGO.MessagePackFormatter | UGO.JsonConverter)]
public readonly partial struct BarId { }


[UnitOf(typeof(int), UGO.ArithmeticOperator | UGO.ValueArithmeticOperator | UGO.IComparable | UGO.ComparisonOperator | UGO.MinMaxMethod | UGO.JsonConverter)]
public readonly partial struct Hp
{
    void Foo()
    {
        _ = this.AsPrimitive();
        _ = this.ToString();

        _ = FooId.NewFooId();
        Guid.NewGuid();
    }
}

[UnitOf(typeof(int), UGO.MessagePackFormatter)]
public readonly partial struct UserId { }


[UnitOf(typeof(int))]
public readonly partial struct SampleValidate
{
    // impl here.
    partial void ValidateInConstructor()
    {
        if (_value > 9999) throw new Exception("Invalid value range: " + _value);
    }
}

[UnitOf(typeof(int), UGO.MessagePackFormatter)]
public readonly partial struct UserId2
{
    public void Foo()
    {
        _ = AsPrimitive();
    }
}


[UnitOf(typeof(string), UGO.ParseMethod)]
public readonly partial struct StringId { }

