using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleChat2.ServerProvider.Library;
using ScriptCoreLib.Reflection.Options;
using System.Collections;
using System.IO;

namespace SimpleChat2.ServerProvider
{
	public partial class RequestDispatcher : Component
	{
		public RequestDispatcher()
		{
			InitializeComponent();
		}

		public RequestDispatcher(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}




		[DefaultValue(8080)]
		public int Port
		{
			get;
			set;
		}

		bool _DispatcherEnabled;

		public bool DispatcherEnabled
		{
			get
			{
				return _DispatcherEnabled;
			}

			set
			{
				_DispatcherEnabled = value;


				if (Rack != null)
				{
					Rack.Stop();
					Rack = null;
				}

				if (value)
				{
					Rack = new VirtualServerRack();
					Rack.Ports = new[] { Port };
					Rack.Start();

					Rack.CommandRequest += new VirtualServerRack.CommandRequestDelegate(Rack_CommandRequest);
				}
			}
		}

		public readonly ArrayList Handlers = new ArrayList();

		void Rack_CommandRequest(System.IO.Stream s, string path)
		{
			var a = new ArrayList();
			var r = default(RequestHandler.IResponse);

			foreach (RequestHandler k in Handlers)
			{
				var n = (RequestHandler.IResponse)Activator.CreateInstance(k.GetTargetType);
				var args = path.GetArguments();

				// rude hack to override design issues
				if (args[0] == n.DefaultRequestPath.Substring(1))
				{
					args[0] = k.GetTargetType.Name;

					k.RegisterInvokeAction(n);

					args.AsParametersTo(
						n.Invoke
					);

					if (n.Content != null)
						r = n;
				}
			}

			var w = new BinaryWriter(s);
			var ww = new StringBuilder();

			// default response

			ww.AppendLine("HTTP/1.0 200 OK");
			ww.AppendLine("Content-Type: " + "text/html");
			ww.AppendLine();

			if (r == null)
				ww.Append("");
			else
				ww.Append(r.Content);

			w.Write(Encoding.UTF8.GetBytes(ww.ToString()));

		}

		VirtualServerRack Rack;


		public event Action Tick;

		private void _DispatcherTimer_Tick(object sender, EventArgs e)
		{
			if (Tick != null)
				Tick();
		}
	}
}
