﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	[Script(Implements = typeof(IEquatable<>))]
	internal interface __IEquatable<T>
	{
		bool Equals(T other);
	}
}
