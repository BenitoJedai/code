using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;

namespace jsc.meta.Commands.Rewrite.RewriteToSplashScreen
{
	partial class RewriteToSplashScreen
	{
		public FileInfo PrimaryAssembly;

		public AvalonSplashComponent Splash;


		public override string ToString()
		{
			return new { PrimaryAssembly }.ToString();
		}
	}
}
