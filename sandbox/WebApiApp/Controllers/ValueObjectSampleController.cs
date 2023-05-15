using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;
using ConsoleApp.Builtins;
using ConsoleApp.Others;
using NullableUnitGenerator;

namespace WebApi.Controllers;


/// <summary></summary>
[ApiController]
[Route("[controller]")]
public class ValueObjectSampleController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ValueObjectSampleController> _logger;

    /// <summary></summary>
    public ValueObjectSampleController(ILogger<ValueObjectSampleController> logger)
    {
        _logger = logger;
    }

    /// <summary></summary>
    [HttpPost("Get")]
    public dynamic Get(ValueObjectSample vol)
    {
        var eo = UnitHelper.ExcludeUndef( new {
            VoNullValue = VoInt.NullValue,
            VoUndefValue = VoLong.UndefValue,
            VoDatetime= new VoDatetime(DateTime.Now.AddDays(1)) ,
            VoDouble= new VoDouble((double)Random.Shared.Next(-20, 55)) ,
            VoDecimal= new VoDecimal((decimal)Random.Shared.Next(-20, 55)) ,
            VoString= new VoString(Summaries[Random.Shared.Next(Summaries.Length)]) ,
            key1 = 10,
            key2 = 1.25 ,
            key3= false ,
            key4= DateTime.Now ,
            key5= new int[] { 1, 2, 3 } ,
            key6= new List<string> { "a", "b", "c" } ,
            key7 = new Dictionary<string, int> { { "sub1", 10 }, { "sub2", 20 } },
        });
        return eo;
    }

    /// <summary></summary>
    [HttpPost("GetList")]
    public IEnumerable<dynamic> GetList(ValueObjectSample vol)
    {
        return new List<dynamic>
        {
            UnitHelper.ExcludeUndef(new ValueObjectSample
            {
                Title = "全部値あり",
                VoBool = true,
                VoByte = 1,
                VoSbyte = 2,
                VoChar = '3',
                VoShort = 4,
                VoUshort = 5,
                VoInt = 6,
                VoUint = 7,
                VoLong = 10,
                VoUlong = 11,
                VoFloat = 12.2f,
                VoDouble = 13.3,
                VoDecimal = 14.4m,
                VoString = "15",
                VoUrlSafeBinary = "",
                VoGuid = VoGuid.NewVoGuid(),
                VoUlid = VoUlid.NewVoUlid(),
                VoDatetime = DateTime.Now,
                VoDateonly = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                VoTimeonly = new TimeOnly(DateTime.Now.TimeOfDay.Ticks),
                VoTimespan = DateTime.Now.TimeOfDay,
                VoByteArray = new byte[]{ 20,21 },
            }),
            UnitHelper.ExcludeUndef(new ValueObjectSample
            {
                Title = "全部NullValue",
                VoBool = VoBool.NullValue,
                VoByte = VoByte.NullValue,
                VoSbyte = VoSbyte.NullValue,
                VoChar = VoChar.NullValue,
                VoShort = VoShort.NullValue,
                VoUshort = VoUshort.NullValue,
                VoInt = VoInt.NullValue,
                VoUint = VoUint.NullValue,
                VoLong = VoLong.NullValue,
                VoUlong = VoUlong.NullValue,
                VoFloat = VoFloat.NullValue,
                VoDouble = VoDouble.NullValue,
                VoDecimal = VoDecimal.NullValue,
                VoString = VoString.NullValue,
                VoUrlSafeBinary = VoUrlSafeBinary.NullValue,
                VoGuid = VoGuid.NullValue,
                VoUlid = VoUlid.NullValue,
                VoDatetime = VoDatetime.NullValue,
                VoDateonly = VoDateonly.NullValue,
                VoTimeonly = VoTimeonly.NullValue,
                VoTimespan = VoTimespan.NullValue,
                VoByteArray = VoByteArray.NullValue,
            }),
            UnitHelper.ExcludeUndef(new ValueObjectSample
            {
                Title = "全部UndefValue",
                VoBool = VoBool.UndefValue,
                VoByte = VoByte.UndefValue,
                VoSbyte = VoSbyte.UndefValue,
                VoChar = VoChar.UndefValue,
                VoShort = VoShort.UndefValue,
                VoUshort = VoUshort.UndefValue,
                VoInt = VoInt.UndefValue,
                VoUint = VoUint.UndefValue,
                VoLong = VoLong.UndefValue,
                VoUlong = VoUlong.UndefValue,
                VoFloat = VoFloat.UndefValue,
                VoDouble = VoDouble.UndefValue,
                VoDecimal = VoDecimal.UndefValue,
                VoString = VoString.UndefValue,
                VoUrlSafeBinary = VoUrlSafeBinary.UndefValue,
                VoGuid = VoGuid.UndefValue,
                VoUlid = VoUlid.UndefValue,
                VoDatetime = VoDatetime.UndefValue,
                VoDateonly = VoDateonly.UndefValue,
                VoTimeonly = VoTimeonly.UndefValue,
                VoTimespan = VoTimespan.UndefValue,
                VoByteArray = VoByteArray.UndefValue,
            }),
            UnitHelper.ExcludeUndef(new ValueObjectSample
            {
                Title = "全部値ValueStateDefaultValue",
                VoBool = VoBool.ValueStateDefaultValue,
                VoByte = VoByte.ValueStateDefaultValue,
                VoSbyte = VoSbyte.ValueStateDefaultValue,
                VoChar = VoChar.ValueStateDefaultValue,
                VoShort = VoShort.ValueStateDefaultValue,
                VoUshort = VoUshort.ValueStateDefaultValue,
                VoInt = VoInt.ValueStateDefaultValue,
                VoUint = VoUint.ValueStateDefaultValue,
                VoLong = VoLong.ValueStateDefaultValue,
                VoUlong = VoUlong.ValueStateDefaultValue,
                VoFloat = VoFloat.ValueStateDefaultValue,
                VoDouble = VoDouble.ValueStateDefaultValue,
                VoDecimal = VoDecimal.ValueStateDefaultValue,
                VoString = VoString.ValueStateDefaultValue,
                VoUrlSafeBinary = VoUrlSafeBinary.ValueStateDefaultValue,
                VoGuid = VoGuid.ValueStateDefaultValue,
                VoUlid = VoUlid.ValueStateDefaultValue,
                VoDatetime = VoDatetime.ValueStateDefaultValue,
                VoDateonly = VoDateonly.ValueStateDefaultValue,
                VoTimeonly = VoTimeonly.ValueStateDefaultValue,
                VoTimespan = VoTimespan.ValueStateDefaultValue,
                VoByteArray = VoByteArray.ValueStateDefaultValue,
            }),
            UnitHelper.ExcludeUndef(new ValueObjectSample
            {
                Title = "全部値省略(Defalut値)",
            }),
        }; 

    }
}
