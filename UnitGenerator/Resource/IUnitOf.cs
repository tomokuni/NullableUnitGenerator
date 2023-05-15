
namespace NullableUnitGenerator;


/// <summary>
/// UnitOf属性を付けたクラスに付与されるインターフェイス
/// </summary>
public interface IUnitOf
{
    public bool IsUndef { get; }
    public bool IsNull { get; }
    public bool IsUndefOrNull { get; }
    public bool HasValue { get; }
    public UnitState State { get; }
}
