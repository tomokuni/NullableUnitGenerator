using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NullableUnitGenerator;

namespace ForReadme;


[UnitOf(typeof(int))]
public readonly partial struct UserId { }


[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator
                   | UnitGenerateOptions.ParseMethod
                   | UnitGenerateOptions.MinMaxMethod
                   | UnitGenerateOptions.ArithmeticOperator
                   | UnitGenerateOptions.ValueArithmeticOperator
                   | UnitGenerateOptions.Comparable)]
public readonly partial struct Hp { }
