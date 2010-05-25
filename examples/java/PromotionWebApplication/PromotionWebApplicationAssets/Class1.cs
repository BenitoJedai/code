using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_4")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_4/Documents")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_5")
]

[assembly: global::ScriptCoreLib.Shared.ScriptResources(
    "assets/PromotionWebApplicationAssets/Application_Files/jsc.configuration_1_0_0_5/Documents")
]

[assembly: ScriptCoreLib.Script]
[assembly: ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.JavaScript)]

namespace PromotionWebApplicationAssets
{
    public class Assets
    {
    }
}
