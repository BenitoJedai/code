using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.RayCaster.Extensions;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    partial class ViewEngineBase
    {
        /// <summary>
        /// Renders a solid color ceiling and floor
        /// </summary>
        public void RenderHorizon()
        {
            buffer.fillRect(
                new Rectangle(0, 0, _ViewWidth, _ViewHeight / 2), 0xa0a0a0
                );

            buffer.fillRect(
                            new Rectangle(0, _ViewHeight / 2, _ViewWidth, _ViewHeight / 2), 0x808080
                            );
        }
    }
}
