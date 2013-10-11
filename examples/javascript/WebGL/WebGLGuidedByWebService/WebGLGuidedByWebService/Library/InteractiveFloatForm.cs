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

namespace WebGLGuidedByWebService.Library
{
    public partial class InteractiveFloatForm : Form
    {
        public InteractiveFloatForm()
        {
            InitializeComponent();
        }
    }

}


namespace ScriptCoreLib.Extensions
{

    public static class XInteractiveInt32Form
    {

        class y
        {
            public DataGridViewTextBoxCell value;


            public string CallerFilePath;
            public int CallerLineNumber;

            public override string ToString()
            {

                return CallerFilePath + ":" + CallerLineNumber;
            }
        }


        static WebGLGuidedByWebService.Library.InteractiveFloatForm f;
        static List<y> lookup = new List<y>();

        public static float ToInteractiveFloatDataGridView(this float value,

            [CallerFilePathAttribute] string CallerFilePath = null,
            [CallerLineNumberAttribute] int CallerLineNumber = 0,
            [CallerMemberNameAttribute] string CallerMemberName = null

            )
        {
            var key = CallerFilePath + ":" + CallerLineNumber;

            var y = lookup.FirstOrDefault(
                k =>
                {
                    return k.ToString() == key;
                }
            );

            if (y == null)
            {
                if (f == null)
                {
                    f = new WebGLGuidedByWebService.Library.InteractiveFloatForm();
                    f.Show();
                }

                y = new y
                {
                    value = new DataGridViewTextBoxCell { Value = "" + value },
                    CallerFilePath = CallerFilePath,
                    CallerLineNumber = CallerLineNumber
                };

                lookup.Add(y);

                var row = new DataGridViewRow();


                row.Cells.Add(y.value);

                row.Cells.AddRange(
                    // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewButtonColumn.set_Text(System.String)]
                    new DataGridViewButtonCell { 
                        Value = "save" 
                    },
                    new DataGridViewTextBoxCell { Value = CallerFilePath },
                    new DataGridViewTextBoxCell { Value = "" + CallerLineNumber }
                );

                f.dataGridView1.Rows.Add(row);
            }

            value = float.Parse((string)y.value.Value);

            return value;
        }



    }
}
