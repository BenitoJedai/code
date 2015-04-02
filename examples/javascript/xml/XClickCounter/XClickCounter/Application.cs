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
using System.Threading.Tasks;
using System.Xml.Linq;
using XClickCounter;
using XClickCounter.Design;
using XClickCounter.HTML.Pages;
using ScriptCoreLib.Query.Experimental;
using XClickCounter.Data;
using System.Threading;
using System.Data.SQLite;

namespace XClickCounter
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		static Application()
		{
			Console.WriteLine("Application.cctor");

			#region QueryExpressionBuilder.WithConnection
			QueryExpressionBuilder.WithConnection =
				y =>
				{
					var cc = new SQLiteConnection();
					cc.Open();
					y(cc);
					cc.Dispose();
				};
			#endregion
		}

		//02000051 ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+<ReadToElements>d__24`1
		//script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0008]
		//		bne.un.s   +0 -2{[0x0001]
		//		ldfld      +1 -1{[0x0000]
		//		ldarg.0    +1 -0}
		//} {[0x0006]
		//ldc.i4.s   +1 -0} , Location =
		// assembly: V:\XClickCounter.Application.exe
		// type: ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+<ReadToElements>d__24`1, XClickCounter.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
		// offset: 0x0008
		//  method:System.Collections.Generic.IEnumerator`1[TElement]
		//System.Collections.Generic.IEnumerable<TElement>.GetEnumerator() }
		//*** Compiler cannot continue... press enter to quit.


		public Application(IApp page)
		{
			// X:\jsc.svn\examples\javascript\LINQ\VBWebSQLScalarXElement\VBWebSQLScalarXElement\Application.vb

			// can we do this?

			//new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox }.AttachToDocument()

			//    // hide the next pre
			//    .css

			//    .@unchecked

			//    .adjacentSibling[IHTMLElement.HTMLElementEnum.pre].style.display = IStyle.DisplayEnum.none;

			// make sure it exists?
			new xxAvatar().Create();


			// how much load will we cause?
			//new IHTMLPre { "count: ", () => new xxAvatar().CountAsync() }.AttachToDocument();
			////new IHTMLPre { "descending: ", () => new xAvatar().CountAsync() }.AttachToDocument();
			//new IHTMLPre { "descending: ",
			//    () => (
			//        from x in new xxAvatar()
			//        orderby x.Key descending
			//        select new xxAvatarRow
			//        {
			//            // message: "type$zrk5B64_bQTe7tpWTj7myyA is not defined"
			//            // we cannot select Key as we are missing the enum type referenced by FromHandle?
			//            //x.Key,

			//            Tag = x.Tag
			//        }
			//    ).FirstOrDefaultAsync()
			//}.AttachToDocument();

			//new IHTMLPre { "descending xml: ",
			//    () => (
			//        from x in new xxAvatar()
			//        orderby x.Key descending
			//        select new xxAvatarRow
			//        {
			//            // message: "type$zrk5B64_bQTe7tpWTj7myyA is not defined"
			//            // we cannot select Key as we are missing the enum type referenced by FromHandle?
			//            //x.Key,

			//            //Tag = x.Tag
			//            z = x.z
			//        }
			//    ).FirstOrDefaultAsync()
			//}.AttachToDocument();

			new IHTMLButton { "start monitoring" }.AttachToDocument().onclick +=
			delegate
			{
				new IHTMLPre { "descending xml: ",
					() => (
						from x in new xxAvatar()
						orderby x.Key descending
						select x.z
					).FirstOrDefaultAsync()
				}.AttachToDocument();
			};


			new IHTMLButton { "InsertAsync" }.AttachToDocument().onclick +=
			 async e =>
			{
				e.Element.disabled = true;


				var q = new
				{

					Thread.CurrentThread.ManagedThreadId,

					Count = await new xxAvatar().CountAsync(),



				};

				await new xxAvatar().InsertAsync(
					new xxAvatarRow
					{
						Tag = "hi! " + q,

						// could we do it in a worker?
						z = new XElement("div",
						new XAttribute("style", "color: blue;"),
						"hello world! " + q
					)

					}
			   );


				// descending xml: <div><style>color: blue;</style>hello world! {{ ManagedThreadId = 1, Count = 0 }}</div>
				new IHTMLPre { "descending xml: ",
					(
						from x in new xxAvatar()
						orderby x.Key descending
						select x.z
					).FirstOrDefaultAsync()
				}.AttachToDocument();


				e.Element.disabled = false;
			};

			//new IHTMLButton { "InsertAsync worker" }.AttachToDocument().onclick +=
			//     async e =>
			//{
			//    e.Element.disabled = true;

			//    await Task.Run(
			//        async delegate
			//    {
			//        // lets have our background thread add to db!


			//        //var cc2 = new SQLiteConnection();
			//        //cc2.Open();


			//        // time for our DataBound DataRepeater class?
			//        await new xxAvatar().InsertAsync(
			//            new xxAvatarRow
			//        {
			//            Tag = "hi! " + new
			//            {
			//                Thread.CurrentThread.ManagedThreadId,

			//                Count = await new xxAvatar().CountAsync()
			//            }
			//        });


			//        // wait some while still inside worker
			//        //await Task.Delay(500);
			//    }
			//    );

			//    e.Element.disabled = false;
			//};
		}
	}

}
