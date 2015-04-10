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
using TestLINQSelectByteArray;
using TestLINQSelectByteArray.Design;
using TestLINQSelectByteArray.HTML.Pages;

namespace TestLINQSelectByteArray
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
			var bytes1 = new byte[] { 0, 1, 2, 3 };

			//  required: T#1[]
			//  found: byte[]
			//  reason: actual argument byte[] cannot be converted to Byte[] by method invocation conversion
			//  where T#1,T#2 are type-variables:
			//    T#1 extends Object declared in method <T#1>Of(T#1[])
			//    T#2 extends Object declared in class __SZArrayEnumerator_1
			//java\JVMCLRLINQSelectByteArray\Program.java:164: error: possible loss of precision
			//            x[i] = ((Short)e[i]).shortValue();
			//                                           ^

			var bytes2 = Enumerable.ToArray(
				 from x in bytes1
				 let y = (byte)(x ^ 0xff)
				 select y
			 );

			new IHTMLPre { new { bytes2 } }.AttachToDocument();
			
			// {{ bytes2 = 255,254,253,252 }}


		}

	}
}
