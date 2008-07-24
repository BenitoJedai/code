using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    using T = UInt32;

    [Script]
    public abstract class TextureBase
    {
        protected T[] items;

        public int Length
        {
            get { return items.Length; }
        }


        [Script]
        public sealed class Entry
        {
            public int XIndex;
            public int YIndex;

            public T Value;
        }

        [Script]
        internal class Enumerator : IEnumerable<Entry>, IEnumerator<Entry>
        {
            private int __1__state;

            public TextureBase __3__target;

            public TextureBase target;

            private int __2__x;
            private Entry __2__current;

            public Enumerator(int __1__state)
            {
                this.__1__state = __1__state;
                return;
            }

            #region IEnumerable<Entry> Members

            public IEnumerator<Entry> GetEnumerator()
            {
                Enumerator _ret = null;

                if (this.__1__state == -2)
                {
                    this.__1__state = 0;
                    _ret = this;
                }
                else
                {
                    _ret = new Enumerator(0);
                }



                _ret.target = this.__3__target;

                return _ret;
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEnumerator<Entry> Members

            public Entry Current
            {
                get { return __2__current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                target = null;

            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this.__1__state == 0)
                {
                    this.__1__state = -1;
                    this.__2__x = 0;
                }
                else if (this.__1__state == 1)
                {
                    this.__1__state = -1;
                    this.__2__x++;
                }
                else
                {
                    return false;
                }

                if (this.__2__x < this.target.items.Length)
                {
                    var x = this.__2__x % this.target.Size;
                    var y = this.__2__x / this.target.Size;

                    this.__2__current = new Entry { XIndex = x, YIndex = y, Value = target[x, y] };
                    this.__1__state = 1;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public IEnumerable<Entry> Entries
        {
            get
            {
                return new Enumerator(-2) { __3__target = this };
            }
        }

        public abstract int Size
        {
            get;
        }

        public Bitmap Bitmap;

        public abstract void Update();

        public abstract T this[int x, int y] { get; set; }
    }
}
