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

namespace AndroidFlashTreasureHunt.IsometricView
{
    public class ApplicationCanvas : InteractiveTransformA.ApplicationCanvas
    {
        public ApplicationCanvas()
        {

        }

        public override bool SkipNonEssentials
        {
            get
            {
                return true;
            }
        }
    }
}
