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
			try
			{
				(new Activity { } as dynamic).setImmersive(i: true);
			}
			catch (Exception err)
			{
				Console.WriteLine(new { err.Message, err.StackTrace });


			}
		}
	}


	[SwitchToCLRContext]
	static class CLRProgram
	{

		[STAThread]
		public static string CLRMain(string data) => data;
	}



}
