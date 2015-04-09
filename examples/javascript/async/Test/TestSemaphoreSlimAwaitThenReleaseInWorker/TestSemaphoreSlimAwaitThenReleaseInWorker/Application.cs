using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSemaphoreSlimAwaitThenReleaseInWorker;
using TestSemaphoreSlimAwaitThenReleaseInWorker.Design;
using TestSemaphoreSlimAwaitThenReleaseInWorker.HTML.Pages;

namespace TestSemaphoreSlimAwaitThenReleaseInWorker
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			new IHTMLButton { "click to test " }.AttachToDocument().onclick +=
				 delegate
				{
					var bytes2sema = new SemaphoreSlim(1);

					bytes2sema.WaitAsync().ContinueWith(delegate
					{
						new IHTMLPre { "worker1 has signaled..." }.AttachToDocument();

					});

					Task.Run(
						 delegate
						{
							bytes2sema.Release();

						}
					);
				};

		}

	}
}

//5841ms SemaphoreSlim.WaitAsync {{ InternalIsEntangled = false } }
//2015-04-09 17:17:18.701 :2895/view-source:51282 6050ms(10) Task scope {{ MemberName = bytes2sema, IsString = false, IsNumber = false, IsDelegate = false, IsProgress = false, TypeIndex = type$vCpL8AJAwTmO6BklLAu35w } }
//2015-04-09 17:17:19.195 :2895/view-source:51282 6544ms[10] __worker_onfirstmessage: {{ ManagedThreadId = 10, href = https://172.17.6.38:2895/view-source#worker, MethodTargetTypeIndex = type$EBlSwjixTjK_bnsLs36D_agQ, MethodTargetType = __c__DisplayClass0_0, MethodToken = igAABjixTjK_bnsLs36D_agQ, MethodType = FuncOfObjectToObject, stateTypeHandleIndex = null, stateType = null, state = [object Object], IsIProgress = false }}
//2015-04-09 17:17:19.200 :2895/view-source:51282 [10] {{ xMember = bytes2sema, xMethodTargetObjectDataTypeIndex = type$vCpL8AJAwTmO6BklLAu35w, xObjectData = null, xIsProgress = null }}6549ms[10] 
//2015-04-09 17:17:19.202 :2895/view-source:51282 6551ms(10) xSemaphoreSlim MessageEvent {{ MemberName0 = bytes2sema } }
//2015-04-09 17:17:19.210 :2895/view-source:51282 6559ms[10] SemaphoreSlim.Release {{ InternalIsEntangled = true } }
//2015-04-09 17:17:19.211 :2895/view-source:51282 6560ms[10] worker xSemaphoreSlim.InternalVirtualRelease, postMessage {{ Name = bytes2sema }}
//2015-04-09 17:17:19.212 :2895/view-source:51282 6561ms(10) xSemaphoreSlim MessageEvent {{ MemberName0 = bytes2sema } }