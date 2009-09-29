using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Collections;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.ComponentCollection))]
	internal class __ComponentCollection : __ReadOnlyCollectionBase
	{
		public readonly ArrayList InternalElements = new ArrayList();

	}
}
