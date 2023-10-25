using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace NullableUnitGeneratorSample.Sandbox.EntityFrameworkApp;


[UnitOf(typeof(int), UGO.ParseMethod | UGO.EntityFrameworkValueConverter)]
public readonly partial struct UserId { }
