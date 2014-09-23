using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/NetworkInformation.webidl

    [Script(InternalConstructor = true)]
    public class INetworkInformation
    {
        // tested by?
        // X:\jsc.svn\examples\javascript\Test\TestDownlinkMax\TestDownlinkMax\Application.cs


        // http://w3c.github.io/netinfo/

        //readonly    attribute ConnectionType type;
        public string type;


        //readonly    attribute Megabit        downlinkMax;
        public double downlinkMax;
    }
}
