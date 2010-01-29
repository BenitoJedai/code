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

namespace Ultra3
{
	[ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	[SWF(width = DefaultWidth, height = DefaultHeight)]
	public class UltraSprite : Sprite
	{
		public const int DefaultWidth = 500;
		public const int DefaultHeight = 400;

		TextField t;

		public UltraSprite()
		{
			// creating the flash object 
			// + stratus
			// + alchemy

			// funny :) i have forgotten how to write anything
			// on flash API ... too much WPF API?
			var r = new Sprite();

			r.graphics.beginFill(0x7070);
			r.graphics.drawRect(8, 8, 64, 64);

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
					r.graphics.drawRect(8, 8, 64, 64);



					RaiseToJavaScript("a", "b");
				};

			r.AttachTo(this);


			ToActionScript +=
				(x, y) =>
				{
					t.text = Class1.ToString(x, y) + Class2.ToString(x, y);
				};
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
