using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using Ultra1.Common;
using Ultra1.Inline;
using ScriptCoreLib.ActionScript.flash.net;

namespace Ultra3
{
	[ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	[SWF(width = DefaultWidth, height = DefaultHeight)]
	public class UltraSprite : Sprite
	{
		public const int DefaultWidth = 800;
		public const int DefaultHeight = 600;

		TextField t;

		Sprite Background;

		public UltraSprite()
		{
			Background = new Sprite();
			Background.AttachTo(this);

		

			// creating the flash object 
			// + stratus
			// + alchemy

			// funny :) i have forgotten how to write anything
			// on flash API ... too much WPF API?
			var r = new Sprite();

			r.graphics.beginFill(0x7070);
			r.graphics.drawRect(8, 22, 64, 64);

			t = new TextField
			{
				// sandbox must be remote it to work!

				text = "click on left - " + Security.sandboxType
			};

			t.AttachTo(this).MoveTo(100, 8);

			Security.allowDomain("*");

			status1 = "!";

			r.click +=
				delegate
				{
					status1 += "+";

					t.text = "sending...";

					r.graphics.beginFill(0xFF70);
					r.graphics.drawRect(8, 22, 64, 64);



					RaiseToJavaScript("a", "b");
				};

			r.AttachTo(this);


			ToActionScript +=
				(x, y) =>
				{
					t.text = Class1.ToString(x, y) + Class2.ToString(x, y);
				};

			
	
		}

		Loader InternalLoader;

		public void LoadFlash(string title,string url)
		{
			if (InternalLoader != null)
			{
				this.Background.removeChild(InternalLoader);
				InternalLoader = null;
			}

			InternalLoader = new Loader();
			InternalLoader.AttachTo(this.Background);

			t.text = "loading: " + title;

			
			try
			{
				// <div style="text-align:center;width:465px;"><iframe title="forked from: [Stardust] Pixel3D with a brilliant radiance - wonderfl build flash online" scrolling="no" src="http://wonderfl.net/blogparts/cffff80f3e7e58d4b330e27b566ff2efa094649b" width="465" height="490" style="border:1px black solid;"></iframe><a href="http://wonderfl.net/code/cffff80f3e7e58d4b330e27b566ff2efa094649b" title="forked from: [Stardust] Pixel3D with a brilliant radiance - wonderfl build flash online">forked from: [Stardust] Pixel3D with a brilliant radiance - wonderfl build flash online</a></div>
				// http://swf.wonderfl.net/swf/usercode/c/cf/cfff/cffff80f3e7e58d4b330e27b566ff2efa094649b.swf

				//var url = new URLRequest("http://wonderfl.net/code/cffff80f3e7e58d4b330e27b566ff2efa094649b/swf");
				var u = new URLRequest(url);

				
				InternalLoader.load(u);
			}
			catch (Exception ex)
			{
				t.text = ex.Message;
			}
		}






		public string status1 { get; set; }



		public delegate void Action1(string x, string y);
		public delegate void Action2(string x, string y);

		/// <summary>
		/// javascript listens, flash talks
		/// </summary>
		public event Action1 ToJavaScript;

		public void RaiseToJavaScript(string x, string y)
		{
			if (ToJavaScript != null)
				ToJavaScript(x, y);
		}



		/// <summary>
		/// javascript talks, flash listens
		/// </summary>
		public event Action2 ToActionScript;

		public void RaiseToActionScript(string x, string y)
		{
			if (ToActionScript != null)
				ToActionScript(x, y);
		}
	}

}
