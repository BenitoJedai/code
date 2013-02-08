using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using System;
using System.IO;

namespace ReadFloat32
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
        }

        public void Invoke(Action<string> yield)
        {
            // 203f4a3d71
            // 20713d4a3f
            {
                var m = new ByteArray();

                // !
                m.endian = Endian.LITTLE_ENDIAN;

                m.writeByte(' ');

                //m.endian
                m.writeFloat(0.79);

                var bytes = m.ToMemoryStream().ToBytes();

                var base64 = Convert.ToBase64String(bytes);

                yield(base64);
            }

            // 20713d4a3f
            {
                var m = new MemoryStream();

                m.WriteByte((byte)' ');

                //                Implementation not found for type import :
                //type: System.IO.BinaryWriter
                //method: Void Write(Single)

                var w = new BinaryWriter(m);

                w.Write((float)0.79);

                var bytes = m.ToBytes();

                var base64 = Convert.ToBase64String(bytes);

                yield(base64);
            }
        }
    }
}
