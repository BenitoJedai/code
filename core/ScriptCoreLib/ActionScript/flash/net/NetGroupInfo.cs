using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/net/NetGroupInfo.html
    [Script(IsNative = true)]
    public class NetGroupInfo
    {
        #region Properties
        /// <summary>
        /// [read-only] Specifies the rate at which the local node is receiving objects from peers via the Object Replication system, in bytes per second.
        /// </summary>
        public double objectReplicationReceiveBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which objects are being copied from the local node to peers by the Object Replication system, in bytes per second.
        /// </summary>
        public double objectReplicationSendBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is receiving posting control overhead messages from peers, in bytes per second.
        /// </summary>
        public double postingReceiveControlBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is receiving posting data from peers, in bytes per second.
        /// </summary>
        public double postingReceiveDataBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is sending posting control overhead messages to peers, in bytes per second.
        /// </summary>
        public double postingSendControlBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is sending posting data to peers, in bytes per second.
        /// </summary>
        public double postingSendDataBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is receiving directed routing messages from peers, in bytes per second.
        /// </summary>
        public double routingReceiveBytesPerSecond { get; private set; }

        /// <summary>
        /// [read-only] Specifies the rate at which the local node is sending directed routing messages to peers, in bytes per second.
        /// </summary>
        public double routingSendBytesPerSecond { get; private set; }

        #endregion

    }
}
