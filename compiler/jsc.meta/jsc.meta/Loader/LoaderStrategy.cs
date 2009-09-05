using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace jsc.meta.Loader
{
	class LoaderStrategy
	{

		public static void Main(string[] args)
		{
			// if this assembly does not embed the referenced
			// assemblies we might need to tell where to look
			// for them.

			// jsc.meta.exe is at c:\util\bin
			// common libraries are at c:\util\lib

			// note that common libraries could be used in
			// script applications and be used in the compilers too
			// not all common libraries are designed for all platforms

			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);


			Program.Main(args);
		}

		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			var bin = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
			var lib = new DirectoryInfo(Path.Combine(bin.FullName, @"..\lib"));

			if (!lib.Exists)
				lib = new DirectoryInfo(@"c:\util\jsc\lib");


			if (lib.Exists)
			{
				var r = new AssemblyName(args.Name);
				if (File.Exists(Path.Combine(bin.FullName, r.Name + ".dll")))
					return null;

				if (File.Exists(Path.Combine(bin.FullName, r.Name + ".exe")))
					return null;

				var lib_dll = Path.Combine(lib.FullName, r.Name + ".dll");
				if (File.Exists(lib_dll))
				{
					return Assembly.LoadFile(lib_dll);
				}

				var lib_exe = Path.Combine(lib.FullName, r.Name + ".exe");
				if (File.Exists(lib_exe))
				{
					return Assembly.LoadFile(lib_exe);
				}
			}
			return null;
		}



	}
}
