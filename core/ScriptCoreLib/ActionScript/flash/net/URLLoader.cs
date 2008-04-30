using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://livedocs.adobe.com/flex/3/langref/flash/net/URLLoader.html
    [Script(IsNative=true)]
    public class URLLoader : EventDispatcher
    {
        #region Fields
        /// <summary>
        /// Indicates the number of bytes that have been loaded thus far during the load operation.
        /// </summary>
        public uint bytesLoaded = 0;

        /// <summary>
        /// Indicates the total number of bytes in the downloaded data.
        /// </summary>
        public uint bytesTotal = 0;

        /// <summary>
        /// The data received from the load operation.
        /// </summary>
        public object data;

        /// <summary>
        /// Controls whether the downloaded data is received as text (URLLoaderDataFormat.TEXT), raw binary data (URLLoaderDataFormat.BINARY), or URL-encoded variables (URLLoaderDataFormat.VARIABLES).
        /// </summary>
        public string dataFormat = "text";

        #endregion

        #region Methods
        /// <summary>
        /// Closes the load operation in progress.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Sends and loads data from the specified URL.
        /// </summary>
        public void load(URLRequest request)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a URLLoader object.
        /// </summary>
        public URLLoader(URLRequest request)
        {
        }

        /// <summary>
        /// Creates a URLLoader object.
        /// </summary>
        public URLLoader()
        {
        }

        #endregion


        #region Events
        /// <summary>
        /// Dispatched after all the received data is decoded and placed in the data property of the URLLoader object.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> complete;

        /// <summary>
        /// Dispatched if a call to URLLoader.load() attempts to access data over HTTP.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<HTTPStatusEvent> httpStatus;

        /// <summary>
        /// Dispatched if a call to URLLoader.load() results in a fatal error that terminates the download.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<IOErrorEvent> ioError;

        /// <summary>
        /// Dispatched when the download operation commences following a call to the URLLoader.load() method.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> open;

        /// <summary>
        /// Dispatched when data is received as the download operation progresses.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ProgressEvent> progress;

        /// <summary>
        /// Dispatched if a call to URLLoader.load() attempts to load data from a server outside the security sandbox.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<SecurityErrorEvent> securityError;

        #endregion

     
    }
}
