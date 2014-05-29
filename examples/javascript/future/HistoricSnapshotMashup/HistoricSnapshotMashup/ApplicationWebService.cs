using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HistoricSnapshotMashup.Data;

namespace HistoricSnapshotMashup
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public List<HistoricsTheEntryRow> data = new List<HistoricsTheEntryRow>();


        public async Task InsertNewHistoric()
        {
            foreach (var item in data)
            {
                if (item.Key == 0)
                    item.Key = new Historics.TheEntry().Insert(item);

                Console.WriteLine(new { item });
            }
        }
    }


}
