using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.ComponentCollection))]
	internal class __ComponentCollection : __ReadOnlyCollectionBase
    {
		public readonly ArrayList InternalElements = new ArrayList();
	}
}
