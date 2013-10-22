using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSSContentDataSource;
using CSSContentDataSource.Design;
using CSSContentDataSource.HTML.Pages;
using System.Data;

namespace CSSContentDataSource
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
            var data = default(DataTable);
            var Current = default(DataRow);

            var pitems = new IHTMLDiv[0];


            Action CurrentChanged = delegate
            {
                Console.WriteLine("CurrentChanged");

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows.AsEnumerable().ElementAt(i) == Current)
                    {
                        pitems[i].style.boxShadow = "blue 0 0 18px inset";
                    }
                    else
                    {
                        pitems[i].style.boxShadow = "gray 0 0 8px inset";

                    }
                }

                data.Columns.AsEnumerable().WithEach(
                    c =>
                    {
                        var selector = "[data-column='" + c.ColumnName + "']";

                        Console.WriteLine(new { selector });

                        // stackoverflow.com/questions/10777684/how-to-use-queryselectorall-only-for-elements-that-have-a-specific-attribute-set
                        // http://stackoverflow.com/questions/8694460/queryselectorall-get-all-tags-that-have-an-attribute-set

                        if (Current == null)
                        {
                            page.DesignTimeStyle.StyleSheet.disabled = false;
                        }
                        else
                        {
                            page.DesignTimeStyle.StyleSheet.disabled = true;

                            Native.document.body.querySelectorAll(selector).WithEach(
                                node =>
                                {
                                    var x = (IHTMLElement)node;

                                    var value = Current[c];

                                    x.css.before.style.content = "'" + new { selector, value }.ToString().Replace("'", "\\'") + "'";

                                }
                            );
                        }
                    }
                );
            };

            Native.window.requestAnimationFrame +=
                async delegate
                {

                    var scope_data = await this.DoEnterData();
                    data = scope_data;


                    //Native.document.body.style.position = IStyle.PositionEnum.@fixed;

                    // per browser?
                    var step = 40;

                    page.beancounter.style.height = (data.Rows.Count * step) + "px";


                    var p = new Parallax();


                    var pitem = p.item.Orphanize();

                    pitems = data.Rows.AsEnumerable().Select(
                        x =>
                        {
                            var y = new Parallax().item;

                            y.AttachTo(p.Container);

                            y.style.height = (100.0 / data.Rows.Count) + "%";

                            return y;
                        }
                    ).ToArray();

                    p.AttachToDocument();

                    Current = data.Rows.AsEnumerable().FirstOrDefault();
                    CurrentChanged();

                    Native.document.title = new { data.Rows.Count }.ToString();


                    //Native.window.onframe +=
                    //    delegate
                    //    {
                    //        p.Container.style.height =
                    //            Native.document.body.scrollHeight + "px";
                    //    };


                    Native.window.onscroll +=
                    delegate
                    {

                        var scrollTop = Math.Max(
                            // why the difference?
                            // IE
                            Native.document.documentElement.scrollTop,
                            // chrome
                            Native.document.body.scrollTop
                        );

                        Native.document.title = new { scrollTop }.ToString();

                        var offset = (int)Math.Floor((double)scrollTop / step);

                        // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRowCollection.get_Item(System.Int32)]
                        Current = data.Rows.AsEnumerable().ElementAt(offset);
                        CurrentChanged();


                    };

                };
        }

    }
}
