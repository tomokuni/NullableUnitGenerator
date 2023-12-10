using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Xunit;

using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;
using UST = NullableUnitGenerator.UnitSchemaType;

namespace NullableUnitGenerator.Tests;


public class UnitValidateTest
{
    [Fact]
    public void Range_int()
    {
        var model = new Entity()
        {
            Id = new VoIntRange(1),
            Id2 = new VoLongRange(1L),
            Id3 = new VoFloatRange(1.0f),
            Id4 = new VoDoubleRange(1.0),
            Id5 = new VoDecimalRange(1m),
            Id7 = new VoDatetimeRange(new DateTime(2023, 12, 01, 00, 00, 00)),
            Id8 = new VoDateonlyRange(new DateOnly(2023, 12, 01)),
            Id9 = new VoTimeonlyRange(new TimeOnly(23, 59, 59)),
            Id10 = new VoTimespanRange(new TimeSpan(23, 59, 59)),
        };
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, true);
        Assert.Equal(9, results.Count);



        var model2 = new Entity()
        {
            Id = new VoIntRange(2),
            Id2 = new VoLongRange(2L),
            Id3 = new VoFloatRange(2.0f),
            Id4 = new VoDoubleRange(2.0),
            Id5 = new VoDecimalRange(2m),
            Id7 = new VoDatetimeRange(new DateTime(2022, 12, 01, 00, 00, 00)),
            Id8 = new VoDateonlyRange(new DateOnly(2022, 12, 01)),
            Id9 = new VoTimeonlyRange(new TimeOnly(00, 59, 59)),
            Id10 = new VoTimespanRange(new TimeSpan(00, 59, 59)),
        };
        var results2 = new List<ValidationResult>();
        var context2 = new ValidationContext(model2);
        Validator.TryValidateObject(model2, context2, results2, true);
        Assert.Empty(results2);

        var model3 = new Entity()
        {
            Id = new VoIntRange(3),
            Id2 = new VoLongRange(3L),
            Id3 = new VoFloatRange(3.0f),
            Id4 = new VoDoubleRange(3.0),
            Id5 = new VoDecimalRange(3m),
            Id6 = new VoStringRange("3"),
            Id7 = new VoDatetimeRange(new DateTime(2023,12,01,00,00,00)),
            Id8 = new VoDateonlyRange(new DateOnly(2023, 12, 01)),
            Id9 = new VoTimeonlyRange(new TimeOnly(23, 59, 59)),
            Id10 = new VoTimespanRange(new TimeSpan(23, 59, 59)),
        };
        var results3 = new List<ValidationResult>();
        var context3 = new ValidationContext(model3);
        Validator.TryValidateObject(model3, context3, results3, true);
        Assert.Equal(9, results3.Count);
    }

    public class Entity
    {
        [DisplayName("ID")]
        [UnitOfSchemaValidateRange()]
        public VoIntRange Id { get; set; }

        [DisplayName("ID2")]
        [UnitOfRange(1, 2), UnitOfOasValidate]
        public VoLongRange Id2 { get; set; }

        [DisplayName("ID3")]
        [UnitOfRange(1, 2), UnitOfOasValidate]
        public VoFloatRange Id3 { get; set; }

        [DisplayName("ID4")]
        [UnitOfRange(1, 2), UnitOfOasValidate]
        public VoDoubleRange Id4 { get; set; }

        [DisplayName("ID5")]
        [UnitOfRange(1, 2), UnitOfOasValidate]
        public VoDecimalRange Id5 { get; set; }

        [DisplayName("ID6")]
        [UnitOfOasValidate]
        public VoStringRange Id6 { get; set; }

        [DisplayName("ID7")]
        [UnitOfOasValidate]
        public VoDatetimeRange Id7 { get; set; }

        [DisplayName("ID8")]
        [UnitOfOasValidate]
        public VoDateonlyRange Id8 { get; set; }

        [DisplayName("ID9")]
        [UnitOfOasValidate]
        public VoTimeonlyRange Id9 { get; set; }

        [DisplayName("ID10")]
        [UnitOfOasValidate]
        public VoTimespanRange Id10 { get; set; }
    }

}


[UnitOf(typeof(int), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Int, Minimum = 2, Maximum = 3)]
public readonly partial struct VoIntRange { }


[UnitOf(typeof(long), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Int), UnitOfOasRange(range: "2~3")]
public readonly partial struct VoLongRange { }


[UnitOf(typeof(double), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Number), UnitOfOasRange(range: "2~3")]
public readonly partial struct VoDoubleRange { }


[UnitOf(typeof(float), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Number), UnitOfOasRange(range: "2~3")]
public readonly partial struct VoFloatRange { }


[UnitOf(typeof(decimal), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Number), UnitOfOasRange(range: "2~3")]
public readonly partial struct VoDecimalRange { }


[UnitOf(typeof(string), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.String), UnitOfOasRange(range: "2~3")]
public readonly partial struct VoStringRange { }


[UnitOf(typeof(DateTime), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Datetime), UnitOfOasRange(range: "2022-12-01T00:00:00.000+09:00~2022-12-31T12:00:00.999+09:00")]
public readonly partial struct VoDatetimeRange { }


[UnitOf(typeof(DateOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Date), UnitOfOasRange(range: "2022-12-01~2022-12-31")]
public readonly partial struct VoDateonlyRange { }


[UnitOf(typeof(TimeOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Time), UnitOfOasRange(range: "00:00:00.000~12:00:00.999")]
public readonly partial struct VoTimeonlyRange { }


[UnitOf(typeof(TimeSpan), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfSchema(UST.Time), UnitOfOasRange(range: "00:00:00.000~12:00:00.999")]
public readonly partial struct VoTimespanRange { }
