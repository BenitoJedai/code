using System;
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
using java.awt.@event;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.ActionScript.flash.external;
using Ultra1.Common;
using System.Diagnostics;
using System.Web;

namespace Ultra4
{

	//[Description("OrcasClientScriptApplication. Write javascript, flash and java applets within a C# project.")]


	public sealed partial class UltraApplication
	{

		public interface IAddShape
		{
			void AddShape1(string color);

			void AddShape2(IParameters1 e);

			IParameters1 ColorControl
			{
				get;
			}
		}


		public interface IParameters1
		{
			string Color { get; set; }
			IParameters1 Redirect { get; }
		}

		public class XSendStatus : Ultra4.IXSendStatus
		{
			public string e;

			public Action At;

			public void SendStatus(string e)
			{
				this.e = e;

				At();
			}
		}

		//public sealed partial class UltraApplet : Applet, IAddShape
		//{

		//    public const int DefaultWidth = 500;
		//    public const int DefaultHeight = 400;

		//    public class __MouseListener : MouseListener
		//    {
		//        public event Action Clicked;

		//        #region MouseListener Members

		//        public void mouseClicked(MouseEvent e)
		//        {
		//            if (Clicked != null)
		//                Clicked();
		//        }

		//        public void mouseEntered(MouseEvent e)
		//        {
		//        }

		//        public void mouseExited(MouseEvent e)
		//        {
		//        }

		//        public void mousePressed(MouseEvent e)
		//        {
		//        }

		//        public void mouseReleased(MouseEvent e)
		//        {
		//        }

		//        #endregion
		//    }

		//    partial void SetStatus(string v);

		//    public override void init()
		//    {
		//        SetStatus("java");

		//        base.resize(DefaultWidth, DefaultHeight);
		//        // creating the java applet

		//        var c = new __MouseListener();

		//        c.Clicked +=
		//            delegate
		//            {
		//                AddShape1("green");
		//                RaiseClicked();
		//            };

		//        this.addMouseListener(c);

		//        //AddShape1("green");
		//    }

		//    partial void RaiseClicked();


		//    int ColorShift = 8;

		//    Color GetBlue(double b)
		//    {
		//        int u = (int)(0xff * b);

		//        u <<= ColorShift;


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

		//    public void AddShape1(string color)
		//    {
		//        if (color == "red")
		//            AddShape(16);
		//        else if (color == "green")
		//            AddShape(8);
		//        else
		//            AddShape(0);

		//    }

		//    private void AddShape(int p)
		//    {
		//        ColorShift = p;
		//        this.invalidate();
		//        this.repaint();
		//        //this.invalidate();
		//    }

		//    public void AddShape2(IParameters1 e)
		//    {
		//        AddShape1(e.Color);
		//    }

		//    public IParameters1 ColorControl
		//    {
		//        get
		//        {
		//            var p = new Parameters2
		//            {
		//            };

		//            p.ColorChanged +=
		//                delegate
		//                {
		//                    this.AddShape2(p);
		//                };

		//            return p;
		//        }
		//    }
		//}



		public sealed partial class UltraSprite : Sprite, IAddShape
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;

			partial void SetStatus(string v);
			partial void RaiseClicked();

			ScriptCoreLib.ActionScript.flash.text.TextField MyText = new ScriptCoreLib.ActionScript.flash.text.TextField();

			public delegate void Delgate1(string a, string b);

			 public event Action AtEvent1;
		

			public event Delgate1 AtEvent2;
		

			public string GetStatus1()
			{
				return "ok";
			}

			public void GetStatus2(IXSendStatus e)
			{
				MyText.text = "GetStatus2..";

				//AtDelay(1000,
				//    delegate
				//    {
				e.SendStatus("ok");

				//        MyText.text = "GetStatus2.. done";
				//    }
				//);


			}

			public UltraSprite()
			{
				MyText.text = "flash!:D 9";
				MyText.MoveTo(128, 8).AttachTo(this);

				// creating the flash object 
				// + stratus
				// + alchemy

				// funny :) i have forgotten how to write anything
				// on flash API ... too much WPF API?
				AddShape(0x7070);
			}

			public void AddShape1(string color)
			{
				if (color == "red")
					AddShape(0xff0000);
				else if (color == "green")
					AddShape(0xff00);
				else
					AddShape(0xff);

				//AtDelay(1000,
				//    delegate
				//    {
				//        var u = ExternalInterface.call("confirm", "hello");

				//        MyText.text = "confirm: " + u;
				//    }
				//);

				if (this.AtEvent1 != null)
					this.AtEvent1();

				RaiseEvent2();
			}

			private void RaiseEvent2()
			{
				if (this.AtEvent2 != null)
					this.AtEvent2("a", "b");
			}

			public void AddShape2(IParameters1 e)
			{
				//MyText.text = "AddShape2..";

				//AddShape1("red");

				var c = e.Color;
				AddShape1(c);
				MyText.text = "AddShape2: " + c;

				//Action AtLater =
				//    delegate
				//    {
				//        //AddShape2Later(e);
				//    };



				//AtDelay(5000, AtLater);
			}

			private static void AtDelay(int delay, Action AtLater)
			{
				var t = new ScriptCoreLib.ActionScript.flash.utils.Timer(delay, 1);

				t.timer +=
					delegate
					{
						AtLater();
					};

				t.start();
			}

			private void AddShape2Later(IParameters1 e)
			{
				try
				{
					var c = e.Color;
					AddShape1(c);
					MyText.text = "AddShape2: " + c;

				}
				catch (Exception ex)
				{
					MyText.text = "ex: " + ex.Message;
				}
			}


			private void AddShape(uint color)
			{
				var r = new Sprite();

				r.graphics.beginFill(color);
				r.graphics.drawRect(8, 8, 64, 64);

				r.click +=
					delegate
					{
						RaiseClicked();
					};

				r.AttachTo(this);
			}


			public IParameters1 ColorControl
			{
				get
				{
					var p = new Parameters2
					{
					};

					p.ColorChanged +=
						delegate
						{
							this.AddShape2(p);
						};

					return p;
				}
			}


		}

		public class Parameters1 : IParameters1
		{
			public string Color { get; set; }
			public IParameters1 Redirect { get; set; }
		}


		public class Parameters2 : IParameters1
		{
			public event Action ColorChanged;
			public event Action ColorQueried;

			string _Color;
			public string Color
			{
				get
				{
					if (ColorQueried != null)
						ColorQueried();

					return _Color;
				}
				set
				{
					_Color = value;
					if (ColorChanged != null)
						ColorChanged();
				}
			}
			public IParameters1 Redirect { get; set; }
		}



		public interface Interface1
		{
			void Method1(string e);
		}

		public class ClassA : Interface1
		{
			public void Method1(string e)
			{
			}
		}

		public class ClassB : Interface1
		{
			public void Method1(string e)
			{

			}
		}

		ClassA A = new ClassA();
		ClassB B = new ClassB();

		public UltraApplication(IHTMLElement e)
		{
			// we are attaching to the DOM now after onload event
			// bootstrap code was generated by jsc.meta and is using ScriptCoreLib
			{
				var x = new IHTMLButton("create UltraSprite");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraSprite();

						CreateButton(o, "blue");
						CreateButton(o, "red");
						CreateButton(o, "green");

						{
							var n = new IHTMLButton("status");

							n.onclick +=
								delegate
								{

									new IHTMLDiv { innerText = "status: " + o.GetStatus1() }.AttachToDocument();



								};

							n.AttachToDocument();
						}

						{
						

							o.AtEvent2 +=
								(a, b) =>
								{
									//Debugger.Break();

									new IHTMLDiv { innerText = "AtEvent2! " +
										
										// jsc.meta does not support generics yet

										// new { a, b }.ToString() 
									
										a + " " + b
									}.AttachToDocument();

								};

							o.AtEvent1 +=
								delegate
								{
									//Debugger.Break();

									new IHTMLDiv { innerText = "AtEvent1! " }.AttachToDocument();

								};
						}

						{
							var n = new IHTMLButton("status 2");

							n.onclick +=
								delegate
								{
									var p = new XSendStatus
									{

									};

									p.At =
										delegate
										{
											//Debugger.Break();

											new IHTMLDiv { innerText = "status 2: " + p.e }.AttachToDocument();
										};


									o.GetStatus2(p);

								};

							n.AttachToDocument();
						}


						o.AttachSpriteToDocument();
					};
			}


			//{
			//    var x = new IHTMLButton("create UltraApplet");

			//    x.AttachToDocument();

			//    x.onclick +=
			//        delegate
			//        {
			//            x.style.color = JSColor.Red;


			//            var o = new UltraApplet();

			//            CreateButton(o, "red");
			//            CreateButton(o, "green");
			//            CreateButton(o, "blue");


			//            o.AttachAppletToDocument();
			//        };
			//}



			{
				var n = new IHTMLButton("webservice");

				n.onclick +=
					delegate
					{


						var ws = new WebService();

						// we only access methods for now. later we could enable
						// string based proxing within session
						// if sessions support storing objects
						// asp.net, php and gae

						ws.Method1("hello",
							x =>
							{
								// back from the web server
								// be it asp.net
								// gae or php... :)

								new IHTMLDiv { innerText = "webservice: " + x }.AttachToDocument();

							}
						);

					};

				n.AttachToDocument();
			}


			{
				var n = new IHTMLButton("webservice manually");

				n.onclick +=
					delegate
					{


						var x = new IXMLHttpRequest();

						x.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/?WebMethod=06000039");
						x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
						x.send("_06000039_e=" + Native.Window.escape("ding dong"));

						x.InvokeOnComplete(
							r =>
							{

								var xx = new IHTMLDiv();

								xx.innerHTML = r.responseText;

								xx.AttachToDocument();
							}
						);


					};

				n.AttachToDocument();
			}
		}

		private static void CreateButton(IAddShape z, string t)
		{
			{
				var n = new IHTMLButton("AddShape1 " + t + " new Parameters1 { Color = t }");
				n.style.color = t;

				n.onclick +=
					delegate
					{
						// can opera java call javascript?
						// does not work in opera for java
						// does not work in IE for flash

						var p = new Parameters2 { Color = t };

						p.ColorQueried +=
							delegate
							{
								new IHTMLDiv { innerText = "ColorQueried" }.AttachToDocument();
							};

						//o.AddShape2 = "";
						z.AddShape2(p);


					};

				n.AttachToDocument();
			}

			{
				var n = new IHTMLButton("AddShape1 " + t + " z.ColorControl.Color");
				n.style.color = t;

				n.onclick +=
					delegate
					{
						new IHTMLDiv { innerText = "var i = z.ColorControl;" }.AttachToDocument();

						var i = z.ColorControl;

						new IHTMLDiv { innerText = "i.Color = t;" }.AttachToDocument();

						i.Color = t;

						new IHTMLDiv { innerText = ";" }.AttachToDocument();

					};

				n.AttachToDocument();
			}
			{
				{
					var n = new IHTMLButton("AddShape1 " + t);

					n.style.color = t;
					n.onclick +=
						delegate
						{
							z.AddShape1(t);

						};

					n.AttachToDocument();


				}
			}
		}

		public sealed class WebService
		{
			// initialized on the server
			

			// rewrite this class to after msbuild step
			public delegate void Func1(string e);
			public delegate void Func2(string e, string f);

			// rewrite this to json service?
			// rewrite this to xml service?
			// rewrite this to http/post service?

			// generate sync wrapper

			// we can talk to other servers or databases here

			public void Method1(string e, Func1 result)
			{
				result("hello: " + e);
			}

			public void Method2(string e, string f, Func2 result)
			{
				result("world: " + e, "hi: " + f);
				result("world2: " + e, "hi2: " + f);
			}

			public void Method3(string e, string f, Func2 result, Func1 result2)
			{
				result2("hello: " + e);
				result("world: " + e, "hi: " + f);
				result("world2: " + e, "hi2: " + f);
			}
		}


	}


}
