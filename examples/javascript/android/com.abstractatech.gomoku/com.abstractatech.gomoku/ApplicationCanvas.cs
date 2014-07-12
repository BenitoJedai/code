using AvalonGomuko.Shared;
using com.abstractatech.gomoku.Avalon.Images;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace com.abstractatech.gomoku
{
    public class ApplicationCanvas : OrcasAvalonApplicationCanvas
    {
        Preview __Preview;

        //const int Intersections = 16;

        //public const int DefaultWidth = 32 * Intersections + 2;
        //public const int DefaultHeight = 32 * Intersections + 2;

        public ApplicationCanvas()
        {
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);


        }

    }
}
