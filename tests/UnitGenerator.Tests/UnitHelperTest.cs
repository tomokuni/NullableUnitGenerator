using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Xunit;
using NullableUnitGenerator;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace NullableUnitGenerator.Tests;


public class UnitHelperTest
{
    [Fact]
    public void ToPascalCase()
    {
        var inputs = new Dictionary<string, string>
        {
            { "Welcome to the Maze", "welcomeToTheMaze" },
            { "Welcome To The Maze", "welcomeToTheMaze" },
            { "Welcome TO The Maze", "welcomeToTheMaze" },
            { "WelcomeToTheMaze", "welcomeToTheMaze" },
            { "WelcomeTOTheMaze", "welcomeToTheMaze" },
            { "Welcome_To_The_Maze", "welcomeToTheMaze" },
            { "Welcome_TO_The_Maze", "welcomeToTheMaze" },
            { "ISODateTime", "isoDateTime" },
            { "ISODateTIME", "isoDateTime" },
            { "IOStreamFILE", "ioStreamFile" },
            { "IoSTREAMFile", "ioStreamFile" },
        };

        foreach ((var src, var expect) in inputs)
        {
            var actual = UnitHelper.ToCamelCase(src);
            Assert.Equal(expect, actual);
        }
    }

}