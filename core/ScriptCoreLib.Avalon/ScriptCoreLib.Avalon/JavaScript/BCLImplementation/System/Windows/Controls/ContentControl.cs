using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
    // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/ContentControl.cs

	[Script(Implements = typeof(global::System.Windows.Controls.ContentControl))]
	internal class __ContentControl : __Control
	{
        protected Action<object> InternalVirtualSetContent;
		protected virtual void InternalSetContent(object e)
		{

		}

		protected virtual object InternalGetContent()
		{
			return null;
		}

		public object Content
		{
			get
			{
				return InternalGetContent();
			}
			set
			{
				InternalSetContent(value);


                // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\BCLImplementation\System\Windows\Controls\Button.cs
                if (InternalVirtualSetContent != null)
                    InternalVirtualSetContent(value);
			}
		}
	}
}
