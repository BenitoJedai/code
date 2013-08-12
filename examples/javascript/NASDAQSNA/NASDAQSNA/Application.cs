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
using System.Xml.Linq;
using NASDAQSNA.Design;
using NASDAQSNA.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;

namespace NASDAQSNA
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
        public Application(IDefault  page)
        {
            @"Hello world".ToDocumentTitle();

            var output = new IHTMLTextArea().AttachToDocument();

            var gexf = new XElement("gexf");

            gexf.Add(
                new XElement("meta",
                    new XElement("creator", "jsc"),
                    new XElement("description", "NASDAQSNA")
                )
            );

            var graph = new XElement("graph",
                new XAttribute("mode", "static"),
                new XAttribute("defaultedgetype", "directed")
            );

            gexf.Add(graph);

            var nodes = new XElement("nodes");

            graph.Add(nodes);

            var edges = new XElement("edges");

            graph.Add(edges);

            output.value = gexf.ToString();
            output.onfocus +=
                delegate
                {
                    output.value = gexf.ToString();
                };
            output.onmouseover +=
             delegate
             {
                 output.value = gexf.ToString();
             };

            var NumericNodeIdLookup = new List<string>();

            Func<string, int> GetNumericNodeId =
                k =>
                {
                    if (!NumericNodeIdLookup.Contains(k))
                        NumericNodeIdLookup.Add(k);


                    return NumericNodeIdLookup.IndexOf(k);
                };

            #region AddRelatedCompanies
            Action<IHTMLDiv, string, Action> AddRelatedCompanies = null;

            AddRelatedCompanies =
                (c, qid, done) =>
                {
                    var nqid = GetNumericNodeId(qid).ToString();
                    var cc = new IHTMLDiv();

                    cc.style.marginLeft = "2em";

                    // Send data from JavaScript to the server tier
                    service.GetRelatedCompanies(
                        qid,
                        (id, CompanyName, Price) =>
                        {
                            var nid = GetNumericNodeId(id).ToString();

                            if (nodes.Elements().Any(k => k.Attribute("id").Name.LocalName == nid))
                            {
                            }
                            else
                            {
                                nodes.Add(
                                    new XElement("node",
                                     new XAttribute("id", nid),
                                     new XAttribute("label", CompanyName)
                                    )
                                );
                            }



                            var btn = new IHTMLButton
                            {
                                innerText = id + " " + CompanyName + " " + Price
                            };

                            btn.style.display = IStyle.DisplayEnum.block;

                            if (qid == id)
                            {
                                btn.disabled = true;

                                if (c == null)
                                {
                                    btn.AttachToDocument();
                                    cc.AttachToDocument();
                                }
                                else
                                {
                                    //btn.AttachTo(c);
                                    cc.AttachTo(c);
                                }

                                if (done != null)
                                    done();
                            }
                            else
                            {
                                edges.Add(
                                    new XElement("edge",
                                     new XAttribute("source", nqid),
                                     new XAttribute("target", nid)
                                    )
                                );

                                btn.AttachTo(cc);

                                var ccc = new IHTMLDiv();

                                ccc.AttachTo(cc);

                                int cx = 0;

                                ccc.ToggleVisible();


                                btn.onmouseover +=
                                    delegate
                                    {
                                        if (cx == 0)
                                        {
                                            btn.style.color = JSColor.Red;

                                            AddRelatedCompanies(ccc, id,
                                                delegate
                                                {
                                                    btn.style.color = JSColor.Blue;

                                                }
                                            );
                                        }


                                        cx++;
                                    };

                                btn.onclick +=
                                    delegate
                                    {
                                        if (cx == 0)
                                        {
                                            btn.style.color = JSColor.Red;

                                            AddRelatedCompanies(ccc, id,
                                                delegate
                                                {
                                                    btn.style.color = JSColor.Blue;

                                                }
                                            );
                                        }


                                        cx++;

                                        ccc.ToggleVisible();

                                    };
                            }
                        }
                    );
                };
            #endregion

            AddRelatedCompanies(null, "NASDAQ:FB", null);
            AddRelatedCompanies(null, "NASDAQ:GOOG", null);
        }

    }
}
