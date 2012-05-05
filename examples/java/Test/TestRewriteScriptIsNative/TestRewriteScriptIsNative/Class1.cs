using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace TestRewriteScriptIsNative
{
    [Script(IsNative = true)]
    public static class R
    {
        [Script(IsNative = true)]
        public static class drawable
        {
            public static int stone_wall_public_domain;
            public static int noisy_grass_public_domain;
        }
    }
}
