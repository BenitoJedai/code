using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


[assembly: Obfuscation(Feature = "script")]

namespace TestStaticOptimizedCode
{
    public class Class1
    {
        [Script(OptimizedCode = @"
		if (that.requestPointerLock) {
		    that.requestPointerLock();
		}
		else if (that.mozRequestPointerLock) {
		    that.mozRequestPointerLock();
		}
		else if (this.webkitRequestPointerLock) {
		    that.webkitRequestPointerLock();
		}
                    
                    ")]
        static void __requestPointerLock(object that)
        {
            // what about websocket mode?
        }

        // http://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html
        [Script(DefineAsStatic = true)]
        public void requestPointerLock()
        {

            // tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
            __requestPointerLock(this);
        }
    }
}
