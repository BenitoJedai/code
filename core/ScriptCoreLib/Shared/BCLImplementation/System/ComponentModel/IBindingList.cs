using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IBindingList))]
	internal interface __IBindingList : __ICollection, __IList, __IEnumerable
	{
		event ListChangedEventHandler ListChanged;
	}
}
