using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;

namespace JVMCLRDynamicInvoke
{
	class Activity
	{
		public void setImmersive(bool i)
		{
			Console.WriteLine("enter setImmersive " + new { i });
		}
	}

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			//			before Invoke
			//__InvokeMemberBinder: { Name = setImmersive, argumentInfoCount = 2 }
			//			__CallSite __InvokeMemberBinder arg1 { subject = JVMCLRDynamicInvoke.Activity@16ec8df, TargetType = JVMCLRDynamicInvoke.Activity, Name = setImmersive, arg1 = true, Candidates = 1 }
			//			enter setImmersive { { i = true } }
			//			after Invoke

			try
			{
				Console.WriteLine("before Invoke");

				(new Activity { } as dynamic).setImmersive(i: true);
				Console.WriteLine("after Invoke");
			}
			catch (Exception err)
			{
				Console.WriteLine(new { err.Message, err.StackTrace });


			}

			Thread.Sleep(6000);
		}
	}


	[SwitchToCLRContext]
	static class CLRProgram
	{

		[STAThread]
		public static string CLRMain(string data) => data;
	}



}
