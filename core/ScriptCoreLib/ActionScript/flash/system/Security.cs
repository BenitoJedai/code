using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/system/Security.html
    [Script(IsNative = true)]
    public static class Security
    {
		#region Methods
		/// <summary>
		/// [static] Loads a cross-domain policy file from a location specified by the url parameter.
		/// </summary>
		public static void loadPolicyFile(string url)
		{
		}

		#endregion

		#region Constructors
		#endregion


        /// <summary>
        /// Determines how Flash Player or AIR chooses the domain to use for certain content settings, including settings for camera and microphone permissions, storage quotas, and storage of persistent shared objects.
        /// </summary>
        static public bool exactSettings { get; private set; }

        /// <summary>
        /// Indicates the type of security sandbox in which the calling file is operating.
        /// </summary>
        static public string sandboxType { get; private set; }


        /// <summary>
        /// This method applies to cross-scripting of ActionScript 3.0 code (in SWF content).
        /// </summary>
        /// <param name="domains"></param>
        public static void allowDomain(string domains)
        {
        }

		/// <summary>
		/// This method applies to cross-scripting of ActionScript 3.0 code (in SWF content).
		/// </summary>
		/// <param name="domains"></param>
		public static void allowInsecureDomain(string domains)
		{
		}


        /// <summary>
        /// Displays the Security Settings panel in Flash Player.
        /// </summary>
        public static void showSettings()
        {

        }
    }
}
