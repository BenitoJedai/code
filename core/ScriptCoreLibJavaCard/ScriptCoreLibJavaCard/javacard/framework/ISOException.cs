using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public class ISOException
	{
		/// <summary>
		/// Throws the Java Card runtime environment-owned instance of the ISOException class with the specified status word.
		/// </summary>
		/// <param name="sw"></param>
		public static void throwIt(short sw)
		{
			throw new __ISOException(sw);
		}

	 
	}

	public sealed class __ISOException : Exception
	{
		public readonly short sw;

		public __ISOException(short sw)
		{
			this.sw = sw;
		}
	} 
}
