using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript
{
    [Obsolete("What if jsc would simply infer historic events and prevent reloads betweeen pages?")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class HistoricAttribute : Attribute
    {

    }
}
    
