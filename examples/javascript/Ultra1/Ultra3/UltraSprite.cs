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


					raise_event1();

				};

			r.AttachTo(this);

	

			FuncSring get_status1 = () => this.status1;

			#region codegen
			Action<string> add_event1 =
				callback =>
				{
					event1 +=
						delegate
						{
							ExternalInterface.call(callback);
						};
				};

			ExternalInterface.addCallback("FunctionTwo", new Action<string>(FunctionTwo).ToFunction());
			ExternalInterface.addCallback("get_status1", get_status1.ToFunction());

			ExternalInterface.addCallback("add_event1", add_event1.ToFunction());
			ExternalInterface.addCallback("raise_event1", new Action(raise_event1).ToFunction());
			#endregion

		}


		public delegate string FuncSring();

	

		public event Action event1;

		public void raise_event1()
		{
			if (event1 != null)
				event1();
		}


		public string status1 { get; set; }

		public void FunctionTwo(string e)
		{
			t.text = e;
		}
	}

}
