using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ArgumentsViaReflection.Library;
using ScriptCoreLib.Reflection.Options;

namespace ArgumentsViaReflection
{

	public class Program
	{


		public static void Main(string[] args)
		{
			foreach (var item in args)
			{
				Console.WriteLine("argument: " + item);
			}


			if (args.Length == 0)
			{
				//args = new[] { "A", "/Tag1.Name:hello", "/Tags.Name:tag 1", "/Tags.Name:tag 2", "/Option2:hey", "/Integer1:56", "/File1:a", @"/Dir2:b\y" };
				args = new[] { "OperationC", "/Mode:tag C", "/Program.Tag1.Name:hello", "/Program.Tags.Name:tag 1", "/Tags.Name:tag 2", "/Program.Option2:hey", "/Program.Integer1:56", "/Program.File1:a", @"/Program.Dir2:b\y" };
			}

			args.AsParametersTo(
				new OperationC().Invoke 
			);

		
		}
	}

}
