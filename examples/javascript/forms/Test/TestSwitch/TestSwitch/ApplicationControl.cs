using TestSwitch;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestSwitch
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        enum Foo
        {
            a, b, c
        }

        void XFoo()
        {
            var _this = this;
            var _checkBox1 = _this.checkBox1;

            var Checked = _checkBox1.Checked;

            if (Checked)
                throw new InvalidOperationException("b error");

            button1.Text = "b";
        }


        Foo foo = Foo.c;
        private void button1_Click(object sender, System.EventArgs e)
        {
            switch (foo)
            {
                case Foo.b:

                    try
                    {
                        var _this = this;
                        var _checkBox1 = _this.checkBox1;

                        var Checked = _checkBox1.Checked;

                        if (Checked)
                            throw new InvalidOperationException("b error");

                        button1.Text = "b";
                    }
                    catch
                    {
                        button1.Text = "b error";
                    }

                    foo = Foo.a;
                    break;

                case Foo.c:
                    {
                        //try
                        //{
                            button1.Text = "c";
                        //}
                        //finally
                        //{
                        //    if (this.checkBox2.Checked)
                        //        button1.Text = "c finally";
                        //}

                        foo = Foo.b;
                        break;
                    }
                default:
                    {
                        button1.Text = "a";
                        foo = Foo.c;
                        break;
                    }
            }
        }

    }
}
