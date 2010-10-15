﻿using System;
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
	}
}
