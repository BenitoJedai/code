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
using HistoryStatesViaWebService;
using HistoryStatesViaWebService.Design;
using HistoryStatesViaWebService.HTML.Pages;
using System.Linq.Expressions;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Data;

namespace HistoryStatesViaWebService
{
    [Obsolete("jsc, please support await for generic methods")]
    public static class X
    {
        //public async static Task<T> AttachToHead<T>(this Task<T> that) where T : IHTMLElement
        public async static Task<IHTMLLink> AttachToHead(this Task<IHTMLLink> that)
        {
            var x = await that;

            x.AttachToHead();

            return x;
        }

        //public async static Task<T> WaitFor<T>(this T that, Task task)
        public async static Task<IHTMLLink> WaitFor(this IHTMLLink that, Task task)
        {
            await task;

            return that;
        }


    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public static void FlashTitle()
        {
            #region flash yellow
            new IStyleSheet().With(
                async css =>
                {
                    css["title"].style.color = "yellow";

                    css.Owner.AttachToHead();


                    await Task.Delay(300);

                    css.Owner.Orphanize();
                }
            );
            #endregion
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // skip transitions at reload but use them later
            new WithTransitionStyle().Content.WaitFor(Native.window.requestAnimationFrameAsync).AttachToHead();



            FlashTitle();


            page.Enter.WhenClicked(
                async delegate
                {
                    // we are in the click handler
                    // the layout was loaded in default state

                    //    _06000002_field_state:
                    //_06000002_field_reason:

                    // jsc why cannot this be null?
                    this.state = new ApplicationState { title = "???" };

                    var newstate = await this.DoEnter();

                    // ready to transition to a new 
                    Native.window.history.pushState(
                        newstate.state,
                        async scope =>
                        {
                            Console.WriteLine("at DoEnter " + new { scope.state });

                            // now what?
                            // we get here either by a click or a reload

                            #region do
                            // we need a copy of childnodes!
                            var oldpagenodes = Native.document.body.childNodes.ToArray();

                            var foopage = new Foo();

                            Native.document.body.Clear();
                            Native.document.body.appendChild(
                                 foopage.body.childNodes.ToArray()
                            );
                            #endregion

                            // should we automate this somehow?
                            var fooapp = new FooApplication(foopage, scope);

                            await scope;

                            #region undo
                            Native.document.body.Clear();
                            Native.document.body.appendChild(
                                 oldpagenodes
                            );

                            // http://connect.microsoft.com/VisualStudio/feedback/details/534869/arrow-keys-stop-working-in-editor
                            // are we sure the state has been restored?

                            // show our intent to reload
                            IStyleSheet.Default["title"].style.color = "red";

                            // wait for any animation, sound to complete, then wipe
                            // what if we move in time during the delay?
                            await Task.Delay(500);

                            Native.document.location.reload();
                            #endregion

                        }
                    );

                }
            );
        }

        public sealed class FooApplication : ApplicationWebService
        {
            static DataTable newscope_data;

            public FooApplication(IFoo foopage, HistoryScope<ApplicationState> fooscope)
            {
                // init state! this will be sent to server at every new web call.
                this.state = fooscope.state;
                FlashTitle();

                // how do we know when we need to shut down?
                // if we go back in time we need to shut down
                // should we have a Dispose method?
                // can we await on scope?



                Native.document.title = state.title;
                Native.document.body.style.borderLeft = "1em red solid";


                foopage.output.innerText = new { this.state.title }.ToString();

                foopage.EnterData.WhenClicked(
                    async delegate
                    {
                        this.reason = "foopage.EnterData.WhenClicked";

                        // what if server has gone away?
                        var newscope = await this.DoEnterData();

                        newscope_data = newscope.state.data;

                        // DataTable is not correctly stored in HistoryAPI yet
                        newscope.state.data = null;

                        // ready to transition to a new 
                        Native.window.history.pushState(
                            newscope.state,
                            async scope =>
                            {
                                Console.WriteLine("at DoEnterData");



                                scope.state.data = newscope_data;
                                newscope_data = null;

                                // now what?
                                // we get here either by a click or a reload

                                #region do
                                // we need a copy of childnodes!
                                var oldpagenodes = Native.document.body.childNodes.ToArray();

                                var goopage = new Goo();

                                Native.document.body.Clear();

                                #endregion

                                Native.document.body.appendChild(
                                    goopage.body.childNodes.ToArray()
                                );

                                // should we automate this somehow?
                                var gooapp = new GooApplication(goopage, scope);

                                // we only use the data once?
                                //scope.state.data = null;


                                await scope;
                                Native.document.body.Clear();


                                #region undo

                                Native.document.body.appendChild(
                                     oldpagenodes
                                );

                                // http://connect.microsoft.com/VisualStudio/feedback/details/534869/arrow-keys-stop-working-in-editor
                                // are we sure the state has been restored?

                                // show our intent to reload
                                IStyleSheet.Default["title"].style.color = "red";

                                // wait for any animation, sound to complete, then wipe
                                // what if we move in time during the delay?
                                await Task.Delay(500);

                                Native.document.location.reload();
                                #endregion

                            }
                        );
                    }
                );



                //Native.document.body.style[await scope].borderLeft = "0.3em yellow solid";

                fooscope.With(
                    async delegate
                    {
                        await fooscope;



                        // time to undo
                        Native.document.body.style.borderLeft = "0.3em yellow solid";
                    }
                );
            }


            public sealed class GooApplication : ApplicationWebService
            {
                public GooApplication(IGoo goopage, HistoryScope<ApplicationState> gooscope)
                {
                    // init state! this will be sent to server at every new web call.
                    this.state = gooscope.state;
                    FlashTitle();


                    Native.document.title = state.title;
                    Native.document.body.style.borderTop = "1em red solid";

                    Action ShowDataTable =
                        delegate
                        {
                            goopage.output.Clear();

                            var f = new Form
                            {
                                Text = new { this.state.data.TableName }.ToString(),
                                ControlBox = false,
                                ShowIcon = false,

                                //WindowState = FormWindowState.Maximized
                            };

                            new DataGridView
                            {
                                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]
                                //BorderStyle = BorderStyle.Fixed3D 
                                //AutoSize = true,
                                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,

                                DataSource = this.state.data,
                                Dock = DockStyle.Fill
                            }.AttachTo(f);

                            // do we need this?

                            f.GetHTMLTarget().AttachTo(goopage.output);

                            f.Show();

                            f.WindowState = FormWindowState.Maximized;
                            f.PopupInsteadOfClosing(
                                HandleFormClosing: false

                                // , 
                                // does not play well with maximized yet
                                //SpecialNoMovement: true
                                );

                        };

                    if (this.state.data == null)
                    {
                        // can we remove this from history then?

                        new IHTMLButton { innerText = "a page reload makes us forget DataTable. go back and get new data!" }.AttachTo(goopage.output).WhenClicked(
                            delegate
                            {

                                Native.window.history.back();
                            }
                        );

                        new IHTMLBreak().AttachTo(goopage.output);

                        new IHTMLButton { innerText = "or get new data, if the server is available" }.AttachTo(goopage.output).WhenClicked(
                            async delegate
                            {
                                Native.document.body.style.borderTop = "1em black solid";

                                this.reason = "page reload makes us forget DataTable";
                                //this.state = (await this.DoEnterData()).state;

                                var x = await this.DoEnterData();

                                this.state = x.state;

                                Native.document.body.style.borderTop = "1em red solid";

                                ShowDataTable();
                            }
                        );
                    }
                    else
                    {
                        ShowDataTable();
                    }

                    #region undo
                    gooscope.With(
                         async delegate
                         {
                             await gooscope;



                             // time to undo
                             Native.document.body.style.borderTop = "0.3em yellow solid";
                         }
                     );
                    #endregion

                }
            }
        }
    }
}
