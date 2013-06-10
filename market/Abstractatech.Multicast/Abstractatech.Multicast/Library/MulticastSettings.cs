using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WithClickOnceLANLauncherShared
{
    // https://code.google.com/p/multicastdotnet/source/browse/#svn%2Ftrunk%2FMulticast%2FMulticast

    public class MulticastSettings
    {
        static public MulticastSettings testSettings = new MulticastSettings()
        {
            Address = IPAddress.Parse("239.1.2.3"),
            Port = 40404,
            TimeToLive = 30
        };

        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public int TimeToLive { get; set; }
    }
}
