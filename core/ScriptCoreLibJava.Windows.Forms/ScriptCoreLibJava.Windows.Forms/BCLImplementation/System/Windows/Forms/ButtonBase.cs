using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.ButtonBase))]
	internal class __ButtonBase : __Control
	{
		public virtual void InternalSetText(string e)
		{
			throw new NotImplementedException();
		}

        // tested by
        // X:\jsc.svn\examples\java\forms\AppletAsyncWhenReady\AppletAsyncWhenReady\ApplicationControl.Designer.cs
        public bool UseVisualStyleBackColor { get; set; }

		public override string Text
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetText(value);
			}
		}
	}
}
