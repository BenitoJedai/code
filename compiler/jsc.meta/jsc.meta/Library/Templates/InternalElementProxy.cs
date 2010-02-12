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

		public class __ExportDelegateContextType
		{
			int Namespace;
			int Counter;

			public __ExportDelegateContextType()
			{
				Namespace = new Random().Next();
			}

			public string GenerateName(string method)
			{
				Counter++;

				return "__InternalElementProxy" + Namespace + "_" + Counter + "_" + method ;
			}
		}


		internal __ExportDelegateContextType __ExportDelegateContext;

		internal static string __ExportDelegate(__InternalElementProxy that, Delegate value, string method)
		{
			if (that.__ExportDelegateContext == null)
				that.__ExportDelegateContext = new __ExportDelegateContextType();

			var __callback = that.__ExportDelegateContext.GenerateName(method);

			IFunction.OfDelegate(value).Export(__callback);

			return __callback;

		}


		internal bool __IsElementLoaded;
		internal readonly List<Action> __Delayed = new List<Action>();

		internal static void __SetElementLoaded(__InternalElementProxy that)
		{
			//Native.Window.alert("__SetElementLoaded");
			that.__IsElementLoaded = true;
			__DelayedInvoke(that);
			that.__Delayed.Clear();
		}

		internal static void __DelayedInvoke(__InternalElementProxy that)
		{
			//Native.Window.alert("__DelayedInvoke");

			// after jsc.meta rewrites this method, jsc cannot handle OpCodes.Leave if it does not return...
			// todo: we should create a new test project and fix it!
			foreach (var item in that.__Delayed)
			{
				//Native.Window.alert("__DelayedInvoke ##");

				item();
			}
		}

		internal static void __AfterElementLoaded(__InternalElementProxy that, Action e)
		{
			if (that.__IsElementLoaded)
			{
				e();
				return;
			}

			that.__Delayed.Add(e);
		}

		public static void OrphanizeLater(IHTMLElement e)
		{
			Native.Window.onbeforeunload +=
				delegate
				{
					e.Orphanize();
				};
		}

		internal static void Retry(Action e)
		{
			// this is a javascript utility method
			// move to ScriptCoreLib/Ultra?

			new ScriptCoreLib.JavaScript.Runtime.Timer(
				tt =>
				{
					try
					{
						e();
						tt.Stop();
					}
					catch
					{

					}
				},
				1, 100
			);
		}
	}
}
