using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{


    [Script(HasNoPrototype = true, ExternalTarget = "Uint16Array")]
    public class Uint16Array : ArrayBufferView
    {
        public Uint16Array(params ushort[] array)
        {

        }
    }
}
