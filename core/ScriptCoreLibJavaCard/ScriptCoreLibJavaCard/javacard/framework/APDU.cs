using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public sealed class APDU
	{
		/// <summary>
		/// This is the "convenience" send method. 
		/// </summary>
		/// <param name="bOff"></param>
		/// <param name="len"></param>
		public void setOutgoingAndSend(short bOff, short len)
		{
		}

		/// <summary>
		/// Returns the APDU buffer byte array.
		/// </summary>
		/// <returns></returns>
		public sbyte[] getBuffer()
		{
			return default(sbyte[]);
		}

		/// <summary>
		/// This is the primary receive method.
		/// </summary>
		/// <returns></returns>
		public short setIncomingAndReceive()
		{
			return default(short);
		}


	}
}
