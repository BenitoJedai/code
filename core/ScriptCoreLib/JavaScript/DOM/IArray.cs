using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;


namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, InternalConstructor=true)]
    public class IArray<TItem>
    {
        [Script(DefineAsStatic = true)]
        public TItem Find(EventHandler<Expando.FindArgs<TItem>> e) 
        {
            Expando.FindArgs<TItem> m = FindMember(e);
            
            if (m == null)
                return default(TItem);

            return m.Item;
        }

        [Script(DefineAsStatic = true)]
        public Expando.FindArgs<TItem> FindMember(EventHandler<Expando.FindArgs<TItem>> e)
        {
            return Expando.Of(this).Find(e);
        }


        [Script]
        public class IncludeArgs
        {
            public bool Include = false;

            public TItem Item;
        }

        public IArray()
        {

        }



        public IArray(TItem[] source, EventHandler<IncludeArgs> predicate)
        {

        }

        [Script(OptimizedCode=@"return [];")]
        static IArray<TItem> InternalConstructor()
        {
            return default(IArray<TItem>);
        }

        static IArray<TItem> InternalConstructor(TItem[] source, EventHandler<IncludeArgs> predicate)
        {
            IArray<TItem> n = new IArray<TItem>();

            foreach (TItem z in source)
            {
                IncludeArgs x = new IncludeArgs();

                x.Item = z;

                predicate(x);

                if (x.Include)
                    n += z;
            }

            return n;
        }

        public static IArray<TItem> operator + (IArray<TItem> e, TItem v)
        {
            e.push(v);

            return e;
        }

        [Script(DefineAsStatic = true)]
        public void ForEach(EventHandler<TItem> handler)
        {
            foreach (TItem x in ToArray())
            {
                handler(x);
            }
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
        public void sort(InternalFunc<TItem, TItem, int> e)
        {
            sort(((BCLImplementation.System.__Delegate)(object)e).InvokePointer);
        }

        public bool IsArray
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando.Of(this).IsArray;
            }
        }

        /// <summary>
        /// autodetects newline string
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public static IArray<TItem> SplitLines(TItem p)
        {
            IArray<TItem> win = Split(p, "\r\n");
            IArray<TItem> unix = Split(p, "\n");

            
            return win.length >= unix.length ? win : unix;
        }


    }
}
