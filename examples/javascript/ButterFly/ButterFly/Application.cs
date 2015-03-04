using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ButterFly.HTML.Pages;

namespace ButterFly
{
	/// <summary>
	/// This type will run as JavaScript.
	/// </summary>
	internal sealed class Application : ApplicationWebService
	{

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefault page)
		{
			new ButterFly.Library.Butterfly(page.PageContainer);


		}

		//02000011 <>f__AnonymousType$83$5`2
		//{ SourceMethod = <FieldName>j__TPar get_FieldName() }
		//{ SourceMethod = <FieldValue>j__TPar get_FieldValue() }
		//{ SourceMethod = Boolean Equals(System.Object) }
		//{ SourceMethod = Int32 GetHashCode() }
		//{ SourceMethod = System.String ToString() }
		//script: error JSC1000: Method: ToString, Type: <>f__AnonymousType$83$5`2; emmiting failed : System.Exception: recursion detected at stack 32
		//   at jsc.RecursionGuard..ctor(RecursionGuard parent) in X:\jsc.internal.git\compiler\jsc\RecursionGuard.cs:line 33
		//   at jsc.RecursionGuard.get_Lock() in X:\jsc.internal.git\compiler\jsc\RecursionGuard.cs:line 49
		//   at jsc.ILBlock.Prestatement.ValidateInlineAssigment(Prestatement p) in X:\jsc.internal.git\compiler\jsc\CodeModel\ILBlock.cs:line 865
		//   at jsc.ILBlock.PrestatementBlock.Populate() in X:\jsc.internal.git\compiler\jsc\CodeModel\ILBlock.cs:line 1632
	}
}
