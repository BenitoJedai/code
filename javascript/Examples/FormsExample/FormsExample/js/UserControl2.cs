using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsExample.js
{
    [ScriptCoreLib.Script]
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Clicked";

            Console.WriteLine("Zen!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("data");
        }

        [SettingsBindable(true), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public override string Text
        {
            get
            {
                return button1.Text;
            }
            set
            {
                button1.Text = value;
            }
        }
    }
}
