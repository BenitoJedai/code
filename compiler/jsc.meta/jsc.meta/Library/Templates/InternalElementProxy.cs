using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;

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


		bool __IsElementLoaded;
		readonly List<Action> __Delayed = new List<Action>();

		internal void __SetElementLoaded()
		{
			//Native.Window.alert("__SetElementLoaded");
			__IsElementLoaded = true;
			__DelayedInvoke();
			__Delayed.Clear();
		}

		private void __DelayedInvoke()
		{
			//Native.Window.alert("__DelayedInvoke");

			// after jsc.meta rewrites this method, jsc cannot handle OpCodes.Leave if it does not return...
			// todo: we should create a new test project and fix it!
			foreach (var item in __Delayed)
			{
				//Native.Window.alert("__DelayedInvoke ##");

				item();
			}
		}

		internal void __AfterElementLoaded(Action e)
		{
			if (__IsElementLoaded)
			{
				e();
				return;
			}

			__Delayed.Add(e);
		}

		public static void OrphanizeLater(IHTMLElement e)
		{
			Native.Window.onbeforeunload +=
				delegate
				{
					e.Orphanize();
				};
		}
	}
}
