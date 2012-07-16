using LinqToObjects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace LinqToObjects
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            Update();
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {
            Update();
        }

        private void textBox3_TextChanged(object sender, System.EventArgs e)
        {
            Update();
        }

        void Update()
        {
            var user_filter = filter.Text.Trim().ToLower();
            var user_filter2 = filter2.Text.Trim().ToLower();

            result.Clear();

            var __users = users.Text.Split(',');


            var query = from i in __users
                        where i.ToLower().Contains(user_filter)
                        let name = i.Trim()
                        let isspecial = i.ToLower().Contains(user_filter2)
                        orderby isspecial ascending, name.Length descending, name
                        select new { isspecial, length = name.Length, name };

            foreach (var v in query)
            {
                var m = "match: " + v;

                if (v.isspecial)
                    m = m.ToUpper();

                result.AppendText(m + Environment.NewLine);
            }
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            Update();
        }
    }
}
