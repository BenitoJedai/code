using ScriptCoreLib;

using System.Linq;
using System;

namespace RayCaster6.ActionScript
{
    using T = UInt32;

    [Script]
    public sealed class Texture256
    {
        readonly T[] items;
 
        public int Length
        {
            get { return items.Length; }
        }

        public Texture256(params T[] value)
            //: this(x, y)
        {

            this.items = new T[256 * 256];

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


        public T this[int x, int y]
        {
            get
            {
                return this.items[x + (y * 256)];
            }
            set
            {
        
                this.items[256 * y + x] = value;
            }
        }

 
        
        
    }


}
