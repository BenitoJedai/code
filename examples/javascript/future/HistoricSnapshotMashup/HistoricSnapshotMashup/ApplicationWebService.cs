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
using System.Diagnostics;

namespace HistoricSnapshotMashup
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IDisposable
    {
        public int random = new Random().Next();

        public List<HistoricsTheEntryRow> data = new List<HistoricsTheEntryRow>();

        public void Dispose()
        {
            // mark all states as invalid?
        }

        public async Task Forget(HistoricsTheEntryKey ForgetEntry)
        {
            new Historics.TheEntyRevoked().Insert(
                // could we allow implicit reverse foren key conversion?
                new HistoricsTheEntyRevokedRow { TheEntry = ForgetEntry }
            );
        }

        public async Task InsertNewHistoric()
        {
            foreach (var item in data)
            {
                if (item.Key == 0)
                {
                    // this method has no effect if no new keys are there.
                    // can we automatically deduce it?

                    item.Key = new Historics.TheEntry().Insert(item);
                }

                Console.WriteLine(new { item });
            }
        }

        public void ForgetAll()
        {
            // user did not want to resume.

            var forget = new Historics.TheEntyRevoked().AsEnumerable();
            foreach (var item in from k in new Historics.TheEntry().AsEnumerable()
                                 where !forget.Any(x => x.TheEntry == k)
                                 select k)
            {
                Forget(item);
            }
        }

        public void Resume(Action<IEnumerable<HistoricsTheEntryRow>> yield)
        {
            // http://msdn.microsoft.com/en-us/library/bb399342(v=vs.110).aspx

            var forget = new Historics.TheEntyRevoked().AsEnumerable();

            // did we have using async example yet?

            // let the client know he can forget a state.
            yield(
                from k in new Historics.TheEntry().AsEnumerable()
                where !forget.Any(x => x.TheEntry == k)
                select k
            );
        }
    }


}
