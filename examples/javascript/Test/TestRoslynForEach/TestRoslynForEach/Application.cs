using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestRoslynForEach;
using TestRoslynForEach.Design;
using TestRoslynForEach.HTML.Pages;
using System.Collections;

namespace TestRoslynForEach
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\Test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs

            //         1c9c: 01:01 RewriteToAssembly error: System.NotImplementedException: The finally clause is not yet implemented!Try to refactor or try roslyn! { SourceMethodOrConstructor = Int32 CalculateSize(TestRoslynForEach.BsonToken), AssemblyQualifiedName = TestRoslynForEach.BsonBinaryWriter, TestRoslynForEach, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null }
            //         at jsc.meta.Commands.Rewrite.RewriteToAssembly.<> c__DisplayClass11c.<> c__DisplayClass12b.< WriteSwitchRewrite > b__cf(ILGenerator flow_il) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 821
            //at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 21

            var x = new BsonBinaryWriter().CalculateSize(
                BsonType.Object, new List<object> { new { x = 1 }, new { x = 2 } }
            );

            Native.document.body += new IHTMLPre { new { x } };
        }

    }

    internal enum BsonType : sbyte
    {
        Number = 1,
        String = 2,
        Object = 3,
        Array = 4,
        Binary = 5,
        Undefined = 6,
        Oid = 7,
        Boolean = 8,
        Date = 9,
        Null = 10,
        Regex = 11,
        Reference = 12,
        Code = 13,
        Symbol = 14,
        CodeWScope = 15,
        Integer = 16,
        TimeStamp = 17,
        Long = 18,
        MinKey = -1,
        MaxKey = 127
    }



    class BsonBinaryWriter
    {
        public int CalculateSize(BsonType t_Type, List<object> value)
        {
            switch (t_Type)
            {
                case BsonType.Object:
                    {
                        foreach (var p in value)
                        {
                            //Console.WriteLine(new { p });

                            Native.document.body += new IHTMLPre { new { p } };

                        }
                        return -1;
                    }

                case BsonType.Integer:
                    return 4;
                case BsonType.Long:
                    return 8;
                case BsonType.Number:
                    return 8;
                case BsonType.Boolean:
                    return 1;
                case BsonType.Null:
                case BsonType.Undefined:
                    return 0;
                case BsonType.Date:
                    return 8;
                default:
                    return 12;
            }
        }
    }
}
