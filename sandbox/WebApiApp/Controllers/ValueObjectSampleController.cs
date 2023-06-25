using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;
using WebApiApp.ValueObject.Builtins;
using WebApiApp.ValueObject.Others;
using NullableUnitGenerator;
using WebApiApp.BusinessLogic;
using System.Dynamic;

namespace WebApiApp.Controllers;


/// <summary></summary>
[ApiController]
[Route("[controller]")]
public class ValueObjectSampleController : ControllerBase
{
    private readonly ILogger<ValueObjectSampleController> _logger;

    /// <summary></summary>
    public ValueObjectSampleController(ILogger<ValueObjectSampleController> logger)
    {
        _logger = logger;
    }


    /// <summary></summary>
    [HttpPost("GetValueSample")]
    public dynamic GetValueSample()
    {
        var vals = new
        {
            Key1 = 10,
            Key2 = 1.25,
            Key3 = false,
            Key4 = DateTime.Now,
            Key5 = new int[] { 1, 2, 3 },
            Key6 = new List<string> { "a", "b", "c" },
            Key7 = new Dictionary<string, int> { { "sub1", 10 }, { "sub2", 20 } },
        };
        return vals;
    }


    /// <summary></summary>
    [HttpPost("GetUnitOfSample")]
    public dynamic GetUnitOfSample()
    {
        var val = DummyVoSample.GetAllValue();
        return val;//.ExcludeUndef(val);
    }


    /// <summary></summary>
    [HttpPost("GetUnitOfSampleList")]
    public IEnumerable<dynamic> GetUnitOfSampleList()
    {
        var list = new List<dynamic>
        {
            DummyVoSample.GetAllValue(),
            DummyVoSample.GetAllNullValue(),
            DummyVoSample.GetAllUndefValue(),
            DummyVoSample.GetAllDefaultValueOfValueState  (),
            DummyVoSample.GetAllDefalut(),
        }; 
        return list.Select(x => UnitHelper.ExcludeUndef(x)).ToList();
    }


    /// <summary></summary>
    [HttpPost("SameReturnUnitOf")]
    public dynamic SameReturnUnitOf(UnitOfSample uos)
    {
        return UnitHelper.ExcludeUndef(uos);
    }


    /// <summary></summary>
    [HttpPost("SameReturnExpandoObject")]
    public dynamic SameReturnExpandoObject(ExpandoObject eo)
    {
        return UnitHelper.ExcludeUndef(eo);
    }

}
