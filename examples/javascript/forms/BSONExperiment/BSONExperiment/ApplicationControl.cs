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

            //            +SourceMethodOrConstructor.DeclaringType { Name = "JsonSerializerInternalReader" FullName = 
            // "Newtonsoft.Json.Serialization.JsonSerializerInternalReader"}
            //            System.Type { System.RuntimeType}
            //            +SourceMethodOrConstructor   { System.Object CreateObject(Newtonsoft.Json.JsonReader, System.Type, Newtonsoft.Json.Serialization.JsonContract, Newtonsoft.Json.Serialization.JsonProperty, Newtonsoft.Json.Serialization.JsonContainerContract, Newtonsoft.Json.Serialization.JsonProperty, System.Object)}
            //            System.Reflection.MethodBase { System.Reflection.RuntimeMethodInfo}

            //            Additional information: Type '<MoveNext>06000c60' from assembly 'BSONExperiment.Application, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null' has a field of an illegal type.

            //        < 0000 > ldarg.0
            //          ...................
            //{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Newtonsoft.Json.Utilities.ReflectionDelegateFactory get_ReflectionDelegateFactory(), DeclaringType = Newtonsoft.Json.Serialization.JsonTypeReflector, Location =
            // assembly: X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\bin\Release\BSONExperiment.exe
            // type: Newtonsoft.Json.Utilities.ConvertUtils, BSONExperiment, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x006c
            //  method:System.Func`2[System.Object, System.Object] CreateCastConverter(TypeConvertKey), ex = System.TypeLoadException: Type '<MoveNext>06000c60' from assembly 'BSONExperiment.Application, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null' has a field of an illegal type.
            //    at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
            //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
            //   at System.Reflection.Emit.TypeBuilder.CreateType()
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass139.<WriteSwitchRewrite>b__128(TypeRewriteArguments e) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 2447
            //   at System.Action`1.Invoke(T obj)
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass49f.<>c__DisplayClass54a.<InternalInvoke>b__42d(TypeBuilder t) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.cs:line 3591



            Debugger.Break();
        }

    }
}
