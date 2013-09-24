using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AndroidNFCComponent
{
    [DefaultEvent("AtTagDiscovered")]
    public class XNFCComponent : Component
    {
        // yay. you can now drag it into ApplicationComponents!

        public IXNFCComponentSource Source { get; set; }

        public event Action<string> AtTagDiscovered
        {
            add
            {
                if (Source == null)
                    return;

                this.Source.XNFCComponentSource_add_AtTagDiscovered(value);
            }
            #region remove NotImplementedException
            //Error	1	'AndroidNFCComponent.XNFCComponent.AtTagDiscovered': 
            // event property must have both add and remove accessors	
            // X:\jsc.svn\examples\javascript\android\AndroidNFCComponent\AndroidNFCComponent\XNFCComponent.cs	15	37	AndroidNFCComponent

            remove
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }

    public interface IXNFCComponentSource
    {
        void XNFCComponentSource_add_AtTagDiscovered(Action<string> y);
    }
}
