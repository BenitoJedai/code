using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebMethodXElementTransferExperiment;
using WebMethodXElementTransferExperiment.Design;
using WebMethodXElementTransferExperiment.HTML.Pages;

namespace WebMethodXElementTransferExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier

            #region invoke service.WebMethod2
            new IHTMLButton { innerText = "invoke service.WebMethod2, manually" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    service.WebMethod2(
                        new data { text = "calling service" },
                        (value, y) =>
                        {
                            //new { value.text }.ToString().ToDocumentTitle();

                            Native.document.body.Add(value);

                            #region calling the yield, manually
                            service.InternalWebServiceInvoke(
                                 value.yield_MethodToken,
                                 new data { text = "calling the yield, manually" },
                                 (yvalue, yy) =>
                                 {
                                     Native.document.body.Add(yvalue);
                                 }
                             );
                            #endregion


                        }
                    );
                }
            );
            #endregion


            #region invoke service.WebMethod2
            new IHTMLButton { innerText = "invoke service.WebMethod2, fixed yield" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    service.WebMethod2(
                        new data { text = "calling service" },
                        (value, y) =>
                        {
                            //new { value.text }.ToString().ToDocumentTitle();

                            Native.document.body.Add(value);



                            #region fix it, this needs to be done by jsc every time
                            if (value.yield_MethodToken != null)
                                if (value.yield == null)
                                {
                                    var zMethodToken = value.yield_MethodToken;
                                    value.yield_MethodToken = null;

                                    value.yield = (zstate, zyield) =>
                                    {
                                        service.InternalWebServiceInvoke(
                                            zMethodToken,
                                            zstate,
                                            zyield
                                        );
                                    };
                                }
                            #endregion



                            value.yield(
                                 new data { text = "calling the yield" },
                                    (yvalue, yy) =>
                                    {
                                        Native.document.body.Add(yvalue);
                                    }
                            );





                        }
                    );
                }
            );
            #endregion

            #region invoke service.WebMethod2
            new IHTMLButton { innerText = "invoke service.WebMethod2, fixed yield as async" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    service.WebMethod2(
                        new data { text = "calling service" },
                        (value, y) =>
                        {
                            //new { value.text }.ToString().ToDocumentTitle();

                            Native.document.body.Add(value);



                            #region fix it, this needs to be done by jsc every time
                            if (value.yield_MethodToken != null)
                                if (value.yield == null)
                                {
                                    var zMethodToken = value.yield_MethodToken;
                                    value.yield_MethodToken = null;

                                    value.yield = (zstate, zyield) =>
                                    {
                                        service.InternalWebServiceInvoke(
                                            zMethodToken,
                                            zstate,
                                            zyield
                                        );
                                    };
                                }
                            #endregion


                            X.AsAsync(value.yield).With(
                                async value_yield =>
                                {
                                    Console.WriteLine("enter async value_yield");
                                    await Task.Delay(500);
                                    Console.WriteLine("call 1 async value_yield");
                                    var call1 = await value_yield(new data { text = "async call 1" });
                                    Native.document.body.Add(call1.Item1);

                                    await Task.Delay(500);
                                    Console.WriteLine("call 2 async value_yield");
                                    var call2 = await value_yield(new data { text = "async call 2" });
                                    Native.document.body.Add(call2.Item1);

                                    Console.WriteLine("exit async value_yield");
                                }
                            );




                        }
                    );
                }
            );
            #endregion



            #region invoke service.WebMethod2
            new IHTMLButton { innerText = "invoke service.WebMethod2 asyncinvoke, manually" }.AttachToDocument().WhenClicked(
                btn =>
                {
                    service.WebMethod2(
                        new data { text = "calling service" },
                        (value, y) =>
                        {
                            //new { value.text }.ToString().ToDocumentTitle();

                            Native.document.body.Add(value);

                            #region calling the yield, manually
                            service.InternalWebServiceInvokeAsync(
                                 value.asyncyield_MethodToken,
                                 new data { text = "calling the asyncyield, manually" },
                                 (yvalue) =>
                                 {
                                     Native.document.body.Add(yvalue);
                                 }
                             );
                            #endregion


                        }
                    );
                }
            );
            #endregion

        }

    }
}
