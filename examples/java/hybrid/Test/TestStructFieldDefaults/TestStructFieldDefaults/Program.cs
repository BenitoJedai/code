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

namespace TestStructFieldDefaults
{
	// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150328

	struct HopToJVM
	{
		public void GetResult() {
			Console.WriteLine("GetResult");
		}
	}

	struct SharedProgram__Invoke_d__0
	{
		public HopToJVM __u___awaiter2;
	}

	static class Program
	{

		//		found 1264 types to be compiled
		//System.NotImplementedException: { ParameterType = TestStructFieldDefaults.SharedProgram__Invoke_d__0&, p = [0x0004]
		//		call       +0 -1{[0x0002]
		//		ldloca.s   +1 -0} , _method = Void Invoke(TestStructFieldDefaults.SharedProgram__Invoke_d__0 ByRef) }

		//static void Invoke(ref SharedProgram__Invoke_d__0 that)
		static void Invoke(SharedProgram__Invoke_d__0 that)
		{
			that.__u___awaiter2.GetResult();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				SharedProgram__Invoke_d__0 x;

				//Invoke(ref x);
				Invoke(x);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
			Thread.Sleep(10000);
		}


	}



	[SwitchToCLRContext]
	static class CLRProgram
	{
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
