using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace QueryableWebServiceExperiment
{

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationAsyncWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        static List<Foo> Foo = new List<Foo>();

        public void Add(Foo e)
        {
            Foo.Add(e);
        }


        public void AsyncEnumerate(XElement filter, Action<Foo> y)
        {
            y(new Foo { Text = filter.ToString() });
        }

        public void AsyncEnumerate(Expression<Func<Foo, bool>> filter, Action<Foo> y)
        {
            var _filter = filter.Compile();

            Foo.Where(_filter).WithEach(y);
        }
    }



}
