using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace BrowserAvalonExample.Code
{
	/// <summary>
	/// Without this class some assemblies are not referenced as they only contain
	/// type mappings but no real type usage.
	/// </summary>
	public interface IAssemblyReferenceToken :
		ScriptCoreLib.Shared.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken,
		BrowserAvalonExample.Shared.Assets.IAssemblyReferenceToken
	{
	}

	public interface IAssemblyReferenceTokenNative :
		BrowserAvalonExample.Shared.Assets.IAssemblyReferenceTokenNative
	{
	}
}
