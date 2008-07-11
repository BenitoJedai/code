using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace LightsOut.ActionScript.Shared
{
#if !NoAttributes
    [Script]
#endif
    public interface IDispatchHelper
    {
        Converter<uint, int> GetInt32 { get; set; }
        Converter<uint, double> GetDouble { get; set; }
        Converter<uint, string> GetString { get; set; }
        Converter<object, int> GetLength { get; set; }

        Converter<uint, int[]> GetInt32Array { get; set; }
        Converter<uint, double[]> GetDoubleArray { get; set; }
        Converter<uint, string[]> GetStringArray { get; set; }
    }
}
