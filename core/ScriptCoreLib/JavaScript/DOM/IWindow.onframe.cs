using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.DOM
{



    partial class IWindow
    {
        [Script]
        public class FrameEvent
        {
            public int counter;

			// is it too costly to do for each frame?
            public Stopwatch delay = Stopwatch.StartNew();


			public static implicit operator bool (FrameEvent e)
			{
				// future C# may allow if (obj)
				// but for now booleans are needed

				// enable 
				// while (await Native.window.async.onresize);
				return ((object)e != null);
			}
		}


        [System.Obsolete("jsc experience")]
        public event System.Action<FrameEvent> onframe
        {
            [Script(DefineAsStatic = true)]
            add
            {
                // https://developer.mozilla.org/en/DOM/window.requestAnimationFrame
                // tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
                // X:\jsc.svn\examples\javascript\synergy\webgl\WebGLEarthByBjorn\WebGLEarthByBjorn\Application.cs



                System.Action loop = null;


                var e = new FrameEvent();

                //int c = 0;

                loop = delegate
                {
                    // exception would stop the loop?
                    value(e);

                    e.delay.Restart();
                    e.counter++;

                    this.requestAnimationFrame += loop;
                };

                this.requestAnimationFrame += loop;
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                throw new System.NotSupportedException();
            }
        }

    }
}
