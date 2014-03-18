using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SQLiteWithDataGridView
{
    partial class ApplicationWebService
    {

        public static partial class Credentials
        {
#if AppEngine
            static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false)
            {
                // Caused by: java.sql.SQLException: Access denied for user 'user1'@'localhost' to database 'sqlitewithdatagridview6.sqlite'

                b.Add("InternalUser", "user3");

                // Error	20	'System.Data.SQLite.SQLiteConnectionStringBuilder' does not contain a definition for 'Password' and no extension method 'Password' accepting a first argument of type 'System.Data.SQLite.SQLiteConnectionStringBuilder' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Credentials\AppEngine.cs	21	15	SQLiteWithDataGridView
                //b.Password = "mypass";
            }
#endif

            //02000030 SQLiteWithDataGridView.Library.GridForm+<>c__DisplayClass12+<<timer1_Tick>b__10>d__15+<MoveNext>06000082
            //{ Location =
            // assembly: W:\SQLiteWithDataGridView.Application.exe
            // type: SQLiteWithDataGridView.Library.GridForm+<>c__DisplayClass12+<<timer1_Tick>b__10>d__15+<MoveNext>06000082, SQLiteWithDataGridView.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x002a
            //  method:Int32 <00d8> nop.try(<MoveNext>06000082, <<timer1_Tick>b__10>d__15 ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef) }
            //script: error JSC1000: Method: <00d8> nop.try, Type: SQLiteWithDataGridView.Library.GridForm+<>c__DisplayClass12+<<timer1_Tick>b__10>d__15+<MoveNext>06000082; emmiting failed : System.NotImplementedException: { ParameterType = SQLiteWithDataGridView.Library.GridForm+<>c__DisplayClass12+<<timer1_Tick>b__10>d__15&, p = [0x002f] br
            //   at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\IdentWriter.cs:line 833
            //   at jsc.IL2ScriptGenerator.OpCode_call_override(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 379
            //   at jsc.IL2ScriptGenerator.<CreateInstructionHandlers>b__f(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.cs:line 698


        }
    }
}
