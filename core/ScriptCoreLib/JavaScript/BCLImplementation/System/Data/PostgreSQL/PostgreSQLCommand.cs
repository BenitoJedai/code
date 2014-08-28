using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.MySQL
{
    //[Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLCommand")]
    //[Obsolete("we need to extend xsqlite and xmysql to have the async methods defined as interfaces")]
    public class __PostgreSQLCommand : __DbCommand
    {
        // 20140828
        // can we expect our data layer to integrate with postgre soon?
        // http://googlecloudplatform.blogspot.com/2014/08/click-to-deploy-gitlab-community-server-on-google-compute-engine.html

        // X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
        // x:\jsc.svn\core\scriptcorelib.async\scriptcorelib.async\query\experimental\queryexpressionbuilderasync.idbconnection.count.cs

        public Task<int> ExecuteNonQueryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> ExecuteScalarAsync()
        {
            throw new NotImplementedException();
        }

        public override global::System.Data.Common.DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override string CommandText
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
