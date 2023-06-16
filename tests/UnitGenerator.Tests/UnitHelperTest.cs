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


    [Fact]
    public void ToPascalCase()
    {
        var inputs = new Dictionary<string, string>
        {
            { "Welcome to the Maze", "WelcomeToTheMaze" },
            { "Welcome To THE Maze", "WelcomeToTheMaze" },
            { "Welcome TO The MAZE", "WelcomeToTheMaze" },
            { "Welcome_To_THE_Maze", "WelcomeToTheMaze" },
            { "Welcome_TO_The_MAZE", "WelcomeToTheMaze" },
            { "WelcomeToTHEMaze", "WelcomeToTheMaze" },
            { "WelcomeTOTheMAZE", "WelcomeToTheMaze" },
            { "ISODateTime", "IsoDateTime" },
            { "ISODateTIME", "IsoDateTime" },
            { "IOStreamFILE", "IoStreamFile" },
            { "IoSTREAMFile", "IoStreamFile" },
            { "IOStream2FILE", "IoStream2File" },
            { "IoSTREAM2File", "IoStream2File" },
            { "IoSTREAMFile2", "IoStreamFile2" },
            { "TELNO1", "Telno1" },
            { "1TELNO", "1Telno" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = UnitHelper.ToPascalCase(src);
            Assert.Equal(expect, actual);
        }
    }


    [Fact]
    public void ToCamelCase()
    {
        var inputs = new Dictionary<string, string>
        {
            { "Welcome to the Maze", "welcomeToTheMaze" },
            { "Welcome To THE Maze", "welcomeToTheMaze" },
            { "Welcome TO The MAZE", "welcomeToTheMaze" },
            { "Welcome_To_THE_Maze", "welcomeToTheMaze" },
            { "Welcome_TO_The_MAZE", "welcomeToTheMaze" },
            { "WelcomeToTHEMaze", "welcomeToTheMaze" },
            { "WelcomeTOTheMAZE", "welcomeToTheMaze" },
            { "WelcomeTTheMAZE", "welcomeTTheMaze" },
            { "ISODateTime", "isoDateTime" },
            { "ISODateTIME", "isoDateTime" },
            { "IOStreamFILE", "ioStreamFile" },
            { "IoSTREAMFile", "ioStreamFile" },
            { "IOStream2FILE", "ioStream2File" },
            { "IoSTREAM2File", "ioStream2File" },
            { "IoSTREAMFile2", "ioStreamFile2" },
            { "TELNO1", "telno1" },
            { "1TELNO", "1Telno" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = UnitHelper.ToCamelCase(src);
            Assert.Equal(expect, actual);
        }
    }


    [Fact]
    public void ToSnakeCase()
    {
        var inputs = new Dictionary<string, string>
        {
            { "Welcome to the Maze", "welcome_to_the_maze" },
            { "Welcome To The Maze", "welcome_to_the_maze" },
            { "Welcome TO The Maze", "welcome_to_the_maze" },
            { "WelcomeToTheMaze", "welcome_to_the_maze" },
            { "WelcomeTOTheMaze", "welcome_to_the_maze" },
            { "Welcome_To_The_Maze", "welcome_to_the_maze" },
            { "Welcome_TO_The_Maze", "welcome_to_the_maze" },
            { "ISODateTime", "iso_date_time" },
            { "ISODateTIME", "iso_date_time" },
            { "IOStreamFILE", "io_stream_file" },
            { "IoSTREAMFile", "io_stream_file" },
            { "IOStream2FILE", "io_stream_2_file" },
            { "IoSTREAM2File", "io_stream_2_file" },
            { "IoSTREAMFile2", "io_stream_file_2" },
            { "TELNO1", "telno_1" },
            { "1TELNO", "1_telno" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = UnitHelper.ToSnakeCase(src);
            Assert.Equal(expect, actual);
        }
    }


    [Fact]
    public void ToKebabCase()
    {
        var inputs = new Dictionary<string, string>
        {
            { "Welcome to the Maze", "welcome-to-the-maze" },
            { "Welcome To The Maze", "welcome-to-the-maze" },
            { "Welcome TO The Maze", "welcome-to-the-maze" },
            { "WelcomeToTheMaze", "welcome-to-the-maze" },
            { "WelcomeTOTheMaze", "welcome-to-the-maze" },
            { "Welcome_To_The_Maze", "welcome-to-the-maze" },
            { "Welcome_TO_The_Maze", "welcome-to-the-maze" },
            { "ISODateTime", "iso-date-time" },
            { "ISODateTIME", "iso-date-time" },
            { "IOStreamFILE", "io-stream-file" },
            { "IoSTREAMFile", "io-stream-file" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = UnitHelper.ToSnakeCase(src, "-");
            Assert.Equal(expect, actual);
        }
    }

}