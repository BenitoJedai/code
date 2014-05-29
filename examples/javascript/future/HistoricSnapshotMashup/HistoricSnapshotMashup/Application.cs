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
using HistoricSnapshotMashup.Data;

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


        static List<HistoricsTheEntryRow> replay = new List<HistoricsTheEntryRow>();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // allow to be called from historic function
            service = this;


            new IStyle(IHTMLElement.HTMLElementEnum.head).display = IStyle.DisplayEnum.block;
            new IStyle(IHTMLElement.HTMLElementEnum.title).display = IStyle.DisplayEnum.block;

            // UI Automation

            // can we show svg as background?
            // we already had multimouse drawing dint we?

            document.onmousemove +=
                e =>
                {
                    // update the curent shadow element



                    // contentText wont work on non pseudo?
                    pseudo[index % 2].contentText =
                        new
                    {
                        random,
                        index,
                        x = e.CursorX,
                        y = e.CursorY,
                        z = window.history.length
                    }
                    .ToString();

                    pseudo[index % 2].style.SetLocation(
                      e.CursorX,
                      e.CursorY
                    );

                    pseudo[index % 2].style.position = IStyle.PositionEnum.@fixed;
                    pseudo[index % 2].style.color = "blue";

                    //current.memory
                };

            //document.documentElement.click();


            // do not resume if browser has historic data.
            if (window.history.length != 1)
                // cant we use console as body?
                //body.innerText = "will not try to resume, as there is history. start a new clean tab insead.";
                document.title = "will not try to resume, as there is history. start a new clean tab insead.";
            else

                // resume only works if a new tab is spawned without any history!
                Resume(
                    e =>
                    {
                        if (!e.Any())
                            return;

                        if (!window.confirm("resume state of " + e.Count()))
                        {
                            ForgetAll();
                            return;
                        }

                        replay = e.ToList();

                        foreach (var item in e)
                        {
                            // lets start to replay.
                            // ?

                            document.body.click();

                            break;
                        }
                        // fake
                        //Console.WriteLine("click " + new { e.x, e.y });

                    }
                );

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

                    long x = e.CursorX;
                    long y = e.CursorY;

                    if (replay.Any())
                    {
                        x = replay[0].x;
                        y = replay[0].y;
                    }

                    window.history.pushState(
                        state: new
                    {
                        index,

                        x,
                        y
                    },


                        yield: async scope =>
                        {
                            if (replay.Any())
                            {
                                service.data.Add(replay[0]);
                                replay.RemoveAt(0);
                            }
                            else
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

                            }

                            // make it stale
                            pseudo[index % 2].style.color = "gray";

                            Console.WriteLine("enter scope " + new { index });
                            index++;
                            // show without mouse hover!
                            pseudo[index % 2].style.color = "blue";

                            //pseudo[index % 2].contentText = new { index, z = window.history.length }.ToString();
                            pseudo[index % 2].contentText =
                                 new
                            {
                                service.random,
                                index,
                                x = scope.state.x,
                                y = scope.state.y,
                                z = window.history.length
                            }
                                     .ToString();

                            var pre = new IHTMLPre {
                                new
                                {
                                    service.random,
                                    index,
                                    x = scope.state.x,
                                    y = scope.state.y,
                                    z = window.history.length
                                }
                            }.AttachToDocument();


                            pre.style.SetLocation(
                                (int)scope.state.x,
                                (int)scope.state.y
                              );

                            pseudo[index % 2].style.SetLocation(
                                        (int)scope.state.x,
                                        (int)scope.state.y
                                      );


                            pseudo[index % 2].style.backgroundColor = "yellow";
                            await Task.Delay(300);
                            pseudo[index % 2].style.backgroundColor = "transparent";

                            // save it! bit late tho
                            await service.InsertNewHistoric();




                            await scope;

                            pre.Orphanize();

                            // so. what state did we remove?
                            var forget = service.data.Last();
                            // pop. does jsc tier split support stack?
                            service.data.RemoveAt(service.data.Count - 1);
                            service.Forget(forget);

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
