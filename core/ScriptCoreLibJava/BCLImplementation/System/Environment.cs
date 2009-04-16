using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{

	[Script(Implements = typeof(global::System.Environment))]
	internal class __Environment
	{
		public static string CurrentDirectory
		{
			get
			{
				// http://www.devx.com/tips/Tip/13804

				var f = new java.io.File(".");
				var c = default(string);

				try
				{
					c = f.getCanonicalPath();
				}
				catch
				{
					throw new csharp.RuntimeException();
				}

				return c;
			}
		}
	}
}
