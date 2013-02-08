extern alias assets;

using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.BinaryReader))]
    internal class __BinaryReader
    {
        public virtual float ReadSingle()
        {
            var r = (BinaryReader)(object)this;
            var bytes = r.ReadBytes(4);

            var _bytes = new assets::ScriptCoreLib.JavaScript.WebGL.Uint8Array(bytes);
            var _floats = new assets::ScriptCoreLib.JavaScript.WebGL.Float32Array(_bytes.buffer, 0, 1);

            // broken?
            //var f = _floats[0];
            var f = ((float[])(object)_floats)[0];

            return f;
        }
    }
}
