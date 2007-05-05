using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.JavaScript.DOM;

using System.Collections.Generic;


namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Random))]
    internal class Random
    {
        public Random()
        {

        }

        public virtual int Next()
        {
            return Native.Math.round(NextDouble() * 0xFFFFFFFF);
        }

        public virtual int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ScriptException("ArgumentOutOfRange_MustBePositive");
            }

            return Native.Math.round(this.NextDouble() * ((double)maxValue));
        }


        public virtual int Next(int minValue, int maxValue)
        {
            if (minValue <= maxValue)
            {
                throw new ScriptException("Argument_MinMaxValue");
            }

            return Next(maxValue - minValue) + minValue;
        }



        public virtual double NextDouble()
        {
            return Native.Math.random();
        }
    }


    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class Debugger
    {
        public static void Break()
        {
            Native.DebugBreak();
        }
    }

    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class List<T> :  IEnumerable<T>
    {
        IArray<T> _items = new IArray<T>();

        public void Add(T item)
        {
            _items.push(item);
        }

        public int Count
        {
            get
            {
                return _items.length;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new ScriptException("ArgumentOutOfRangeException");
            }

            _items.splice(index, 1);
        }

        public List()
        {

        }

        public List(IEnumerable<T> collection)
        {
            foreach (T v in InternalSequenceImplementation.AsEnumerable(collection))
            {
                this.Add(v);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new ScriptException("ArgumentOutOfRangeException");
                }
                return this._items[index];
            }
            set
            {
                if (index >= this.Count)
                {
                    throw new ScriptException("ArgumentOutOfRangeException");
                }
                this._items[index] = value;
            }
        }


        public T[] ToArray()
        {
            return _items.ToArray();
        }



        public IEnumerator<T> GetEnumerator()
        {
            return InternalSequenceImplementation.AsEnumerable(ToArray()).GetEnumerator();
        }




        #region IEnumerable Members


        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }

    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    internal class ArrayList
    {
        readonly IArray<object> items = new IArray<object>();

        public void Add(object e)
        {
            items.push(e);
        }
    }

    [Script(Implements = typeof(global::System.WeakReference))]
    internal class WeakReference
    {
        public WeakReference(object e)
        {
            // weak reference not supported
        }
    }

    [Script(Implements = typeof(global::System.Threading.Monitor))]
    internal class Monitor
    {
        public static void Enter(object e)
        {
        }

        public static void Exit(object obj)
        {
        }
    }
}