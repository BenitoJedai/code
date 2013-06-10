using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace WithClickOnceLANLauncher
{
    partial class ApplicationWebServiceMulticast
    {
        private IContainer components;

     

        private void InitializeComponent()
        {
            this.multicastListenerComponent1 = new WithClickOnceLANLauncherShared.MulticastListenerComponent();
            this.multicastSendComponent1 = new WithClickOnceLANLauncherShared.MulticastSendComponent();
            // 
            // multicastListenerComponent1
            // 
            this.multicastListenerComponent1.AtData += new System.Action<string>(this.multicastListenerComponent1_AtData);

        }

        private WithClickOnceLANLauncherShared.MulticastListenerComponent multicastListenerComponent1;
        private WithClickOnceLANLauncherShared.MulticastSendComponent multicastSendComponent1;
    }
}
