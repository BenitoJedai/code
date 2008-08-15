using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace FlashResources.AvalonTest
{
	/// <summary>
	/// Without this class some assemblies are not referenced as they only contain
	/// type mappings but no real type usage.
	/// </summary>
	public interface IAssemblyReferenceTokenNative :
		FlashResources.Shared.AvalonCode.IAssemblyReferenceTokenNative
	{
	}
}
