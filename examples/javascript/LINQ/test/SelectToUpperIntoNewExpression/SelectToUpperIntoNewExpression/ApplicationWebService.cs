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
using System.Diagnostics;
using System.Collections.Specialized;

namespace SelectToUpperIntoNewExpression
{
    class data(public string goo, string goo2 = "goo2")
    {


        public string this[string e]
        {
            set
            {
            }
        }

        public string this[string e, long i]
        {
            set
            {
            }
        }
    }




    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            // we would like to have a cursor? can we?

            new Data.PerformanceResourceTimingData2.ApplicationPerformance().Insert(
                new Data.PerformanceResourceTimingData2ApplicationPerformanceRow { Tag = " hi ", connectStart = 1, connectEnd = 2 }
            );

            var loc0 = "loc0";

            // are we able to provide Funcs from the client?
            // do we even have a whitelist of funcs the client can send us?
            Func<string, string> Special = x =>
                x + " not in sql, at selection on server " + new { loc0 };

            var loc1 = new { Special };

            var z = from ss in new Data.PerformanceResourceTimingData2.ApplicationPerformance()

                    orderby ss.Key

                    //let datagroup0 = new { datagroup0Tag = ss.Tag }

                    select new
                    {
                        datagroup0 = new[] { new { a = 0 }, new { a = 1 } },


                        // Error	279	An expression tree may not contain an anonymous method expression	X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs	47	29	SelectToUpperIntoNewExpression
                        // why not?
                        datagroup3 = new XElement("tag", new XElement("u", ss.Tag), "text element", new XAttribute("style", "color:red;")),

                        //f = new Action(
                        //    delegate
                        //    {
                        //    }
                        //),
                        goo = loc1.Special(ss.Tag),
                        //goo2 = ss.Tag.StaticSpecial(),

                        //tuple0 = Tuple.Create(ss.Tag, ss.connectStart),

                        //dict = new StringDictionary
                        //dict = new Dictionary<string, string>
                        //dict = new data(ss.Tag, "goo2")
                        //{
                        //    ["tag", ss.connectStart] = "complex",

                        //    [ss.Tag] = ss.Tag,
                        //    ["tag"] = "goo",

                        //    $goo = "goo"
                        //},

                        datagroup1 = new { datagroup1Tag = ss.Tag, z = "???", datagroup2 = new { datagroup1Tag = ss.Tag.ToLower() } },
                        datagroup2 = new { datagroup1Tag = ss.Tag.ToUpper(), loc0 },

                        // what about named args and optional args?
                        // Error	284	An expression tree may not contain a call or invocation that uses optional arguments	X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs	49	38	SelectToUpperIntoNewExpression
                        // why not? roslyn fix it :D
                        // Error	279	An expression tree may not contain a named argument specification	X:\jsc.svn\examples\javascript\LINQ\test\SelectToUpperIntoNewExpression\SelectToUpperIntoNewExpression\ApplicationWebService.cs	52	38	SelectToUpperIntoNewExpression

                        datagroup4 = new data(ss.Tag, "goo2"),

                        // after we moved from member names to index, what does it take to do arrays?
                        datagroup5 = new[] { ss.connectStart, ss.connectEnd },

                        xlower = ss.Tag.ToLower(),
                        x = ss.Tag.ToUpper()
                    };

            // ToDataTable?
            var zzz = z.AsDataTable();
            var zz = z.FirstOrDefault();

            Debugger.Break();
        }



    }


    public static class x
    {
        public static string StaticSpecial(this string e)
        {
            return e;
        }
    }
}
