using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: ScriptResources(ConsoleWorm.js.Assets.Path)]

namespace ConsoleWorm.js
{
    [Script]
    static class Assets
    {
        public const string Path = "assets/ConsoleWorm";
    }
}
