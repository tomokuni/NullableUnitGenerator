﻿using Sample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenOpts;

//var a = UGO.JsonConverterDictionaryKey;

//var has = UGO.JsonConverterDictionaryKey.HasFlag(UGO.Validate);
//Console.WriteLine(has);

var json = JsonSerializer.Serialize(new Dictionary<Guid, string> { { Guid.NewGuid(), "hogemoge" } });



Console.WriteLine(json);

int cnt = 10000000;
var sw = new Stopwatch();
sw.Start();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999);
    var b = Random.Shared.Next(999999999);
    sw.Start();
    var res = a == b;
}
sw.Stop();
Console.WriteLine($"数値比較 : {sw.ElapsedMilliseconds}");

sw.Restart();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999).ToString();
    var b = Random.Shared.Next(999999999).ToString();
    sw.Start();
    var res = a == b;
}
sw.Stop();
Console.WriteLine($"文字列比較 : {sw.ElapsedMilliseconds}");

sw.Restart();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999);
    var b = Random.Shared.Next(999999999);
    sw.Start();
    var res = a.ToString() == b.ToString();
}
sw.Stop();
Console.WriteLine($"文字列化比較 : {sw.ElapsedMilliseconds}");



//[UnitOf(typeof(int))]
//public readonly partial struct NoNamespace
//{
//}

[UnitOf(typeof(Guid), UGO.IComparable)]
public readonly partial struct FooId { }

[UnitOf(typeof(Ulid), UGO.IComparable | UGO.MessagePackFormatter | UGO.JsonConverter | UGO.JsonConverterDictionaryKey)]
public readonly partial struct BarId { }

namespace Sample
{

    [UnitOf(typeof(int), UGO.ArithmeticOperator | UGO.ValueArithmeticOperator | UGO.IComparable | UGO.ComparisonOperator | UGO.MinMaxMethod | UGO.JsonConverter | UGO.JsonConverterDictionaryKey)]
    public readonly partial struct Hp
    {
        // public static Hp operator +(in Hp x, in Hp y) => new Hp(checked((int)(x.value + y.value)));

        void Foo()
        {
            _ = this.AsPrimitive();
            _ = this.ToString();

            _ = FooId.NewFooId();
            Guid.NewGuid();
            //public static readonly Guid Empty;
            //Guid.Empty

            // public static readonly Ulid Empty = default(Ulid);
            // Ulid.Empty


        }

    }

    [UnitOf(typeof(int), UGO.MessagePackFormatter)]
    public readonly partial struct UserId { }


    [UnitOf(typeof(int))]
    public readonly partial struct SampleValidate
    {
        // impl here.
        partial void ValidationWithCustomCode(ref List<string> refMsg)
        {
            if (m_value > 9999) throw new Exception("Invalid value range: " + m_value);
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
}

