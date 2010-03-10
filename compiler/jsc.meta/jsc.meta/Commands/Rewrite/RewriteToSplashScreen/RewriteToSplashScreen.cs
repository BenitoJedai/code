using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;

namespace jsc.meta.Commands.Rewrite.RewriteToSplashScreen
{
	public partial class RewriteToSplashScreen : CommandBase
	{
		public override void Invoke()
		{
			// rewrite two assemblies and inject splash

			this.ToConsole();
		}
	}

	namespace Templates
	{
		internal class InternalApplication
		{
			public static void Main(string[] args)
			{
			}
		}

		internal class InternalSplashScreen
		{
			public static void ShowDialogSplash(Action e)
			{
			}
		}

		internal class InternalSplashScreenApplication
		{
			public static void Main(string[] args)
			{
				InternalSplashScreen.ShowDialogSplash(
					delegate
					{
						InternalApplication.Main(args);
					}
				);
			}
		}
	}
}
