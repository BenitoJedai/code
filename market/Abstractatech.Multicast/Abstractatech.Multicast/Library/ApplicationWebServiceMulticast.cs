using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WithClickOnceLANLauncher
{
    public partial class ApplicationWebServiceMulticast : Component
    {
        // what about RSA encryption?

        public ApplicationWebServiceMulticast()
        {
            InitializeComponent();
        }

        public int Port { get; set; }
        public string Host { get; set; }

        private void multicastListenerComponent1_AtData(string listen)
        {
            Console.WriteLine(

                new { server = new { listen } }
                );

            try
            {
                var xml = XElement.Parse(listen);

                if (xml.Value.StartsWith("Where are you?"))
                {
                    this.multicastSendComponent1.Send(
                        "Visit me at " + this.Host + ":" + this.Port
                    );

                }
            }
            catch
            {

            }
        }
    }
}
