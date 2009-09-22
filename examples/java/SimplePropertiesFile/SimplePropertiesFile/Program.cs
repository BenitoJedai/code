using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimplePropertiesFile.Library;
using System.Runtime.InteropServices;
using System.IO;

namespace SimplePropertiesFile
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			var p = new MyProperties();

			new FileInfo("p.txt").ToFields(p);

			Console.WriteLine("SleepCount: " + p.SleepCount);
			Console.WriteLine("SleepDuration: " + p.SleepDuration);
		}
	}

	class MyProperties
	{
		public int SleepCount = 2;
		public int SleepDuration = 500;
	}
}
