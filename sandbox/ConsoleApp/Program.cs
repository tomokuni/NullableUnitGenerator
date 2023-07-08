using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

using NullableUnitGeneratorSample.Sandbox.ConsoleApp;   

var json = JsonSerializer.Serialize(new Dictionary<Guid, string> { { Guid.NewGuid(), "hogemoge" } });



Console.WriteLine(json);

int cnt = 10000000;
var sw = new Stopwatch();
var cntTrue = 0;
sw.Start();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999);
    var b = Random.Shared.Next(999999999);
    sw.Start();
    if (a == b)
        cntTrue++;
}
sw.Stop();
Console.WriteLine($"数値比較 : {sw.ElapsedMilliseconds} {cntTrue}回");

cntTrue = 0;
sw.Restart();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999).ToString();
    var b = Random.Shared.Next(999999999).ToString();
    sw.Start();
    if (a == b)
        cntTrue++;
}
sw.Stop();
Console.WriteLine($"文字列比較 : {sw.ElapsedMilliseconds} {cntTrue}回");

cntTrue = 0;
sw.Restart();
for (int i = 0; i < cnt; i++)
{
    sw.Stop();
    var a = Random.Shared.Next(999999999);
    var b = Random.Shared.Next(999999999);
    sw.Start();
    if (a.ToString() == b.ToString())
        cntTrue++;
}
sw.Stop();
Console.WriteLine($"文字列化比較 : {sw.ElapsedMilliseconds} {cntTrue}回");

