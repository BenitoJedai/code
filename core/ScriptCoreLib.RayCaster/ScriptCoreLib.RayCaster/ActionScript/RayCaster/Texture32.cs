using ScriptCoreLib;

using System.Linq;
using System;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    using T = UInt32;
    using ScriptCoreLib.ActionScript.flash.display;

    
    [Script]
    public sealed class Texture32 : TextureBase
    {
        readonly T[] items;

        public int Length
        {
            get { return items.Length; }
        }

        public const int SizeConstant = 32;

        public override int Size
        {
            get { return SizeConstant; }
        }

        public Texture32(params T[] value)
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


        public static implicit operator Texture32(Bitmap bd)
        {
            return Texture32.Of(bd, bd.bitmapData.transparent);
        }

        bool alpha;

        public static Texture32 Of(Bitmap bd, bool alpha)
        {
            var t = new Texture32
            {
                Bitmap = bd,
            };

            t.alpha = alpha;
            t.Update();

            return t;
        }

        public override void Update()
        {
            var t = this;
            var bdata = Bitmap.bitmapData;

            if (bdata.width == SizeConstant)
                for (var j = 0; j < SizeConstant; j++)
                    for (var k = 0; k < SizeConstant; k++)
                    {
                        if (alpha)
                            t[j, k] = bdata.getPixel32(j, k);
                        else
                            t[j, k] = bdata.getPixel(j, k) & 0x00ffffff;
                    }
            else
                throw new Exception("This texture must be a size of " + SizeConstant);
        }

    }


}
