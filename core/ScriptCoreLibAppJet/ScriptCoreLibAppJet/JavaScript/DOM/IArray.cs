using ScriptCoreLib;
using System;
using ScriptCoreLibAppJet.JavaScript.Runtime;


namespace ScriptCoreLibAppJet.JavaScript.DOM
{
    [Script(HasNoPrototype = true, InternalConstructor=true)]
    public class IArray<TItem>
    {
        public IArray()
        {

        }


     
        [Script(OptimizedCode=@"return [];")]
        static IArray<TItem> InternalConstructor()
        {
            return default(IArray<TItem>);
        }


        public static IArray<TItem> operator + (IArray<TItem> e, TItem v)
        {
            e.push(v);

            return e;
        }

  

        public void push(TItem e)
        {

        }

        public TItem shift()
        {
            return default(TItem);
        }

        public void unshift(TItem e)
        {
        }

        public TItem pop()
        {
            return default(TItem);
        }

        /// <summary>
        /// http://codepunk.hardwar.org.uk/ajs44.htm
        /// </summary>
        public void splice(int index, int iRemove, TItem new0)
        {

        }

        public void splice(int index, int iRemove)
        {

        }

		// http://www.w3schools.com/jsref/jsref_slice_array.asp
		public IArray<TItem> slice(int index)
		{
			return default(IArray<TItem>);
		}


        public int length;

        public string join()
        {
            return default(string);
        }

        public string join(string e)
        {
            return default(string);
        }

        public TItem this[int i]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (TItem)Expando.InternalGetMember(this, i);
            }
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.InternalSetMember(this, i, value);
            }
        }

		[Script(DefineAsStatic = true)]
		public int indexOf(TItem item)
		{
			var j = -1;

			for (int i = 0; i < length; i++)
			{
				if (Expando.ReferenceEquals(this[i], item))
				{
					j = i;
					break;
				}
			}

			return j;
		}


        [Script(DefineAsStatic=true)]
        public TItem[] ToArray()
        {
            return (TItem[])(object)this;
        }

        public static implicit operator TItem[](IArray<TItem> m)
        {
            return m.ToArray();
        }

        [Script(OptimizedCode="return e.split(d);")]
        public static IArray<TItem> Split(TItem e, string d)
        {
            return default(IArray<TItem>);
        }

        //[Script(OptimizedCode = "return e.split(d);")]
        //public static IArray<TItem> Split(TItem e, char d)
        //{
        //    return default(IArray<TItem>);
        //}

        public void sort(IFunction e)
        {

        }

        [Script(DefineAsStatic = true)]
        public void sort(Func<TItem, TItem, int> e)
        {
            sort(((BCLImplementation.System.__Delegate)(object)e).InvokePointer);
        }

        //public bool IsArray
        //{
        //    [Script(DefineAsStatic = true)]
        //    get
        //    {
        //        return Expando.Of(this).IsArray;
        //    }
        //}

     


    }
}
