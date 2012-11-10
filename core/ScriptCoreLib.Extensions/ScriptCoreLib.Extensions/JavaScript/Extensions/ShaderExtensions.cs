using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class ShaderExtensions
    {
        /// <summary>
        /// To be used with CSS filter shaders
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToDataUrl(this ScriptCoreLib.GLSL.Shader s)
        {
            // tested by X:\jsc.svn\examples\javascript\CSSShaderGrayScale\CSSShaderGrayScale\Application.cs
            var url = "data:x-shader/x-vertex;base64," + Convert.ToBase64String(Encoding.ASCII.GetBytes(s.ToString()));
            return url;
        }
    }
}
