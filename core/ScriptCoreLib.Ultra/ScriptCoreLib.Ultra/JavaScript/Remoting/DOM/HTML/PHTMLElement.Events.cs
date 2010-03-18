using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	[InferAdditionalMembers]
	partial interface PHTMLElement
	{
		// this remoting namespace should be generated from IDL...

		// we need a Type Enhancer. What we define here manually could can be used
		// by our front end compiler.
		// what we do not define here should be enhanced by the compiler

		// can we let jsc enhance this class?

		event PEventAction onclick;

		#region via InferAdditionalMembers
		event PEventAction onmouseover;
		event PEventAction onmouseout;
		#endregion

	}


	partial class PIHTMLElement
	{
		public event PEventAction onclick
		{
			add { this.InternalElement.onclick += ToEventHandler(value); }
			remove { }
		}

		#region via InferAdditionalMembers
		public event PEventAction onmouseout
		{
			add { this.InternalElement.onmouseout += ToEventHandler(value); }
			remove { }
		}

		public event PEventAction onmouseover
		{
			add { this.InternalElement.onmouseover += ToEventHandler(value); }
			remove { }
		}
		#endregion

		private static ScriptCoreLib.Shared.EventHandler<ScriptCoreLib.JavaScript.DOM.IEvent> ToEventHandler(PEventAction value)
		{
			return e =>
			{
				value(
					new PIEvent
					{
						InternalEvent = e
					}
				);
			};
		}


	}



}
