using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
	[Script(HasNoPrototype = true)]
	public class IMouseDownEvent<TTargetElement> : IEvent<TTargetElement> where TTargetElement : IHTMLElement
	{
		// http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/MouseEventHitRegion.idl

		// tested by ?

		[Script(DefineAsStatic = true)]
		public void CaptureMouse()
		{
			var Element = ((IHTMLElement)this.Element);

			Action StopCapture = null;
			Action<IEvent> __mouseup = null;

			__mouseup = delegate
			{
				StopCapture();

				Element.onmouseup -= __mouseup;
			};

			Element.onmouseup += __mouseup;

			// no reason to keep default behaviour to select text
			this.PreventDefault();

			StopCapture = Element.CaptureMouse();

		}
	}




	// X:\opensource\github\WootzJs\WootzJs.Web\MouseEvent.cs
	// http://src.chromium.org/viewvc/blink/trunk/Source/core/events/MouseEvent.idl
	// http://www.w3.org/TR/DOM-Level-2-Events/events.html#Events-MouseEvent
	[Script(InternalConstructor = true)]
	public class IMouseEvent : IEvent
	{
		public IMouseEvent() { }

		internal static IMouseEvent InternalConstructor()
		{
			return (IMouseEvent)new IFunction("return document.createEvent('MouseEvent');").apply(null);
		}



		public void initMouseEvent(
			string typeArg,
			bool canBubbleArg,
			bool cancelableArg,
			object viewArg,
			long detailArg,
			long screenXArg,
			long screenYArg,
			long clientXArg,
			long clientYArg,
			bool ctrlKeyArg,
			bool altKeyArg,
			bool shiftKeyArg,
			bool metaKeyArg,
			int buttonArg,
			object relatedTargetArg)
		{
		}


	}
}
