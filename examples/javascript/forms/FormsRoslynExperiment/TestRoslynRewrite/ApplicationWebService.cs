// Error	2	The type or namespace name 'Roslyn' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\forms\FormsRoslynExperiment\TestRoslynRewrite\ApplicationWebService.cs	1	7	TestRoslynRewrite


using Roslyn.Scripting.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace TestRoslynRewrite
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // http://blog.filipekberg.se/tag/roslyn/

            var engine = new ScriptEngine();
            var session = engine.CreateSession();

            try
            {
                Console.WriteLine(e);

                var result = session.Execute(e);


                // Send it back to the caller.
                y("output: " + result);
            }
            catch (Exception ex)
            {
                y("error: " + ex.Message);

            }
        }

    }
}
