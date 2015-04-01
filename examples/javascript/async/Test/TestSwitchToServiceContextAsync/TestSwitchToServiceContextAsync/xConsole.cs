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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSwitchToServiceContextAsync;
using TestSwitchToServiceContextAsync.Design;
using TestSwitchToServiceContextAsync.HTML.Pages;

namespace TestSwitchToServiceContextAsync
{

	#region xConsole
	//class xConsole : StringWriter
	[Obsolete("jsc:js does not allow to overrider an override?")]
	public class xConsole : TextWriter
	{
		// http://www.danielmiessler.com/study/encoding_encryption_hashing/
		[Obsolete("can we have encrypted encoding?")]
		public override Encoding Encoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void Write(string value)
		{
			var p = new IHTMLCode { innerText = value }.AttachToDocument();
			var s = p.style;

			// jsc, enum tostring?
			if (Console.ForegroundColor == ConsoleColor.Red)
				s.color = "red";

			if (Console.ForegroundColor == ConsoleColor.Blue)
				s.color = "blue";

			if (Console.ForegroundColor == ConsoleColor.Gray)
				s.color = "gray";
		}

		public override void WriteLine(object value)
		{
			Write("" + value);

			new IHTMLBreak { }.AttachToDocument();
		}

		public override void Write(char value)
		{
			if (value == (char)127)
			{
				Native.body.Clear();
				return;
			}

			Write(new string(value, 1));
		}

		public override void WriteLine(string value)
		{
			//Console.WriteLine(new { value });


			Write(value);

			new IHTMLBreak { }.AttachToDocument();
		}
	}
	#endregion


}
