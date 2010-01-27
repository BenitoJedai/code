using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Commands
{
	public abstract class CommandBase
	{
		// Any field in this class will act as a commandline parameter

		public abstract void Invoke();


		public static implicit operator Action(CommandBase e)
		{
			return e.Invoke;
		}


	}
}
