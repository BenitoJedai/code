﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

namespace OrcasUltraWebApplication3
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var cloud = new OrcasUltraWebApplication2.TheCloudEffect();

			cloud.AttachSpriteToDocument();


			cloud.AtLogoClick +=
				delegate
				{
					Native.Window.alert("from flash");
				};

			new IHTMLDiv { innerHTML = "Hello world!" }.AttachToDocument();

			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachToDocument();

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachToDocument();

							}
						);

					};
			}

			{
				var btn = new IHTMLButton { innerText = "Load ExampleLibrary" }.AttachToDocument();

				btn.onclick +=
					delegate
					{
						btn.disabled = true;

						var s = new IHTMLScript { type = "text/javascript", src = "ExampleLibrary.js" };

						s.onload +=
							delegate
							{
								var f = new IFunction("e", "return ExampleLibrary_Method1(e);");

								var btn2 = new IHTMLButton { innerText = "Invoke ExampleLibrary_Method1" }.AttachToDocument();

								btn2.onclick +=
									delegate
									{
										f.apply(Native.Window, "hello world");
									};
							};

						s.AttachToDocument();
					};
			}
		}

	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}

}
