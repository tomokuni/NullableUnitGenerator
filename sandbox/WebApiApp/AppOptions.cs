namespace WebApiApp;


/// <summary>
/// appsettings.json に記載しているオプション格納用クラス
/// </summary>
public class AppOptions
{
    /// <summary>オプション記載のタグ名</summary>
    public const string Section = "AppOptions";

    /// <summary>URLのパスベース</summary>
    public string PathBase { get; set; } = "";

    /// <summary>DB接続文字列</summary>
    public string DbConnectionString { get; set; } = "";
}
