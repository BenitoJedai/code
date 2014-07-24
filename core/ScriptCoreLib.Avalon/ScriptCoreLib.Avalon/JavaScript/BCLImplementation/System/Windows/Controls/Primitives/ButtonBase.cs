using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives
{
    // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/Primitives/ButtonBase.cs

    [Script(Implements = typeof(global::System.Windows.Controls.Primitives.ButtonBase))]
    internal abstract class __ButtonBase : __ContentControl
    {
        // X:\jsc.svn\examples\javascript\Avalon\Test\TestShadowTextBox\TestShadowTextBox\ApplicationCanvas.cs
        public event RoutedEventHandler Click;

        public void InternalRaiseClick()
        {
            if (Click != null)
                Click(this, new RoutedEventArgs());
        }
    }
}
