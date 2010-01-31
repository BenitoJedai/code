﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.applet;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using System.ComponentModel;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using Ultra1.Common;
using Ultra1.Inline;

namespace Ultra3
{






	[ScriptApplicationEntryPoint]
	[Description("OrcasClientScriptApplication. Write javascript, flash and java applets within a C# project.")]
	public class UltraApplication
	{


		// possible names:
		// 1. application
		// 2. document
		// 3. element
		// 4. control



		public UltraApplication(IHTMLElement e)
		{
			// we are attaching to the DOM now after onload event
			// bootstrap code was generated by jsc.meta and is using ScriptCoreLib

			new IHTMLDiv
			{
				innerText = "javascript to flash does not work in IE"
			}.ToWarningColor().AttachToDocument();

			new IHTMLDiv
			{
				innerText = "flash needs to run from http://"
			}.ToWarningColor().AttachToDocument();

			new IHTMLDiv
			{
				innerText = "Create sprite, add event, raise event, click on color"
			}.AttachToDocument();

			#region UltraSprite javascript to flash
			{
				var x = new IHTMLButton("create UltraSprite javascript to flash");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraSprite();

						o.AttachToDocument();

						var f4 = new IHTMLButton("UltraSprite.raise_event1");

						f4.onclick +=
							delegate
							{
								o.RaiseToActionScript("q", "w");
							};

						// how we get it:
						var f3 = new IHTMLButton("UltraSprite.add_event1");

						f3.onclick +=
							delegate
							{
								f3.style.color = Color.Blue;

								o.ToJavaScript +=
									(xx, y) =>
									{
										f3.style.color = JSColor.Red;

										new IHTMLDiv { innerText = Class1.ToString(xx, y)  + Class2.ToString(xx, y) }.AttachToDocument();
									};
							};


						f4.AttachToDocument();
						f3.AttachToDocument();


						//AddLoadFlashButton(o, "Startust", 
						//    "http://swf.wonderfl.net/swf/usercode/c/cf/cfff/cffff80f3e7e58d4b330e27b566ff2efa094649b.swf");
						//AddLoadFlashButton(o, "Smoke", 
						//    "http://swf.wonderfl.net/swf/usercode/e/e5/e545/e5458ebf7b16817e3529321415c4f17ce965515a.swf"
						//    //"http://swf.wonderfl.net/swf/usercode/e/e5/e545/e5458ebf7b16817e3529321415c4f17ce965515a.swf"
						//    );
						AddLoadFlashButton(o, "Spider Solitaire",
							"http://games.mochiads.com/c/g/avalon-spider-solitare/SpiderFlash.swf"
							);
					};
			}
			#endregion


			{
				var x = new IHTMLButton("create UltraApplet proxied");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraApplet();

						o.AttachToDocument();

						var f1 = new IHTMLButton("RaiseToJava");

						f1.AttachToDocument();

						UltraApplet.Action1 a =
							(xx, y) =>
							{
								new IHTMLDiv { innerText = Class1.ToString(xx, y) + Class2.ToString(xx, y) }.AttachToDocument();
								
							};

						f1.onclick +=
							delegate
							{
								o.RaiseToJava("hi from js", "");
							};

						var f2 = new IHTMLButton("add_event");

						f2.AttachToDocument();

						f2.onclick +=
							delegate
							{
								o.AtToJavaScript += a;

							};

					};
			}


		}

		private static void AddLoadFlashButton(UltraSprite o, string Title, string Source)
		{
			var f5 = new IHTMLButton(Title);

			f5.onclick +=
				delegate
				{
					o.LoadFlash(
						Title, Source
					);

				};

			f5.AttachToDocument();

		}


	}


}
