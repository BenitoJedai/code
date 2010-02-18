using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLScript : IHTMLElement
	{


		#region Constructor

		public IHTMLScript()
		{
			// InternalConstructor
		}


		static IHTMLObject InternalConstructor()
		{
			return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.script);
		}

		#endregion

		public string src;
		public string type;

		#region event onload
		public event Action onload
		{
			// http://www.rgagnon.com/javadetails/java-0176.html
			// http://bytes.com/topic/javascript/answers/147231-applet-onload-alert-hi
			// http://www.irt.org/script/4013.htm
			// http://www.blooberry.com/indexdot/html/tagpages/attributes/onload.htm

			[Script(DefineAsStatic = true)]
			add
			{
				//Native.Window.alert("add_onload!");

				__onload.CombineDelegate(this, value);
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				throw new NotSupportedException();
			}
		}
		#endregion

		public string readyState;

		[Script]
		internal static class __onload
		{
			// will HTML5 enable a more nicer solution?

			internal static void CombineDelegate(IHTMLScript a, Action value)
			{
				var whenloaded = true;


				a.InternalEvent(true,
					(Action)delegate
					{
						var f = a.readyState;

						var done = false;


						if (f == null)
							done = whenloaded;

						if (f == "loaded")
							done = whenloaded;


						if (f == "complete")
							done = whenloaded;

						if (done)
						{
							whenloaded = false;
							value();
						}
					},
					"load",
					"onreadystatechange"
				);



			}
		}
	}
}
