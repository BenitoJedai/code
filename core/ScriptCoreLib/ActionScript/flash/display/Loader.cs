using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.system;

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


        #region Methods
        /// <summary>
        /// Cancels a load() method operation that is currently in progress for the Loader instance.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Loads a SWF, JPEG, progressive JPEG, unanimated GIF, or PNG file into an object that is a child of this Loader object.
        /// </summary>
        public void load(URLRequest request, LoaderContext context)
        {
        }

        /// <summary>
        /// Loads a SWF, JPEG, progressive JPEG, unanimated GIF, or PNG file into an object that is a child of this Loader object.
        /// </summary>
        public void load(URLRequest request)
        {
        }

        /// <summary>
        /// Loads from binary data stored in a ByteArray object.
        /// </summary>
        public void loadBytes(ByteArray bytes, LoaderContext context)
        {
        }

        /// <summary>
        /// Loads from binary data stored in a ByteArray object.
        /// </summary>
        public void loadBytes(ByteArray bytes)
        {
        }

        /// <summary>
        /// Removes a child of this Loader object that was loaded by using the load() method.
        /// </summary>
        public void unload()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a Loader object that you can use to load files, such as SWF, JPEG, GIF, or PNG files.
        /// </summary>
        public Loader()
        {
        }

        #endregion


    }
}
