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
using CSSTableSelector;
using CSSTableSelector.Design;
using CSSTableSelector.HTML.Pages;

namespace CSSTableSelector
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

            new IHTMLTable().With(
                table =>
                {
                    var tbody = table.AddBody();

                    var selectedRow = 0;
                    var selectedColumn = 0;

                    // Uncaught TypeError: Cannot call method 'agAABmASXzSNfpl_bHJLUOA' of null 
                    //tbody.css.first.child.adjacentSibling["*"].style.backgroundColor = "red";


                    //tbody.css[IHTMLElement.HTMLElementEnum.tr][() => selectedRow][IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "yellow";
                    tbody.css[() => selectedRow][IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "yellow";

                    //tbody.css[() => selectedRow][IHTMLElement.HTMLElementEnum.td].siblings.style.backgroundColor = "green";
                    //tbody.css[() => selectedRow][IHTMLElement.HTMLElementEnum.td].adjacentSibling.style.backgroundColor = "red";

                    //tbody.css[() => selectedRow][">td"].style.backgroundColor = "yellow";
                    tbody.css[IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td][() => selectedColumn].style.backgroundColor = "yellow";

                    //tbody.css[">tr"][() => selectedColumn].style.backgroundColor = "cyan";


                    // { selectorText = >td, rule = [style-id="0"] > tr:nth-child(1) > td } 
                    //var header = tbody.css[IHTMLElement.HTMLElementEnum.tr][1][IHTMLElement.HTMLElementEnum.td];

                    tbody.css[0][IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "gray";

                    tbody.css[0][0].style.backgroundColor = "red";



                    tbody.css[() => selectedRow][() => selectedColumn].style.backgroundColor = "red";


                    tbody.css.odd.style.backgroundColor = "cyan";

                    //tbody.css[1][1].siblings["(:nth-child(8))"].style.backgroundColor = "red";

                    //tbody.css[1][IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "gray";



                    for (int y = 0; y < 22; y++)
                    {
                        var tr = tbody.AddRow();

                        for (int x = 0; x < 22; x++)
                        {
                            var td = tr.AddColumn("?");

                            var p = new { x, y };

                            td.onmousemove +=
                                delegate
                                {
                                    Native.document.title = p.ToString();

                                    selectedRow = p.y;
                                    selectedColumn = p.x;
                                };
                        }
                    }

                }
             ).AttachToDocument();
        }

    }
}
