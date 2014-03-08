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

namespace AIRAvalonPasswordField
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);


            var p = new PasswordBox { Password = "auto size me" };

            p.AttachTo(this);

            //NotImplementedException
            //    at ScriptCoreLib.ActionScript.BCLImplementation.System.Windows::__FrameworkElement/InternalSetWidth_79f8bffe_06000042()[V:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\Windows\__FrameworkElement.as:48]
            //    at ScriptCoreLib.ActionScript.BCLImplementation.System.Windows::__FrameworkElement/set Width()[V:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\Windows\__FrameworkElement.as:63]
            //    at AIRAvalonPasswordField::ApplicationCanvas()[V:\web\AIRAvalonPasswordField\ApplicationCanvas.as:57]
            //    at AIRAvalonPasswordField::ApplicationSprite()[V:\web\AIRAvalonPasswordField\ApplicationSprite.as:34]


            p.Width = 400;


            //Implementation not found for type import :
            //type: System.Windows.Media.ScaleTransform
            //method: Void .ctor(Double, Double)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            var aspect = 1.0;

            this.SizeChanged +=
                delegate
                {
                    aspect = this.ActualWidth / 400.0;

                    this.RenderTransform = new ScaleTransform { ScaleX = aspect, ScaleY = aspect };
                };

            this.RenderTransform = new ScaleTransform { ScaleX = 2.0, ScaleY = 2.0 };

        }

    }
}
