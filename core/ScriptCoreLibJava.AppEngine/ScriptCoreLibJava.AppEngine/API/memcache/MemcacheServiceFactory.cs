using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.AppEngine.API.memcache
{
	// http://code.google.com/intl/et-EE/appengine/docs/java/javadoc/com/google/appengine/api/memcache/MemcacheServiceFactory.html
	[Script(IsNative = true)]
	public static class MemcacheServiceFactory
	{
		/// <summary>
		/// Gets a handle to the cache service.
		/// </summary>
		/// <returns></returns>
		public static MemcacheService getMemcacheService()
		{
			return default(MemcacheService);
		}
	}
}
