using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.StringBuilder))]
	internal class __StringBuilder
	{
		java.lang.StringBuffer InternalBuffer;

		public __StringBuilder()
		{
			InternalBuffer = new java.lang.StringBuffer();
		}

		public __StringBuilder Append(string e)
		{
			InternalBuffer.append(e);

			return this;
		}

		public override string ToString()
		{
			return InternalBuffer.ToString();
		}
	}
}
