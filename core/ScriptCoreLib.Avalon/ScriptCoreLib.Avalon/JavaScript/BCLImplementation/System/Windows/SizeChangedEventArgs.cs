using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.SizeChangedEventArgs))]
    internal class __SizeChangedEventArgs : __RoutedEventArgs
    {
        public bool HeightChanged { get; internal set; }
        public Size NewSize { get; internal set; }
        public Size PreviousSize { get; internal set; }
        public bool WidthChanged { get; internal set; }
    }
}
