using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_4")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_4/Documents")
]

[assembly: ScriptCoreLib.Script(IsScriptLibrary = true)]
[assembly: ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.JavaScript)]

namespace PromotionWebApplicationAssets
{
    public class Class1
    {
    }
}
