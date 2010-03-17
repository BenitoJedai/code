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

		public readonly ArrayList BaseInterfaces = new ArrayList();

		bool IsBaseInterfacesFrozen;

		public static void AddBaseInterface(InternalLookup that, InternalLookup n)
		{
			if (that.IsBaseInterfacesFrozen)
				return;

			that.BaseInterfaces.Add(n);
		}

		public static void FreezeBaseInterfaces(InternalLookup that)
		{
			that.IsBaseInterfacesFrozen = true;
		}



		public static string FromType(InternalLookup that, object e)
		{
			var i = that.Items.IndexOf(e);

			//Console.WriteLine("    <Item Index='" + i + "' />");

			if (i < 0)
			{

				// we should sync keys now

				var j = new GetCurrentIndex { j = that.CurrentIndex + 1 };

				j.Invoke(that);

				InsertKeyValuePairToBaseInterfaces(that, e, j.j);

				return InsertKeyValuePair(that, e, j.j);
			}

			return (string)that.Keys[i];
		}

		private static void InsertKeyValuePairToBaseInterfaces(InternalLookup that, object e, int j)
		{
			for (int i = 0; i < that.BaseInterfaces.Count; i++)
			{
				var item = (InternalLookup)that.BaseInterfaces[i];
				InsertKeyValuePair(item, e, j);
			}
		}

		class GetCurrentIndex
		{
			public int j;

			public void Invoke(InternalLookup that)
			{
				for (int i = 0; i < that.BaseInterfaces.Count; i++)
				{
					var item = (InternalLookup)that.BaseInterfaces[i];
					var jj = item.CurrentIndex + 1;

					if (j < jj)
						j = jj;
				}
			}

		}

		public int CurrentIndex;

		private static string InsertKeyValuePair(InternalLookup that, object e, int j)
		{
			that.CurrentIndex = j;

			that.Items.Add(e);

			var k = that.Prefix + j;

			that.Keys.Add(k);

			return k;
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
				this.Prefix = "_DefinedByJavaScript";

			}

			public static _Consumer LazyConstructor(_Consumer e)
			{
				if (e == null)
					return new _Consumer { };

				return e;
			}
		}

		public class _Provider : InternalLookup
		{
			public _Provider()
			{
				this.Prefix = "_DefinedByPlugin";
			}

			public static _Provider LazyConstructor(_Provider e)
			{
				if (e == null)
					return new _Provider { };

				return e;
			}
		}
	}


}
