using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataReader")]
    public class __SQLiteDataReader : __DbDataReader
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbDataReader.cs

        public SQLResultSet r;
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteDataReader.cs
        // X:\jsc.svn\examples\javascript\linq\WebSQLXElement\WebSQLXElement\Application.cs

        public long InternalIndex = -1;

        public override void Close()
        {
            // can we close it?

            this.r = null;
            this.InternalIndex = -2;
        }

        public override bool Read()
        {
            InternalIndex++;

            return InternalIndex < (long)r.rows.length;
        }

        public dynamic InternalCurrent
        {
            get
            {
                return this.r.rows.item((ulong)this.InternalIndex);
            }
        }



        public override object this[int i]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs
                return this[GetName(i)];
            }
        }
        public override object this[string name]
        {
            get
            {
                return InternalCurrent[name];
            }
        }

        public override int GetOrdinal(string name)
        {
            //0:729ms { xName = xid }
            //0:729ms { xName = Name }
            //0:729ms { xName = Location }
            //0:730ms { name = Name, i = 1 }

            var x = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of((object)InternalCurrent);
            var names = x.GetMemberNames().Select(xName =>
            {
                //Console.WriteLine(new { xName });
                return (string)xName;
            }).ToList();

            var i = names.IndexOf(name);

            //Console.WriteLine(new { name, i });

            return i;
        }

        public override string GetString(int i)
        {
            var x = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of((object)InternalCurrent);
            var names = x.GetMemberNames().Select(xName =>
            {
                //Console.WriteLine(new { xName });
                return (string)xName;
            }).ToList();
            var name = names[i];

            string value = InternalCurrent[name];
            return value;
        }

        public override int GetInt32(int i)
        {
            var x = this[i];

            // X:\jsc.svn\examples\javascript\test\TestObjectToInt32\TestObjectToInt32\Program.cs

            // unless it already was stored as int? :P

            var xString = "" + x;

            var xDouble = 0.0;

            double.TryParse(xString, out xDouble);

            return (int)xDouble;
        }

        public override string GetName(int ordinal)
        {
            // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs
            return ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMemberNames((object)this.InternalCurrent).ElementAtOrDefault(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override int FieldCount
        {
            get
            {
                // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs


                return ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMemberNames((object)this.InternalCurrent).Length;
            }
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }
    }
}
