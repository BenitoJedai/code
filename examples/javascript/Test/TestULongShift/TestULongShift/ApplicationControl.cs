using TestULongShift;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System;

namespace TestULongShift
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            DoUpdate();
        }

        private void DoUpdate()
        {
            var pow = (ulong)Math.Pow(2, trackBar1.Value);

            label1.Text = new { trackBar1.Value, pow }.ToString();

            //            arg[0] is typeof System.EventHandler
            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.TrackBar.add_Scroll(System.EventHandler)]

            //02000012 TestULongShift.ApplicationControl
            //script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)
            //  [0x0075] ldloc.s    +1 -0

            // assembly: V:\TestULongShift.Application.exe
            // type: TestULongShift.ApplicationControl, TestULongShift.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0075
            //  method:Void trackBar1_Scroll(System.Object, System.EventArgs)






            var i = ulong.Parse(
                textBox1.Text, System.Globalization.NumberStyles.HexNumber
            );

            textBox5.Text = "" + i;

            // X:\jsc.svn\examples\javascript\Test\TestBitShiftRight\TestBitShiftRight\Application.cs

            //new BitArray(
            ShowInputBits(i);


            //var x = i >> trackBar1.Value;
            var x = i / pow;

            //textBox2.Text = x.ToString("x");

            ShowOutputBits(x);
        }

        private void ShowOutputBits(ulong x)
        {
            textBox4.Clear();

            // X:\jsc.svn\examples\javascript\test\TestULongToByteCast\TestULongToByteCast\Application.cs
            var a = BitConverter.GetBytes(x);
            foreach (var _byte in a.Reverse())
            {
                var bits = new System.Collections.BitArray(new[] { _byte });

                for (int index = 7; index >= 0; index--)
                {
                    var bit = bits[index];


                    if (bit)
                        textBox4.AppendText("1");
                    else
                        textBox4.AppendText("0");
                }

                textBox4.AppendText(" ");
            }
        }

        private void ShowInputBits(ulong i)
        {
            //script: error JSC1000: No implementation found for this native method, please implement [static System.BitConverter.GetBytes(System.UInt64)]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at TestULongShift.ApplicationControl.trackBar1_Scroll,

            var a = BitConverter.GetBytes(i);

            // X:\jsc.internal.svn\compiler\jsx.reflector\ReflectorWindow.AddField.cs

            //textBox3.Text = x.ToString("x");

            //textBox3.SuspendLayout();
            textBox3.Clear();

            foreach (var _byte in a.Reverse())
            {
                // script: error JSC1000: No implementation found for this native method, please implement [System.Collections.BitArray.get_Item(System.Int32)]

                var bits = new System.Collections.BitArray(new[] { _byte });

                for (int index = 7; index >= 0; index--)
                {
                    var bit = bits[index];

                    if (bit)
                        textBox3.AppendText("1");
                    else
                        textBox3.AppendText("0");
                }

                textBox3.AppendText(" ");
            }
            //textBox3.ResumeLayout();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoUpdate();

        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            DoUpdate();
        }

    }
}
