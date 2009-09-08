using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Integration
{
	public interface IAssemblyReferenceToken :
		
		ScriptCoreLib.Shared.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken
	{
	}
}
