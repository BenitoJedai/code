using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestGAEAnnotations
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            Table1 t1 = new Table1();

            // When can we send the table to the client as is? :)
            y(e);
        }

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class FooAttribute : Attribute
    {
    
    }

    [Foo]
    public sealed class Table1
    {
        [Foo]
        public string Field1;
    }
}
