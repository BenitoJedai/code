﻿using ScriptCoreLib;

using System.Linq;
using System;

namespace RayCaster1.source.java
{
    using T = Int32;

    [Script]
    public class Array2DInt32
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


        public Array2DInt32(int x, int y)
        {
            this._XLength = x;
            this._YLength = y;

            this.items = new T[x * y];
        }

        public Array2DInt32(int x, int y, params T[] value) : this(x, y)
        {
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
                if (x < 0) return EmptyValue;
                if (y < 0) return EmptyValue;
                if (x >= this._XLength) return EmptyValue;
                if (y >= this._YLength) return EmptyValue;

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


    }


}
