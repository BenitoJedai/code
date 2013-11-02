using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbDataAdapter))]
    public abstract class __DbDataAdapter
    {
        public DbCommand InternalSelectCommand;
        public DbCommand SelectCommand
        {
            get
            {
                return InternalSelectCommand;
            }
            set
            {
                InternalSelectCommand = value;
            }
        }


        public int Fill(DataTable dataTable)
        {
            //  The number of rows successfully added 

            // x:\jsc.svn\examples\javascript\appengine\webnotificationsviadataadapter\webnotificationsviadataadapter\applicationwebservice.cs
            // x:\jsc.svn\examples\javascript\dropfileintosqlite\dropfileintosqlite\schema\table1.cs
            // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationWebService.cs

            //            02000012 ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter
            //script: error JSC1000: Method: Fill, Type: ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbDataAdapter; emmiting failed : System.ArgumentNullException: Value cannot be null.
            //   at jsc.ILFlowStackItem.InlineLogic(Prestatement p) in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILFlow.cs:line 68

            var data = default(DataTable);
            var reader = this.InternalSelectCommand.ExecuteReader();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131101
            using (reader)
            {

                while (reader.Read())
                {
                    if (data == null)
                    {
                        data = new DataTable();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var n = reader.GetName(i);

                            data.Columns.Add(n);
                        }
                    }


                    var row = data.NewRow();
                    data.Rows.Add(row);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var n = reader.GetName(i);
                        var ft = reader.GetFieldType(i);

                        if (ft == typeof(string))
                        {
                            row[i] = reader.GetString(i);

                        }
                        else if (ft == typeof(long))
                        {
                            row[i] = "" + reader.GetInt64(i);
                        }
                    }
                }
            }


            if (data == null)
                return 0;

            dataTable.Merge(data);

            return data.Rows.Count;
        }
    }
}
