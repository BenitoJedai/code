using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimpleChat.Library;
using System.IO;

namespace SimpleChat
{
	public class WebServerProvider : WebServer
	{
		public WebServerComponent Context;

		public void Shutdown()
		{
			if (InternalThread != null)
				InternalThread.Abort();
		}

		Thread InternalThread;

		public void Start()
		{
			InternalThread = this.Port.ToListener(
				s =>
				{
					// somebody connected to us


					var w = new BinaryWriter(s);

					foreach (var k in this.Locals)
					{
						w.Write(Encoding.ASCII.GetBytes(k.Name + "\n"));
					}

					s.Close();
				}
			);
		}
	}
}

