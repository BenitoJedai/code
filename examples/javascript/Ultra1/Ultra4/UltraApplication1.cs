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
using java.awt.@event;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.ActionScript.flash.external;
using Ultra1.Common;

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


		public sealed partial class UltraApplet : Applet, IAddShape
		{

			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;

			public class __MouseListener : MouseListener
			{
				public event Action Clicked;

				#region MouseListener Members

				public void mouseClicked(MouseEvent e)
				{
					if (Clicked != null)
						Clicked();
				}

				public void mouseEntered(MouseEvent e)
				{
				}

				public void mouseExited(MouseEvent e)
				{
				}

				public void mousePressed(MouseEvent e)
				{
				}

				public void mouseReleased(MouseEvent e)
				{
				}

				#endregion
			}

			partial void SetStatus(string v);

			public override void init()
			{
				SetStatus("java");

				base.resize(DefaultWidth, DefaultHeight);
				// creating the java applet

				var c = new __MouseListener();

				c.Clicked +=
					delegate
					{
						AddShape1("green");
						RaiseClicked();
					};

				this.addMouseListener(c);

				//AddShape1("green");
			}

			partial void RaiseClicked();


			int ColorShift = 8;

			Color GetBlue(double b)
			{
				int u = (int)(0xff * b);

				u <<= ColorShift;


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

			public void AddShape1(string color)
			{
				if (color == "red")
					AddShape(16);
				else if (color == "green")
					AddShape(8);
				else
					AddShape(0);

			}

			private void AddShape(int p)
			{
				ColorShift = p;
				this.invalidate();
				this.repaint();
				//this.invalidate();
			}

			public void AddShape2(IParameters1 e)
			{
				AddShape1(e.Color);
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



		public sealed partial class UltraSprite : Sprite, IAddShape
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;

			partial void SetStatus(string v);
			partial void RaiseClicked();

			public UltraSprite()
			{


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

			}

			public void AddShape2(IParameters1 e)
			{
				AddShape1(e.Color);
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

			string _Color;
			public string Color
			{
				get { return _Color; }
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


					

						o.AttachSpriteToDocument();
					};
			}


			{
				var x = new IHTMLButton("create UltraApplet");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						x.style.color = JSColor.Red;


						var o = new UltraApplet();

						CreateButton(o, "red");
						CreateButton(o, "green");


				

						o.AddShape1("green");
						o.AttachAppletToDocument();
					};
			}


			var ws = new WebService();

			ws.Method1("hello",
				x =>
				{
					// back from the web server
					// be it asp.net
					// gae or php... :)
				}
			);

		}

		private static void CreateButton(IAddShape z, string t)
		{
			{
				var n = new IHTMLButton("AddShape1 " + t + " send interface");
				n.style.color = t;

				n.onclick +=
					delegate
					{
						//o.AddShape2 = "";
						z.AddShape2(
							new Parameters1
							{
								Color = t
							}
						);

					};

				n.AttachToDocument();
			}

			{
				var n = new IHTMLButton("AddShape1 " + t + " get interface");
				n.style.color = t;

				n.onclick +=
					delegate
					{
						var i = z.ColorControl;

						i.Color = t;
					};

				n.AttachToDocument();
			}
			{
				{
					var n = new IHTMLButton("AddShape1 "  + t);

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

		public class WebService
		{
			public object Context;

			// rewrite this class to after msbuild step
			public delegate void Func1(string e);
			public delegate void Func2(string e);

			// rewrite this to json service?
			// rewrite this to xml service?
			// rewrite this to http/post service?

			// generate sync wrapper

			public void Method1(string e, Func1 result)
			{

			}

			public void Method2(string e, Func2 result)
			{

			}
		}


	}


}
