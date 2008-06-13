using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

////[assembly: ScriptResources(FlashNonobaExample.ActionScript.Assets.Path)]

namespace FlashNonobaExample.ActionScript
{
    /// <summary>
    /// This class should define all links and references to 
    /// external and embeded assets.
    /// </summary>
    [Script]
    internal static class Assets
    {
        /// <summary>
        /// The files from solution folder 'web/assets/FlashNonobaExample' 
        /// that are marked as 'Embedded Resource' will be extracted by jsc
        /// to this path. The value only reflects the real folder.
        /// </summary>
        public const string Path = "assets/FlashNonobaExample";
    }
}
