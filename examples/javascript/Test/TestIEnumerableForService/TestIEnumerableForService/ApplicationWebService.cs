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

namespace TestIEnumerableForService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebServiceForJavaScript.cs



        //        enter ConvertElementTypeArrayToString
        //InternalWebMethodInfo.AddField { FieldName = field_foo, FieldValue =  }
        //< 0006 0x0100 bytes

        public IEnumerable<foo> foo;

        // X:\jsc.svn\examples\javascript\Test\TestWebMethodTaskOfIEnumerable\TestWebMethodTaskOfIEnumerable\ApplicationWebService.cs
        // X:\jsc.svn\examples\java\Test\TestRoslynIfNull\TestRoslynIfNull\Class1.cs

        public async Task Invoke(IEnumerable<foo> xfoo)
        {
            //        xfoo:
            //            {
            //                goo = root#0, Count = 0 }
            //xfoo: {
            //                    goo = root#1, Count = 1 }
            //xfoo: {
            //                        goo = root#2, Count = 2 }
            //xfoo: {
            //                            goo = root#3, Count = 3 }
            //xoo: {
            //                                goo = root#0, Count = 0 }
            //xoo: {
            //                                    goo = root#1, Count = 1 }
            //xoo: {
            //                                        goo = root#2, Count = 2 }
            //xoo: {
            //                                            goo = root#3, Count = 3 }

            if (xfoo != null)
                foreach (var item in xfoo)
                {
                    if (item.children != null)
                        Console.WriteLine("xfoo: " + new { item.goo, Count = item.children.Count() });
                    else
                        Console.WriteLine("xfoo: " + new { item.goo });
                }

            if (foo != null)
                foreach (var item in foo)
                {
                    if (item.children != null)
                        Console.WriteLine("xoo: " + new { item.goo, Count = item.children.Count() });
                    else
                        Console.WriteLine("xoo: " + new { item.goo });
                }
        }


    }

    // X:\jsc.svn\examples\javascript\test\TestToString\TestToString\Application.cs
    // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversions.cs
    public class foo(public string goo, public IEnumerable<foo> children = null) { }
    //public class foo(public string goo, public params foo[] children) { }
}
