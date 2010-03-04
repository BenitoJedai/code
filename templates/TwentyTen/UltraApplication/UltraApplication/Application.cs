using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using java.applet;
using java.awt;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Diagnostics.Contracts;

namespace UltraApplication
{


	public sealed class UltraApplication
	{

		public UltraApplication(IHTMLElement e)
		{
			// we are attaching to the DOM now after onload event
			// bootstrap code was generated by jsc.meta and is using ScriptCoreLib

			//{
			//    var x = new IHTMLButton("create UltraSprite proxied");

			//    x.AttachToDocument();

			//    x.onclick +=
			//        delegate
			//        {
			//            var o = new UltraSprite();

			//            o.AttachSpriteToDocument();
			//        };
			//}

			//{
			//    var x = new IHTMLButton("create UltraApplet proxied");

			//    x.AttachToDocument();

			//    x.onclick +=
			//        delegate
			//        {
			//            var o = new UltraApplet();

			//            o.AttachAppletToDocument();
			//        };
			//}

			{
				var x = new IHTMLButton("UltraWebService.Hello");

				x.AttachToDocument();

				HelloAction Hello = new UltraWebService().Hello;

				x.onclick +=
					delegate
					{
						Hello(
						x: "XXX",

							result:
							ee =>
							{
								new IHTMLDiv { innerText = ee }.AttachToDocument();
							}
						);
					};
			}

		}

		//public sealed class UltraApplet : Applet
		//{
		//    public const int DefaultWidth = 500;
		//    public const int DefaultHeight = 400;


		//    public override void init()
		//    {
		//        base.resize(DefaultWidth, DefaultHeight);
		//        // creating the java applet

		//    }

		//    static Color GetBlue(double b)
		//    {
		//        int u = (int)(0xff * b);

		//        return new Color(u);
		//    }

		//    public override void paint(global::java.awt.Graphics g)
		//    {
		//        // old school gradient :)

		//        var h = this.getHeight();
		//        var w = this.getWidth();

		//        for (int i = 0; i < h; i++)
		//        {

		//            g.setColor(GetBlue(1 - (double)i / (double)h));
		//            g.drawLine(0, i, w, i);
		//        }
		//    }
		//}



		//public sealed class UltraSprite : Sprite
		//{
		//    public const int DefaultWidth = 500;
		//    public const int DefaultHeight = 400;

		//    public UltraSprite()
		//    {
		//        // creating the flash object 
		//        // + stratus
		//        // + alchemy

		//        // funny :) i have forgotten how to write anything
		//        // on flash API ... too much WPF API?
		//        var r = new Sprite();

		//        r.graphics.beginFill(0x7070);
		//        r.graphics.drawRect(8, 8, 64, 64);


		//        r.AttachTo(this);
		//    }


		//}


		/*
		 jsc needs to implement IL simplifier first. :) 
		 
		public event Action EventMember;

public event Action EventMember
{
    add
    {
        Action action2;
        Action eventMember = this.EventMember;
        do
        {
            action2 = eventMember;
            Action action3 = (Action) Delegate.Combine(action2, value);
            eventMember = Interlocked.CompareExchange<Action>(ref this.EventMember, action3, action2);
        }
        while (eventMember != action2);
    }
    remove
    {
        Action action2;
        Action eventMember = this.EventMember;
        do
        {
            action2 = eventMember;
            Action action3 = (Action) Delegate.Remove(action2, value);
            eventMember = Interlocked.CompareExchange<Action>(ref this.EventMember, action3, action2);
        }
        while (eventMember != action2);
    }
}
 

 

		*/

		public void T\u0418хомирIvanov()
		{
			// http://www.devtheweb.net/blog/2010/02/25/things-you-probably-didnt-know-about-csharp/
		}
	}

	public delegate void HelloAction(string x = "2010 X", StringAction result = null);
	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{

		public void Hello(string x = "2010", StringAction result = null)
		{
			result(x + " hi");
		}


	
	}
}
