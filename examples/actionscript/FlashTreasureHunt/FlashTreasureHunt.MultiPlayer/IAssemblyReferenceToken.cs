﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashTreasureHunt
{
	public interface IAssemblyReferenceToken :
		ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Nonoba.IAssemblyReferenceToken
	{
	}
}
