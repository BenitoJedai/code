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
using HistoricSnapshotMashup;
using HistoricSnapshotMashup.Design;
using HistoricSnapshotMashup.HTML.Pages;
using ScriptCoreLib.JavaScript.Native;

namespace HistoricSnapshotMashup
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static int index;

        static ApplicationWebService service;

        // public static implicit operator CSSStyleRuleMonkier(CSSStyleRuleMonkier[] collection);
        static CSSStyleRuleMonkier[] pseudo = new[]
        {
            body.css.before,
            body.css.after
        };

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // allow to be called from historic function
            service = this;



            // UI Automation

            // can we show svg as background?
            // we already had multimouse drawing dint we?

            document.onmousemove +=
                e =>
                {
                    // update the curent shadow element



                    // contentText wont work on non pseudo?
                    pseudo[index % 2].contentText =
                        new { index, x = e.CursorX, y = e.CursorY, z = window.history.length }
                    .ToString();

                    pseudo[index % 2].style.SetLocation(
                      e.CursorX,
                      e.CursorY
                    );

                    pseudo[index % 2].style.position = IStyle.PositionEnum.@fixed;
                    pseudo[index % 2].style.color = "blue";

                    //current.memory
                };

            document.onclick +=
                e =>
                {

                    // did jsc do background compilation and update our snapshot
                    // and add the new version into AB testing view by jsc zombie server?
                    // or did I have to hit F5 again?

                    // can we flip the new shadow dom element into view?

                    // Error	2	No overload for method 'pushState' takes 2 arguments	X:\jsc.svn\examples\javascript\future\HistoricSnapshotMashup\HistoricSnapshotMashup\Application.cs	84	21	HistoricSnapshotMashup

                    //HistoryExtensions.pushState(
                    //    h: window.history,

                    window.history.pushState(
                        state: new
                    {
                        index,

                        x = e.CursorX,
                        y = e.CursorY
                    },


                        yield: async scope =>
                        {
                            // prepare new data to be saved
                            service.data.Add(
                                new Data.HistoricsTheEntryRow
                            {
                                x = scope.state.x,
                                y = scope.state.y,
                                index = scope.state.index,
                            }
                            );



                            // make it stale
                            pseudo[index % 2].style.color = "gray";

                            Console.WriteLine("enter scope " + new { index });
                            index++;
                            // show without mouse hover!
                            pseudo[index % 2].style.color = "blue";

                            //pseudo[index % 2].contentText = new { index, z = window.history.length }.ToString();
                            pseudo[index % 2].contentText =
                                 new { index, x = scope.state.x, y = scope.state.y, z = window.history.length }
                             .ToString();

                            pseudo[index % 2].style.SetLocation(
                                scope.state.x,
                                scope.state.y
                              );


                            pseudo[index % 2].style.backgroundColor = "yellow";
                            await Task.Delay(300);
                            pseudo[index % 2].style.backgroundColor = "transparent";

                            // save it! bit late tho
                            await service.InsertNewHistoric();



                            await scope;

                            // so. what state did we remove?

                            Console.WriteLine("exit scope " + new { index });

                            // this is from the future then? click forward to reactivate?
                            pseudo[index % 2].style.color = "red";
                            // undo???
                            // exclusive?
                            index--;

                            pseudo[index % 2].contentText = new { index, z = window.history.length }.ToString();

                            // show without mouse hover!
                            pseudo[index % 2].style.color = "blue";
                            pseudo[index % 2].style.transition = "";
                            pseudo[index % 2].style.backgroundColor = "cyan";
                            await Task.Delay(300);
                            pseudo[index % 2].style.backgroundColor = "transparent";
                        }
                    );



                };
        }

    }
}
