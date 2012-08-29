using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(HasNoPrototype = true, ExternalTarget = "Float32Array")]
    public class Float32Array : ArrayBufferView
    {
        public Float32Array(params float[] array)
        {

        }
    }
}
