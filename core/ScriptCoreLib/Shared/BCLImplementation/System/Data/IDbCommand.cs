using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.IDbCommand))]
    internal interface __IDbCommand : IDisposable
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        IDataParameterCollection Parameters { get; }

        //Implementation not found for type import :
        //type: System.Data.IDbCommand
        //method: System.Data.IDataParameterCollection get_Parameters()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        IDbDataParameter CreateParameter();

        IDataReader ExecuteReader();


        object ExecuteScalar();

        //message = "\n\n Implementation not found for type import : \n type: System.Data.IDbCommand\n method: System.Object ExecuteScalar()\n Did you forget to add the [Script] attribute? \n Please double check the signature! \n \n assembly: V:\\XSLXAssetWithXElement.Applicat...

        int ExecuteNonQuery();
        string CommandText { get; set; }
    }
}
