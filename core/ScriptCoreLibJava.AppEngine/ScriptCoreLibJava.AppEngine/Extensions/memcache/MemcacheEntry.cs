using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.AppEngine.API.memcache;

namespace ScriptCoreLibJava.AppEngine.Extensions.memcache
{
	[Script]
	public class MemcacheEntry
	{
		public MemcacheService Context;

		public object Key;

		public int ExpirationInSeconds = 60 * 5;

		public object Value
		{
			get
			{
				return Context.get(Key);
			}
			set
			{
				Context.put(Key, value, Expiration.byDeltaSeconds(ExpirationInSeconds));
			}
		}

		public long ValueInt64
		{
			set
			{
				this.Value = value;
			}
			get
			{
				return (long)this.Value;
			}
		}

		public bool Exists
		{
			get
			{
				return Context.contains(Key);
			}
		}

		public long Increment()
		{
			return Increment(1);
		}

		public long Increment(long delta)
		{
			return this.Context.increment(Key, delta);
		}

		public static implicit operator MemcacheEntry(Type TargetType)
		{
			return MemcacheServiceFactory.getMemcacheService().ToEntry(TargetType);
		}
	}

	[Script]
	public static class MemcacheEntryExtensions
	{
		public static MemcacheEntry ToEntry(this MemcacheService Context, Type TargetType)
		{
			return new MemcacheEntry { Context = Context, Key = TargetType.FullName };
		}


		public static MemcacheEntry ToEntry(this MemcacheService Context, object Key)
		{
			return new MemcacheEntry { Context = Context, Key = Key };
		}

		public static MemcacheEntry ToEntry(this MemcacheService Context, object Key, int ExpirationInSeconds)
		{
			return new MemcacheEntry { Context = Context, Key = Key, ExpirationInSeconds = ExpirationInSeconds };
		}

		[Script, Serializable]
		public class MemcacheEntryPathNode
		{
			public object Parent;
			public object Current;
		}

		public static MemcacheEntry ToEntry(this MemcacheEntry Entry, object Key)
		{
			return new MemcacheEntry
			{
				Context = Entry.Context,
				Key =
					new MemcacheEntryPathNode
					{
						Parent = Entry.Key,
						Current = Key
					},
			};
		}

		public static MemcacheEntry ToEntry(this MemcacheEntry Entry, object Key, int ExpirationInSeconds)
		{
			return new MemcacheEntry
			{
				Context = Entry.Context,
				Key =
					new MemcacheEntryPathNode
					{
						Parent = Entry.Key,
						Current = Key
					},
				ExpirationInSeconds = ExpirationInSeconds
			};
		}
	}
}
