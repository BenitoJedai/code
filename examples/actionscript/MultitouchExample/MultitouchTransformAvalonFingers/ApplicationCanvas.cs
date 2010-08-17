// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using MultitouchTransformAvalonFingers;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Collections.Generic;
using MultitouchTransform.Library;

namespace MultitouchTransformAvalonFingers
{
    public class ApplicationCanvas : MultitouchTransform.ApplicationCanvas
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public ApplicationCanvas()
        {
            var s = new Stack<Movable>(this.Movables.Reverse());
            var x = new Dictionary<int, Movable>();

            this.TouchDown +=
                 (sender, e) =>
                 {
                     x[e.TouchDevice.Id] = s.Pop();
                 };

            this.TouchMove +=
                (sender, e) =>
                {
                    var tp = e.GetTouchPoint(this);

                    x[e.TouchDevice.Id].MoveTo(tp.Position.X, tp.Position.Y);
                };

            this.TouchUp +=
                 (sender, e) =>
                 {
                     s.Push(x[e.TouchDevice.Id]);
                     x[e.TouchDevice.Id] = null;
                 };
        }
    }
}
