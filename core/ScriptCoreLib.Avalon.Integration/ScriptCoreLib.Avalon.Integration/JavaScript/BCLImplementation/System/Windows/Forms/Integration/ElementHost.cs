using System.Windows;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.Integration
{
	[Script(Implements = typeof(global::System.Windows.Forms.Integration.ElementHost))]
	internal class __ElementHost : __Control
	{
		public IHTMLDiv InternalElement { get; set; }

		public __ElementHost()
        {
			this.InternalElement = new IHTMLDiv { name = "__ElementHost" };
			this.InternalElement.style.padding = "0";
			this.InternalElement.style.SetSize(32, 32);
			//this.InternalElement.style.backgroundColor = "red";

			//this.InternalSetDefaultFont();
        }

		public override IHTMLElement HTMLTargetRef
		{
			get
			{
				return InternalElement;
			}
		}

		UIElement InternalChild;
		public UIElement Child
		{
			get
			{
				return InternalChild;
			}

			set
			{
				// clear current!

				InternalChild = value;

				var e = ((__UIElement)value).InternalGetDisplayObjectDirect();

				this.InternalElement.appendChild(e);
			}
		}
	}
}
