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
                new { ID = 3, DealerOtherText = "!!!"},
                new { ID = 20, DealerOtherText = ""}
            };

            //{ DealerContactText = z, c = 0 }
            //{ DealerContactText = hello , c = 1 }
            //{ DealerContactText = zz, c = 0 }

            {
                var dealercontacts = from contact in DealerContact

                                     // http://msdn.microsoft.com/en-us/library/bb311040.aspx
                                     // With equals, the left key consumes the outer source sequence, and the right key consumes the inner source. The outer source is only in scope on the left side of equals and the inner source sequence is only in scope on the right side.
                                     join dealer in Dealer on contact.DealerId equals dealer.ID // into g

                                     // add one more
                                     join other in DealerOther on contact.DealerId equals other.ID


                                     group other by new { other.ID, dealer, contact } into g

                                     // new viewrow { x = ?, }
                                     select new
                                     {
                                         g.Key.ID,
                                         g.Key.dealer,
                                         g.Key.contact,

                                         g.Last().DealerOtherText

                                         //contact,
                                         //dealer,

                                         //c = g.Count()

                                         //other 


                                     };


                var dealercontacts0 = dealercontacts.AsEnumerable();
            }

            {
                var dealercontacts = from other in DealerOther

                                     //join other in DealerOther on contact.DealerId equals other.ID


                                     group other by other.ID into g

                                     // http://msdn.microsoft.com/en-us/library/bb311040.aspx
                                     // With equals, the left key consumes the outer source sequence, and the right key consumes the inner source. The outer source is only in scope on the left side of equals and the inner source sequence is only in scope on the right side.
                                     join dealer in Dealer on g.Key equals dealer.ID // into g

                                     // add one more

                                     //from contact in DealerContact
                                     join contact in DealerContact on dealer.ID equals contact.DealerId


                                     // new viewrow { x = ?, }
                                     select new
                                     {
                                         contact,
                                         dealer,

                                         other = g.Last()

                                         //g.Key.ID,
                                         //g.Key.dealer,
                                         //g.Key.contact,

                                         //g.Last().DealerOtherText

                                         //contact,
                                         //dealer,

                                         //c = g.Count()

                                         //other 


                                     };


                var dealercontacts0 = dealercontacts.AsEnumerable();
            }
        }

    }
}
