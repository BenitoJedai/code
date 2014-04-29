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
using TestLINQJoin;
using TestLINQJoin.Design;
using TestLINQJoin.HTML.Pages;

namespace TestLINQJoin
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140429


            var DealerContact = new[] { 
                new { DealerId = 2, DealerContactText = ""},
                new { DealerId = 3, DealerContactText = "hello "},
                new { DealerId = 4, DealerContactText = ""}
            };

            var Dealer = new[] { 
                new { ID = 1, DealerText = ""},
                new { ID = 3, DealerText = "world"},
                new { ID = 5, DealerText = ""}
            };


            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static System.Linq.Enumerable.Join(
            // System.Collections.Generic.IEnumerable`1[[<>f__AnonymousType$234$0`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],
            // [System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[<>f__AnonymousType$239$1`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Func`2[[<>f__AnonymousType$234$0`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Func`2[[<>f__AnonymousType$239$1`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], System.Func`3[[<>f__AnonymousType$234$0`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[<>f__AnonymousType$239$1`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[<>f__AnonymousType$244$2`2[[<>f__AnonymousType$234$0`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[<>f__AnonymousType$239$1`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], TestLINQJoin.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]])]

            var dealercontacts = from contact in DealerContact
                                 join dealer in Dealer on contact.DealerId equals dealer.ID

                                 // new viewrow { x = ?, }

                                 select new { contact, dealer };

            dealercontacts.WithEach(
                x =>
                {
                    new IHTMLPre {
                        new
                        {
                            x.contact.DealerContactText,
                            x.dealer.DealerText,
                        }
                    }.AttachToDocument();
                }
            );

        }

    }
}
