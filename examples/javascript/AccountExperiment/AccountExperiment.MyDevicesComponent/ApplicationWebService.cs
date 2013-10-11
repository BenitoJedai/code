using AccountExperiment.MyDevicesComponent.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountExperiment.MyDevicesComponent
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component,
        IMyDevicesComponent_MyDevices
    {
        MyDevices devices = new MyDevices();



        public long account;

        public Task<long> MyDevices_Insert(
            string name,
            string value)
        {
            return devices.Insert(
                new MyDevicesQueries.Insert
                {
                    account = account,
                    name = name,
                    value = value,
                    ticks = 0
                }
            );

        }

        // jsc you are not unescaping params?
        public Task MyDevices_SelectByAccount( Action<long, string, string> yield)
        {
            devices.SelectByAccount(
                new MyDevicesQueries.SelectByAccount { account = account },
                r =>
                {
                    long id = r.id;

                    string name = r.name;
                    string value = r.value;

                    yield(id, name, value);
                }
            );

            return Task.FromResult(default(object));
        }


        public Task MyDevices_Update(
            long id,
            string name,
            string value)
        {
            return devices.Update(
                new MyDevicesQueries.Update
                {
                    account = account,
                    id = id,
                    name = name,
                    value = value
                }
            );
        }
    }

    public interface IMyDevicesComponent_MyDevices
    {
        // consumers may need to wrap with session id
        Task<long> MyDevices_Insert(string name, string value);
        Task MyDevices_SelectByAccount(Action<long, string, string> yield);
        Task MyDevices_Update(long id, string name, string value);
    }
}
