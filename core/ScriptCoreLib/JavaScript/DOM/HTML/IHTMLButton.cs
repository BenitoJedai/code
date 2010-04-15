using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLButton : IHTMLElement
	{
		public bool disabled;

		#region Constructor

		public IHTMLButton()
		{
			// InternalConstructor
		}

		public IHTMLButton(IHTMLDocument doc)
		{
			// InternalConstructor
		}

		static IHTMLButton InternalConstructor()
		{
			return (IHTMLButton)((object)new IHTMLElement(HTMLElementEnum.button));
		}

		static IHTMLButton InternalConstructor(IHTMLDocument doc)
		{
			return (IHTMLButton)((object)new IHTMLElement(HTMLElementEnum.button, doc));
		}

		#endregion


		#region Constructor

		public IHTMLButton(string e)
		{
			// InternalConstructor
		}

		static IHTMLButton InternalConstructor(string e)
		{
			IHTMLButton b = new IHTMLButton();

			b.appendChild(e);

			return b;
		}

		#endregion


		public static IHTMLButton Create(string p, EventHandler h)
		{
			var b = new IHTMLButton(p);

			b.onclick += (e) => Helper.Invoke(h);
			b.AttachToDocument();

			return b;
		}
	}
}
