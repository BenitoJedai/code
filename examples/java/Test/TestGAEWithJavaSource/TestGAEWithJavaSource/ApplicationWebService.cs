using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestGAEWithJavaSource
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            /*
             
 [javac] P:\java\TestGAEWithJavaSource\ApplicationWebService.java:4: package foo does not exist
 [javac] import foo.foo;
 [javac]           ^
 [javac] P:\java\TestGAEWithJavaSource\ApplicationWebService.java:4: package foo does not exist
 [javac] import foo.foo;
 [javac]           ^
 [javac] P:\java\TestGAEWithJavaSource\ApplicationWebService.java:20: cannot find symbol
 [javac] symbol  : class foo
 [javac] location: class TestGAEWithJavaSource.ApplicationWebService
 [javac]         foo foo0;
 [javac]         ^
 [javac] P:\java\TestGAEWithJavaSource\ApplicationWebService.java:22: cannot find symbol
 [javac] symbol  : class foo
 [javac] location: class TestGAEWithJavaSource.ApplicationWebService
 [javac]         foo0 = new foo();             
              
             */

            var v = new foo.foo();

            // Send it back to the caller.
            y(v.getString());
        }

    }
}
