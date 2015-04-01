using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.ISupportInitialize))]
	internal interface __ISupportInitialize
	{
		// tested by ?

        void BeginInit();
        void EndInit();
	}
}
