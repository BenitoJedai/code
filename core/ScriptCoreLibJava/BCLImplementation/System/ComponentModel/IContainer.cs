using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.ComponentModel;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IContainer))]
	internal interface __IContainer : IDisposable
	{
		ComponentCollection Components { get; }

		void Add(IComponent component);
		void Add(IComponent component, string name);
		void Remove(IComponent component);
	}
}
