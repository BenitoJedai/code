using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public class JCSystem
	{
		/// <summary>
		/// This method is invoked by the applet to trigger the object deletion service of the Java Card runtime environment.
		/// </summary>
		public static void requestObjectDeletion()
		{
		}
          

	}
}
