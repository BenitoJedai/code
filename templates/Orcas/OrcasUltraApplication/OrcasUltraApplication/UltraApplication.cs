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
using java.awt;
using ScriptCoreLib.JavaScript.DOM;
using System.ComponentModel;

namespace OrcasUltraApplication
{

	//[Description("OrcasClientScriptApplication. Write javascript, flash and java applets within a C# project.")]

	public sealed class UltraApplication
	{
		public sealed class UltraApplet : Applet
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;


			public override void init()
			{
				base.resize(DefaultWidth, DefaultHeight);
				// creating the java applet

			}

			static Color GetBlue(double b)
			{
				int u = (int)(0xff * b);

				return new Color(u);
			}

			public override void paint(global::java.awt.Graphics g)
			{
				// old school gradient :)

				var h = this.getHeight();
				var w = this.getWidth();

				for (int i = 0; i < h; i++)
				{

					g.setColor(GetBlue(1 - (double)i / (double)h));
					g.drawLine(0, i, w, i);
				}
			}
		}



		public sealed class UltraSprite : Sprite
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;

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


				r.AttachTo(this);
			}


		}

		public UltraApplication(IHTMLElement e)
		{
			// we are attaching to the DOM now after onload event
			// bootstrap code was generated by jsc.meta and is using ScriptCoreLib

			{
				var x = new IHTMLButton("create UltraSprite proxied");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraSprite();

						o.AttachSpriteToDocument();
					};
			}

			{
				var x = new IHTMLButton("create UltraApplet proxied");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraApplet();

						o.AttachAppletToDocument();
					};
			}

		}


	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{

		public void Hello(string x, StringAction result)
		{
			result(x + " hi");
		}
	}
}
