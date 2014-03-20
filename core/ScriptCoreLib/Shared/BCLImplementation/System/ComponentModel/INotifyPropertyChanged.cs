using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.INotifyPropertyChanged))]
	public interface __INotifyPropertyChanged
	{
        event PropertyChangedEventHandler PropertyChanged;
	}
}
