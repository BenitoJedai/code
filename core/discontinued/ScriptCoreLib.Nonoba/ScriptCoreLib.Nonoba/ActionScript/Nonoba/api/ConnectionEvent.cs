using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Nonoba.api
{
    [Script(IsNative = true)]
    public class ConnectionEvent : Event
    {
        #region Constants
        /// <summary>
        /// 
        /// </summary>
        public static readonly string DISCONNECT = "onDisconnect";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string INIT = "onInit";

        #endregion

    }
}
