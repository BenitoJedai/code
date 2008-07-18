using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

////[assembly: ScriptResources(Graphics_beginBitmapFillExample.ActionScript.Assets.Path)]

namespace Graphics_beginBitmapFillExample.ActionScript
{
    /// <summary>
    /// This class should define all links and references to 
    /// external and embeded assets.
    /// </summary>
    [Script]
    internal static class Assets
    {
        /// <summary>
        /// The files from solution folder 'web/assets/Graphics_beginBitmapFillExample' 
        /// that are marked as 'Embedded Resource' will be extracted by jsc
        /// to this path. The value only reflects the real folder.
        /// </summary>
        public const string Path = "assets/Graphics_beginBitmapFillExample";
    }
}
