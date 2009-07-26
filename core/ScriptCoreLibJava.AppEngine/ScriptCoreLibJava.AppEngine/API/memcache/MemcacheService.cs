using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.AppEngine.API.memcache
{
	
	// http://code.google.com/intl/et-EE/appengine/docs/java/javadoc/com/google/appengine/api/memcache/MemcacheService.html
	
	/// <summary>
	/// The Java API for the App Engine Memcache service. This offers a fast distrubted cache for commonly-used data. The cache is limited both in duration and also in total space, so objects stored in it may be discarded at any time.
	/// Note that null is a legal value to store in the cache, or to use as a cache key. Although the API is written for Objects, both keys and values should be Serializable, although future versions may someday accept specific types of non-Serializable Objects.
	/// The values returned from this API are mutable copies from the cache; altering them has no effect upon the cached value itself until assigned with one of the put methods. Likewise, the methods returning collections return mutable collections, but changes do not affect the cache.
	/// </summary>
	[Script(IsNative = true)]
	public interface MemcacheService
	{
		/// <summary>
		/// Tests whether a given value is in cache, even if its value is null.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool contains(object key);
          
		/// <summary>
		/// Fetches a previously-stored value, or null if unset.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		object get(object key);

		/// <summary>
		/// Convenience put, equivalent to put(key, value, expiration, SetPolicy.SET_ALWAYS).
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="expires"></param>
		void put(object key, object value, Expiration expires);
          
		/// <summary>
		/// Atomically fetches, increments, and stores a given integral value.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="delta"></param>
		/// <returns></returns>
		long increment(object key, long delta);

		/// <summary>
		/// Get the name of the namespace that will be used in API calls.
		/// </summary>
		/// <returns></returns>
		string getNamespace();
          
		/// <summary>
		/// Change the namespace used in API calls.
		/// </summary>
		/// <param name="newNamespace"></param>
		void setNamespace(string newNamespace);
          
          
	}
}
