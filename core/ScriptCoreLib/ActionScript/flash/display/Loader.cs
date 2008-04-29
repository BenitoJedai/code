using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/display/Loader.html
    [Script(IsNative = true)]
    public class Loader : DisplayObjectContainer
    {
        #region Properties
        /// <summary>
        /// [read-only] Contains the root display object of the SWF file or image (JPG, PNG, or GIF) file that was loaded by using the load() or loadBytes() methods.
        /// </summary>
        public DisplayObject content { get; private set; }

        /// <summary>
        /// [read-only] Returns a LoaderInfo object corresponding to the object being loaded.
        /// </summary>
        public LoaderInfo contentLoaderInfo { get; private set; }

        #endregion


        /// <summary>
        /// Loads a SWF, JPEG, progressive JPEG, unanimated GIF, or PNG file into an object that is a child of this Loader object.
        /// </summary>
        /// <param name="request"></param>
        public void load(URLRequest request)
        {
        }

    }
}
