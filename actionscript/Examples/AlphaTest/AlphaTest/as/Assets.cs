using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: ScriptResources(AlphaTest.js.Assets.Path)]

namespace AlphaTest.js
{
    [Script]
    internal static class Assets
    {
        public const string Path = "assets/AlphaTest";
    }
}
