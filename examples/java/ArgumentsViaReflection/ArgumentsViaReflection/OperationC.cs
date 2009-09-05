using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Reflection.Options;

namespace ArgumentsViaReflection
{
	class OperationC : ProgramArguments
	{
		public string Mode;

		public ProgramArguments Program;

		public void Invoke()
		{
			Console.WriteLine("Mode: " + Mode);

			var p = this.Program;

			Console.WriteLine("Option1: " + p.Option1);
			Console.WriteLine("Option2: " + p.Option2);
			Console.WriteLine("Tag1.Name: " + p.Tag1.Name);
			Console.WriteLine("Tag1.File1: " + p.Tag1.File1.FullName);
			Console.WriteLine("Tag2.Name: " + p.Tag2.Name);
			Console.WriteLine("Tag2.File1: " + p.Tag2.File1.FullName);
			Console.WriteLine("Integer1: " + p.Integer1);
			Console.WriteLine("File1: " + p.File1.FullName);
			Console.WriteLine("Dir2: " + p.Dir2.FullName);

			if (p.Tags != null)
				foreach (var t in p.Tags)
				{
					Console.WriteLine("Tags.Name: " + t.Name);

				}
		}
	}
}
