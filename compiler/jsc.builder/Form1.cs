using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace jsc.builder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var buttons = new List<Button>();

            var a = new List<Action>();
            var cd = default(string);
            var btn = default(Button);

            var NotifyNextAction = default(Action<Action>);

            foreach (var item in File.ReadLines("commands.bat"))
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;

                if (btn == null)
                {
                    if (item.StartsWith("echo "))
                    {
                        btn = new Button
                        {
                            TextAlign = ContentAlignment.MiddleLeft,
                            AutoSize = true,
                            Text = (buttons.Count + 1) + ". " + item.Substring(5),
                            Dock = DockStyle.Top,
                            Padding = new Padding(12),
                            Margin = new Padding(12),

                        };
                        buttons.Add(btn);
                    }
                }
                else
                {
                    var _ = btn;
                    if (item.StartsWith("rem "))
                    {
                        btn.Enabled = false;
                        a.Add(() => this.Controls.Add(_));
                        btn = null;
                        cd = null;
                    }
                    else if (item.StartsWith("cd "))
                    {
                        cd = item.Substring(3);
                    }
                    else if (item.StartsWith("call "))
                    {
                        var cmd = item.Substring(5);

                        // Create the ToolTip and associate with the Form container.
                        ToolTip toolTip1 = new ToolTip();

                        // Set up the delays for the ToolTip.
                        toolTip1.AutoPopDelay = 5000;
                        toolTip1.InitialDelay = 1000;
                        toolTip1.ReshowDelay = 500;
                        // Force the ToolTip text to be displayed whether or not the form is active.
                        toolTip1.ShowAlways = true;

                        // Set up the ToolTip text for the Button and Checkbox.
                        toolTip1.SetToolTip(btn, cmd);

                        var _cd = cd;

                        var NextAction = default(Action);

                        Action InvokeAction =
                            delegate
                            {
                                _.ForeColor = Color.Blue;

                                foreach (var item1 in buttons)
                                {
                                    item1.Enabled = false;
                                }

                                var c = "__command.bat";

                                Console.WriteLine(DateTime.Now.ToString() + " " + cmd);

                                File.WriteAllText(c,
                                    @"
@echo off
rem echo " + _.Text + @"
call " + cmd + @"
set __ERRORLEVEL=%ERRORLEVEL%
rem echo error: %ERRORLEVEL%
rem echo error: %__ERRORLEVEL%
if %ERRORLEVEL% == 0 exit /B
echo.
echo.
color 0C 
echo *** ERROR: %__ERRORLEVEL%
echo.
echo.
pause
exit /B %__ERRORLEVEL%
"
                                );


                                var p = Process.Start(
                                     new ProcessStartInfo(Path.GetFullPath(c))
                                     {
                                         WorkingDirectory = _cd
                                     }
                                 );

                                p.EnableRaisingEvents = true;
                                p.Exited +=
                                    delegate
                                    {
                                        this.Invoke(
                                            new Action(
                                                () =>
                                                {
                                                    foreach (var item1 in buttons)
                                                    {
                                                        item1.Enabled = true;
                                                    }

                                                    if (p.ExitCode != 0)
                                                    {
                                                        _.ForeColor = Color.Red;
                                                        Console.WriteLine(DateTime.Now.ToString() + " ERROR " + p.ExitCode);

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(DateTime.Now.ToString() + " OK");

                                                        if (NextAction != null)
                                                        {
                                                            var EnableNextAction = false;
                                                            using (var ff = new Form2())
                                                            {


                                                                ff.timer1.Tick +=
                                                                    delegate
                                                                    {
                                                                        EnableNextAction = true;
                                                                    };


                                                                ff.StartPosition = FormStartPosition.CenterParent;
                                                                ff.ShowDialog(this);
                                                            }
                                                            if (EnableNextAction)
                                                                NextAction();
                                                        }
                                                    }


                                                }
                                            )
                                        );
                                    };
                            };

                        if (NotifyNextAction != null)
                            NotifyNextAction(InvokeAction);

                        NotifyNextAction = value => NextAction = value;

                        btn.Click +=
                            delegate
                            {
                                InvokeAction();
                            };

                        a.Add(() => this.Controls.Add(_));
                        btn = null;
                        cd = null;
                    }
                }
            }

            foreach (var item in a.AsEnumerable().Reverse())
            {
                item();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
