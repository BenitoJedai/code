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

            x[-1] = default(Movable);
            var _ = x[-1];

            this.TouchDown +=
                 (sender, e) =>
                 {
                     var id = e.TouchDevice.Id;
                     Console.WriteLine("TouchDown: " + id);

                     {
                         var m = s.Pop();

                         if (m == null)
                             Console.WriteLine("m == null on pop");

                         x[id] = m;
                     }

                     {
                         var m = x[id];

                         if (m == null)
                         {
                             var keys = x.Keys.Aggregate("[", (q, k) => q + k + ", ");

                             WriteStatus(new { Missing = "TouchDown m", id });
                             return;
                         }
                     }
                 };

            this.TouchMove +=
                (sender, e) =>
                {
                    var id = e.TouchDevice.Id;

                    Console.WriteLine("TouchMove: " + id);

                    var tp = e.GetTouchPoint(this);
                    var p = tp.Position;

                    this.WriteStatus(
                        new
                        {
                            e.TouchDevice.Id,
                            p.X,
                            p.Y
                        }
                    );

                    if (!x.ContainsKey(id))
                    {
                        WriteStatus(new { Missing = id });
                        return;
                    }

                    var m = x[id];

                    if (m == null)
                    {
                        var keys = x.Keys.Aggregate("[", (q, k) => q + k + ", ");

                        WriteStatus(new { Missing = "m", id });
                        return;
                    }

                    m.MoveTo(p.X, p.Y);
                };

            this.TouchUp +=
                 (sender, e) =>
                 {
                     Console.WriteLine("TouchUp: " + e.TouchDevice.Id);

                     s.Push(x[e.TouchDevice.Id]);
                     x[e.TouchDevice.Id] = null;
                 };
        }
    }
}
