using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{


    [Script(HasNoPrototype = true, ExternalTarget = "Uint8ClampedArray")]
    public class Uint8ClampedArray : ArrayBufferView
    {
        public Uint8ClampedArray(params byte[] array)
        {

        }

        public byte this[uint i]
        {
            get { return 0; }
            set { }
        }
    }
}
