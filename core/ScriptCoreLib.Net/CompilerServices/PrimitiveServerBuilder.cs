using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace ScriptCoreLib.CompilerServices
{
	public class PrimitiveServerBuilder
	{
		// classes in this namespace are to be used to build
		// alternative versions additionally to user written assemblies

		// types built within this assembly can reference this assembly for previously 
		// written functionality

		// also not that this assembly depends on ScriptCoreLib but not on ScriptCoreLibJava. 
		// if we were to build a server with java in mind we should add a reference to ScriptCoreLibJava
		// to do that we should be referencing ScriptCoreLibJava at some point... but not within this assembly.

		// this assembly shall support flash network providers like nonoba and kongregate

		// the functionality from jsc.server could also be included within this assembly
		
		// some functionality shall be refactored to ScriptCoreLib.Net.Server assembly
		// to enable java support via [Optimization("script")]
		public static string GetVersionInformation()
		{
			return "powered by ScriptCoreLib.Net";
		}

		public static void StartRouter(Type t)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("will start router: " + t.FullName);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

	
		public static void EmitVersionInformation(ILGenerator il)
		{
			// kind of funny as this assembly will have parts where the IL will be compiled to target languages like actionscript
			// and also this assemlby contains code to build some of the IL which might end up being retranslated to that language

			Func<string> _GetVersionInformation = GetVersionInformation;

			var _a = il.DeclareLocal(typeof(string));

			il.EmitCall(OpCodes.Call, _GetVersionInformation.Method, null);
			il.Emit(OpCodes.Stloc, _a);
			il.EmitWriteLine(_a);
		}
	}
}
