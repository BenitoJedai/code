using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class NonobaExtensions
	{
		public static MemoryStream GetMemoryStream(this Message m, uint index)
		{
			return m.GetByteArray(index).ToMemoryStream();
		}
	}
}
