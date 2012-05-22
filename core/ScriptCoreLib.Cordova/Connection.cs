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
    /// The connection object gives access to the device's cellular and wifi connection information
    /// http://docs.phonegap.com/en/1.7.0/cordova_connection_connection.md.html#Connection
    /// </summary>
    [Script(IsNative = true)]
    public class Connection
    {
        #region Constructor

        public Connection()
        {

        }

        #endregion

        #region METHODS

        public void getPicture(Action<object> cameraSuccess, Action<string> cameraError,CameraOptions options=null)
        {

        }

        #endregion


        #region Properties 

        /// <summary>
        /// Checks the active network connection that is being used
        /// </summary>
        public string type { get { return default(string); } }

        #endregion

        #region Constants 

        public static string UNKNOWN = "unknown";
        public static string ETHERNET = "ethernet";
        public static string WIFI = "wifi";
        public static string CELL_2G = "2g";
        public static string CELL_3G = "3g";
        public static string CELL_4G = "4g";
        public static string NONE = "none";

        #endregion
    }
}
