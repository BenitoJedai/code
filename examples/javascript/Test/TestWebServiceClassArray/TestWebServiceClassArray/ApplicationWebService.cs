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

namespace TestWebServiceClassArray
{
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
        public Task<Test> WebMethod2()
        {
            var i = new Test
            {
                arr = new[] { new Test2(), new Test2() }
            };

            i.arr = i.arr.AddToArray(new Test2());



            return i.AsResult();
        }
        public class Test2
        {
        }
        public class Test
        {
            //List<Test2> first;
            public Test2[] arr;
            public long sec;
        }
    }
    public static class ExtensionMethods
    {
        public static TestWebServiceClassArray.ApplicationWebService.Test2[] AddToArray (this TestWebServiceClassArray.ApplicationWebService.Test2[] input,
            TestWebServiceClassArray.ApplicationWebService.Test2 addedValue)
        {
            return input.Concat(new []{addedValue}).ToArray();
        }
        public static TestWebServiceClassArray.ApplicationWebService.Test2[] AddToArray2(this TestWebServiceClassArray.ApplicationWebService.Test2[] input,
            TestWebServiceClassArray.ApplicationWebService.Test2 addedValue)
        {
            var inp = input.ToList();
            inp.Add(addedValue);
            return inp.ToArray();
        }
    }
}