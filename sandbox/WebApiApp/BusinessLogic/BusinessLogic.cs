using ConsoleApp.Builtins;
using ConsoleApp.Others;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using WebApiApp.Models;
using NullableUnitGenerator;

namespace WebApiApp.BusinessLogic;


/// <summary>
/// 
/// </summary>
public class DummyVoSample
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSample GetAllValue()
    {
        return new UnitOfSample
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
            VoGuid = VoGuid.NewVoGuid(),
            VoUlid = VoUlid.NewVoUlid(),
            VoDatetime = DateTime.Now,
            VoDateonly = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            VoTimeonly = new TimeOnly(DateTime.Now.TimeOfDay.Ticks),
            VoTimespan = DateTime.Now.TimeOfDay,
            VoByteArray = new byte[] { 22, 22, 22 },
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSample GetAllNullValue()
    {
        return new UnitOfSample
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
            VoGuid = VoGuid.NullValue,
            VoUlid = VoUlid.NullValue,
            VoDatetime = VoDatetime.NullValue,
            VoDateonly = VoDateonly.NullValue,
            VoTimeonly = VoTimeonly.NullValue,
            VoTimespan = VoTimespan.NullValue,
            VoByteArray = VoByteArray.NullValue,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSample GetAllUndefValue()
    {
        return new UnitOfSample
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
            VoGuid = VoGuid.UndefValue,
            VoUlid = VoUlid.UndefValue,
            VoDatetime = VoDatetime.UndefValue,
            VoDateonly = VoDateonly.UndefValue,
            VoTimeonly = VoTimeonly.UndefValue,
            VoTimespan = VoTimespan.UndefValue,
            VoByteArray = VoByteArray.UndefValue,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSample GetAllValueStateDefaultValue()
    {
        return new UnitOfSample
        {
            Title = "全部ValueStateDefaultValue",
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
            VoGuid = VoGuid.ValueStateDefaultValue,
            VoUlid = VoUlid.ValueStateDefaultValue,
            VoDatetime = VoDatetime.ValueStateDefaultValue,
            VoDateonly = VoDateonly.ValueStateDefaultValue,
            VoTimeonly = VoTimeonly.ValueStateDefaultValue,
            VoTimespan = VoTimespan.ValueStateDefaultValue,
            VoByteArray = VoByteArray.ValueStateDefaultValue,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSample GetAllDefalut()
    {
        return new UnitOfSample
        {
            Title = "全部値省略(Defalut値)",
        };
    }

}
