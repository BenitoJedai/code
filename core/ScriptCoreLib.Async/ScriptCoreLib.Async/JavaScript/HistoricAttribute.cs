using ScriptCoreLib.JavaScript.DOM;
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
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form
    }

    delegate void HistoricAction<T>(HistoryScope<T> a);
}

