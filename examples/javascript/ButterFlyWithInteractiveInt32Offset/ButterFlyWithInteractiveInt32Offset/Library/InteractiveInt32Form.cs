using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButterFlyWithInteractiveInt32Offset.Library
{
    public partial class InteractiveInt32Form : Form
    {
        public InteractiveInt32Form()
        {
            InitializeComponent();
        }
    }

    public static class X
    {
        static List<InteractiveInt32Form> lookup = new List<InteractiveInt32Form>();

        public static int ToInteractiveInt32Form(this int e,

            [CallerFilePathAttribute] string CallerFilePath = null,
            [CallerLineNumberAttribute] int CallerLineNumber = 0,
            [CallerMemberNameAttribute] string CallerMemberName = null

            )
        {
            var f = lookup.FirstOrDefault(
                k =>
                {
                    return k.label1.Text == CallerFilePath && k.label2.Text == "" + CallerLineNumber;
                }
            );

            if (f == null)
            {
                f = new InteractiveInt32Form { };
                f.label1.Text = CallerFilePath;
                f.label2.Text = "" + CallerLineNumber;
                f.label3.Text = CallerMemberName;

                f.textBox1.Text = "" + e;
                f.Show();

                lookup.Add(f);
            }

            e = int.Parse(f.textBox1.Text);

            return e;
        }
    }
}
