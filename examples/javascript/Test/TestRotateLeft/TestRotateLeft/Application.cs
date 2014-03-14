using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestRotateLeft;
using TestRotateLeft.Design;
using TestRotateLeft.HTML.Pages;

namespace TestRotateLeft
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\test\TestMD5Experiment\TestMD5Experiment\Library\helper.cs
        public static uint RotateLeft(uint uiNumber, ushort shift)
        {
            // 0:26ms RotateLeft { uiNumber = 3614090487, shift = 7, value = -21 } view-source:36394
            // RotateLeft { uiNumber = 3614090487, shift = 7, value = 3042081771 }
            
            // (b << (c & 31)) >>> 0: 3042081664

            var value0 = (uiNumber >> 32 - shift);

            // 0:19ms RotateLeft { uiNumber = 3614090487, shift = 7, value = -1252885525, value0 = 107, value1 = 3042081664 } 
            var value1 = (uiNumber << shift);

            var value = (value0 | value1);


            // RotateLeft { uiNumber = 3614090487, shift = 7, value = 3042081771, value0 = 107, value1 = 3042081664 }
            // 0:21ms RotateLeft { uiNumber = 3614090487, shift = 7, value = -1252885525, value0 = 107, value1 = -1252885632 } 

            Console.WriteLine(
               "RotateLeft " +
               new { uiNumber, shift, value, value0, value1 }
               );

            return value;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            RotateLeft(3614090487, 7);
        }

    }
}
