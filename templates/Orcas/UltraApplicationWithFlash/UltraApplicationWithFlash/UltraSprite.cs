using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;
using ScriptCoreLib.JavaScript;

namespace UltraApplicationWithFlash
{
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

			Console.WriteLine("UltraSprite.ctor");

			r.graphics.beginFill(0x7000);
			r.graphics.drawRect(8, 8, 64, 64);


			r.AttachTo(this);
		}

		public void BuildPage(IHTMLBuilder body)
		{
			Console.WriteLine("UltraSprite.BuildPage");


			Action Continue =
				delegate
				{
					body.CreateAttachedElement("div",
						div =>
						{
							div.SetContent(div, div, "test");

							div.innerText = "hello";

							div.CreateAttachedElement("span",
								span =>
								{
									span.innerText = "Click buttons!";
									span.setAttribute("style", "color: red;");

									div.CreateAttachedElement("button",
										button1 =>
										{
											button1.innerText = "Button 1";

											button1.onclick +=
												delegate
												{
													span.innerText = "Button 1";
													span.setAttribute("style", "color: blue;");

													{
														var r = new Sprite();

														r.graphics.beginFill(0xff0000);
														r.graphics.drawRect(8, 8, 64, 64);

														r.AttachTo(this);
													}
												};


										}
									);

									div.CreateAttachedElement("button",
										button1 =>
										{
											button1.innerText = "Button 2";

											button1.onclick +=
												delegate
												{
													span.innerText = "Button 2";
													span.setAttribute("style", "color: green;");

													{
														var r = new Sprite();

														r.graphics.beginFill(0xffff00);
														r.graphics.drawRect(8, 8, 64, 64);

														r.AttachTo(this);
													}
												};


										}
									);
								}
							);
						}
					);
				};


			{
				var r = new Sprite();

				r.graphics.beginFill(0xff);
				r.graphics.drawRect(8, 8, 64, 64);

				r.AttachTo(this);

				r.click +=
					delegate
					{
						r.Orphanize();

						Continue();
					};
			}
			//broken for ie again!

			//Console.WriteLine("ex: " + ScriptCoreLib.ActionScript.flash.external.ExternalInterface.available);

			//ScriptCoreLib.ActionScript.flash.external.ExternalInterface.call("(function () { alert('hi'); } )()");
			//ScriptCoreLib.ActionScript.flash.external.ExternalInterface.call("alert", "yay");
		}

		public void PingPongService(IPingPong e, Action<IPingPong> y)
		{
			y(e);
		}
	}

	public class JavaScriptPingPong : IPingPong
	{
		public Action AtMethod1;

		public void Method1()
		{
			AtMethod1();
		}
	}

	public interface IPingPong
	{
		void Method1();
	}

	public delegate void IHTMLBuilderAction(IHTMLBuilder e);

	public interface IHTMLBuilder
	{
		void CreateElement(string name, IHTMLBuilderAction yield);
		void CreateAttachedElement(string name, IHTMLBuilderAction yield);

		void Add(IHTMLBuilder child);

		event Action onclick;

		string innerText { set; }

		void setAttribute(string key, string value);

		void SetContent(IHTMLBuilder a, IHTMLBuilder b, string e);
	}

	public static class IHTMLBuilderExtensions
	{
		public static IHTMLBuilder AttachTo(this IHTMLBuilder e, IHTMLBuilder c)
		{
			e.Add(c);

			return e;
		}
	}

	public class IHTMLBuilderImplementation : IHTMLBuilder
	{
		public IHTMLElement InternalElement;

		public void SetContent(IHTMLBuilder a, IHTMLBuilder b, string e)
		{
			this.innerText = e;

			//Debugger.Break();

			a.innerText = e;
			b.innerText = e;
		}

		#region IHTMLBuilder Members

		public void CreateElement(string name, IHTMLBuilderAction yield)
		{
			//Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.CreateElement"));

			yield(
				(IHTMLBuilderImplementation)InternalElement.ownerDocument.createElement(name)
			);
		}

		public void CreateAttachedElement(string name, IHTMLBuilderAction yield)
		{
			//Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.CreateElement"));

			var v = (IHTMLBuilderImplementation)InternalElement.ownerDocument.createElement(name);


			this.Add(v);

			yield(
				v
			);
		}

		public void Add(IHTMLBuilder child)
		{
			//Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.Add"));

			var o = (object)child;
			var i = child as IHTMLBuilderImplementation;

			if (i != null)
				InternalElement.Add(i.InternalElement);
			else
				Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.Add child is null?"));
		}

		public event Action onclick
		{
			add
			{
				//Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.add_onclick"));
				this.InternalElement.onclick +=
					delegate
					{
						value();
					};
			}

			remove
			{

			}
		}

		public string innerText
		{
			set
			{
				//Native.Document.body.appendChild(new IHTMLDiv("IHTMLBuilderImplementation.set_innerText"));

				this.InternalElement.innerText = value;
			}
		}

		public void setAttribute(string key, string value)
		{
			this.InternalElement.setAttribute(key, value);
		}

		#endregion

		public static implicit operator IHTMLBuilderImplementation(IHTMLElement e)
		{
			return new IHTMLBuilderImplementation { InternalElement = e };
		}

	}
}
