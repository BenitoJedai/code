using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Drawing;
using System.Threading;
using jsc.meta.Commands.Reference;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Windows.Input;

namespace jsc.meta.Commands.Test
{
    [Description("Every command shall provide meaningful commands to be tested.")]
    public partial class TestChooser : CommandBase
    {
        public override void Invoke()
        {
            Application.EnableVisualStyles();

            var f = new Form
            {
                Width = 400,
                Height = 300,
                ShowInTaskbar = true,
                ShowIcon = false,
                Text = "jsc - Test Chooser",
                FormBorderStyle = FormBorderStyle.Sizable
            };

            f.Load +=
                delegate
                {
                    f.Activate();
                };

            var p = new FlowLayoutPanel
            {

                Dock = DockStyle.Fill,
                AutoScroll = true
            }.AttachTo(f);


            #region AddButton
            Action<string, Action> AddButton =
                (Text, Handler) =>
                {

                    var p1 = new Button { Text = Text, TextAlign = ContentAlignment.MiddleLeft }.AttachTo(p);
                    p.SetFlowBreak(p1, true);

                    p.ClientSizeChanged +=
                        delegate
                        {
                            p1.Width = p.ClientSize.Width - p1.Margin.Horizontal;
                        };

                    p1.Click +=
                        (sender, args) =>
                        {
                            var bw = new BackgroundWorker();

                            p.Enabled = false;



                            bw.DoWork +=
                                delegate
                                {

                                    try
                                    {
                                        Handler();
                                    }
                                    catch (Exception exc)
                                    {
                                        // Enclosing type(s) not found for type 'FetchArrayResult' in assembly 'ScriptCoreLib, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

                                        Debugger.Launch();
                                        Debugger.Break();
                                    }
                                };

                            bw.RunWorkerCompleted +=
                                delegate
                                {
                                    p.Enabled = true;

                                    f.Focus();
                                };

                            bw.RunWorkerAsync();

                        };
                };
            #endregion

            AddTests(AddButton);

            AddButton("Close Test Chooser", () => f.Invoke(new Action(f.Close)));


            Application.Run(f);
        }


    }
}
