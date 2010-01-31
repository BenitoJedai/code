using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace jsc.meta.Library.Templates
{
	public abstract class __InternalElementProxy
	{
		public IHTMLElement __InternalElement;

		public static implicit operator IHTMLElement(__InternalElementProxy e)
		{
			return e.__InternalElement;
		}

		public delegate string __FuncString();

		static int Counter = 1;

		public string CombineDelegates(string name, Delegate value)
		{
			Counter++;

			var __callback = "__InternalElementProxy_" + name + "_" + Counter;

			IFunction.OfDelegate(value).Export(__callback);

			return __callback;

		}
	}
}
