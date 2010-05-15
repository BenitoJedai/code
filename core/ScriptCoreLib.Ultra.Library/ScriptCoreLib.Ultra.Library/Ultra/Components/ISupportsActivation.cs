using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Components
{
	public interface ISupportsActivation
	{
		event Action Activated;
		event Action Deactivated;
	}
}
