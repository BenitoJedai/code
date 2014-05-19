using BSONExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BSONExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // https://github.com/scriptcs/scriptcs/pull/525

            // how do we use bson writer?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140519-1
            // X:\opensource\github\Newtonsoft.Json\Src\Newtonsoft.Json\Bson


            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            var z = new Newtonsoft.Json.Bson.BsonWriter(w);

            z.WriteStartObject();

            z.WritePropertyName("hello");

            z.WriteValue("world");



            z.Close();

            // http://blogs.msdn.com/b/visualstudioalm/archive/2014/02/06/json-debugger-visualizer-in-visual-studio-2013.aspx


            Debugger.Break();

        }

    }
}
