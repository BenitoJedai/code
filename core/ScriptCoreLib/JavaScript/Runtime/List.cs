using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Runtime
{


    [Script]
    public class List<TItem> where TItem : class
    {
        public IArray<TItem> ListArray = new IArray<TItem>();

        public event EventHandler<TItem> ItemAdded;
        public event EventHandler<TItem> ItemRemoved;
        public event EventHandler ItemsCleared;

        public TItem Last
        {
            get
            {
                if (Count > 0)
                    return this[Count - 1];

                return null;
            }
        }

        public TItem First
        {
            get
            {
                if (Count > 0)
                    return this[0];

                return null;
            }
        }

        public int Count
        {
            get
            {
                return ListArray.length;
            }
        }

        public void Add(TItem e)
        {
            ListArray.push(e);

            if (ItemAdded != null) ItemAdded(e);
        }

        public void Add(params TItem[] e)
        {
            foreach (TItem x in e) Add(x);
        }

        public TItem[] ToArray()
        {
            return ListArray.ToArray();
        }


        public TOut[] ToArray<TOut>(EventHandler<Predicate<TItem, TOut>> h)
        {
            IArray<TOut> a = new IArray<TOut>();
            Predicate<TItem, TOut> p = new Predicate<TItem, TOut>();

            foreach (TItem v in ListArray.ToArray())
            {
                p.TargetIn = v;
                p.Value = true;

                Helper.Invoke(h, p);

                if (p.Value)
                    a.push(p.TargetOut);
            }

            return a.ToArray();
        }

        public static implicit operator TItem[](List<TItem> m)
        {
            return m.ToArray();
        }

        /// <summary>
        /// if predicate value is set to true, item will be removed
        /// </summary>
        /// <param name="h"></param>
        public void RemoveBy(EventHandler<Predicate<TItem>> h)
        {
            IArray<TItem> n = new IArray<TItem>();
            Predicate<TItem> f = new Predicate<TItem>();

            foreach (TItem v in ListArray.ToArray())
            {
                f.Target = v;
                f.Value = false;


                Helper.Invoke(h, f);

                if (!f.Value)
                {
                    n.push(v);
                }
            }

            this.ListArray = n;
        }

        public void RemoveAll()
        {
            while (this.Count > 0)
                RemoveLast();
        }

        public void Remove(params TItem[] items)
        {
            Helper.ForEach(items, Remove);
        }

        public void Remove(TItem item)
        {
            Expando.FindArgs<TItem> z = ListArray.FindMember(
                delegate(Expando.FindArgs<TItem> x)
                {
                    x.Found = (x.Item == item);
                }
            );

            if (z != null)
            {
                ListArray.splice(z.Member.Index, 1);
            }

            Helper.Invoke(ItemRemoved, item);

        }

        public void Clear()
        {
            ListArray.splice(0, Count);

            if (ItemsCleared != null)
                ItemsCleared();
        }

        public TItem this[int i]
        {
            get
            {
                return this.ListArray[i];
            }
        }

        public List<TItem> this[int offset, int length]
        {
            get
            {
                var a = new List<TItem>();

                for (int i = 0; i < length; i++)
                {
                    var z = offset + i;

                    if (ContainsIndex(z))
                        a.Add(this[z]);
                }

                return a;
            }
        }

        public bool ContainsIndex(int i)
        {
            if (i < 0)
                return false;

            if (i < Count)
                return true;

            return false;
        }

        public TItem Find(EventHandler<Predicate<TItem>> h)
        {
            Predicate<TItem> p = new Predicate<TItem>();


            for (int i = 0; i < this.Count; i++)
            {
                p.Target = this[i];
                p.Value = false;

                Helper.Invoke(h, p);

                if (p.Value)
                    return p.Target;

            }

            return null;
        }



        public int IndexOf(TItem e)
        {
            var j = -1;

            for (int i = 0; i < Count; i++)
            {
                if (this[i] == e)
                {
                    j = i;
                    break;
                }
            }

            return j;
        }



        public bool Contains(TItem e)
        {
            return IndexOf(e) > -1;
        }

        public bool All(EventHandler<Predicate<TItem>> h)
        {
            var r = new Predicate<TItem>();

            r.Value = true;

            foreach (TItem v in ToArray())
            {
                r.Invoke(h);

                if (!r.Value)
                    break;
            }

            return r;
        }

        public void RemoveFirst()
        {
            Remove(First);
        }

        public void RemoveLast()
        {
            Remove(Last);

        }

        public TItem Shift()
        {
            var i = First;

            RemoveFirst();

            return i;
        }

        public TItem Pop()
        {
            var i = Last;

            RemoveLast();

            return i;
        }

        public void ForEachReversed(EventHandler<TItem> h)
        {
            int i = Count;

            while (i > 0)
            {
                i--;

                Helper.Invoke(h, this[i]);
            }
        }

        public void ForEach(EventHandler<TItem> h)
        {
            Helper.ForEach(ToArray(), h);
        }

        public TItem[] PopAll()
        {
            var a = ToArray();

            RemoveAll();

            return a;
        }
    }
}
