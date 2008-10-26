using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Carousel
{
	/// <summary>
	/// Without this class some assemblies are not referenced as they only contain
	/// type mappings but no real type usage.
	/// </summary>
	public interface IAssemblyReferenceToken :
		ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.IAssemblyReferenceToken
	{
	}
}
