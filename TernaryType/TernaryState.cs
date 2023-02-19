#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;

namespace Estable.TernaryType;


/// <summary>
/// TernaryState
/// </summary>
[Flags]
public enum TernaryState
{
    /// <summary>Undefined State</summary>
    Undef = 0,

    /// <summary>Null State</summary>
    Null = 1,

    /// <summary>Value State</summary>
    Value = 3,
}

