using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace ExampleGallery
{
    public class OptionWithShadowAndType : OptionWithShadow
    {
        public event Action TargetInitialized;

        public OptionWithShadowAndType(Func<Image> i, Type t, Func<string> Text)
            : base(i)
        {
            this.Initialize +=
                delegate
                {
                    this.Target = (Canvas)Activator.CreateInstance(t);

                    if (TargetInitialized != null)
                        TargetInitialized();
                };

        
            this.Caption.Text = Text();
        }
    }

}
