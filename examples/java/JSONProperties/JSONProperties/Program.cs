using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JSONProperties.Library;
using System.Runtime.InteropServices;
using ScriptCoreLib.JSON;
using System.IO;

namespace JSONProperties
{
	public partial class Program
	{


		public static void Main(string[] args)
		{





			// Notes:
			// 1. All referenced assemblies shall
			//    define [assembly:Obfuscation(feature = "script")]
			// 2. Turn off "optimize code" option in release build
			// 3. All used .net APIs must be defined by ScriptCoreLibJava
			// 4. Generics are not supported.
			// 5. Check post build event
			// 6. Build in release build configuration for java version

			Console.WriteLine("JSONProperties. Crosscompiled from C# to Java.");
			Console.WriteLine("---------------------------------");

			if (File.Exists("data.json"))
			{
				Console.WriteLine("Previous configuration:");

				var Previous = MyData.Parse(File.ReadAllText("data.json"));

				foreach (var k in Previous)
				{
					Console.WriteLine(k.Name + " - " + k.Target);
				}
			}

			var Default = new[] {
				new MyData { Name = "Jane Doe", Target = "0.0.0.0:0" },
				new MyData { Name = "Mike Smith", Target = "0.0.0.0:0" }
			};

			Console.WriteLine("Default configuration:");
			foreach (var k in Default)
			{
				Console.WriteLine(k.Name + " - " + k.Target);
			}

			File.WriteAllText("data.json", MyData.ToString(Default));


		}


	}
}
