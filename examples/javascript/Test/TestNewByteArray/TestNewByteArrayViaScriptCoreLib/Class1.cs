using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestNewByteArrayViaScriptCoreLib
{
	[Script]
	public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
	{
		// X:\jsc.svn\examples\javascript\Test\TestNewByteArray\TestNewByteArrayViaScriptCoreLib\Class1.cs

		public Class1()
		{
			//  b = new Uint8ClampedArray(16);
			var bytes = new byte[0x10];

			//  c = [0, 1, 2, 3];
			var bytes1 = new byte[] { 0, 1, 2, 3 };


			//c = /* StructAsByteArray */ [0, 1, 2, 3];

			//var x = new ScriptCoreLib.JavaScript.WebGL.Uint8ClampedArray(bytes);

		}
	}
}
