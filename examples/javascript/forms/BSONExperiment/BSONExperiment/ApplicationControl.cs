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
            // X:\jsc.svn\examples\javascript\test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs

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


            //private static bool ValuesEquals(JValue v1, JValue v2)
            //{
            //    return (v1 == v2 || (v1._valueType == v2._valueType && Compare(v1._valueType, v1._value, v2._value) == 0));
            //}


            //           { trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Int32 Compare(Newtonsoft.Json.Linq.JTokenType, System.Object, System.Object), DeclaringType = Newtonsoft.Json.Linq.JValue, Location =
            //assembly: X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\bin\Release\BSONExperiment.exe
            //type: Newtonsoft.Json.Linq.JValue, BSONExperiment, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null
            //offset: 0x0025
            // method:Boolean ValuesEquals(Newtonsoft.Json.Linq.JValue, Newtonsoft.Json.Linq.JValue), ex = System.NullReferenceException: Object reference not set to an instance of an object.
            //  at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass131.<>c__DisplayClass141.<WriteSwitchRewrite>b__e7(ILGenerator flow_il) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 1329
            //  at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 21
            //  at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass131.<WriteSwitchRewrite>b__d7(ILFlow SourceFlow) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 1222

            Debugger.Break();
        }

    }
}
