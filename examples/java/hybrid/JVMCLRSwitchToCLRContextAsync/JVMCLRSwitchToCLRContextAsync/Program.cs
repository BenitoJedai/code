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
using System.Threading.Tasks;

namespace JVMCLRSwitchToCLRContextAsync
{

	static class Program
	{
		static Program()
		{
			Console.WriteLine(typeof(object) + " Program prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{

			var ebytes = CLRProgram.CLRMain(
				 m: null,
				 e: null
			);


			SharedProgram.Invoke("hi").Wait();




		


			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
			Thread.Sleep(10000);
		}


	}

	class SharedProgram
	{
		public static async Task Invoke(string e)
		{
			Console.WriteLine(typeof(object) + " enter " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });




			Console.WriteLine(typeof(object) + " exit " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
		}
	}

	//System.Object Program prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
	//System.Object enter JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
	//System.Object exit JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }
	//System.Object CLRProgram prep for JVMCLRSwitchToCLRContextAsync.SharedProgram{ ManagedThreadId = 1 }



	[SwitchToCLRContext]
	static class CLRProgram
	{
		static CLRProgram()
		{
			Console.WriteLine(typeof(object) + " CLRProgram prep for " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
		}

		[STAThread]
		public static byte[] CLRMain(
			byte[] e,
			byte[] m
				)
		{


			return null;
		}
	}



}
