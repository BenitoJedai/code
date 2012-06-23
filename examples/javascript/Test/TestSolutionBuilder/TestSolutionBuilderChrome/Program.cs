// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace TestSolutionBuilderChrome
{
	using ScriptCoreLib.Shared.Avalon.Extensions;
	using System.Windows.Media;

	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// In debug build you can just hit F5 and debug the server side code.
		/// </summary>
		/// <param name="args">Commandline arguments</param>
		public static void Main(string[] args)
		{
			foreach (SolidColorBrush item in 0x6C7476.ToSolidColorBrush().ToGradient(
				0x536274.ToSolidColorBrush(), 8
				))
			{
				Console.WriteLine(
@"<div style='background-color: #"
+ item.Color.R.ToString("x2")
+ item.Color.G.ToString("x2")
+ item.Color.B.ToString("x2") 
+ "; width: 100%;\n height: 2px; overflow: hidden;'></div>"
				);
			}

			// Prepare the yield value for
			RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
		}


	}
}
