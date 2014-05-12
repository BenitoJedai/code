using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestGroupJoin
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2()
        {
            var DealerContact = new[] { 
                new { DealerId = 2, DealerContactText = "z"},
                new { DealerId = 3, DealerContactText = "hello "},
                new { DealerId = 4, DealerContactText = "zz"}
            };

            var Dealer = new[] { 
                new { ID = 1, DealerText = ""},
                new { ID = 3, DealerText = "world"},
                new { ID = 5, DealerText = ""}
            };


            var DealerOther = new[] { 
                new { ID = 0, DealerOtherText = ""},
                new { ID = 3, DealerOtherText = "!!"},
                new { ID = 20, DealerOtherText = ""}
            };

            //{ DealerContactText = z, c = 0 }
            //{ DealerContactText = hello , c = 1 }
            //{ DealerContactText = zz, c = 0 }

            var dealercontacts = from contact in DealerContact
                                 join dealer in Dealer on contact.DealerId equals dealer.ID into g

                                 // add one more
                                 //join other in DealerOther on contact.DealerId equals other.ID

                                 // new viewrow { x = ?, }
                                 select new
                                 {
                                     contact,
                                     //dealer,

                                     c = g.Count()

                                     //other 
                                 };
        }

    }
}
