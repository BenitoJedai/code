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

		public int Offset;
		public int Step;

		public static string FromType(InternalLookup that, object e)
		{
			var i = that.Items.IndexOf(e);

			if (i < 0)
			{
				i = that.Items.Count;

				that.Items.Add(e);
			}

			return "" + (i * that.Step + that.Offset);
		}

		public static object ToType(InternalLookup that, string e)
		{
			// if it looks like our code but does not exist then throw
			// if it looks remote code return null

			// implement and test here
			// code contracts?

			return that.Items[0];

			var i = (int.Parse(e) - that.Offset) / that.Step;

			return that.Items[i];
		}

		public class _Consumer : InternalLookup
		{
			public _Consumer()
			{
				this.Step = 2;
				this.Offset = 100;
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
				this.Step = 2;
				this.Offset = 101;
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
