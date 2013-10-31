using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataGridWithHeaders
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        [Obsolete("experimental")]
        public WebServiceHandler WebServiceHandler { get; set; }

        public Task<DataTable> Headers
        {
            get
            {
                var x = new DataTable
                {
                    TableName = "Headers"
                };

                //Implementation not found for type import :
                //type: System.Data.DataColumnCollection
                //method: System.Data.DataColumn Add(System.String)
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!


                x.Columns.Add("key");
                x.Columns.Add("value");

                var h = this.WebServiceHandler.Context.Request.Headers;

                foreach (var item in h.AllKeys)
                {
                    var r = x.NewRow();

                    r[0] = item;
                    r[1] = h[item];

                    x.Rows.Add(r);

                }


                return x.ToTaskResult();
            }
        }

    }
}
