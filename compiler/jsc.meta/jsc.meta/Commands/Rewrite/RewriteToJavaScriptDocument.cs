using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace jsc.meta.Commands.Rewrite
{
	[Description("This command will tare an assembly to compile java and flash objects separatly.")]
	public partial class RewriteToJavaScriptDocument : CommandBase
	{
		/* usage:
				if $(ConfigurationName)==Debug goto :eof
				c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)"		 
		 */

		/* How was this feature implemented in the long run?
		 * 
		 * 1. Adding a new command to the command chain
		 * 2. Test if the parameters are passed with a test project
		 * 3. Save to the svn
		 */

		public override void Invoke()
		{
			Console.WriteLine("RewriteToJavaScriptDocument: " + this.assembly.FullName);
		}
	}
}
