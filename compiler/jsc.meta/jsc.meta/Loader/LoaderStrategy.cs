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

            LoaderStrategyImplementation.Initialize();

			Program.Main(args);
		}




	}
}
