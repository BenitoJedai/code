using ScriptCoreLib;

using System.Linq;
using System;

namespace RayCaster6.ActionScript
{
    using T = UInt32;

    [Script]
    public sealed class Texture256 : TextureBase
    {
        readonly T[] items;
 
        public int Length
        {
            get { return items.Length; }
        }

        const int SizeConstant = 256;

        public override int Size
        {
            get { return SizeConstant; }
        }
        
        

        public Texture256(params T[] value)
            //: this(x, y)
        {

            this.items = new T[SizeConstant * SizeConstant];

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


        public override T this[int x, int y]
        {
            get
            {
                return this.items[x + (y * SizeConstant)];
            }
            set
            {

                this.items[SizeConstant * y + x] = value;
            }
        }

 
        
        
    }


}
