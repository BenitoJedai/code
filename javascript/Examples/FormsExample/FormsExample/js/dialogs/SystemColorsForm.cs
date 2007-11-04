using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib;

namespace FormsExample.js.dialogs
{
    [Script]
    public partial class SystemColorsForm : Form
    {
        public SystemColorsForm()
        {
            InitializeComponent();

            int i = 0;

            Action<Color, string> Next =
                (c, n) =>
                {
                    var x = new ColorDemo { ColorPanel = c, ColorName = n };

                    x.Top = i;

                    i += x.Height;

                    this.Controls.Add(
                        x
                    );
                };

            Next(SystemColors.ActiveBorder, "ActiveBorder");
            Next(SystemColors.ActiveCaption, "ActiveCaption");
            Next(SystemColors.ActiveCaptionText, "ActiveCaptionText");
            Next(SystemColors.AppWorkspace, "AppWorkspace");
            Next(SystemColors.ButtonFace, "ButtonFace");
            Next(SystemColors.ButtonHighlight, "ButtonHighlight");
            Next(SystemColors.ButtonShadow, "ButtonShadow");
            Next(SystemColors.Control , "Control");
            Next(SystemColors.ControlDark, "ControlDark");
            Next(SystemColors.ControlDarkDark, "ControlDarkDark");
            Next(SystemColors.ControlLight, "ControlLight");
            Next(SystemColors.ControlLightLight, "ControlLightLight");
            Next(SystemColors.ControlText, "ControlText");
            Next(SystemColors.Desktop, "Desktop");
            //Next(SystemColors.GradientActiveCaption = Get( MySystemColors.GradientActiveCaption );
            //Next(SystemColors.GradientInactiveCaption = Get( MySystemColors.GradientInactiveCaption );
            Next(SystemColors.GrayText, "GrayText");
            Next(SystemColors.Highlight, "Highlight");
            Next(SystemColors.HighlightText, "HighlightText");
            //Next(SystemColors.HotTrack = Get( MySystemColors.HotTrack );
            Next(SystemColors.InactiveBorder, "InactiveBorder");
            Next(SystemColors.InactiveCaption, "InactiveCaption");
            Next(SystemColors.InactiveCaptionText, "InactiveCaptionText");
            Next(SystemColors.Info, "Info");
            Next(SystemColors.InfoText, "InfoText");
            Next(SystemColors.Menu , "Menu");
            //Next(SystemColors.MenuBar = Get( MySystemColors.MenuBar );
            //Next(SystemColors.MenuHighlight = Get( MySystemColors.MenuHighlight );
            Next(SystemColors.MenuText, "MenuText");
            Next(SystemColors.ScrollBar, "ScrollBar");
            Next(SystemColors.Window, "Window");
            Next(SystemColors.WindowFrame, "WindowFrame");
            Next(SystemColors.WindowText, "WindowText");

            this.Height = i + 30;
        }
    }
}
