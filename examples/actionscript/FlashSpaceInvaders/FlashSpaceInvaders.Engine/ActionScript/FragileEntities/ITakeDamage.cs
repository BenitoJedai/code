using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashSpaceInvaders.ActionScript.FragileEntities
{
	[Script]
	public interface ITakeDamage
	{
		void TakeDamage(double damage);

	}
}
