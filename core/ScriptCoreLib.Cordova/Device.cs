using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// The device object describes the device's hardware and software
    /// http://docs.phonegap.com/en/1.7.0/cordova_device_device.md.html#Device
    /// </summary>
    [Script(IsNative = true)]
    public class Device
    {
        #region Constructor

        public Device()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the device's model name
        /// </summary>
        public string name
        {
            get { return default(string); }
            set { }
        }

        /// <summary>
        /// Get the version of Cordova running on the device
        /// </summary>
        public string cordova
        {
            get { return default(string); }
            set { }
        }

        /// <summary>
        /// Get the device's operating system name
        /// </summary>
        public string platform
        {
            get { return default(string); }
            set { }
        }

        /// <summary>
        /// Get the device's Universally Unique Identifier (UUID).
        /// </summary>
        public string uuid
        {
            get { return default(string); }
            set { }
        }

        /// <summary>
        /// Get the operating system version
        /// </summary>
        public string version
        {
            get { return default(string); }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public Capture capture;

        #endregion


    }
}
