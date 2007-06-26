using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GGearAlpha.js
{
    using textarea = IHTMLTextArea;
    using div = IHTMLDiv;
    using img = IHTMLImage;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;

    [Script]
    class Class2
    {
        public const string Alias = "Class2";
        public const string DefaultData = "Class2Data";

        public Class2(IHTMLElement e)
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
                    r.style.background = "url(assets/GearsDemo1/back.jpg)";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                    r.style.margin = "0";
                    r.style.padding = "0";
                }
            );

            IStyleSheet.Default.AddRule(".shadow",
                r =>
                {

                    r.style.background = "url(assets/GearsDemo1/shadow.png) repeat-x";
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );


            IStyleSheet.Default.AddRule(".workspace0",
                r =>
                {
                    r.style.marginTop = "4em";
                    r.style.marginLeft = "2em";
                    r.style.marginRight = "2em";

                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );

            IStyleSheet.Default.AddRule(".workspace",
                r =>
                {
                    r.style.background = "black";
                    r.style.Opacity = 0;
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.width = "100%";
                    r.style.height = "100%";
                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );

            IStyleSheet.Default.AddRule(".error",
                r =>
                {
                    r.style.color = "red";
                    r.style.backgroundColor = "white";
                }
            );

            IStyleSheet.Default.AddRule(".content",
                r =>
                {
                    r.style.border = "1px solid gray";
                    r.style.backgroundColor = "transparent";
                    r.style.position = IStyle.PositionEnum.absolute;
                }
            );

            var shadow = new div { className = "shadow" };
            var workspace0 = new div { className = "workspace0" };
            var workspace = new div { className = "workspace" };

            workspace0.innerHTML = "You can create new postcards by clicking on the background image. You can drag those postcards by their borders. You can use your mouse wheel to zoom in or out, too.";

            shadow.appendChild(workspace0, workspace);

            new img
            {
                src = "assets/GearsDemo1/postcard-alpha.png",
                alt = "postcard"
            }.InvokeOnComplete(
                // at this point we have the image and we know how large it is

                postcard =>
                {

                    var p0 = new Postcard(postcard, workspace);

                    p0.CurrentDrag.Position.X = 400;
                    p0.CurrentDrag.Position.Y = 200;
                    p0.AttachTo(shadow);


                    GoogleGearsFactory.Database db = null;

                    try
                    {
                        db = new GoogleGearsFactory.Database();

                        db.open("demo1");
                        db.execute(@"
        create table if not exists Postcards
        (Id varchar(255), Text varchar(255), X int, Y int)
                ");
                    }
                    catch (System.Exception exc)
                    {
                        var err = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre, exc.Message) { className = "error" };


                        workspace0.appendChild(err);
                    }

                    Func<PostcardEntry, Postcard> Spawn =
                        template =>
                        {
                            var p2 = new Postcard(postcard, workspace);

                            p2.CurrentDrag.Position.X = template.X;
                            p2.CurrentDrag.Position.Y = template.Y;
                            p2.AttachTo(shadow);

                            System.Console.WriteLine("new: " + p2.Id);

                            if (template.Id == null)
                            {

                                db.Insert("Postcards",
                                    new PostcardEntry { Id = p2.Id, Text = p2.Text, /* Zoom = p2.Zoom,*/ X = p2.CurrentDrag.Position.X, Y = p2.CurrentDrag.Position.Y }
                                );
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

                                    db.execute("delete from Postcards where Id = ?", p2.Id);
                                };

                            p2.Changed +=
                                delegate
                                {
                                    System.Console.WriteLine("changed: " + new PostcardEntry { Id = p2.Id, Text = p2.Text/*, Zoom = p2.Zoom*/, X = p2.CurrentDrag.Position.X, Y = p2.CurrentDrag.Position.Y }.ToString());

                                    db.execute("delete from Postcards where Id = ?", p2.Id);
                                    db.Insert("Postcards",
                                        new PostcardEntry { Id = p2.Id, Text = p2.Text/*, Zoom = p2.Zoom*/, X = p2.CurrentDrag.Position.X, Y = p2.CurrentDrag.Position.Y }
                                    );
                                };

                            p2.Text = template.Text;

                            return p2;
                        };

                         

                    workspace.onclick +=
                        delegate(IEvent ev)
                        {
                            Spawn(
                                new PostcardEntry { X = ev.CursorX, Y = ev.CursorY }
                            );

                        };

                    var query = from Data in db.AsEnumerable<PostcardEntry>(
                                                "select * from Postcards",
                                                typeof(PostcardEntry)
                                            )
                                select new { Control = Spawn(Data), Data = Data };

                    foreach (var v in query)
	                {
                        System.Console.WriteLine("item: " + v.Data.ToString()) ;
                		 
	                }
                }
            );

            Native.Document.body.appendChild(shadow);
        }

        [Script]
        sealed class PostcardEntry
        {
            public string Id;
            public string Text;
            //public double Zoom;
            public int X;
            public int Y;

            public override string ToString()
            {
                return "{Id = " + Id + ", Text = " + Text + /*", Zoom = " + Zoom +*/ ", X = " + X + ", Y = " + Y + "}";
            }
        }

        [Script]
        class SerializedObject
        {
            public string Id;

            public SerializedObject()
            {
                this.Id = "$id+" + new System.Random().NextDouble();

            }
        }

        [Script]
        class Postcard : SerializedObject
        {
            readonly img postcard;

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

                Content.style.fontSize = Zoom + "em";
                Content.Zoom(Zoom / 2, postcard);
                Content.SetCenteredLocation(CurrentDrag.Position);
            }

            public event Action DoubleClick;

            public Postcard(img postcard, div workspace)
            {

                this.postcard = postcard;


                this.layer = new div();
                this.img0 = (img)postcard.cloneNode(false);

                layer.style.backgroundColor = "red";

                layer.style.Opacity = 0;


                Content = new textarea { className = "content" };

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
                    delegate(IEvent ev)
                    {
                        var z = Zoom;

                        z += (0.05 * ev.WheelDirection);

                        if (z < 0.3)
                            z = 0.3;

                        if (z > 2)
                            z = 2;

                        Zoom = z;
                    };


                CurrentDrag.DragMove +=
                    delegate
                    {
                        ZoomAndMove();

                        Changed.SafeInvoke();
                    };

                CurrentDrag.Enabled = true;
            }

            public void Dispose()
            {
                img0.Dispose();
                layer.Dispose();
                Content.Dispose();
            }

            public void AttachTo(IHTMLElement e)
            {
                e.appendChild(img0, layer, Content);
                ZoomAndMove();

            }
        }

        static Class2()
        {
            Native.Spawn(Class2.Alias, e => new Class2(e));
        }

    }
}
