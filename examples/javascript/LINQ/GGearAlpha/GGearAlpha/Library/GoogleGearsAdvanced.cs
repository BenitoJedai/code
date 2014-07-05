﻿using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Extensions;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using ScriptCoreLib.Query.Experimental;

namespace GGearAlpha.js
{
    using textarea = IHTMLTextArea;
    using span = IHTMLSpan;
    using div = IHTMLDiv;
    using img = IHTMLImage;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using ScriptCoreLib.JavaScript.Runtime;
    using System.Linq;
    using System;
    using System.Data.SQLite;



    public class GoogleGearsAdvanced
    {
        public GoogleGearsAdvanced()
        {

            //Native.Window.alert("alert 1");

            CreateStyles();

            //Native.Window.alert("alert 2");

            var shadow = new div { className = "shadow" };
            var toolbar = new div { className = "toolbar" };
            var workspace0 = new div { className = "workspace0" };
            var workspace = new div { className = "workspace" };

            workspace0.appendChild(
                new div("You can create new postcards by clicking on the background image. You can drag those postcards by their borders. You can use your mouse wheel to zoom in or out, too. All postcards will be saved via Google Gears. Doubleclick a postcard to delete it."));

            shadow.appendChild(workspace0, workspace, toolbar);



            toolbar.innerHTML = "<a href='http://gears.google.com/'>get google gears</a> | <a href='http://jsc.sourceforge.net/'>jsc homepage</a> | <a href='http://zproxy.wordpress.com/'>blog</a>";
            //AppendError(workspace0, "loading 1...");


            var BackgroundTween = new TweenDataDouble();


            BackgroundTween.ValueChanged +=
                delegate
            {
                workspace.style.Opacity = BackgroundTween.Value;
            };

            BackgroundTween.Value = 0.5;

            workspace.onmouseover +=
                delegate
            {
                BackgroundTween.Value = 0;
            };

            workspace.onmouseout +=
                  delegate
            {
                BackgroundTween.Value = 0.5;
            };

            //AppendError(workspace0, "loading 2...");

            new HTML.Images.FromAssets.postcard_alpha().With(
                postcard =>

            new HTML.Images.FromAssets.postcard_alpha_200_unfocus().With(
                postcard200 =>
                {
                    // at this point we have the image and we know how large it is
                    //AppendError(workspace0, "loading 3...");


                    // X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs
                    // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
                    var cc = new SQLiteConnection();
                    cc.Open();
                    // Cannot read property 'transaction' of null"


                    new xPostcard().Create(cc);

                    //catch (System.Exception exc)
                    //{
                    //    var err_msg = exc.Message;
                    //    AppendError(workspace0, err_msg);
                    //}

                    //AppendError(workspace0, "loading 4...");

                    #region Spawn
                    Func<PostcardEntry, Postcard> Spawn =
                        template =>
                        {
                            var p2 = new Postcard(postcard, postcard200, workspace);

                            p2.CurrentDrag.Position.X = template.X;
                            p2.CurrentDrag.Position.Y = template.Y;
                            p2.AttachTo(shadow);

                            System.Console.WriteLine("new: " + p2.Id);

                            if (template.Text == null)
                            {
                                template.Text = new[] { "Hello!", "Yo!", "Whuzza!", "Howdy!", "Cheers!" }.Randomize().First() + "\nWrite something here!";
                            }

                            if (template.Zoom100 == 0)
                            {
                                template.Zoom100 = (int)System.Math.Floor((new System.Random().NextDouble() + 0.5) * 100);
                            }

                            p2.Content.value = template.Text;
                            p2.SetZoom(template.Zoom100 / 100);

                            if (template.Id == null)
                            {
                                try
                                {
                                    new xPostcard().Insert(cc,
                                        new xPostcardRow
                                    {
                                        Id = p2.Id,
                                        Text = p2.Text,
                                        Zoom100 = (int)System.Math.Floor(p2.Zoom * 100),
                                        X = p2.CurrentDrag.Position.X,
                                        Y = p2.CurrentDrag.Position.Y
                                    }
                                    );
                                }
                                catch (System.Exception exc)
                                {
                                    AppendError(workspace0, exc.Message);
                                }
                            }
                            else
                            {
                                p2.Id = template.Id;
                            }

                            p2.DoubleClick +=
                                delegate
                            {
                                System.Console.WriteLine("dispose: " + p2.Id);
                                p2.Dispose();

                                //db.execute("delete from Postcards where Id = ?", p2.Id);

                                Console.WriteLine("delete:");
                                (
                                     from x in new xPostcard()
                                     where x.Id == p2.Id
                                     select x
                                 ).Delete(cc);

                                //new xPostcard().Where(x => x.Id == p2.Id).Delete(cc);
                                Console.WriteLine("delete: done?");

                            };

                            p2.Changed +=
                                delegate
                            {
                                System.Console.WriteLine("changed: " + new PostcardEntry { Id = p2.Id, Text = p2.Text, Zoom100 = (int)System.Math.Floor(p2.Zoom * 100), X = p2.CurrentDrag.Position.X, Y = p2.CurrentDrag.Position.Y }.ToString());

                                Console.WriteLine("delete:");

                                new xPostcard().Where(x => x.Id == p2.Id).Delete(cc);

                                new xPostcard().Insert(
                                    cc, new xPostcardRow
                                {
                                    Id = p2.Id,
                                    Text = p2.Text,
                                    Zoom100 = (int)System.Math.Floor(p2.Zoom * 100),
                                    X = p2.CurrentDrag.Position.X,
                                    Y = p2.CurrentDrag.Position.Y
                                }
                                );
                            };

                            System.Console.WriteLine("item: " + template.ToString());




                            p2.ZoomAndMove();

                            p2.CurrentDrag.DragStart +=
                                delegate
                            {
                                p2.AttachTo(shadow);
                            };

                            return p2;
                        };
                    #endregion


                    workspace.onclick +=
                        delegate (IEvent ev)
                    {
                        Spawn(
                            new PostcardEntry { X = ev.CursorX, Y = ev.CursorY }
                        );

                    };

                    #region LoadFromDatabase
                    new { }.With(
                        async delegate
                    {
                        var query = from Data in new xPostcard()
                                    select Data;

                        foreach (var v in await query.AsEnumerableAsync(cc))
                        {
                            Spawn(
                                new PostcardEntry
                            {
                                Id = v.Id,
                                Text = v.Text,
                                X = (int)v.X,
                                Y = (int)v.Y,
                                Zoom100 = Math.Max(60, (int)v.Zoom100),

                            }
                            );
                        }
                    }
                    );
                    #endregion
            }
            )
            );

            Native.Document.body.appendChild(shadow);
        }







        private static void CreateStyles()
        {
            IStyleSheet.Default.AddRule("html",
                r =>
                {
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.margin = "0";
                    r.style.padding = "0";
                }
            );

            IStyleSheet.Default.AddRule("body",
                r =>
                {
                    new HTML.Images.FromAssets.back().ToBackground(r.style);
                    //r.style.background = "url(assets/GearsDemo1/back.jpg)";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.margin = "0";
                    r.style.padding = "0";
                }
            );

            IStyleSheet.Default.AddRule(".shadow",
                r =>
                {

                    new HTML.Images.FromAssets.shadow().ToBackground(r.style);
                    r.style.backgroundRepeat = "repeat-x";
                    //r.style.background = "url(assets/GearsDemo1/shadow.png) repeat-x";
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );


            IStyleSheet.Default.AddRule(".workspace0",
                r =>
                {


                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;

                    new HTML.Images.FromAssets.power2().ToBackground(r.style, repeat: false);
                    //r.style.background = "url(assets/GearsDemo1/power2.png) no-repeat";
                }
            );

            IStyleSheet.Default.AddRule(".workspace0 div",
                r =>
                {
                    r.style.paddingTop = "4em";
                    r.style.paddingBottom = "4em";
                    r.style.paddingLeft = "2em";
                    r.style.paddingRight = "2em";

                }
            );

            IStyleSheet.Default.AddRule(".workspace",
                r =>
                {
                    r.style.cursor = IStyle.CursorEnum.pointer;
                    r.style.background = "black";
                    r.style.Opacity = 0;
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.fontSize = "2em";
                }
            );



            IStyleSheet.Default.AddRule(".toolbar",
                r =>
                {

                    new HTML.Images.FromAssets.shadow_bottom_100().ToBackground(r.style);
                    r.style.backgroundRepeat = "repeat-x";
                    r.style.backgroundPosition = "bottom";
                    //r.style.background = "url(assets/GearsDemo1/shadow-bottom-100.png) repeat-x bottom";
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.bottom = "0px";
                    r.style.width = "100%";
                    r.style.padding = "4em";
                    r.style.paddingBottom = "1em";
                    r.style.paddingTop = "1em";

                }
            );

            IStyleSheet.Default.AddRule(".toolbar a",
                r =>
                {

                    r.style.color = "white";

                }
            );

            IStyleSheet.Default.AddRule(".toolbar a:hover",
                r =>
                {

                    r.style.color = "red";

                }
            );

            IStyleSheet.Default.AddRule(".error",
                r =>
                {
                    r.style.padding = "1em";
                    r.style.color = "red";
                    r.style.backgroundColor = "white";
                }
            );


            IStyleSheet.Default.AddRule(".content",
                r =>
                {
                    r.style.border = "1px solid gray";
                    r.style.backgroundColor = "transparent";
                    new HTML.Images.FromAssets.shadow_bottom_100().ToBackground(r.style);
                    r.style.backgroundRepeat = "repeat-x";
                    r.style.backgroundPosition = "bottom";
                    //r.style.background = "url(assets/GearsDemo1/shadow-bottom-100.png) repeat-x bottom";
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.overflow = IStyle.OverflowEnum.auto;
                }
            );
            IStyleSheet.Default.AddRule(".content", "background-attachment: fixed;", 0);

            IStyleSheet.Default.AddRule(".content:focus",
                r =>
                {
                    new HTML.Images.FromAssets.green_bottom().ToBackground(r.style);

                    // http://www.w3schools.com/cssref/pr_background-repeat.asp
                    r.style.backgroundRepeat = "repeat-x";
                    r.style.backgroundPosition = "bottom";

                    //r.style.background = "url(assets/GearsDemo1/green-bottom.png) repeat-x bottom";
                }
            );
        }

        private static void AppendError(IHTMLDiv workspace0, string err_msg)
        {
            var err = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre, err_msg) { className = "error" };


            workspace0.appendChild(err);
        }

        #region dump
        static void DualDump(object left, object right)
        {
            var t = new IHTMLTable();
            var b = t.AddBody();
            var r = b.AddRow();

            Dump(left, r.AddColumn(), right);
            Dump(right, r.AddColumn(), left);

            t.AttachToDocument();
        }

        private static IHTMLElement Dump(object xs, IHTMLElement to, object diff)
        {

            var c = new IHTMLDiv();

            c.style.backgroundColor = Color.White;
            c.style.border = "1px solid gray";
            c.style.padding = "1em";
            c.style.fontFamily = IStyle.FontFamilyEnum.Consolas;

            var ttx = new IHTMLDiv(xs.ToString());

            c.appendChild(ttx);

            var dx = Expando.Of(diff);

            foreach (var v in Expando.Of(xs).GetMembers())
            {
                var tt = default(IHTMLDiv);
                var ok = true;

                if (dx != null)
                {
                    if (dx.Contains(v.Name))
                    {
                        if (dx[v.Name] == v.Self)
                            ok = false;
                    }
                }

                if (ok)
                {
                    if (v.Self.IsFunction)
                    {
                        tt = new IHTMLDiv(v.Self.TypeString + " " + v.Name);
                        tt.style.color = Color.Red;

                    }
                    else if (v.Self.IsObject)
                    {
                        tt = new IHTMLDiv(v.Self.TypeString + " " + v.Name + " = " + v.Self.ToString());
                        tt.style.color = Color.Blue;
                    }
                    else
                        tt = new IHTMLDiv(v.Self.TypeString + " " + v.Name + " = " + v.Self.ToString());

                    c.appendChild(tt);
                }
            }

            to.appendChild(c);

            return to;

        }
        #endregion

        [Script]
        sealed partial class PostcardEntry : ISerializedObject
        {

            public string Text;
            public int Zoom100 = 0;
            public int X;
            public int Y;

            public override string ToString()
            {
                return "{Id = " + Id + ", Text = " + Text + ", Zoom100 = " + Zoom100 + ", X = " + X + ", Y = " + Y + "}";
            }
        }


        partial class PostcardEntry // mixin
        {
            public string Id;


            public /* override */ string VirtualId
            {
                get
                {
                    return this.Id;
                }
                set
                {
                    this.Id = value;
                }
            }
        }

        partial class Postcard // mixin
        {
            public string Id;

            public override string VirtualId
            {
                get
                {
                    return this.Id;
                }
                set
                {
                    this.Id = value;
                }
            }
        }


        [Script]
        partial class Postcard : SerializedObject
        {


            readonly img postcard;
            readonly img postcard200;

            public readonly div layer;
            public readonly img img0;

            public readonly textarea Content;
            public readonly DragHelper CurrentDrag;

            public event Action Changed;

            private double _Zoom = 0.75;

            public double Zoom
            {
                get { return _Zoom; }
                set
                {
                    _Zoom = value;

                    ZoomAndMove();

                    Changed.SafeInvoke();
                }
            }

            public string Text
            {
                get
                {
                    return Content.value;
                }
                set
                {
                    Content.value = value;

                    Changed.SafeInvoke();
                }
            }



            public void ZoomAndMove()
            {
                img0.Zoom(Zoom, postcard);
                img0.SetCenteredLocation(CurrentDrag.Position);

                layer.Zoom(Zoom, postcard);
                layer.SetCenteredLocation(CurrentDrag.Position);

                var x = postcard.width * Zoom / 2;
                var y = postcard.height * Zoom / 2;

                Content.style.fontSize = Zoom + "em";


                Content.style.SetLocation(
                    (int)System.Math.Floor(CurrentDrag.Position.X - x + (x * 0.05)),
                    (int)System.Math.Floor(CurrentDrag.Position.Y - y + (x * 0.3)),
                    (int)System.Math.Floor(x * 1.5), (int)System.Math.Floor(y * 1.3)
                    );
            }

            public event Action DoubleClick;

            public Postcard(img postcard, img postcard200, div workspace)
            {

                this.postcard = postcard;
                this.postcard200 = postcard200;


                this.layer = new div();
                this.img0 = (img)postcard200.cloneNode(false);

                layer.style.backgroundColor = "red";

                layer.style.Opacity = 0;

                layer.onmouseover +=
                    delegate
                {
                    this.img0.src = postcard.src;
                };

                layer.onmouseout +=
                    delegate
                {
                    this.img0.src = postcard200.src;
                };

                Content = new textarea { className = "content" };

                Content.onmouseover +=
                    delegate
                {
                    this.img0.src = postcard.src;
                };

                Content.onmouseout +=
                    delegate
                {
                    this.img0.src = postcard200.src;
                };

                Content.onchange +=
                    delegate
                {
                    Changed.SafeInvoke();
                };


                this.CurrentDrag = new DragHelper(layer);



                layer.style.border = "1px solid red";

                layer.onselectstart += Native.DisabledEventHandler;

                layer.ondblclick +=
                    delegate
                {
                    if (DoubleClick != null)
                        DoubleClick();
                };

                layer.onmousewheel +=
                    delegate (IEvent ev)
                {
                    var z = Zoom;

                    z += (0.05 * ev.WheelDirection);

                    z = SetZoom(z);
                    ZoomAndMove();
                };


                CurrentDrag.DragMove +=
                    delegate
                {
                    ZoomAndMove();

                    Changed.SafeInvoke();
                };

                CurrentDrag.Enabled = true;


            }

            public double SetZoom(double z)
            {
                if (z < 0.3)
                    z = 0.3;

                if (z > 2)
                    z = 2;

                _Zoom = z;
                return z;
            }

            public void Dispose()
            {
                img0.Orphanize();
                layer.Orphanize();
                Content.Orphanize();
            }

            public void AttachTo(IHTMLElement e)
            {
                e.appendChild(img0, layer, Content);
                ZoomAndMove();

            }
        }

        //static GoogleGearsAdvanced()
        //{
        //    typeof(GoogleGearsAdvanced).Spawn();

        //}

    }
}
