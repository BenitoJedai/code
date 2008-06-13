using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Nonoba.api
{
    [Script(IsNative = true)]
    public static class NonobaAPI
    {
        #region Methods
        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public static Connection MakeMultiplayer(Stage stage, string s, double p)
        {
            return default(Connection);
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public static Connection MakeMultiplayer(Stage stage, string s)
        {
            return default(Connection);
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public static Connection MakeMultiplayer(Stage stage)
        {
            return default(Connection);
        }

        #endregion

    }
}
