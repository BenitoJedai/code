using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
    internal class __UdpClient
    {
        // what comes after tcp?
        // what about async API ?

        // X:\jsc.svn\examples\java\hybrid\JVMCLRUDPSendAsync\JVMCLRUDPSendAsync\Program.cs
        // X:\jsc.svn\examples\java\ConsoleMulticastExperiment\ConsoleMulticastExperiment\Program.cs
        // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
        // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs

        public __UdpClient()
        {

        }


        public Socket Client { get; set; }


        public Action vClose;
        public void Close() { vClose(); }


        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\Sockets\UdpReceiveResult.cs
        public Func<Task<__UdpReceiveResult>> vReceiveAsync;
        public Task<__UdpReceiveResult> ReceiveAsync() { return vReceiveAsync(); }





        public SendAsyncDelegate vSendAsync;
        public delegate Task<int> SendAsyncDelegate(byte[] datagram, int bytes, string hostname, int port);
        public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port) { return vSendAsync(datagram, bytes, hostname, port); }

    }
}
