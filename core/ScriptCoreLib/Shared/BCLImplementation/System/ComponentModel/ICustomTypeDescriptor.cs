using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	// https://msdn.microsoft.com/en-us/library/system.componentmodel.icustomtypedescriptor(v=vs.110).aspx

	[Script(Implements = typeof(global::System.ComponentModel.ICustomTypeDescriptor))]
	public interface __ICustomTypeDescriptor
	{
		// https://msdn.microsoft.com/en-us/library/system.componentmodel.typedescriptionprovider.aspx
	}
}
