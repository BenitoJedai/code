using ScriptCoreLib;

using System.Linq;
using System;

namespace RayCaster6.ActionScript
{
    using T = SByte;

    [Script]
    public sealed class Array2DSByte
    {
        readonly T[] items;

        readonly int _XLength;
        readonly int _YLength;

        public int Length
        {
            get { return items.Length; }
        }

        public int XLength
        {
            get { return _XLength; }
        }

        public int YLength
        {
            get { return _YLength; }
        }


        //public Array2DSByte(int x, int y)
        //{
 
        //}

        public Array2DSByte(int x, int y, params T[] value)
            //: this(x, y)
        {
            this._XLength = x;
            this._YLength = y;

            this.items = new T[x * y];

            for (int i = 0; i < value.Length; i++)
            {
                if (i < this.items.Length)
                {
                    this.items[i] = value[i];
                }
                else
                    break;
            }
            
        }

        public readonly T EmptyValue = default(T);

        public T this[int x, int y]
        {
            get
            {
                //if (x < 0) return EmptyValue;
                //if (y < 0) return EmptyValue;
                //if (x >= this._XLength) return EmptyValue;
                //if (y >= this._YLength) return EmptyValue;

                return this.items[this._XLength * y + x];
            }
            set
            {
                if (x < 0) return;
                if (y < 0) return;
                if (x >= this._XLength) return;
                if (y >= this._YLength) return;

                this.items[this._XLength * y + x] = value;
            }
        }

        //public T this[PointInt32 p]
        //{
        //    get
        //    {
        //        return this[p.X, p.Y];
        //    }
        //    set
        //    {
        //        this[p.X, p.Y] = value;
        //    }
        //}

        
        
    }


}
