using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/IPHostEntry.cs

	[Script(Implements = typeof(global::System.Net.IPHostEntry))]
	internal class __IPHostEntry
	{
		public IPAddress[] AddressList { get; set; }
	}
}
