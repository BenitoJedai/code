using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace TestNoDecoration
{
	/// <summary>
	/// Without this class some assemblies are not referenced. At this time we actually
	/// have to make sure that those libraries are ready to be loaded as jsc will look 
	/// for the implementing types.
	/// In the future releases of jsc this might not be neccesary
	/// </summary>
	public interface IAssemblyReferenceToken :
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.IAssemblyReferenceToken
	{
	}
}
