using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet.JavaScript.AppJet;

namespace ScriptCoreLibAppJet.JavaScript.Library
{
	[Script]
	public static class Extensions
	{
		public static object At(this StorableCollection source, int index)
		{
			var x = source;

			if (index > 0)
				x = x.skip(index);

			return x.first();
		}

	

		public static T ToStorableObject<T>(this string k, T value)
		{
			if (!Native.storage.Contains(k))
				Native.storage.SetValue(k, value);

			return Native.storage.GetValue<T>(k);

		}


		public static StorableCollection ToStorableCollection(this string k)
		{
			if (!Native.storage.Contains(k))
				Native.storage.SetValue(k, new StorableCollection());

			return Native.storage.GetValue<StorableCollection>(k);

		}

		[Script(OptimizedCode = "return e[k];")]
		public static T GetValue<T>(this Storage e, string k)
		{
			return default(T);
		}

		[Script(OptimizedCode = "return !!e[k];")]
		public static bool Contains(this Storage e, string k)
		{
			return default(bool);
		}

		[Script(OptimizedCode = "return e[k] = value;")]
		public static void SetValue<T>(this Storage e, string k, T value)
		{
		}

		public static void ToConsole(this string e)
		{
			Native.printHTML(e);
		}
	}
}
