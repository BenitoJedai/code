using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace OrcasAppJetApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.FirstOrDefault() == "appjet")
			{
				Console.WriteLine("creating install script...");

				Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "web");

				using (var w = new StreamWriter(File.OpenWrite("AppJet.js")))
				{
					w.BaseStream.SetLength(0);

					
					w.WriteLine("/* appjet:version 0.1 */ ");
					w.WriteLine("// " + DateTime.Now.ToString());

					foreach (var k in SharedHelper.LocalModulesOf(typeof(Program).Assembly, ScriptType.JavaScript))
					{
						w.WriteLine(File.ReadAllText(k + ".js"));
					}

				}

			}
		}
	}
}
