using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortCloner.Library;
using System.Runtime.InteropServices;
using ScriptCoreLib.Reflection.Options;

namespace PortCloner
{
	sealed class Clone
	{
		public delegate void CloneAction(Clone e);

		public int ServerPort;

		public string TargetHost;
		public int TargetPort;

		public CloneAction AfterInvoke;

		public void Invoke()
		{
			if (AfterInvoke != null)
				AfterInvoke(this);
		}
	}

	public partial class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(@"
usage: 
	PortCloner.exe Clone /TargetHost:localhost /TargetPort:80
	PortClonerMetaScript.jar.bat Clone /TargetHost:localhost /TargetPort:80
");

			args.AsParametersTo(
				new Clone
				{
					AfterInvoke = c =>
					{
						Console.WriteLine(c.TargetHost + ":" + c.TargetPort);
					}
				}.Invoke
			);

		}
	}
}
