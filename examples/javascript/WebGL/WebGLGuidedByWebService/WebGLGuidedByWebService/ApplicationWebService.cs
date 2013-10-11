using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebGLGuidedByWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //     0978:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = System.Single }
        //at jsc.meta.Library.ILStringConversions.Prepare(Type SourceType, Func`2 FieldSelector) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 849


        //[FormatException: Input string was not in a correct format.]
        //   System.Number.ParseDouble(String value, NumberStyles options, NumberFormatInfo numfmt) +10650587
        //   System.Convert.ToDouble(String value) +48
        //   WebGLGuidedByWebService.Global.Invoke(InternalWebMethodInfo ) +184
        //   ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplicati

        //_06000008_field_x:null
        //_06000008_field_y:null

        // why are these fields passed in as null?

        public double x;
        public double y;

        public int syncframe = 0;
        public int c = 0;

        public Task<ApplicationWebService[]> onsyncframe()
        {
            // we can breakpoint here, and modify live code
            Console.Title = new { syncframe, c }.ToString();

            var z = new List<ApplicationWebService>();

            z.Add(
                new ApplicationWebService { x = -8, y = -8 }
            );

            z.Add(
                new ApplicationWebService { x = 8, y = 8 }
            );

            z.Add(
                new ApplicationWebService { x = -8, y = 0 }
            );


            for (int i = 0; i < 16; i++)
            {
                z.Add(
                    new ApplicationWebService { x = -3 * i, y = 0 }
                );

            }


            for (int i = 0; i < 16; i++)
            {
                z.Add(
                    new ApplicationWebService { y = -3 * i, x = 0 }
                );

            }

            return z.ToArray().ToTaskResult();
        }
    }
}
