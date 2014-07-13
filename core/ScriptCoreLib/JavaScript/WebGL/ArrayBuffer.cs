﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(HasNoPrototype = true, ExternalTarget = "ArrayBuffer")]
    public class ArrayBuffer
    {
        public long byteLength;





        public static implicit operator byte[](ArrayBuffer data)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Uint8ClampedArray.cs

            return new Uint8ClampedArray(data);
        }
    }
}
