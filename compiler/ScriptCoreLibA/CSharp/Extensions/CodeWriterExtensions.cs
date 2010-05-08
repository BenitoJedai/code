using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Library;

namespace ScriptCoreLib.CSharp.Extensions
{
	[Obsolete("see ScriptCoreLib.Ultra.Studio.Languages")]
	public static class CodeWriterExtensions
	{
		public static void Using(this CodeWriter w, string u)
		{
			w.Statement("using " + u + ";");
		}

		public static void Namespace(this CodeWriter w, string ns, Action body)
		{
			w.Block("namespace " + ns, body);
				
		}
	}
}
