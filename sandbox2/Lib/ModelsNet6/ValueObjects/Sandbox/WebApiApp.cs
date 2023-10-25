using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace NullableUnitGeneratorSample.Sandbox.WebApiApp;


[UnitOf(typeof(int), UGO.ParseMethod | UGO.EntityFrameworkValueConverter)]
public readonly partial struct UserId { }
