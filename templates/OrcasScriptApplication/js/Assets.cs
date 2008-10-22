using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(OrcasScriptApplication.js.Assets.Path)]

namespace OrcasScriptApplication.js
{
    [Script]
    internal static class Assets
    {
        public const string Path = "assets/OrcasScriptApplication";
    }
}
