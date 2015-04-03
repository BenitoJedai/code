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
using TestRedirectWebWorker;
using TestRedirectWebWorker.Design;
using TestRedirectWebWorker.HTML.Pages;

namespace TestRedirectWebWorker
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		static Application()
		{
			// 1ms {{ src = https://192.168.1.196:24813/view-source }}
			// 0ms {{ currentScript = [object HTMLScriptElement] }}
			Console.WriteLine(
				new { Native.document.currentScript.src }

				);

		}

		public Application(IApp page)
		{
			// GetScriptApplicationSourceForInlineWorker { value = view-source#worker }

			//Native.document.curr
			new IHTMLPre { new { Native.document.currentScript } }.AttachToDocument();
			//{ { currentScript = null } }

			// we could jump to encrypted secondary app here...
			new IHTMLButton { "prefetch source (edit and continue can update code?)" }.AttachToDocument().WhenClicked(
					//async 
					button =>
					{
						//Request URL:http://192.168.43.252:17858/view-source
						//Request Method:0

						new IXMLHttpRequest(
							ScriptCoreLib.Shared.HTTPMethodEnum.GET,
							"view-source",
							r =>
							{
								//new IHTMLPre { new { r.responseType } }.AttachToDocument();
								new IHTMLPre { new { r.responseText.Length } }.AttachToDocument();

								var aFileParts = new[] { r.responseText };
								var oMyBlob = new Blob(aFileParts, new { type = "text/html" });	// the blob


								var url = oMyBlob.ToObjectURL();

								InternalInlineWorker.ScriptApplicationSourceForInlineWorker = url;

								new IHTMLPre { new { InternalInlineWorker.ScriptApplicationSourceForInlineWorker } }.AttachToDocument();
							}

						);



					}
			);

			new IHTMLButton { "work" }.AttachToDocument().WhenClicked(
				async button =>
				{
					var state = new
					{
						input_for_other_thread = new { Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground }
					};

					// X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs

					//var x = await Task<string>.Factory.StartNew(
					var x = await Task.Factory.StartNew(
						state,
						scope =>
						{
							// Console is special. { scope = [object Object] }

							Console.WriteLine(
								//"Console is special. " + new { scope }
								"Console is special. " + new { scope.input_for_other_thread.ManagedThreadId }
							);



							return new
							{
								output = "who is serializing this? only the browser API? jsc not helping here yet?",

								scope,

								inside = new
								{
									Thread.CurrentThread.ManagedThreadId,
									Thread.CurrentThread.IsBackground
								}
							};
							//return "who is serializing this? only the browser API? jsc not helping here yet?";
						}
					//, state
					);


					new IHTMLPre { new { x.output, x.scope, x.inside } }.AttachToDocument();

				}
			);

		}

	}
}
