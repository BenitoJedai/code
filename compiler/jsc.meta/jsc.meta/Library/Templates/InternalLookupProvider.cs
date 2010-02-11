using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace jsc.meta.Library.Templates
{
	public abstract class InternalLookup
	{
		public readonly ArrayList Items = new ArrayList();
		public readonly ArrayList Keys = new ArrayList();

		public string Prefix;

		public static string FromType(InternalLookup that, object e)
		{
			var i = that.Items.IndexOf(e);

			if (i < 0)
			{
				i = that.Items.Count;

				that.Items.Add(e);

				var k = that.Prefix + i;

				that.Keys.Add(k);

				return k;
			}

			return (string)that.Keys[i];
		}

		public static object ToType(InternalLookup that, string e)
		{
			var i = that.Keys.IndexOf(e);

			if (i < 0)
			{
				return null;
			}

			return that.Items[i];
		}

		public class _Consumer : InternalLookup
		{
			public _Consumer()
			{
				this.Prefix = "_Consumer";
				
			}

			public static _Consumer LazyConstructor(_Consumer e)
			{
				if (e == null)
					return new _Consumer();

				return e;
			}
		}

		public class _Provider : InternalLookup
		{
			public _Provider()
			{
				this.Prefix = "_Provider";
			}

			public static _Provider LazyConstructor(_Provider e)
			{
				if (e == null)
					return new _Provider();

				return e;
			}
		}
	}


}
