using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.Cursor))]
	internal class __Cursor : IDisposable
	{
		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion

		public static implicit operator Cursor(__Cursor e)
		{
			return (Cursor)(object)e;
		}
	}
}
