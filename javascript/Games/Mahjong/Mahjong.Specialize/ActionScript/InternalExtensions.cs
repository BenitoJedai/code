using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.Specialize.ActionScript
{
	[Script]
	internal static class InternalExtensions
	{
		public static DisplayObject GetStageChild(this DisplayObject e)
		{
			DisplayObject r = null;
			DisplayObject p = e;

			while (r == null)
			{
				if (p.parent == p.stage)
				{
					r = p;
				}
				else
				{
					p = p.parent;
				}
			}

			return r;
		}

		public static IEnumerable<DisplayObject> Siblings(this DisplayObject e)
		{
			return e.parent.Children().Where(k => k != e);
		}


	}
}
