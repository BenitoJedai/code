using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace jsc.meta.Tools
{
	static partial class ToolsExtensions
	{
		/// <summary>
		/// The target library is prepared to be compiled into
		/// javascript by jsc.
		/// </summary>
		/// <param name="TargetAssembly"></param>
		public static void ToJavaScript(this FileInfo TargetAssembly)
		{
			// we should run jsc in another appdomain actually
			// just to be sure our nice tool gets unloaded :)

			jsc.Program.TypedMain(
				new jsc.CompileSessionInfo
				{
					Options = new jsc.CommandLineOptions
					{
						TargetAssembly = TargetAssembly,
						IsJavaScript = true,
						IsNoLogo = true
					}
				}
			);
		}

	}
}
