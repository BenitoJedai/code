using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Components;

namespace TestSolutionBuilderWithViewer.Flash
{
	internal sealed class SaveActionSprite : SaveAction, ISaveActionWhenReady
	{
		// this sprite is internal currently because non-internal ultra applications
		// cannot use it.
		// when simplifier is implemented this problem fades away.

		public const int DefaultWidth = 22;
		public const int DefaultHeight = 22;

		public void WhenReady(Action<ISaveAction> y)
		{
			y(this.Implementation);
		}
	}
}
