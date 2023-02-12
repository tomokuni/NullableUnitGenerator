using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。

namespace TernaryUnitGenerator.Ternary;


/// <summary>
/// ITernaryState
/// </summary>
public interface ITernaryState
{
    /// <summary>undefined value instance.</summary>
    public static ITernaryState UndefinedValue { get; }
    //public static Option UndefinedValue
    //    => new();

    /// <summary>null value instance.</summary>
    public static ITernaryState NullValue { get; }
    //public static Option NullValue
    //    => new((T?)null);

    /// <summary>default value instance.</summary>
    public static ITernaryState DefaultValue { get; }
    //public static Option DefaultValue
    //    => new(default(T));

    /// <summary>default value instance.</summary>
    public static Type BaseType { get; }
    //public static Type BaseType
    //    => typeof(T);


    internal const byte UNDEFINED_VALUE = 0;
    internal const byte NULL_VALUE = 1;
    internal const byte HAS_VALUE = 3;

    public const string UNDEFINED_STRING = "<undefined>";
    public const string NULL_STRING = "<null>";


    //internal readonly T m_value = default(T);
    //internal readonly byte m_state = UNDEFINED_VALUE;


    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndefined { get; } // => m_state == UNDEFINED_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull { get; } // => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue { get; }
    //    => m_state == HAS_VALUE;

    /// <summary>return value state (UNDEFINED_VALUE or NULL_VALUE or HAS_VALUE).</summary>
    /// <returns><b>UNDEFINED_VALUE</b><br/><b>NULL_VALUE</b><br/><b>HAS_VALUE</b></returns>
    public byte State { get; }

    /// <summary>return value if HasValue is true; &lt;null&gt; if IsNull is true; &lt;undefined&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;null&gt;</b> : if IsNull is true<br/><b>&lt;undefined&gt;</b> : if IsUndefined is true</returns>
    public string ValueString { get; }

    /// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    /// <returns>true if obj is an instance of <#= Type #> or <#= Name #> and equals the value of this instance; otherwise, false.</returns>
    public bool Equals(object? obj);
}
