using ScriptCoreLib;

using System.Linq;
using System;

namespace RayCaster6.ActionScript
{
    using T = UInt32;
    using ScriptCoreLib.ActionScript.flash.display;

    [Script]
    public sealed class Texture64 : TextureBase
    {
        readonly T[] items;

        public int Length
        {
            get { return items.Length; }
        }

        public const int SizeConstant = 64;

        public override int Size
        {
            get { return SizeConstant; }
        }

        public Texture64(params T[] value)
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


        public static implicit operator Texture64(Bitmap bd)
        {
            var t = new Texture64();


            var bdata = bd.bitmapData;

            if (bdata.width == 64)
                for (var j = 0; j < 64; j++)
                    for (var k = 0; k < 64; k++)
                    {
                        t[j, k] = bdata.getPixel(j, k);
                    }
            else if (bdata.width == 256)
                for (var j = 0; j < 64; j++)
                    for (var k = 0; k < 64; k++)
                    {
                        var j4 = j * 4;
                        var k4 = k * 4;


                        var c = bdata.getPixel(j4, k4); ;

                        t[j, k] = bdata.getPixel(j, k);
                    }

            return t;
        }

    }


}
