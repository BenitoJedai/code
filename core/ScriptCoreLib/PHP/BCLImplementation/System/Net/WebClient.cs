using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.WebClient))]
	internal class __WebClient : __Component
	{
        public Encoding Encoding { get; set; }

        public string DownloadString(string u)
        {
            var content = Native.API.file_get_contents(u);


            return content;
        }

		public string DownloadString(Uri u)
		{
			var content = Native.API.file_get_contents(u.ToString());


			return content;
		}
	}
}
