using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SimpleChat
{
	public class WebServerComponent : Component
	{
		WebServerProvider[] InternalList = new WebServerProvider[0];

		public event WebServerProviderAction Shutdown;
		public event WebServerProviderAction Start;


		public WebServer[] Configuration
		{
			set
			{
				var e = value;

				foreach (var i in InternalList)
				{
					var m = default(WebServer);
					foreach (var j in e)
					{
						if (j.Port == i.Port)
						{
							m = j;
							break;
						}
					}
					if (m == null)
					{
						i.Shutdown();

						if (this.Shutdown != null)
							this.Shutdown(i);
					}
				}

				var a = new WebServerProvider[e.Length];

				for (int ki = 0; ki < e.Length; ki++)
				{
					var i = e[ki];
					var m = default(WebServerProvider);
					foreach (var j in InternalList)
					{
						if (j.Port == i.Port)
						{
							m = j;
							break;
						}
					}
					if (m == null)
					{
						m = new WebServerProvider
						{
							Context = this,
							Locals = i.Locals,
							Port = i.Port
						};
						m.Start();
						if (this.Start != null)
							this.Start(m);
					}
					else
					{
						m.Locals = i.Locals;
					}

					a[ki] = m;
				}

				this.InternalList = a;
			}
		}

	}


	public delegate void WebServerProviderAction(WebServerProvider e);



}
