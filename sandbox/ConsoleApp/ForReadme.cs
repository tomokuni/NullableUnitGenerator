using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ForReadme;


[UnitOf(typeof(int))]
public readonly partial struct UserId { }


[UnitOf(typeof(int), UGO.ImplicitOperator
                   | UGO.ParseMethod
                   | UGO.MinMaxMethod
                   | UGO.ArithmeticOperator
                   | UGO.ValueArithmeticOperator
                   | UGO.Comparable)]
public readonly partial struct Hp { }
