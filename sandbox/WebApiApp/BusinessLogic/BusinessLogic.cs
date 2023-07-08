using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using NullableUnitGeneratorSample;

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
    public static UnitOfSampleModel GetAllValue()
    {
        return new UnitOfSampleModel
        {
            VoTitle = "全部値あり",
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
            VoGuid = VoGuidSample.NewVoGuidSample(),
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
    public static UnitOfSampleModel GetAllNullValue()
    {
        return new UnitOfSampleModel
        {
            VoTitle = "全部NullValue",
            VoBool = VoBoolSample.NullValue,
            VoByte = VoByteSample.NullValue,
            VoSbyte = VoSbyteSample.NullValue,
            VoChar = VoCharSample.NullValue,
            VoShort = VoShortSample.NullValue,
            VoUshort = VoUshortSample.NullValue,
            VoInt = VoIntSample.NullValue,
            VoUint = VoUintSample.NullValue,
            VoLong = VoLongSample.NullValue,
            VoUlong = VoUlongSample.NullValue,
            VoFloat = VoFloatSample.NullValue,
            VoDouble = VoDoubleSample.NullValue,
            VoDecimal = VoDecimalSample.NullValue,
            VoString = VoStringSample.NullValue,
            VoGuid = VoGuidSample.NullValue,
            VoDatetime = VoDatetimeSample.NullValue,
            VoDateonly = VoDateonlySample.NullValue,
            VoTimeonly = VoTimeonlySample.NullValue,
            VoTimespan = VoTimespanSample.NullValue,
            VoByteArray = VoByteArraySample.NullValue,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSampleModel GetAllUndefValue()
    {
        return new UnitOfSampleModel
        {
            VoTitle = "全部UndefValue",
            VoBool = VoBoolSample.UndefValue,
            VoByte = VoByteSample.UndefValue,
            VoSbyte = VoSbyteSample.UndefValue,
            VoChar = VoCharSample.UndefValue,
            VoShort = VoShortSample.UndefValue,
            VoUshort = VoUshortSample.UndefValue,
            VoInt = VoIntSample.UndefValue,
            VoUint = VoUintSample.UndefValue,
            VoLong = VoLongSample.UndefValue,
            VoUlong = VoUlongSample.UndefValue,
            VoFloat = VoFloatSample.UndefValue,
            VoDouble = VoDoubleSample.UndefValue,
            VoDecimal = VoDecimalSample.UndefValue,
            VoString = VoStringSample.UndefValue,
            VoGuid = VoGuidSample.UndefValue,
            VoDatetime = VoDatetimeSample.UndefValue,
            VoDateonly = VoDateonlySample.UndefValue,
            VoTimeonly = VoTimeonlySample.UndefValue,
            VoTimespan = VoTimespanSample.UndefValue,
            VoByteArray = VoByteArraySample.UndefValue,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSampleModel GetAllDefaultValueOfValueState  ()
    {
        return new UnitOfSampleModel
        {
            VoTitle = "全部DefaultValueOfValueState  ",
            VoBool = VoBoolSample.DefaultValueOfValueState  ,
            VoByte = VoByteSample.DefaultValueOfValueState  ,
            VoSbyte = VoSbyteSample.DefaultValueOfValueState  ,
            VoChar = VoCharSample.DefaultValueOfValueState  ,
            VoShort = VoShortSample.DefaultValueOfValueState  ,
            VoUshort = VoUshortSample.DefaultValueOfValueState  ,
            VoInt = VoIntSample.DefaultValueOfValueState  ,
            VoUint = VoUintSample.DefaultValueOfValueState  ,
            VoLong = VoLongSample.DefaultValueOfValueState  ,
            VoUlong = VoUlongSample.DefaultValueOfValueState  ,
            VoFloat = VoFloatSample.DefaultValueOfValueState  ,
            VoDouble = VoDoubleSample.DefaultValueOfValueState  ,
            VoDecimal = VoDecimalSample.DefaultValueOfValueState  ,
            VoString = VoStringSample.DefaultValueOfValueState  ,
            VoGuid = VoGuidSample.DefaultValueOfValueState  ,
            VoDatetime = VoDatetimeSample.DefaultValueOfValueState  ,
            VoDateonly = VoDateonlySample.DefaultValueOfValueState  ,
            VoTimeonly = VoTimeonlySample.DefaultValueOfValueState  ,
            VoTimespan = VoTimespanSample.DefaultValueOfValueState  ,
            VoByteArray = VoByteArraySample.DefaultValueOfValueState  ,
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UnitOfSampleModel GetAllDefalut()
    {
        return new UnitOfSampleModel
        {
            VoTitle = "全部値省略(Defalut値)",
        };
    }

}
