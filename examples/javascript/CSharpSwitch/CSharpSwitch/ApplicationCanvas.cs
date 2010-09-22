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

namespace CSharpSwitch
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        static void SetColor (int i, Rectangle r)
          {
              switch (i)
              {
                  case 1:
                      r.Fill = Brushes.Red;
                      break;
                  case 2:
                      r.Fill = Brushes.Blue;
                      break;
                  default:
                      r.Fill = Brushes.Yellow;
                      break;
              }
          }

        public ApplicationCanvas()
        {

            var c = 0;

            Action Update = () => SetColor(c, r);

            Update();

            r.MouseLeftButtonUp +=
                delegate
                {
                    c++;
                    Update();
                };
            
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            
        }

    }
}
