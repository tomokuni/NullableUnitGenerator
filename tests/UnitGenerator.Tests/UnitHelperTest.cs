using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace NullableUnitGenerator.Tests;


public class UnitHelperTest
{
    [Fact]
    public void GetTypeAndAttributes()
    {
        var unitAttrs = UnitHelper.GetTypeAndAttributes<UnitOfAttribute>();
        Assert.NotEmpty(unitAttrs);

        var oasAttrs = UnitHelper.GetTypeAndAttributes<UnitOfOasAttribute>();
        Assert.NotEmpty(oasAttrs);
    }


    [Fact]
    public void ExcludeUndef()
    {
        // ExpandoObject
        var expandoSrc1 = new ExpandoObject() as IDictionary<string, dynamic>;
        expandoSrc1.Add("VoIntUndef", new VoInt());
        expandoSrc1.Add("VoIntNull", new VoInt((int?)null));
        expandoSrc1.Add("VoIntValue", new VoInt(1));

        // Anonymous
        var anonymousSrc2 = new
        {
            VoIntUndef = new VoInt(),
            VoIntNull = new VoInt((int?)null),
            VoIntValue = new VoInt(1)
        };

        var expandoActual1 = UnitHelper.ExcludeUndef(expandoSrc1);
        var anonymousActual2 = UnitHelper.ExcludeUndef(anonymousSrc2);
        Assert.Equal(expandoActual1, anonymousActual2);
        Assert.Equal(expandoActual1.VoIntNull, new VoInt((int?)null));
        Assert.Equal(expandoActual1.VoIntValue, new VoInt(1));
        var ex = Assert.Throws<RuntimeBinderException>(() => expandoActual1.VoIntUndef);
        Assert.Equal("'System.Dynamic.ExpandoObject' does not contain a definition for 'VoIntUndef'", ex.Message);


        // Class
        var classSrc3 = new ClassSrc3
        {
            VoIntUndef = new VoInt(),
            VoIntNull = new VoInt((int?)null),
            VoIntValue = new VoInt(1)
        };
        var classActual3 = UnitHelper.ExcludeUndef(classSrc3);
        Assert.Equal(expandoActual1, classActual3);

        // Struct
        var structSrc4 = new StructSrc4
        {
            VoIntUndef = new VoInt(),
            VoIntNull = new VoInt((int?)null),
            VoIntValue = new VoInt(1)
        };
        var structActual4 = UnitHelper.ExcludeUndef(structSrc4);
        Assert.Equal(expandoActual1, structActual4);

        // Record
        var recordSrc5 = new RecordSrc5
        {
            VoIntUndef = new VoInt(),
            VoIntNull = new VoInt((int?)null),
            VoIntValue = new VoInt(1)
        };
        var recordActual5 = UnitHelper.ExcludeUndef(recordSrc5);
        Assert.Equal(expandoActual1, recordActual5);

        // RecordStruct
        var recordStructSrc6 = new RecordStructSrc6
        {
            VoIntUndef = new VoInt(),
            VoIntNull = new VoInt((int?)null),
            VoIntValue = new VoInt(1)
        };
        var recordStructActual6 = UnitHelper.ExcludeUndef(recordStructSrc6);
        Assert.Equal(expandoActual1, recordStructActual6);
    }

    private class ClassSrc3
    {
        public VoInt VoIntUndef { get; set; } = new VoInt();
        public VoInt VoIntNull { get; set; } = new VoInt((int?)null);
        public VoInt VoIntValue { get; set; } = new VoInt(1);
    };

    private class StructSrc4
    {
        public VoInt VoIntUndef { get; set; } = new VoInt();
        public VoInt VoIntNull { get; set; } = new VoInt((int?)null);
        public VoInt VoIntValue { get; set; } = new VoInt(1);
    };

    private class RecordSrc5
    {
        public VoInt VoIntUndef { get; set; } = new VoInt();
        public VoInt VoIntNull { get; set; } = new VoInt((int?)null);
        public VoInt VoIntValue { get; set; } = new VoInt(1);
    };

    private class RecordStructSrc6
    {
        public VoInt VoIntUndef { get; set; } = new VoInt();
        public VoInt VoIntNull { get; set; } = new VoInt((int?)null);
        public VoInt VoIntValue { get; set; } = new VoInt(1);
    };

}