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
		public short __setOutgoingAndSend_bOff;
		public short __setOutgoingAndSend_len;

		/// <summary>
		/// This is the "convenience" send method. 
		/// </summary>
		/// <param name="bOff"></param>
		/// <param name="len"></param>
		public void setOutgoingAndSend(short bOff, short len)
		{
			__setOutgoingAndSend_bOff = bOff;
			__setOutgoingAndSend_len = len;
		}

		/// <summary>
		/// Returns the APDU buffer byte array.
		/// </summary>
		/// <returns></returns>
		public sbyte[] getBuffer()
		{
			return __buffer;
		}

		/// <summary>
		/// This is the primary receive method.
		/// </summary>
		/// <returns></returns>
		public short setIncomingAndReceive()
		{
			return default(short);
		}

		sbyte[] __buffer = new sbyte[0xff + 0xf];
	
	}
}
