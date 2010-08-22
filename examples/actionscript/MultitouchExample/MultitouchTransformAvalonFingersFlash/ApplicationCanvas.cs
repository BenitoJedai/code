// For more information visit:
// http://studio.jsc-solutions.net/


using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Collections.Generic;
using MultitouchTransform.Library;

namespace MultitouchTransformAvalonFingersFlash
{
    public class ApplicationCanvas : MultitouchTransform.ApplicationCanvas
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public ApplicationCanvas()
        {


            // http://connect.microsoft.com/VisualStudio/feedback/details/527886/touchdown-does-not-trigger-until-the-touch-moves

            var t = this.ToTouchEvents(this.Movables);

            t.TouchMove +=
                (m, e) =>
                {
                    var tp = e.GetTouchPoint(this);
                    var p = tp.Position;

                    m.MoveTo(p.X, p.Y);

                   
                };

        }
    }
}
