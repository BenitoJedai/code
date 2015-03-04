﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
	[Script(Implements = typeof(global::System.Security.Cryptography.SHA1))]
	internal abstract class __SHA1 : __HashAlgorithm
	{
		// https://github.com/dotnet/coreclr/blob/master/src/vm/sha1.h
		// https://github.com/dotnet/coreclr/blob/master/src/vm/sha1.cpp

	}
}
