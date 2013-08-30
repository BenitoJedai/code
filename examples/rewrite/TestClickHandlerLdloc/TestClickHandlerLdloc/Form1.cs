using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClickHandlerLdloc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Click(object sender, EventArgs e)
        {

            var yields = new
            {
                foo0 = new Action(delegate { }),
                foo1 = new Action(delegate { }),
                stackreversal0 = "1",
                foo2 = new Action(delegate { }),
                foo3 = new Action(delegate { }),
                foo4 = new Action(delegate { }),
                stackreversal1 = "2",
                stackreversal2 = "3",
                foo5 = new Action(delegate { }),
                foo6 = new Action(delegate { })
            };

        }

        public Action<string, string, string> foo;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Click +=
                delegate
                {
                    // need to add handler withing delegate
                    // to see caching in action
                    this.DoubleClick +=
                        delegate
                        {

                        };


                    //this.foo("", "", "");
                };
        }
    }
}
