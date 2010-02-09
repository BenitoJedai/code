using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Library
{
	public class VirtualDictionary<TKey, TValue>
	{
		public readonly Dictionary<TKey, TValue> BaseDictionary = new Dictionary<TKey, TValue>();
		
		public event Action<TKey, Action<Action>> ResolveWithContinuation;
		public event Action<TKey> Resolve;

		public TValue this[TKey k, Action<Action> Continuation]
		{
			get
			{
				// nested type declaring type problem...

				if (!BaseDictionary.ContainsKey(k))
					if (ResolveWithContinuation != null)
						ResolveWithContinuation(k, Continuation);

				return BaseDictionary[k];
			}

		}

		public TValue this[TKey k]
		{
			get
			{
				if (!BaseDictionary.ContainsKey(k))
				{
					if (Resolve != null)
						Resolve(k);
					else if (ResolveWithContinuation != null)
						ResolveWithContinuation(k, null);
				}

				return BaseDictionary[k];
			}
			set
			{
				BaseDictionary[k] = value;
			}
		}

		public static implicit operator Func<TKey, TValue>(VirtualDictionary<TKey, TValue> e)
		{
			return k => e[k];
		}
	}
}
