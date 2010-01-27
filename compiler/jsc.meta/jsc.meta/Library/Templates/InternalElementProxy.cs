using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace jsc.meta.Library.Templates
{
	public abstract class __InternalElementProxy
	{
		public IHTMLElement __InternalElement;

		public static implicit operator IHTMLElement(__InternalElementProxy e)
		{
			return e.__InternalElement;
		}
	}
}
