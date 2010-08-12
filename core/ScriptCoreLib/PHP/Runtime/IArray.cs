using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.PHP;

namespace ScriptCoreLib.PHP.Runtime
{




    [Script(InternalConstructor = true)]
    public class IArray<TIndex, TValue> : IArray<TIndex>
    {

        public IArray(params TValue[] e)
        {
            // InternalConstructor
        }

        static IArray<TIndex, TValue> InternalConstructor(params TValue[] e)
        {
            IArray<TIndex, TValue> z = new IArray<TIndex, TValue>();

            z.AddRange(e);

            return z;
        }

        [Script(DefineAsStatic = true)]
        public void AddRange(TValue[] e)
        {
            foreach (TValue v in e)
            {
                Push(v);
            }
        }

        [Script(DefineAsStatic = true)]
        public void Push(TValue v)
        {
            base.Push(v);
        }



        #region Constructor

        public IArray()
        {
            // InternalConstructor
        }

        [Script(OptimizedCode = @"return array();")]
        static IArray<TIndex, TValue> InternalConstructor()
        {
            return default(IArray<TIndex, TValue>);
        }

        #endregion

        
        [Script(DefineAsStatic = true)]
        public new TValue[] ToArray()
        {
            return (TValue[])(object)this;
        }

        public static implicit operator TValue[](IArray<TIndex, TValue> m)
        {
            return m.ToArray();
        }



        public new TValue this[TIndex i]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (TValue)Expando.GetArrayMember(this, i);
            }
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.SetArrayMember(this, i, value);
            }
        }
    }

    [Script(InternalConstructor = true)]
    public class IArray<TIndex> : IArray
    {

        public new TIndex[] Keys
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (IArray<int, TIndex>) API.array_keys(this);
            }
        }

        #region Constructor

        public IArray()
        {
            // InternalConstructor
        }

        [Script(OptimizedCode = @"return array();")]
        static IArray<TIndex> InternalConstructor()
        {
            return default(IArray<TIndex>);
        }

        #endregion

        public object this[TIndex i]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando.GetArrayMember(this, i);
            }
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.SetArrayMember(this, i, value);
            }
        }


    }

    [Script(InternalConstructor = true)]
    public class IArray
    {
        public static IArray<int, string> Split(string e, string sep)
        {
            return (IArray<int, string>)((object)Native.API.explode(sep, e));
        }

        public static class API
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

        }

        #region Constructor

        public IArray()
        {
            // InternalConstructor
        }

        [Script(OptimizedCode = @"return array();")]
        static IArray InternalConstructor()
        {
            return default(IArray);
        }

        public IArray(params object[] e)
        {
            // InternalConstructor
        }

        static IArray InternalConstructor(params object[] e)
        {
            IArray z = new IArray();

            z.AddRange(e);

            return z;
        }

        [Script(DefineAsStatic = true)]
        public void AddRange(object[] e)
        {
            foreach (object v in e)
            {
                Push(v);
            }
        }

        #endregion

        /// <summary>
        /// converts each array member from object to array
        /// </summary>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public IArray MembersToArray()
        {
            IArray a = new IArray();

            foreach (object z in Keys.ToArray())
            {
                a[z] = Expando.Of(this[z]).GetMembers();
            }

            return a;
        }

        [Script(DefineAsStatic = true)]
        public object[] ToArray()
        {
            return (object[])(object)this;
        }

        public static implicit operator object[](IArray m)
        {
            return m.ToArray();
        }

        public object this[object i]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando.GetArrayMember(this, i);
            }
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.SetArrayMember(this, i, value);
            }
        }

        [Script(DefineAsStatic = true)]
        public void Push(object v)
        {

            API.array_push(this, v);
        }

        [Script(DefineAsStatic = true)]
        public string Pop()
        {
            return (string)API.array_pop(this);
        }

        public IArray Keys
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return (IArray)API.array_keys(this);
            }
        }

        public IArray SortedKeys
        {
            [Script(DefineAsStatic = true)]
            get
            {
                IArray n = (IArray)API.array_keys(this);

                API.sort(n);

                return n;
            }
        }

        public int Length
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return API.count(this);
            }
        }


        [Script(DefineAsStatic = true)]
        public bool Exists(object p)
        {
            bool b = false;

            object[] z = ToArray();

            for (int i = 0; i < z.Length && !b; i++)
            {
                b = z[i] == p;
            }

            return b;
        }

        [Script(DefineAsStatic = true)]
        public bool Contains(string lang)
        {
            return Native.API.in_array(lang, this);
        }
    }

    [Script, System.Obsolete]
    internal class List<TItem> 
        //where TItem : class
    {
        public IArray<int, TItem> BaseList = new IArray<int, TItem>();

        public event EventHandler<TItem> ItemAdded;

        public int IndexOf(TItem e)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Helper.VariableEquals( this[i] , e) )
                    return i;
            }

            return -1;
        }

        public int Count
        {
            get
            {
                return BaseList.Length;
            }
        }

        public TItem this[int i]
        {
            get
            {
                return BaseList[i];
            }
        }

        public void Add(TItem e)
        {
            BaseList.Push(e);
            Helper.Invoke(ItemAdded, e);
        }

        public TItem[] ToArray(EventHandler< Predicate<TItem>> h)
        {
            IArray<int, TItem> a = new IArray<int, TItem>();

            Predicate<TItem> x = new Predicate<TItem>();


            foreach (TItem v in BaseList.ToArray())
            {
                x.Target = v;
                x.Value = true;

                Helper.Invoke(h, x);

                if (x.Value)
                    a.Push(x.Target);
            }

            return a.ToArray();
        }

        public TItem[] ToArray()
        {
            return BaseList.ToArray();
        }

        public void Add(params TItem[] e)
        {
            foreach (TItem v in e)
            {
                Add(v);
            }
        }

        public TItem Find<TValue>(EventHandler<Predicate<TItem, TValue>> convert, TValue value)
        {
            TItem r = default(TItem);

            foreach (TItem v in BaseList.ToArray())
            {
                Predicate<TItem, TValue> x = new Predicate<TItem, TValue>();

                x.Value = true;

                x.TargetIn = v;

                Helper.Invoke(convert, x);

                if (x.Value)
                {

                    if (Helper.VariableEquals( x.TargetOut, value))
                    {


                        r = v;



                        break;
                    }
                }
            }

            return r;

        }

        public TItem Find(EventHandler<Predicate<TItem>> h)
        {
            Predicate<TItem> x = new Predicate<TItem>();

            TItem r = default(TItem);

            foreach (TItem v in BaseList.ToArray())
            {
                x.Target = v;
                x.Value = true;

                Helper.Invoke(h, x);

                if (x.Value)
                {
                    r = v;

                    break;
                }
            }

            return r;
        }

        public void Remove(EventHandler< Predicate<TItem>> p)
        {
            if (p == null)
                return;

            IArray<int, TItem> _old = BaseList;
                
            BaseList = new IArray<int, TItem>();

            Predicate<TItem> x = new Predicate<TItem>();

            foreach (TItem v in _old.ToArray())
            {
                x.Target = v;
                x.Value = false;

                p(x);

                if (!x.Value)
                {
                    BaseList.Push(v);
                }
            }
        }

        public int CountOf(EventHandler<Predicate<TItem>> p)
        {
            int u = 0;

            Predicate<TItem> x = new Predicate<TItem>();

            foreach (TItem v in BaseList.ToArray())
            {
                x.Target = v;
                x.Value = false;

                p(x);

                if (x.Value)
                    u++;
            }

            return u;
        }

        public List<T> Convert<T>(EventHandler<Predicate<TItem, T>> h)
            //where T : class
        {
            List<T> ret = new List<T>();

            foreach (TItem v in this.ToArray())
            {
                Predicate<TItem, T> p = new Predicate<TItem, T>();

                p.TargetIn = v;
                p.Value = true;

                Helper.Invoke(h, p);

                if (p.Value)
                {
                    ret.Add(p.TargetOut);
                }

            }

            return ret;
        }
    }
}
