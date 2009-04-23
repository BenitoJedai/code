using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet;
using ScriptCoreLibAppJet.JavaScript.Library;
using OrcasAppJetApplication.Library;

namespace OrcasAppJetApplication
{
	[Script]
	public static class Server
	{
		[Script, Serializable]
		public sealed class DataItem
		{
			public string id;

			public string Key;
			public string Value;
		}

		[Script, Serializable]
		public sealed class SmartClient
		{
			public string id;

			public string url;
			public string data;
		}


		public static void Render()
		{
			// /* appjet:version 0.1 */ 

			Native.page.setMode("plain");

			

			var c = "stream1".ToStorableCollection();

			("<p>" + c.size() + "<p>").ToConsole();

			c.add(
				new DataItem
				{
					Key = "hello",
					Value = "world"
				}
			);

			"<ol>".ToConsole();

			for (int i = 0; i < c.size(); i++)
			{
				var k = (DataItem)c.At(i);

				("<li>" + k.Key + ": " + k.Value + "</li>").ToConsole();
			}

			"</ol>".ToConsole();


			if (!Native.storage.Contains("counter"))
				Native.storage.SetValue("counter", 0);

			Native.storage.SetValue("counter", (Native.storage.GetValue<int>("counter") + 1));

			Native.print("hello world: " + Native.storage.GetValue<int>("counter"));

			"<hr />".ToConsole();

			if (Native.request.isGet)
			{
				if (Native.request.path == "/x")
				{
					"<p>you found a <b>secret</b>!</p>".ToConsole();
				}
				else
				{
					"<a href='/x'>Go left</a>".ToConsole();
				}

				if (Native.request.path.StartsWith("/y"))
				{
					"<p>you found another <b>secret</b>!</p>".ToConsole();
				}
				else
				{
					"<a href='/y0'>Go right</a>".ToConsole();

				}

				"<a href='/'>Home</a>".ToConsole();
			}



		


		}

		static Server()
		{
			Native.import("storage");
			Render();
		}
	}
}
