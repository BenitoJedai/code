using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.Query;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections.Generic
{
	[Script(Implements = typeof(global::System.Collections.Generic.List<>))]
	internal class __List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
	{
		public object _items = Native.API.array();

		internal static class API
		{
			#region bool sort ( array &array [, int sort_flags] )

			/// <summary>
			/// This function sorts an array. Elements will be arranged from lowest to highest when this function has completed. 
			/// </summary>
			/// <param name="_&array">array &amp;array</param>
			[Script(IsNative = true)]
			public static bool sort(object _array) { return default(bool); }

			#endregion


			#region array array_keys ( array input [, mixed search_value [, bool strict]] )

			/// <summary>  
			/// array_keys() returns the keys, numeric and string, from the input array.   
			/// </summary>  
			/// <param name="_input">array input</param>  
			[Script(IsNative = true)]
			public static object array_keys(object _input) { return default(object); }

			#endregion


			#region object count

			/// <summary>
			/// Returns the number of elements in var, which is typically an array, since anything else will have one element. 
			/// </summary>
			/// <param name="_var">mixed var</param>
			[Script(IsNative = true)]
			public static int count(object _var) { return default(int); }

			#endregion



			#region int array_push

			/// <summary>
			/// array_push() treats array as a stack, and pushes the passed variables onto the end of array. The length of array increases by the number of variables pushed.
			/// </summary>
			/// <param name="_array">array &amp;array</param>
			/// <param name="_mixed">mixed var </param>
			[Script(IsNative = true)]
			public static int array_push([ScriptParameterByRef] object _array, object _mixed) { return default(int); }

			#endregion


			#region object array_pop

			/// <summary>
			/// array_pop() pops and returns the last value of the array, shortening the array by one element. If array is empty (or is not an array), NULL will be returned. 
			/// </summary>
			/// <param name="_array">array &amp;array </param>
			[Script(IsNative = true)]
			public static object array_pop([ScriptParameterByRef] object _array) { return default(object); }

			#endregion

			#region bool is_array

			/// <summary>
			/// Finds whether the given variable is an array. 
			/// </summary>
			/// <param name="_mixed">mixed var </param>
			[Script(IsNative = true)]
			public static bool is_array(object _mixed) { return default(bool); }

			#endregion

			[Script(IsNative = true)]
			public static object array_splice([ScriptParameterByRef] object _input, int _offset, int _length, object _replacement) { return default(object); }

			[Script(IsNative = true)]
			public static object array_slice([ScriptParameterByRef] object _array, int _offset, int _length) { return default(object); }

			[Script(IsNative = true)]
			public static bool in_array(object _needle, [ScriptParameterByRef]  object _haystack, bool _strict)
			{
				return default(bool);
			}
		}

		public __List()
		//: this(null)
		{

		}

		//public __List(IEnumerable<T> collection)
		//{
		//    // cannot have this check as the default ctor will pass null anyway
		//    //if (collection == null)
		//    //    throw new global::System.Exception("collection is null");

		//    if (collection != null)
		//        this.AddRange(collection);
		//}

		public void ForEach(Action<T> action)
		{
			foreach (var e in this)
			{
				action(e);
			}
		}

		public void Add(T item)
		{
			API.array_push(_items, item);
		}

		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T v in collection)
			{
				this.Add(v);
			}
		}

		public void Clear()
		{
			API.array_splice(_items, 0, Count, null);
		}

		public int Count
		{
			get { return (int)API.count(_items); }
		}



		#region IList<T> Members

		public int IndexOf(T item)
		{
			var j = -1;

			for (int i = 0; i < Count; i++)
			{
				if (Object.ReferenceEquals(this[i], item))
				{
					j = i;
					break;
				}
			}

			return j;
		}

		public void Insert(int index, T item)
		{
			API.array_splice(_items, index, 0, item);
		}

		public void RemoveAt(int index)
		{
			API.array_splice(_items, index, 1, null);
		}

		public T this[int index]
		{
			get
			{
				// php limitation...
				var r = ArrayReference;
				return r[index];
			}
			set
			{
				var r = ArrayReference;

				r[index] = value;
			}
		}

		#endregion

		#region ICollection<T> Members


		public bool Contains(T item)
		{
			return API.in_array(item, _items, true);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException("");
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(""); }
		}

		public bool Remove(T item)
		{
			var i = IndexOf(item);

			if (i < 0)
				return false;

			RemoveAt(i);

			return true;
		}

		public int RemoveAll(Predicate<T> filter)
		{
			var c = 0;

			for (int i = Count - 1; i >= 0; i--)
			{
				if (filter(this[i]))
				{
					API.array_splice(_items, i, 1, null);
				}
				else
					c++;
			}


			return c;
		}

		#endregion

		#region IEnumerable<T> Members

		public __Enumerator GetEnumerator()
		{
			var e = new SZArrayEnumerator<T>(this.ToArray());
			
			return new __Enumerator
			{
				value = e
			};
		}

		#endregion

		#region IEnumerable Members

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		private T[] ArrayReference
		{
			get
			{
				return (T[])(object)this._items;
			}
		}

		private T[] ArrayReferenceCloned
		{
			get
			{
				return (T[])(object)API.array_slice(_items, 0, Count);
			}
		}


		public T[] ToArray()
		{
			// testme: should return a new array

			return ArrayReferenceCloned;
		}


		[Script(Implements = typeof(global::System.Collections.Generic.List<>.Enumerator))]
		public class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			internal IEnumerator<T> value;

			//internal __Enumerator() : this(null) { }
			//internal __Enumerator(__List<T> list)
			//{
			//    if (list == null)
			//        return;

			//    //value = InternalSequenceImplementation.AsEnumerable(list.ToArray()).GetEnumerator();


			//}


			#region IEnumerator<T> Members

			public T Current
			{
				get { return value.Current; }
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				value.Dispose();
			}

			#endregion

			#region IEnumerator Members

			object IEnumerator.Current
			{
				get { return value.Current; }
			}

			public bool MoveNext()
			{
				return value.MoveNext();
			}

			public void Reset()
			{
				value.Reset();
			}

			#endregion
		}



		#region IEnumerable<T> Members

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region IList Members

		int IList.Add(object value)
		{
			throw new NotImplementedException("");
		}

		void IList.Clear()
		{
			throw new NotImplementedException("");
		}

		bool IList.Contains(object value)
		{
			throw new NotImplementedException("");
		}

		int IList.IndexOf(object value)
		{
			throw new NotImplementedException("");
		}

		void IList.Insert(int index, object value)
		{
			throw new NotImplementedException("");
		}

		bool IList.IsFixedSize
		{
			get { throw new NotImplementedException(""); }
		}

		bool IList.IsReadOnly
		{
			get { throw new NotImplementedException(""); }
		}

		void IList.Remove(object value)
		{
			throw new NotImplementedException("");
		}

		void IList.RemoveAt(int index)
		{
			throw new NotImplementedException("");
		}

		object IList.this[int index]
		{
			get
			{
				throw new NotImplementedException("");
			}
			set
			{
				throw new NotImplementedException("");
			}
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(global::System.Array array, int index)
		{
			throw new NotImplementedException("");
		}

		int ICollection.Count
		{
			get { throw new NotImplementedException(""); }
		}

		bool ICollection.IsSynchronized
		{
			get { throw new NotImplementedException(""); }
		}

		object ICollection.SyncRoot
		{
			get { throw new NotImplementedException(""); }
		}

		#endregion


		public void Reverse()
		{
			var clone = this.ToArray();

			for (int i = 0; i < clone.Length; i++)
			{
				this[clone.Length - 1 - i] = clone[i];
			}


		}
	}
}
