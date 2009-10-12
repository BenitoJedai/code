using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library
{
	public class VirtualDictionary<TKey, TValue> 
	{
		public readonly Dictionary<TKey, TValue> BaseDictionary = new Dictionary<TKey, TValue>();
		public event Action<TKey> Resolve;

		public TValue this[TKey k]
		{
			get
			{
				if (!BaseDictionary.ContainsKey(k))
					if (Resolve != null)
						Resolve(k);

				return BaseDictionary[k];
			}
			set
			{
				BaseDictionary[k] = value;
			}
		}

	}
}
