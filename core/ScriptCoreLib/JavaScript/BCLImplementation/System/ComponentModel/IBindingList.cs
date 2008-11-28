using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.IBindingList))]
	internal interface __IBindingList 
	{
		event ListChangedEventHandler ListChanged;
	}
}
