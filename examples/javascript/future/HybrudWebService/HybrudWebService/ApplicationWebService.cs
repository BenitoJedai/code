using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

namespace HybrudWebService
{
    using System.Linq.Expressions;
    using HybrudWebService.Data;
    using AsyncDataPerformanceResourceTimingData2ApplicationPerformance = ApplicationWebService;
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public XElement Header = new XElement("h1", "JSC - The.NET crosscompiler for web platforms");

        public async Task<string> RunAsynchroniouslyOnServer()
        {
            //ystem.NotImplementedException: { SourceMethod = Void < RunSynchroniously > b__0(System.Threading.Tasks.Task) }
            //s.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.WriteMethod(MethodInfo SourceMethod)

            Console.WriteLine("enter RunAsynchroniouslyOnServer");


            Header.Value = "at RunAsynchroniouslyOnServer";

            // will it sync back fields if we are async void? no
            // needs to be of Task

            return "done";
        }

        public string RunSynchroniously()
        {
            // if we were to do a background thread here it would run on the server?

            Native.css.style.backgroundColor = "yellow";

            Header.Value = "inside RunSynchroniously, but running in a client.";

            //RunAsynchroniouslyOnServer().ContinueWith(
            //    // ah this already causes trouble
            //    fromServer =>
            //    {
            //        Native.css.style.backgroundColor = "transparent";
            //        // now what
            //    }
            //);

            // so if I wanted to get some data.
            // how would I be able to do it?

            // this needs to happen on serverside.

            //var x = new Data.PerformanceResourceTimingData2.ApplicationPerformance();
            //var x = new AsyncDataPerformanceResourceTimingData2ApplicationPerformance();
            //var tkey = x.Insert(new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = "hi" }
            //    );

            //var y = from k in new AsyncDataPerformanceResourceTimingData2ApplicationPerformance()
            //        select k.Tag.ToUpper();

            //// this needs to happen on the client side
            //Header.Add(
            //    new XElement("b", y.FirstOrDefault())
            //);


            RunAsynchroniouslyOnServer();

            return "this will run on the client because return type is not void nor Task!";
        }

        //private async Task<PerformanceResourceTimingData2ApplicationPerformanceKey> Insert(PerformanceResourceTimingData2ApplicationPerformanceRow r)
        //{
        //    var x = new Data.PerformanceResourceTimingData2.ApplicationPerformance();
        //    return x.Insert(r);
        //}


        //public async Task<string> __Select(Expression x)
        //{
        //    var q = from k in new Data.PerformanceResourceTimingData2.ApplicationPerformance()
        //            select k.Tag.ToUpper();

        //    return q.FirstOrDefault();
        //}
    }

    //class SelectCallSite<TSelection>
    //{
    //    public AsyncDataPerformanceResourceTimingData2ApplicationPerformance x;
    //    public Expression<Func<PerformanceResourceTimingData2ApplicationPerformanceRow, TSelection>> s;
    //}


    //static class x
    //{
    //    public static async Task<TSelection> FirstOrDefault<TSelection>(this SelectCallSite<TSelection> z)
    //    {
    //        var u = await z.x.__Select(z.s);


    //        return (TSelection)(object)u;
    //    }


    //    public static SelectCallSite<TSelection> Select<TSelection>(
    //        this AsyncDataPerformanceResourceTimingData2ApplicationPerformance x,
    //        Expression<Func<PerformanceResourceTimingData2ApplicationPerformanceRow, TSelection>> s)
    //    {
    //        return new SelectCallSite<TSelection> { x = x, s = s };
    //    }
    //}
}
