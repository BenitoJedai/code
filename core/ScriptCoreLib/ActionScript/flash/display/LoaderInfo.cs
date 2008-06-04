using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.system;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/LoaderInfo.html
    [Script(IsNative=true)]
    public class LoaderInfo : EventDispatcher
    {
        #region Properties
        /// <summary>
        /// [read-only] The ActionScript version of the loaded SWF file.
        /// </summary>
        public uint actionScriptVersion { get; private set; }

        /// <summary>
        /// [read-only] When an external SWF file is loaded, all ActionScript 3.0 definitions contained in the loaded class are stored in the applicationDomain property.
        /// </summary>
        public ApplicationDomain applicationDomain { get; private set; }

        /// <summary>
        /// [read-only] The bytes associated with a LoaderInfo object.
        /// </summary>
        public ByteArray bytes { get; private set; }

        /// <summary>
        /// [read-only] The number of bytes that are loaded for the media.
        /// </summary>
        public uint bytesLoaded { get; private set; }

        /// <summary>
        /// [read-only] The number of compressed bytes in the entire media file.
        /// </summary>
        public uint bytesTotal { get; private set; }

        /// <summary>
        /// [read-only] Expresses the trust relationship from content (child) to the Loader (parent).
        /// </summary>
        public bool childAllowsParent { get; private set; }

        /// <summary>
        /// [read-only] The loaded object associated with this LoaderInfo object.
        /// </summary>
        public DisplayObject content { get; private set; }

        /// <summary>
        /// [read-only] The MIME type of the loaded file.
        /// </summary>
        public string contentType { get; private set; }

        /// <summary>
        /// [read-only] The nominal frame rate, in frames per second, of the loaded SWF file.
        /// </summary>
        public double frameRate { get; private set; }

        /// <summary>
        /// [read-only] The nominal height of the loaded file.
        /// </summary>
        public int height { get; private set; }

        /// <summary>
        /// [read-only] The Loader object associated with this LoaderInfo object.
        /// </summary>
        public Loader loader { get; private set; }

        /// <summary>
        /// [read-only] The URL of the SWF file that initiated the loading of the media described by this LoaderInfo object.
        /// </summary>
        public string loaderURL { get; private set; }

        /// <summary>
        /// [read-only] An object that contains name-value pairs that represent the parameters provided to the loaded SWF file.
        /// </summary>
        public object parameters { get; private set; }

        /// <summary>
        /// [read-only] Expresses the trust relationship from Loader (parent) to the content (child).
        /// </summary>
        public bool parentAllowsChild { get; private set; }

        /// <summary>
        /// [read-only] Expresses the domain relationship between the loader and the content: true if they have the same origin domain; false otherwise.
        /// </summary>
        public bool sameDomain { get; private set; }

        /// <summary>
        /// [read-only] An EventDispatcher instance that can be used to exchange events across security boundaries.
        /// </summary>
        public EventDispatcher sharedEvents { get; private set; }

        /// <summary>
        /// [read-only] The file format version of the loaded SWF file.
        /// </summary>
        public uint swfVersion { get; private set; }

        /// <summary>
        /// [read-only] The URL of the media being loaded.
        /// </summary>
        public string url { get; private set; }

        /// <summary>
        /// [read-only] The nominal width of the loaded content.
        /// </summary>
        public int width { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// [static] Returns the LoaderInfo object associated with a SWF file defined as an object.
        /// </summary>
        public static LoaderInfo getLoaderInfoByDefinition(object @object)
        {
            return default(LoaderInfo);
        }

        #endregion

        #region Constructors
        #endregion

        #region Events
        /// <summary>
        /// Dispatched when data has loaded successfully.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> complete;

        /// <summary>
        /// Dispatched when a network request is made over HTTP and an HTTP status code can be detected.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<HTTPStatusEvent> httpStatus;

        /// <summary>
        /// Dispatched when the properties and methods of a loaded SWF file are accessible.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> init;

        /// <summary>
        /// Dispatched when an input or output error occurs that causes a load operation to fail.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<IOErrorEvent> ioError;

        /// <summary>
        /// Dispatched when a load operation starts.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> open;

        /// <summary>
        /// Dispatched when data is received as the download operation progresses.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ProgressEvent> progress;

        /// <summary>
        /// Dispatched by a LoaderInfo object whenever a loaded object is removed by using the unload() method of the Loader object, or when a second load is performed by the same Loader object and the original content is removed prior to the load beginning.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> unload;

        #endregion


    }
}
