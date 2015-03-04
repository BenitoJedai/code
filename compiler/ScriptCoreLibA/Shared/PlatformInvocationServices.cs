using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared
{
	/// <summary>
	/// Methods inside this type shall be implemented for each platform separatly
	/// </summary>
	public class PlatformInvocationServices
	{
		// https://github.com/dotnet/coreclr/blob/master/src/vm/dllimport.h
		// https://github.com/dotnet/coreclr/blob/master/src/vm/dllimport.cpp

		// tested by?
		// jni for android NDK?
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140902
		// X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\Program.cs
		// X:\jsc.svn\core\ScriptCoreLibJava.jni\BCLImplementation\ScriptCoreLib\Shared\PlatformInvocationServices.cs


		public static object StringOrNullCPtr(string e)
        {
            return default(object);
        }

        public static IDisposable CreateCMallocCollector()
        {
            return default(IDisposable);
        }

        public static IntPtr OfInt32(IDisposable c, int value)
        {
            return default(IntPtr);
        }

        public static object IntPtrToPointerToken(IntPtr ptr)
        {
            return default(object);
        }

        public static bool InvokeBoolean(string DllName, string EntryPoint, params object[] e)
        {
            return default(bool);
        }

		public static int InvokeInt32(string DllName, string EntryPoint, params object[] e)
		{
			return default(int);
		}

        public static IntPtr InvokeIntPtr(string DllName, string EntryPoint, params object[] e)
        {
            return default(IntPtr);
        }

        public static string InvokeString(string DllName, string EntryPoint, params object[] e)
        {
            return default(string);
        }

        public static void InvokeVoid(string DllName, string EntryPoint, params object[] e)
        {
        }
	}
}
