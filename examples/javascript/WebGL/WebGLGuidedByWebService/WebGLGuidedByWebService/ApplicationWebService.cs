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
        //type$gTZJ8H4FJz6UwC8P_arIh6g.x = null;
        //type$gTZJ8H4FJz6UwC8P_arIh6g.y = null;


        // script: error JSC1000: No implementation found for this native method, please implement [static System.Convert.ToString(System.Single)]
        // first run should also pass down the field values
        // since js is precached, how do we do it?
        // should we use an init cookie?

        // what if we used WebGL GLSL vec2 here?
        public float x;
        public float y;


        public int syncframe = 0;
        public int c = 0;

        // what about events and virtual events?

        public long onsyncframe_ElapsedMilliseconds;

        // what about debugger visualizers?
        // http://community.devexpress.com/blogs/markmiller/archive/2012/12/03/here-s-your-game-changer-debug-visualizer-in-coderush-for-visual-studio.aspx
        public XElement xml;

        public Task<ApplicationWebService[]> onsyncframe()
        {
            // we can breakpoint here, and modify live code

            if (onsyncframe_ElapsedMilliseconds == 0)
                onsyncframe_ElapsedMilliseconds = 1000;

            Console.Title = new { c, syncframe, onsyncframe_ElapsedMilliseconds, fps = 1000 / onsyncframe_ElapsedMilliseconds }.ToString();


            // DebuggerVisualizer?

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
