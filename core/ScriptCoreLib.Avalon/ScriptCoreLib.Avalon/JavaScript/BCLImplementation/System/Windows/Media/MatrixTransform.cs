using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.MatrixTransform))]
	internal class __MatrixTransform
	{
		public Matrix Matrix { get; set; }
	}
}
