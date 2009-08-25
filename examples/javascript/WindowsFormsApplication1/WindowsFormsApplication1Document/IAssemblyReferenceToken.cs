using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace WindowsFormsApplication1Document
{
	interface IAssemblyReferenceToken :
		ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.IAssemblyReferenceToken
	{

	}

}
