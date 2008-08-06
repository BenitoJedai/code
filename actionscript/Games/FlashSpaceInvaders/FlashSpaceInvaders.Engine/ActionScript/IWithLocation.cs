using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public interface IWithLocation
	{
		Point Location { get; }
	}
}
