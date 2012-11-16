using FormsDropFileIntoSQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.IO;

namespace FormsDropFileIntoSQLite
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            var f = e.Data.GetFormats();
            // FileDrop
            var x = f.Select(k => e.Data.GetData(k)).ToArray();

            var FileDrop = (string[])e.Data.GetData(DataFormats.FileDrop);

            FileDrop.WithEach(
                k =>
                {
                    var fi = new FileInfo(k);

                    dataGridView1.Rows.Add(
                        fi.Extension,
                        fi.Name,
                        fi.Length
                    );
                }
            );

        }
    }
}
