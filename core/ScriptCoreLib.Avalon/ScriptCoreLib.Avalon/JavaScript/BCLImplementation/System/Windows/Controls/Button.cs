using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	// http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/Button.cs

	[Script(Implements = typeof(global::System.Windows.Controls.Button))]
	internal class __Button : __ButtonBase
	{
		// X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\Program.cs
		// X:\jsc.svn\examples\actionscript\test\TestShadowDOMForFlash\TestShadowDOMForFlash\Application.cs

		// http://caniuse.com/shadowdom
		// wont work for ipad, safari, ie?
		// no reason to try it forms then yet?
		// for chrome apps it would be fine to use..


		// what about chrome custom element api?
		// see X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\BCLImplementation\System\Windows\Controls\TextBox.cs
		public const string ElementName = "avalon-button";




		// tested by
		// X:\jsc.svn\examples\javascript\Avalon\Test\TestShadowTextBox\TestShadowTextBox\ApplicationCanvas.cs

		static __Button()
		{
			// test against worker mode
			if (Native.document == null)
				return;

			Native.document.registerElement(
				name: ElementName,

				// can we sync with the ctor?
				// if we were called from ctor yield to it. otherwise recreate?

				createdCallback:
					(IHTMLElement e) =>
					{
						// um. this would be the new way do do ctor.
						// like we do for Application(html) already?


					}
			);
		}

		// pass this to the element ctor?
		public IHTMLElement InternalDisplayObject;

		// code creates class, we create element.
		public __Button()
		{
			InternalDisplayObject = new IHTMLElement(ElementName);

			var s = InternalDisplayObject.createShadowRoot();

			var button = new IHTMLButton();

			button.AttachTo(s);

			button.onclick +=
				delegate
				{
					InternalRaiseClick();
				};

			this.InternalVirtualSetContent =
				value =>
				{
					// what aboout? shadow dom ContentElement?
					// X:\jsc.svn\examples\javascript\Avalon\Test\TestShadowTextBox\TestShadowTextBox\ApplicationCanvas.cs

					button.innerText = "" + value;
				};
		}


		public override DOM.HTML.IHTMLElement InternalGetDisplayObject()
		{
			return InternalDisplayObject;
		}
	}
}
