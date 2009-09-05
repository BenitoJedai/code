using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ArgumentsViaReflection.Library;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

namespace ArgumentsViaReflection
{

	public class Program
	{


		public static void Main(string[] args)
		{

			var p = new ProgramArguments();

			foreach (var item in args)
			{
				Console.WriteLine("argument: " + item);
			}

			if (args.Length == 0)
				args = new[] { "A", "/Tag1.Name:hello", "/Tags.Name:tag 1", "/Tags.Name:tag 2", "/Option2:hey", "/Integer1:56", "/File1:a", @"/Dir2:b\y" };

			args.AsParametersTo(
				new ParameterDispatcher(p)
				{
					{"A", 
						delegate 
						{
							Console.WriteLine("Operation A");
						}
					},
					{"B", 
						delegate 
						{
							Console.WriteLine("Operation B");
					
						}
					},

					delegate
					{
						Console.WriteLine("Operation default");
					}
				}
			);

			Console.WriteLine("Option1: " + p.Option1);
			Console.WriteLine("Option2: " + p.Option2);
			Console.WriteLine("Tag1.Name: " + p.Tag1.Name);
			Console.WriteLine("Tag2.Name: " + p.Tag2.Name);
			Console.WriteLine("Integer1: " + p.Integer1);
			Console.WriteLine("File1: " + p.File1.FullName);
			Console.WriteLine("Dir2: " + p.Dir2.FullName);
		}
	}

}
