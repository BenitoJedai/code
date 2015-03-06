using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Sockets
{
	// http://referencesource.microsoft.com/#System/net/System/Net/Sockets/NetworkStream.cs
	// https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/NetworkStream.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\Sockets\NetworkStream.cs
	// x:\jsc.svn\core\scriptcorelibjava\bclimplementation\system\net\sockets\networkstream.cs

	[Script(Implements = typeof(global::System.Net.Sockets.NetworkStream))]
	public class __NetworkStream : __Stream
	{
		// x:\jsc.svn\market\synergy\javascript\chrome\chrome\bclimplementation\system\net\sockets\tcplistener.cs



		public static implicit operator global::System.Net.Sockets.NetworkStream(__NetworkStream i)
		{
			return (global::System.Net.Sockets.NetworkStream)(object)i;
		}

		public static implicit operator __NetworkStream(global::System.Net.Sockets.NetworkStream i)
		{
			return (__NetworkStream)(object)i;
		}
	}
}
