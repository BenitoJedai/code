using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/netinfo/NetworkInformation.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/NetworkInformation.webidl

    [Script(InternalConstructor = true)]
    public class INetworkInformation
    {
        // http://global.samsungtomorrow.com/samsung-electronics-launches-the-worlds-first-lte-advanced-tri-band-carrier-aggregation-smartphone/
        // New technology delivers download speeds of 300Mbps,

        // tested by?
        // X:\jsc.svn\examples\javascript\Test\TestDownlinkMax\TestDownlinkMax\Application.cs


        // http://w3c.github.io/netinfo/

        //readonly    attribute ConnectionType type;
        public string type;


        //readonly    attribute Megabit        downlinkMax;
        public double downlinkMax;
    }
}
