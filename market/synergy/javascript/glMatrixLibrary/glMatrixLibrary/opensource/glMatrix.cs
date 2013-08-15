using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// should jsc allow statics already?
public class glMatrix
{
    [Script(ExternalTarget = "(mat4)")]
    public static glMatrixLibrary.mat4 mat4;

    [Script(ExternalTarget = "(mat3)")]
    public static glMatrixLibrary.mat3 mat3;

    [Script(ExternalTarget = "(vec3)")]
    public static glMatrixLibrary.vec3 vec3;
}
