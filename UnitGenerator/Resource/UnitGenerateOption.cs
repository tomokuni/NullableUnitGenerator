using System;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator の生成オプション
/// </summary>
[Flags]
public enum UnitGenerateOption
{
    /// <summary>None</summary>
    None = 0,

    /// <summary>
    /// Maximum optional specification of Numeric types independent of other packages<br/>
    /// 他のパッケージに依存しない最大限のオプション指定<br/><br/>
    /// If an error occurs with this option, remove the option that causes the error and specify it separately<br/>
    /// このオプションを指定してエラーが発生する場合は、エラーが発生するオプションを外して個別に指定する
    /// </summary>
    GeneralOptions = ImplicitOperator | IComparable | ComparisonOperator | ArithmeticOperator | ValueArithmeticOperator | ParseMethod | MinMaxMethod,

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
    /// Option to implement arithmetic operators ( + )<br/>
    /// 算術演算子( + ) を実装するオプション
    /// </summary>
    ArithmeticAddOperator = 0b0000_0000_0001_0000,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( - )<br/>
    /// 算術演算子( - ) を実装するオプション
    /// </summary>
    ArithmeticSubOperator = 0b0000_0000_0010_0000,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( * )<br/>
    /// 算術演算子( * ) を実装するオプション
    /// </summary>
    ArithmeticMulOperator = 0b0000_0000_0100_0000,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( / )<br/>
    /// 算術演算子( / ) を実装するオプション
    /// </summary>
    ArithmeticDivOperator = 0b0000_0000_1000_0000,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( % )<br/>
    /// 算術演算子( % ) を実装するオプション
    /// </summary>
    ArithmeticModOperator = 0b0000_0001_0000_0000,

    /// <summary>
    /// <b>ArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( +, -, *, /, % )<br/>
    /// 算術演算子( +, -, *, /, % ) を実装するオプション
    /// </summary>
    ArithmeticOperator = ArithmeticAddOperator | ArithmeticSubOperator | ArithmeticMulOperator | ArithmeticDivOperator | ArithmeticModOperator,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( ++ ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( ++ ) を実装するオプション
    /// </summary>
    ValueArithmeticIncOperator = 0b0000_0010_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( -- ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( -- ) を実装するオプション
    /// </summary>
    ValueArithmeticDecOperator = 0b0000_0100_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( + ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( + ) を実装するオプション
    /// </summary>
    ValueArithmeticAddOperator = 0b0000_1000_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( - ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( - ) を実装するオプション
    /// </summary>
    ValueArithmeticSubOperator = 0b0001_0000_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( * ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( * ) を実装するオプション
    /// </summary>
    ValueArithmeticMulOperator = 0b0010_0000_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( / ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( / ) を実装するオプション
    /// </summary>
    ValueArithmeticDivOperator = 0b0100_0000_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( % ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( % ) を実装するオプション
    /// </summary>
    ValueArithmeticModOperator = 0b1000_0000_0000_0000,

    /// <summary>
    /// <b>ValueArithmeticOperator</b><br/>
    /// Option to implement arithmetic operators ( ++, --, +, -, *, /, % ) with the original numeric type<br/>
    /// 元の数値型との算術演算子( ++, --, +, -, *, /, % ) を実装するオプション
    /// </summary>
    ValueArithmeticOperator = ValueArithmeticIncOperator | ValueArithmeticDecOperator | ValueArithmeticAddOperator | ValueArithmeticSubOperator | ValueArithmeticMulOperator | ValueArithmeticDivOperator | ValueArithmeticModOperator,

    /// <summary>
    /// <b>ParseMethod</b><br/>
    /// Option to implement Parse, TryParse methods<br/>
    /// Parse, TryParse メソッドを実装するオプション
    /// </summary>
    ParseMethod = 0b0000_0000_0000_0001_0000_0000_0000_0000,

    /// <summary>
    /// <b>MinMaxMethod</b><br/>
    /// Option to implement Min, Max methods<br/>
    /// Min, Max メソッドを実装するオプション
    /// </summary>
    MinMaxMethod = 0b0000_0000_0000_0010_0000_0000_0000_0000,

    /// <summary>
    /// <b>JsonConverter</b>
    /// </summary>
    JsonConverter = 0b0000_0000_0001_0000_0000_0000_0000_0000,

    /// <summary>
    /// <b>MessagePackFormatter</b>
    /// </summary>
    MessagePackFormatter = 0b0000_0000_0010_0000_0000_0000_0000_0000,

    /// <summary>
    /// <b>DapperTypeHandler</b>
    /// </summary>
    DapperTypeHandler = 0b0000_0001_0000_0000_0000_0000_0000_0000,

    /// <summary>
    /// <b>EntityFrameworkValueConverter</b>
    /// </summary>
    EntityFrameworkValueConverter = 0b0000_0010_0000_0000_0000_0000_0000_0000,
}
