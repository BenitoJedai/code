using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: ScriptResources(ImageReflection.js.Assets.Path)]

namespace ImageReflection.js
{
    [Script]
    internal static class Assets
    {
        public const string Path = "assets/ImageReflection";
    }
}
