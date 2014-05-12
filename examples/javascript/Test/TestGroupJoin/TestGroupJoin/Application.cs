using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestGroupJoin;
using TestGroupJoin.Design;
using TestGroupJoin.HTML.Pages;

namespace TestGroupJoin
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\forms\MultipleXLSXAssets\MultipleXLSXAssets\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\Test\TestLINQJoin\TestLINQJoin\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs


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

            dealercontacts.WithEach(
                x =>
                {
                    // { DealerContactText = hello , DealerText = world, DealerOtherText = !! }

                    new IHTMLPre {
                        new
                        {
                            x.contact.DealerContactText,
                            
                            x.c,

                            //x.other.DealerOtherText,
                        }
                    }.AttachToDocument();
                }
            );
        }

    }
}
