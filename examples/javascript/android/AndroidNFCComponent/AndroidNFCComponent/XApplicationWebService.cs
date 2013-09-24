using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AndroidNFCComponent
{
    public class XApplicationWebService : Component, IXNFCComponentSource
    {
        public readonly ApplicationWebService service = new ApplicationWebService();



        public void XNFCComponentSource_add_AtTagDiscovered(Action<string> y)
        {
            // this is where we need to manually to event stream
            this.service.XNFCComponentSource_add_AtTagDiscovered(y);
        }
    }

}
