using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.ActionScript.Components;

namespace PromotionWebApplication1
{
	public sealed class SaveActionSprite : SaveAction, ISaveActionWhenReady
	{
		public const int DefaultWidth = 24 - 2;
		public const int DefaultHeight = 24 - 2;


		public void WhenReady(Action<ISaveAction> y)
		{
			y(this);
		}
	}
}
