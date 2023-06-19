﻿using System;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator の生成オプション
/// </summary>
[Flags]
public enum UnitGenOpts
{
    /// <summary>None</summary>
    None = 0,

    /// <summary>
    /// Maximum optional specification of Numeric types independent of other packages<br/>
    /// If an error occurs with this option, remove the option that causes the error and specify it separately<br/><br/>
    /// 他のパッケージに依存しない最大限のオプション指定<br/>
    /// このオプションを指定してエラーが発生する場合は、エラーが発生するオプションを外して個別に指定する<br/><br/>
    /// <b>MaxExtent</b> = ImplicitOperator | IComparable | ComparisonOperator | ArithmeticOperator | ValueArithmeticOperator | ParseMethod | MinMaxMethod
    /// </summary>
    MaxExtent = ImplicitOperator | IComparable | ComparisonOperator | ArithmeticOperator | ValueArithmeticOperator | ParseMethod | MinMaxMethod,

    /// <summary>
    /// Maximum optional specification of DateTime type independent of other packages<br/>
    /// If an error occurs with this option, remove the option that causes the error and specify it separately<br/><br/>
    /// DateTime 系の型に対する、他のパッケージに依存しない最大限のオプション指定<br/>
    /// このオプションを指定してエラーが発生する場合は、エラーが発生するオプションを外して個別に指定する<br/><br/>
    /// <b>MaxExtentForDateTime</b> = ImplicitOperator | IComparable | ComparisonOperator | ParseMethod
    /// </summary>
    MaxExtentForDateTime = ImplicitOperator | IComparable | ComparisonOperator | ParseMethod,

    /// <summary>
    /// <b>ImplicitOperator</b><br/>
    /// Option to implement the implicit conversion operator<br/>
    /// 暗黙的変換演算子を実装するオプション
    /// </summary>
    ImplicitOperator = 0b0000_0000_0000_0001,

    /// <summary>
    /// <b>IComparable</b><br/>
    /// Option to implement the IComparable interface (CompareTo method)<br/>
    /// IComparable インターフェイス (CompareTo メソッド) を実装するオプション
    /// </summary>
    IComparable = 0b0000_0000_0000_0010,

    /// <summary>
    /// <b>ComparisonOperator</b><br/>
    /// Option to implement comparison operators ( >, &lt;, >=, &lt;= )<br/>
    /// 比較演算子( >, &lt;, >=, &lt;= ) を実装するオプション
    /// </summary>
    ComparisonOperator = 0b0000_0000_0000_0100,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( +, -, *, /, % )<br/>
    /// 算術演算子( +, -, *, /, % ) を実装するオプション
    /// </summary>
    ArithmeticOperator = 0b0000_0000_0000_1000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( ++, --, +, -, *, /, % ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( ++, --, +, -, *, /, % ) を実装するオプション
    /// </summary>
    ValueArithmeticOperator = 0b0000_0000_0001_0000,

    /// <summary>
    /// <b>ParseMethod</b><br/>
    /// Option to implement Parse, TryParse methods<br/>
    /// Parse, TryParse メソッドを実装するオプション
    /// </summary>
    ParseMethod = 0b0000_0000_0010_0000,

    /// <summary>
    /// <b>MinMaxMethod</b><br/>
    /// Option to implement Min, Max methods<br/>
    /// Min, Max メソッドを実装するオプション
    /// </summary>
    MinMaxMethod = 0b0000_0000_0100_0000,

    /// <summary>
    /// <b>JsonConverter</b>
    /// </summary>
    JsonConverter = 0b0000_0000_0000_0001_0000_0000_0000_0000,

    /// <summary>
    /// <b>JsonConverterDictionaryKey</b>
    /// </summary>
    JsonConverterDictionaryKey = 0b0000_0000_0000_0010_0000_0000_0000_0000,

    /// <summary>
    /// <b>MessagePackFormatter</b>
    /// </summary>
    MessagePackFormatter = 0b0000_0000_0000_0100_0000_0000_0000_0000,

    /// <summary>
    /// <b>DapperTypeHandler</b>
    /// </summary>
    DapperTypeHandler = 0b0000_0000_0001_0000_0000_0000_0000_0000,

    /// <summary>
    /// <b>EntityFrameworkValueConverter</b>
    /// </summary>
    EntityFrameworkValueConverter = 0b0000_0000_0010_0000_0000_0000_0000_0000,
}
