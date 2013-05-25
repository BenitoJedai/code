using TestDropFile;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.IO;

namespace TestDropFile
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = "DragDrop";

            //e.Data.GetFormats().WithEach(
            //    f =>
            //    {
            //        label1.Text += "\n" + f;
            //    }
            //);

            // just a path
            // jsc: fixme
            //(e.Data.GetData("FileDrop") as string[]).WithEach(


            var FileDrop = (string[])e.Data.GetData("FileDrop");


            FileDrop.WithEach(
                path =>
                {
                    label1.Text += "\n" + path;

                    if (File.Exists(path))
                    {
                        var f = new FileInfo(path);

                        label1.Text += "\n is a file " + f.Length + " bytes";

                        if (path.EndsWith(".txt"))
                        {
                            //new ScriptCoreLib.JavaScript.DOM.FileReader);

                            // await?
                            f.ReadAllText(
                                text =>
                                {
                                    label1.Text += "\n " + text;
                                }
                            );


                        }
                    }
                }
            );


        }



        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }


        private void ApplicationControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

        }

    }
}
