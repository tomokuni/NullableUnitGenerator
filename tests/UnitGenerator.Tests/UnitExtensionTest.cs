using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;

using Microsoft.CSharp.RuntimeBinder;

using Xunit;

namespace NullableUnitGenerator.Tests;


public class UnitExtensionTest
{

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
            var actual = src.ToPascalCase();
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
            var actual = src.ToCamelCase();
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
            { "IOStream2FILE", "io_stream2_file" },
            { "IoSTREAM2File", "io_stream2_file" },
            { "IoSTREAMFile2", "io_stream_file2" },
            { "TELNO1", "telno1" },
            { "1TELNO", "1_telno" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = src.ToSnakeCase();
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
            var actual = src.ToSnakeCase("-");
            Assert.Equal(expect, actual);
        }
    }


    [Fact]
    public void ToDateTime()
    {
        var expect = new DateTime(2022, 12, 10, 10, 2, 3);
        Assert.Equal(expect, "2022-12-10T10:02:03+09:00".ToDateTime());

        expect = new DateTime(2022, 12, 10, 10, 2, 3, 999);
        Assert.Equal(expect, "2022-12-10T10:02:03.999+09:00".ToDateTime());
        Assert.Equal(expect, "2022-12-10T10:02:03.999".ToDateTime());
        Assert.Equal(expect, "2022-12-10T01:02:03.999Z".ToDateTime());
        Assert.Equal(expect, "2022-12-10 10:02:03.999".ToDateTime());
        Assert.Equal(expect, "2022/12/10T10:02:03.999+09:00".ToDateTime());
        Assert.Equal(expect, "2022/12/10T01:02:03.999Z".ToDateTime());
        Assert.Equal(expect, "2022/12/10 10:02:03.999".ToDateTime());

        expect = new DateTime(2022, 12, 10, 10, 2, 3).AddMicroseconds(999999.9);
        Assert.Equal(expect, "2022-12-10T10:02:03.9999999+09:00".ToDateTime());
    }


    [Fact]
    public void ToJsonString_DateTime()
    {
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022-12-10T10:02:03.999+09:00".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022-12-10T10:02:03.999".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022-12-10T01:02:03.999Z".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022-12-10 10:02:03.999".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022/12/10T10:02:03.999+09:00".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022/12/10T01:02:03.999Z".ToDateTime().ToJsonString());
        Assert.Equal("2022-12-10T10:02:03.999+09:00", "2022/12/10 10:02:03.999".ToDateTime().ToJsonString());

        Assert.Equal("2022-12-10T10:02:03.9999999+09:00", "2022-12-10T10:02:03.9999999+09:00".ToDateTime().ToJsonString());
    }


    [Fact]
    public void ToDateOnly()
    {
        var expect = new DateOnly(2022, 12, 10);
        Assert.Equal(expect, "2022-12-10T10:02:03.999+09:00".ToDateOnly());
        Assert.Equal(expect, "2022-12-10T01:02:03.999Z".ToDateOnly());
        Assert.Equal(expect, "2022-12-10 10:02:03.999".ToDateOnly());
    }


    [Fact]
    public void ToJsonString_DateOnly()
    {
        Assert.Equal("2022-12-10", "2022 -12-10T10:02:03.999+09:00".ToDateOnly().ToJsonString());
        Assert.Equal("2022-12-10", "2022-12-10T01:02:03.999Z".ToDateOnly().ToJsonString());
        Assert.Equal("2022-12-10", "2022-12-10 10:02:03.999".ToDateOnly().ToJsonString());
    }


    [Fact]
    public void ToTimeOnly()
    {
        var expect = new TimeOnly(8, 2, 3, 999);
        Assert.Equal(expect, "2022-12-10T08:02:03.999+09:00".ToTimeOnly());
        Assert.Equal(expect, "2022-12-09T23:02:03.999Z".ToTimeOnly());
        Assert.Equal(expect, "2022-12-10 08:02:03.999".ToTimeOnly());
        Assert.Equal(expect, "08:02:03.999+09:00".ToTimeOnly());
        Assert.Equal(expect, "23:02:03.999Z".ToTimeOnly());
        Assert.Equal(expect, "08:02:03.999".ToTimeOnly());
        Assert.Equal(expect, "08:02:03.999+09:00".ToTimeOnly());
        Assert.Equal(expect, "23:02:03.999Z".ToTimeOnly());
        Assert.Equal(expect, "08:02:03.999".ToTimeOnly());
    }


    [Fact]
    public void ToJsonString_TimeOnly()
    {
        Assert.Equal("08:02:03.999", "2022-12-10T08:02:03.999+09:00".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "2022-12-09T23:02:03.999Z".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "2022-12-10 08:02:03.999".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "T08:02:03.999+09:00".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "T23:02:03.999Z".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "T08:02:03.999".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "08:02:03.999+09:00".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "23:02:03.999Z".ToTimeOnly().ToJsonString());
        Assert.Equal("08:02:03.999", "08:02:03.999".ToTimeOnly().ToJsonString());
    }


    [Fact]
    public void ToTimeSpan()
    {
        Assert.Equal(new TimeSpan( 0, 00, 00, 00), "0".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 00, 00, 00), "1".ToTimeSpan());
        Assert.Equal(new TimeSpan(-1, 00, 00, 00), "-1".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 00, 00), "1.2".ToTimeSpan());
        Assert.Equal(new TimeSpan( 0, 02, 03, 00), "2:3".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 03, 00), "1.2:3".ToTimeSpan());
        Assert.Equal(new TimeSpan( 0, 02, 03, 04), "2:3:4".ToTimeSpan());
        Assert.Equal(new TimeSpan( 0, 02, 03, 00, 900), "2:3:.9".ToTimeSpan());
        Assert.Equal(new TimeSpan( 0, 02, 03, 04, 900), "2:3:4.9".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 03, 04), "1.2:3:4".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 03, 00, 900), "1.2:3:.9".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 03, 04, 900), "1.2:3:4.9".ToTimeSpan());
        Assert.Equal(new TimeSpan( 1, 02, 03, 04) + new TimeSpan(9999999), "1.02:03:04.9999999".ToTimeSpan());
        Assert.Equal(new TimeSpan(-1, -02, -03, -04) + new TimeSpan(-9999999), "-1.02:03:04.9999999".ToTimeSpan());
        Assert.Equal(new TimeSpan(2, 01, 02, 03) + new TimeSpan(9999999), "49:02:03.9999999".ToTimeSpan());
        Assert.Equal(new TimeSpan(1, 00, 00, 00), "24:00:00".ToTimeSpan());
    }


    [Fact]
    public void ToJsonString_TimeSpan()
    {
        Assert.Equal("00:00:00", "0".ToTimeSpan().ToJsonString());
        Assert.Equal("00:00:00", "-0".ToTimeSpan().ToJsonString());
        Assert.Equal("1.00:00:00", "1".ToTimeSpan().ToJsonString());
        Assert.Equal("-1.00:00:00", "-1".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:00:00", "1.2".ToTimeSpan().ToJsonString());
        Assert.Equal("02:03:00", "2:3".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:03:00", "1.2:3".ToTimeSpan().ToJsonString());
        Assert.Equal("02:03:04", "2:3:4".ToTimeSpan().ToJsonString());
        Assert.Equal("02:03:00.9", "2:3:.9".ToTimeSpan().ToJsonString());
        Assert.Equal("02:03:04.9", "2:3:4.9".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:03:04", "1.2:3:4".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:03:00.9", "1.2:3:.9".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:03:04.9", "1.2:3:4.9".ToTimeSpan().ToJsonString());
        Assert.Equal("1.02:03:04.9999999", "1.02:03:04.9999999".ToTimeSpan().ToJsonString());
        Assert.Equal("-1.02:03:04.9999999", "-1.02:03:04.9999999".ToTimeSpan().ToJsonString());
        Assert.Equal("2.02:03:04.9999999", "50:03:04.9999999".ToTimeSpan().ToJsonString());
        Assert.Equal("1.00:00:00", "24:00:00".ToTimeSpan().ToJsonString());
        Assert.Equal("02:00:00", "2::".ToTimeSpan().ToJsonString());
        Assert.Equal("00:03:00", ":3:".ToTimeSpan().ToJsonString());
        Assert.Equal("00:00:04", "::4".ToTimeSpan().ToJsonString());
        Assert.Equal("-02:00:00", "-2::".ToTimeSpan().ToJsonString());
        Assert.Equal("-00:03:00", "-:3:".ToTimeSpan().ToJsonString());
        Assert.Equal("-00:00:04", "-::4".ToTimeSpan().ToJsonString());

        Assert.Equal("00:00:00", "0".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("00:00:00", "-0".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("24:00:00", "1".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("-24:00:00", "-1".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:00:00", "1.2".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("02:03:00", "2:3".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:03:00", "1.2:3".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("02:03:04", "2:3:4".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("02:03:00.9", "2:3:.9".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("02:03:04.9", "2:3:4.9".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:03:04", "1.2:3:4".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:03:00.9", "1.2:3:.9".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:03:04.9", "1.2:3:4.9".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("26:03:04.9999999", "1.02:03:04.9999999".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("-26:03:04.9999999", "-1.02:03:04.9999999".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("50:03:04.9999999", "50:03:04.9999999".ToTimeSpan().ToJsonString(enableDay: false));
        Assert.Equal("24:00:00", "24:00:00".ToTimeSpan().ToJsonString(enableDay: false));
    }

}