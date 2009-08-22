using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.Socket))]
	internal class __Socket
	{
		public java.net.Socket InternalSocket;
	}
}
