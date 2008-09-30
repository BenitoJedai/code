﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IContainer))]
	internal interface __IContainer
	{
		ComponentCollection Components { get; }

		void Add(IComponent component);
	
		void Add(IComponent component, string name);

		void Remove(IComponent component);
	}
}
