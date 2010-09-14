using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.Extensions
{
	// namespace clash
	public static class ActionScriptAvalonExtensions
	{
        public static T AutoSizeTo<T>(this T c, Stage s) where T : Panel
        {
            // resize without scale, thanks.

            s.align = StageAlign.TOP_LEFT;
            s.scaleMode = StageScaleMode.NO_SCALE;

            s.resize +=
                e =>
                {

                    c.SizeTo(s.stageWidth, s.stageHeight);
                };

            c.SizeTo(s.stageWidth, s.stageHeight);

            return c;
        }
	}
}
