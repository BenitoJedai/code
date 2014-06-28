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
using TestTypeHandle;
using TestTypeHandle.Design;
using TestTypeHandle.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.BCLImplementation.System;

namespace TestTypeHandle
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            var t = typeof(Application);

            var h = t.TypeHandle;

            // i.constructor.prototype
            var i = h.Value;

            //          function LFTSZNW_bajmpg67CwzN3Xw() { }
            //          LFTSZNW_bajmpg67CwzN3Xw.TypeName = "IDbDataParameter";
            //          LFTSZNW_bajmpg67CwzN3Xw.Assembly = di7d9h5asU2PTUWT6s85lQ;
            //          LFTSZNW_bajmpg67CwzN3Xw.Interfaces =
            //{
            //          IzkeSBiD_aTGMsWPjgYVYEg:
            //              1
            //  , f7G82WqfyzOLoZ_b8v0KVxw:
            //              1
            //};

            //          var type$LFTSZNW_bajmpg67CwzN3Xw = LFTSZNW_bajmpg67CwzN3Xw.prototype;
            //          type$LFTSZNW_bajmpg67CwzN3Xw.constructor = LFTSZNW_bajmpg67CwzN3Xw;


            // 0:28ms {{ i = [object Object] }}
            Console.WriteLine(
                new { i }
            );

            // whats the name of the typeHandle?

            var scope = Expando.Of(Native.self).GetMemberNames();

            Console.WriteLine(new { scope.Length });

            //0:53ms { { i = [object Object] } }
            //0:75ms { { Length = 4634 } }
            //0:104ms { { item = type$MHnq0rsJXjG69YnNoGDJfQ } }
            //0:105ms { { xt = < Namespace >.Application } }

            foreach (var item in scope)
            {
                dynamic s = Native.self;

                object value = s[item];

                if (value == (object)i)
                {
                    Console.WriteLine(new { item });

                    IntPtr ii = s[item];
                    RuntimeTypeHandle xh = new __RuntimeTypeHandle(ii);
                    var xt = Type.GetTypeFromHandle(xh);

                    //stateTypeHandleIndex = type$XjKww8iSKT_aFTpY_bSs5vBQ, stateType = < Namespace >.,
                    Console.WriteLine(new { xt });

                    break;
                }
            }

        }

    }
}
