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


        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20150520

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

            //{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Boolean IsEnum(System.Type), DeclaringType = Newtonsoft.Json.Utilities.TypeExtensions, Location =
            // assembly: X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\bin\Release\BSONExperiment.exe
            // type: Newtonsoft.Json.Utilities.ValidationUtils, BSONExperiment, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000e
            //  method:Void ArgumentTypeIsEnum(System.Type, System.String), ex = System.NotImplementedException: The finally clause is not yet implemented! Try to refactor!
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass11c.<>c__DisplayClass12b.<WriteSwitchRewrite>b__cf(ILGenerator flow_il)

            Debugger.Break();
        }

    }
}
