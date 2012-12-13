using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.media;

namespace FlashSpaceInvaders.ActionScript.FragileEntities
{
	[Script]
	public interface IDeathSound
	{
		Sound GetDeathSound();
	}
}
