using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace xchrome.BCLImplementation.System.Net.Sockets
{
	[Script(Implements = typeof(global::System.Net.Sockets.Socket))]
	public class __Socket
	{
		// https://www.chromium.org/developers/how-tos/how-to-set-up-visual-studio-debugger-visualizers
		// https://www.chromium.org/developers/how-tos/build-instructions-windows/work-around-for-msvs-2013

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp

		public static implicit operator global::System.Net.Sockets.Socket(__Socket i) => (global::System.Net.Sockets.Socket)(object)i;
		public static implicit operator __Socket(global::System.Net.Sockets.Socket i) => (__Socket)(object)i;


		public delegate void BindDelegate(EndPoint localEP);
		public BindDelegate vBind;
		public void Bind(EndPoint localEP) => vBind(localEP);
	}
}
