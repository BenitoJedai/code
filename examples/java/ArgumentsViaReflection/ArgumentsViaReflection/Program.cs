using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ArgumentsViaReflection.Library;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;

namespace ArgumentsViaReflection
{
	public class Program2
	{
		public string Option1 = "hello";
	}

	public class Program
	{
		public string Option1 = "hello";
		public string Option2 = "world";

		public int Integer1 = 0;
		public FileInfo File1 = new FileInfo("file1");
		public DirectoryInfo Dir2 = new DirectoryInfo("dir1");

		public class Tag
		{
			public string Name = "Tag";
		}

		public Tag Tag1 = new Tag { Name = "Tag1" };
		public Tag Tag2 = new Tag { Name = "Tag2" };

		public static void Main(string[] args)
		{
			var p = new Program();

			if (args.Length == 0)
				args = new[] { "", "Tag1.Name:hello", "Option2:hey", "Integer1:56", "File1:a", @"Dir2:b\y" };

			args.AsParametersTo(p);

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
