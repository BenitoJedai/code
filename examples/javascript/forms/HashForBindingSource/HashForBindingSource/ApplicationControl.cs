using HashForBindingSource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System;

namespace HashForBindingSource
{
    [DefaultEvent("zeBindingSourceCurrentChanged")]
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // um this event no longer fires?
            // wh the fk not?

            //Debugger.Break();


        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            // set it to come from the server?
            // can the client actually stream?
            // can the server actually stream?

            //this.webBrowser1.DocumentStream 

            this.webBrowser1.DocumentText =

                // rename to DocumentText ?
                HashForBindingSource.HTML.Pages.FooSource.Text;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.webBrowser1.DocumentText =

                // rename to DocumentText ?
                HashForBindingSource.HTML.Pages.GooSource.Text;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {

            this.zeBindingSource.Position =
                (this.zeBindingSource.Position + 1) % this.zeBindingSource.Count
                ;

        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            var hash = "#blue";

            FindHash(hash);

            // this.zeBindingSource[0] {System.Data.DataRowView}

        }

        public void FindHash(string hash)
        {
            // AssemblyQualifiedName = "HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateBindingSource, HashForBindingSource.DataSourcez, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            var x = this.zeBindingSource.DataSource;
            // +		x as BindingSource	null	System.Windows.Forms.BindingSource
            // 		x is System.Type	true	bool
            //var y = (x as BindingSource).DataSource;
            //HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateBindingSource

            var zero = this.zeBindingSource[0];

            var rows = Enumerable.Range(0,
                this.zeBindingSource.Count
            ).Select(
                Position =>
                {
                    //Console.WriteLine(new { Position });

                    //var item = this.zeBindingSource[Position];
                    //Console.WriteLine(new { item });

                    return new
                    {
                        Position,

                        (
                        (HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateRow)

                        // this faults!
                            //(item as System.Data.DataRowView).Row

                            (
                                (System.Data.DataRowView)(this.zeBindingSource[Position])
                            ).Row
                        ).hash

                    };
                }
            );

            rows.Where(xx => xx.hash == hash).FirstOrDefault().With(
                xx =>
                {
                    // did we find it?


                    this.zeBindingSource.Position = xx.Position;

                }
            );
        }

        private void zeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            this.label2.Text =
                //this.zeBindingSource.Current.ToString();

                          (
                        (HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateRow)

                        // this faults!
                //(item as System.Data.DataRowView).Row

                            (
                                (System.Data.DataRowView)(this.zeBindingSource.Current)
                            ).Row
                        ).hash;
        }



        public event Action zeBindingSourceCurrentChanged
        {
            add
            {
                this.zeBindingSource.CurrentChanged +=
                    delegate
                    {
                        value();
                    };
            }
            remove
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("dock it");

            xWebBrowser1.Dock = DockStyle.Fill;

        }
    }
}
