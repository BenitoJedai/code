using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.ActionScript.MochiLibrary.Ad
{
    [Script(IsNative = true)]
    public class MochiServices
    {
		/// <summary>
		/// Retrieves and initializes the MochiServices.swf from MochiAds servers. Any API calls executed prior to the complete download and initialization of the MochiServices.swf are queued and will be executed once the API is available 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="root"></param>
        public static void connect(string key, object root)
        {

        }
    }


}