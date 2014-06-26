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

        #region async
        #region setTimeout
        [Obsolete]
        public int setTimeout(string code, int time)
        {
            return default(int);
        }

        public int setTimeout(IFunction code, int time)
        {
            return default(int);
        }

        public int setTimeout(System.Action code, int time)
        {
            // X:\jsc.svn\examples\javascript\test\TestSetTimeout\TestSetTimeout\Application.cs
            // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
            return default(int);
        }
        #endregion

        #region setInterval
        [Obsolete]
        public int setInterval(string code, int time)
        {
            return default(int);
        }

        public int setInterval(IFunction code, int time)
        {
            return default(int);
        }

        public
            int setInterval(System.Action code, int time)
        {
            return default(int);
        }
        #endregion


        public void clearTimeout(int i)
        {

        }

        public void clearInterval(int i)
        {

        }
        #endregion


    }
}
