using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

using UnitGenerator.Tests;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace NullableUnitGenerator.Tests;


public class UnitOfAttributeTest
{
    //[Fact]
    //public void Range_int()
    //{
    //    var model = new Entity()
    //    {
    //        Id = new VoIntRange(1),
    //        Id2 = new VoLongRange(1L),
    //        Id3 = new VoFloatRange(1.0f),
    //        Id4 = new VoDoubleRange(1.0),
    //        Id5 = new VoDecimalRange(1m),
    //    };
    //    var results = new List<ValidationResult>();
    //    var context = new ValidationContext(model);
    //    Validator.TryValidateObject(model, context, results, true);
    //    Assert.Equal(5, results.Count);

    //    var model2 = new Entity()
    //    {
    //        Id = new VoIntRange(2),
    //        Id2 = new VoLongRange(2L),
    //        Id3 = new VoFloatRange(2.0f),
    //        Id4 = new VoDoubleRange(2.0),
    //        Id5 = new VoDecimalRange(2m),
    //    };
    //    var results2 = new List<ValidationResult>();
    //    var context2 = new ValidationContext(model2);
    //    Validator.TryValidateObject(model2, context2, results2, true);
    //    Assert.Empty(results2);

    //    var model3 = new Entity()
    //    {
    //        Id = new VoIntRange(3),
    //        Id2 = new VoLongRange(3L),
    //        Id3 = new VoFloatRange(3.0f),
    //        Id4 = new VoDoubleRange(3.0),
    //        Id5 = new VoDecimalRange(3m),
    //    };
    //    var results3 = new List<ValidationResult>();
    //    var context3 = new ValidationContext(model3);
    //    Validator.TryValidateObject(model3, context3, results3, true);
    //    Assert.Equal(5, results3.Count);
    //}

    //public class Entity
    //{
    //    [DisplayName("ID")]
    //    [UnitOfRange(1, 2), UnitOfOasValidate(ErrorMessage = "入力に誤りがあります")]
    //    public VoIntRange Id { get; set; }

    //    [DisplayName("ID2")]
    //    [UnitOfRange(1, 2), UnitOfOasValidate]
    //    public VoLongRange Id2 { get; set; }

    //    [DisplayName("ID3")]
    //    [UnitOfRange(1, 2), UnitOfOasValidate]
    //    public VoFloatRange Id3 { get; set; }

    //    [DisplayName("ID4")]
    //    [UnitOfRange(1, 2), UnitOfOasValidate]
    //    public VoDoubleRange Id4 { get; set; }
        
    //    [DisplayName("ID5")]
    //    [UnitOfRange(1, 2), UnitOfOasValidate]
    //    public VoDecimalRange Id5 { get; set; }
    //}

}


//[UnitOf(typeof(int), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
//[UnitOfOas("integer", range: "2-3")]
//public readonly partial struct VoIntRange { }


//[UnitOf(typeof(long), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
//[UnitOfOas("integer", range: "2-3")]
//public readonly partial struct VoLongRange { }


//[UnitOf(typeof(double), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
//[UnitOfOas("number", range: "2-3")]
//public readonly partial struct VoDoubleRange { }


//[UnitOf(typeof(float), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
//[UnitOfOas("number", range: "2-3")]
//public readonly partial struct VoFloatRange { }


//[UnitOf(typeof(decimal), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
//[UnitOfOas("number", range: "2-3")]
//public readonly partial struct VoDecimalRange { }

