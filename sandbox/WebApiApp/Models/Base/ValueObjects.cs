using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using NullableUnitGenerator;

namespace WebApiApp.Models.Base;

//[UnitOf(typeof(int), UGO.ParseMethod | UGO.MinMaxMethod | UGO.ArithmeticOperator | UGO.ValueArithmeticOperator | UGO.Comparable)]
//[JsonConverter(typeof(VoIntJsonConverter))]
public readonly partial struct VoInt
{ }

[UnitOf(typeof(double), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.MinMaxMethod | UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.IComparable | UnitGenerateOptions.ComparisonOperator | UnitGenerateOptions.JsonConverterSupport | UnitGenerateOptions.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDouble
{ }

[UnitOf(typeof(decimal), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.MinMaxMethod | UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.IComparable | UnitGenerateOptions.ComparisonOperator | UnitGenerateOptions.JsonConverterSupport | UnitGenerateOptions.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDecimal
{ }

[UnitOf(typeof(DateTime), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.IComparable | UnitGenerateOptions.ComparisonOperator | UnitGenerateOptions.JsonConverterSupport | UnitGenerateOptions.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDatetime
{ }

[UnitOf(typeof(string), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.JsonConverterSupport | UnitGenerateOptions.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoString
{ }
