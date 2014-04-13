using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Alchemy.Headers
{
	using AS3_Val = AS3_h._AS3_Val;
	using AS3_ThunkProc = AS3_h._AS3_ThunkProc;

	[Script(IsNative = true, Header = "AS3.h", IsSystemHeader = false)]
	public static class AS3_h
	{


		/* AS3 value creation */
		public static AS3_Val AS3_String(string str)
		{
			return default(AS3_Val);
		}

		public static void AS3_Release(AS3_Val obj)
		{
		}

		public static AS3_Val AS3_Ptr(object p)
		{
			return default(AS3_Val);
		}

		public static AS3_Val AS3_Object(string tt, __arglist)
		{
			return default(AS3_Val);
		}

		public static void AS3_ArrayValue(AS3_Val array, string tt, __arglist)
		{

		}

		public static AS3_Val AS3_Function(object data, AS3_ThunkProc proc)
		{
			return default(AS3_Val);
		}

		/// <summary>
		/// notify that the library has initialized (does not return) 
		/// </summary>
		/// <param name="libData"></param>
		public static void AS3_LibInit(AS3_Val libData)
		{
		}


		[Script(PointerName = "AS3_Val", HasNoPrototype = true)]
		public class _AS3_Val
		{
			/* ref counted AS3 value type */
			// typedef struct _AS3_Val *AS3_Val;

		}

		[Script(PointerName = "AS3_ThunkProc", HasNoPrototype = true)]
		public delegate AS3_Val _AS3_ThunkProc(object self, AS3_Val args);

	}



}
