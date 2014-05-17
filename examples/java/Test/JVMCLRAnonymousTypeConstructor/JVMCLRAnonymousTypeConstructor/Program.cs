using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRAnonymousTypeConstructor
{


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            Expression<Func<int, object>> y = x => new { x };

            //java.lang.Object, rt
            //Type.GetConstructor { FullName = __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_49__27__53_0_1, parameters = 1, InternalTypeDescription = class __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_49__27__53_0_1 }
            //Type.GetConstructor { parameter = java.lang.String, InternalTypeDescription = class java.lang.String }
            //Type.GetConstructor { SourceConstructor = .ctor(java.lang.Object) }
            //Type.GetConstructor { FullName = __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_49__27__53_0_1, constructor = .ctor(), InternalConstructor = , DeclaringType =  }

            Console.WriteLine(
                new { y }
                );


            //java.lang.Object, rt
            //Type.GetConstructor { FullName = __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_47__27__51_0_1, parameters = 1, InternalTypeDescription = class __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_47__27__51_0_1 }
            //Type.GetConstructor { parameter = java.lang.Integer, InternalTypeDescription = class java.lang.Integer }
            //Type.GetConstructor { SourceConstructor = .ctor(java.lang.Object) }
            //Type.GetConstructor { FullName = __AnonymousTypes__JVMCLRAnonymousTypeConstructor__i__d_jvm.__f__AnonymousType_47__27__51_0_1, constructor = .ctor(), InternalConstructor = , DeclaringType =  }
            //Expression.New { constructor = .ctor(), DeclaringType = , arguments = ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1@dc57db, members = [LScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo;@c24c0 }
            //Lambda { body = NewExpression { Constructor = .ctor(), Type =  } }
            //{ y = { Body = NewExpression { Constructor = .ctor(), Type =  }, Parameters = ScriptCoreLib.Shared.BCLImplementation.System.Collections.ObjectModel.__ReadOnlyCollection_1@d73c7a } }
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089


            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );



            MessageBox.Show("click to close");

        }
    }


}
