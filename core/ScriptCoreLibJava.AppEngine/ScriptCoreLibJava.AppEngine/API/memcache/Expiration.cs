using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.AppEngine.API.memcache
{
	// http://code.google.com/intl/et-EE/appengine/docs/java/javadoc/com/google/appengine/api/memcache/Expiration.html
	[Script(IsNative = true)]
	public class Expiration
	{
		/// <summary>
		/// Creates an Expiration for some number of seconds in the future.
		/// </summary>
		/// <param name="secondsDelay"></param>
		/// <returns></returns>
		public static Expiration byDeltaSeconds(int secondsDelay)
		{
			return default(Expiration);
		}
	}
}
