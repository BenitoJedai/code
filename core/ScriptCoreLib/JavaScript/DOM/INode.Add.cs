using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.DOM
{
	public partial class INode
	{
		// will roslyn allow Add extensions?




		[Script(DefineAsStatic = true)]
		public void Add(INode e)
		{
			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			this.appendChild(e);
		}

		[Script(DefineAsStatic = true)]
		public void Add(INode[] e)
		{
			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			foreach (var item in e)
			{
				this.appendChild(item);
			}
		}

		[Script(DefineAsStatic = true)]
		public virtual void Add(string e)
		{
			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			this.appendChild(new ITextNode(e));
		}


		[Script(DefineAsStatic = true)]
		public void Add(byte[] e)
		{
			// x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs

			var w = "";

			foreach (var item in e)
			{
				w += item.ToString("x2");

			}

			var x = new ITextNode(w);

			this.appendChild(x);
		}

		[Script(DefineAsStatic = true)]
		public void Add(object e)
		{
			// would this work for the compiled assets/html blocks too?

			var xXElement = e as XElement;
			if (xXElement != null)
			{
				// X:\jsc.svn\examples\javascript\test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
				if (this.nodeName.ToLower() == "iframe")
				{
					var aFileParts = new[] { xXElement.ToString() };
					var oMyBlob = new Blob(aFileParts, new { type = "text/html" });

					var url = URL.createObjectURL(oMyBlob);

					((IHTMLIFrame)this).src = url;

					return;
				}

				// X:\jsc.svn\examples\javascript\Test\vb\TestXElementLiteral\TestXElementLiteral\Application.vb
				this.appendChild(
					xXElement.AsHTMLElement()
					);

				return;
			}

			// X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			// x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs
			var x = new ITextNode("" + e);

			// what if the object is anonymous
			// could we have special logic for it?

			// actually all we want to know is will ToString change. IToStringChangedEvent ?
			if (e is Stopwatch)
			{
				Native.window.onframe +=
					ee =>
					{
						x.nodeValue = "" + e;


						// stop when stopwatch is paused?
					};
			}

			this.appendChild(x);
		}

		[Script(DefineAsStatic = true)]
		public void Add(System.Func<object> e)
		{
			// what about implicit operators for other elements?
			// X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			var x = e().ToString();
			var text = new ITextNode(x);

			this.appendChild(text);

			new Timer(
				t =>
				{
					if (text.parentNode == null)
					{
						System.Console.WriteLine("INode.Add timer stopped");
						t.Stop();
						return;
					}

					var y = e().ToString();
					if (y != text.nodeValue)
					{
						text.nodeValue = y;

						return;
					}

					// how many iterations before we stop the timer?
				},

				// time to attach to DOM
				duetime: 33,
				interval: 1000 / 15
			);


		}



		[Script(DefineAsStatic = true)]
		public void Add(System.Func<Task<XElement>> e)
		{
			// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

			// what about implicit operators for other elements?
			// X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			//var text = new ITextNode("");
			INode text = new IHTMLSpan("");

			this.appendChild(text);

			var TotalElapsedMilliseconds = 0L;

			new Timer(
				t =>
				{
					if (text.parentNode == null)
					{
						System.Console.WriteLine("INode.Add timer stopped");
						t.Stop();
						return;
					}

					t.Enabled = false;

					var sw = Stopwatch.StartNew();

					e().ContinueWith(
						x =>
						{
							var Result = x.Result.AsHTMLElement();

							// what if there is no data?
							if (Result != null)
							{
								// DOM rewrite in process. is the caller trusted? is data trusted?
								this.replaceChild(
									Result,
									text
								);

								text = Result;

							}

							t.Enabled = true;
						}
					);


					// how many iterations before we stop the timer?
				},

				// time to attach to DOM
				duetime: 33,
				interval: 1000 / 15
			);


		}



		[Script(DefineAsStatic = true)]
		public void Add(Task<XElement> e)
		{
			// x:\jsc.svn\examples\javascript\xml\xclickcounter\xclickcounter\application.cs
			// placeholder.
			var text = new IHTMLSpan("");

			this.appendChild(text);


			e.ContinueWith(
				x =>
				{
					var Result = x.Result.AsHTMLElement();



					// DOM rewrite in process. is the caller trusted? is data trusted?
					this.replaceChild(
						Result,
						text
					);
				}
			);
		}


		[Script(DefineAsStatic = true)]
		public void Add<TResult>(Task<TResult> e)
		{
			// x:\jsc.svn\examples\javascript\xml\xclickcounter\xclickcounter\application.cs

			// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

			// what about implicit operators for other elements?
			// X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			//var text = new ITextNode("");
			var text = new IHTMLSpan("");

			this.appendChild(text);



			var sw = Stopwatch.StartNew();

			e.ContinueWith(
				x =>
				{

					var xx = (__Task<object>)x;

					var Result = xx.Result;

					// if its xml would we want to do something special?

					var y = System.Convert.ToString(
						xx.Result
					);

					//TotalElapsedMilliseconds += sw.ElapsedMilliseconds;
					//text.title = new { TotalElapsedMilliseconds, sw.ElapsedMilliseconds }.ToString();


					if (y != text.innerText)
					{
						text.innerText = y;
					}

				}
			);





		}

		[Script(DefineAsStatic = true)]
		public void Add<TResult>(System.Func<Task<TResult>> e)
		{
			// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

			// what about implicit operators for other elements?
			// X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

			// Implementing Collection Initializers
			// http://msdn.microsoft.com/en-us/library/bb384062.aspx

			//var text = new ITextNode("");
			var text = new IHTMLSpan("");

			this.appendChild(text);

			var TotalElapsedMilliseconds = 0L;

			new Timer(
				t =>
				{
					if (text.parentNode == null)
					{
						System.Console.WriteLine("INode.Add timer stopped");
						t.Stop();
						return;
					}

					t.Enabled = false;

					var sw = Stopwatch.StartNew();

					e().ContinueWith(
						x =>
						{

							var xx = (__Task<object>)x;

							var Result = xx.Result;


							var y = System.Convert.ToString(
								xx.Result
							);

							TotalElapsedMilliseconds += sw.ElapsedMilliseconds;
							text.title = new { TotalElapsedMilliseconds, sw.ElapsedMilliseconds }.ToString();


							if (y != text.innerText)
							{
								text.innerText = y;
							}

							t.Enabled = true;
						}
					);


					// how many iterations before we stop the timer?
				},

				// time to attach to DOM
				duetime: 33,
				interval: 1000 / 15
			);


		}
	}
}