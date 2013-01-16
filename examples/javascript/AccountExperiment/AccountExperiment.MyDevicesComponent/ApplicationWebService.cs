using AccountExperiment.MyDevicesComponent.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace AccountExperiment.MyDevicesComponent
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component,
        IMyDevicesComponent_MyDevices
    {
        public MyDevices devices = new MyDevices();

        public void MyDevices_Insert(string account, string name, string value, Action<string> yield)
        {
            var id = devices.Insert(
                new MyDevicesQueries.Insert
                {
                    account = long.Parse(account),
                    name = name,
                    value = value,
                    ticks = 0
                }
            );

            yield("" + id);
        }



        public void MyDevices_SelectByAccount(string account, Action<string, string, string> yield, Action done)
        {
            devices.SelectByAccount(
                new MyDevicesQueries.SelectByAccount { account = long.Parse(account) },
                r =>
                {
                    long id = r.id;

                    string name = r.name;
                    string value = r.value;

                    yield("" + id, name, value);
                }
            );

            done();
        }


        public void MyDevices_Update(string account, string id, string name, string value, Action done)
        {
            devices.Update(
                new MyDevicesQueries.Update
                {
                    account = long.Parse(account),
                    id = long.Parse(id),
                    name = name,
                    value = value
                }
            );

            done();
        }
    }

    public interface IMyDevicesComponent_MyDevices
    {
        // consumers may need to wrap with session id
        void MyDevices_Insert(string account, string name, string value, Action<string> yield);
        void MyDevices_SelectByAccount(string account, Action<string, string, string> yield, Action done);
        void MyDevices_Update(string account, string id, string name, string value, Action done);
    }
}
