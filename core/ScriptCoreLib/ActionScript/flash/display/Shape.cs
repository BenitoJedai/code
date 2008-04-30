using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/Shape.html
    [Script(IsNative=true)]
    public class Shape : DisplayObject
    {
        #region Properties
        /// <summary>
        /// [read-only] Specifies the Graphics object belonging to this Shape object, where vector drawing commands can occur.
        /// </summary>
        public Graphics graphics { get; private set; }

        #endregion

    }
}
