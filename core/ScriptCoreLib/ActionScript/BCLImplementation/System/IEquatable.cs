using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
	[Script(Implements = typeof(IComparable))]
	internal interface IEquatable<T>
	{
		bool Equals(T other);
	}
}
