using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace MyEditor.source.js.Controls
{
    [Script]
    public class Editor : SpawnControlBase
    {
        // http://download.dojotoolkit.org/release-0.2.2/dojo-0.2.2-widget/demos/widget/Editor.html
        // http://www.dynamicdrive.com/dynamicindex16/richtexteditor/index.htm
        // http://www.mozilla.org/editor/midas-spec.html
        // http://typetester.maratz.com/

        // http://tinymce.moxiecode.com/example_full.php?example=true
        // http://www.mozilla.org/editor/ie2midas.html

        public const string Alias = "fx.Editor";

        IHTMLDiv Control = new IHTMLDiv();
        IHTMLIFrame Frame = new IHTMLIFrame();
        IHTMLTextArea Text = new IHTMLTextArea();


        [Script]
        class fx
        {
            public string Value;

            public static implicit operator fx(string Value)
            {
                return new fx { Value };
            }

            public static implicit operator IHTMLImage(fx e)
            {
                return new IHTMLImage("fx/" + e.Value);
            }
        }

        IHTMLImage Spinner = (fx)"spinner.gif";

        private int _Height;

        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;

                this.Text.style.height = this._Height + "px";
                this.Frame.style.height = this._Height + "px";

            }
        }




        [Script]
        public class ColorPopup
        {
            public static Color[] colors = {
                0x000000, 0x993300, 0x333300, 0x003300, 0x003366, 0x000080, 0x333399, 0x333333,
                0x800000, 0xff6600, 0x7e7e00, 0x007e00, 0x007e7e, 0x0000fc, 0x666699, 0x7e7e7e,
                0xff0000, 0xff9900, 0x99cc00, 0x339966, 0x33cccc, 0x3366ff, 0x800080, 0x999999,
                0xff00ff, 0xffcc00, 0xffff00, 0x00ff00, 0x00ffff, 0x00ccff, 0x993366, 0xc0c0c0,
                0xff99cc, 0xffcc99, 0xffff99, 0xccffcc, 0xccffff, 0x99ccff, 0xcc99ff, 0xffffff
            };

            public readonly IHTMLDiv pal = new IHTMLDiv();

            public bool IsHot;

            public static ColorPopup Of(ToolbarButton t, EventHandler<Color> h)
            {
                var z = new ColorPopup();


                z.AttachTo(t);
                z.Changed +=
                    delegate
                    {
                        h(z.SelectedColor);
                    };

                return z;
            }

            public ColorPopup()
            {
                pal.style.position = IStyle.PositionEnum.absolute;
                pal.style.border = "1px solid gray";
                //pal.style.padding = "2px";
                pal.style.SetSize(138, 86);
                pal.style.backgroundColor = Color.White;


                pal.appendChild(
                    CreatePalette(colors)
                );

                pal.onmouseover +=
                    delegate
                    {
                        this.IsHot = true;
                    };

                pal.onmouseout +=
                    delegate
                    {
                        this.IsHot = false;
                    };

                pal.onclick +=
                    delegate
                    {
                        pal.Dispose();
                    };

            }

            public IHTMLDiv[] CreatePalette(Color[] e)
            {
                var a = new IHTMLDiv[e.Length];

                for (int i = 0; i < e.Length; i++)
                {
                    a[i] = CreatePalette(e[i]);
                }

                return a;
            }

            public IHTMLDiv CreatePalette(Color e)
            {
                var c1 = new IHTMLDiv();
                c1.style.SetSize(11, 11);
                c1.style.border = "1px solid gray";
                c1.style.backgroundColor = e;
                c1.style.Float = IStyle.FloatEnum.left;
                c1.style.margin = "2px";
                c1.style.overflow = IStyle.OverflowEnum.hidden;
                c1.style.cursor = IStyle.CursorEnum.pointer;




                c1.onclick +=
                    delegate(IEvent xe)
                    {
                        this.SelectedColor = e;

                        this.pal.Dispose();

                        Helper.Invoke(this.Changed);
                    };

                return c1;
            }

            public Color SelectedColor;

            public event EventHandler Changed;

            /// <summary>
            /// Hides this popup only if it is not hot - user selected another control in the page.
            /// But if user clicks on a color, a manual action is required.
            /// </summary>
            public void Hide()
            {
                if (this.IsHot)
                    return;

                Fader.FadeAndRemove(this.pal, 0, 50);
            }



            public void Show(IHTMLElement e, int x, int y)
            {
                this.IsHot = false;

                e.appendChild(pal);

                pal.Show();
                pal.style.SetLocation(x, y);
            }

            public void AttachTo(ToolbarButton b)
            {
                b.Button.onclick +=
                    delegate
                    {
                        this.Show(b.Control, 0, b.Control.Bounds.Height);
                    };


                b.Button.onblur +=
                     delegate
                     {
                         this.Hide();
                     };
                    
            }
        }

        public Editor(IHTMLElement e)
            : base(e)
        {
            var ttoolbar = new IHTMLDiv();

            //this.Control.style.border = "1px solid gray";

            var cnt = new IHTMLDiv();
            var cnt2 = new IHTMLDiv();

            cnt2.appendChild(this.Text);

            cnt2.Hide();

            this.Text.style.backgroundColor = JSColor.Transparent;
            this.Text.style.border = "0";
            this.Text.style.overflow = IStyle.OverflowEnum.auto;
            this.Text.style.display = IStyle.DisplayEnum.block;
            this.Text.style.width = "100%";

            //cnt.style.backgroundColor = Color.White;
            //cnt.style.backgroundRepeat = "repeat-x";

            this.Frame.allowTransparency = true;

            this.Frame.style.border = "0";
            this.Frame.style.overflow = IStyle.OverflowEnum.auto;
            this.Frame.style.display = IStyle.DisplayEnum.block;
            this.Frame.style.width = "100%";


            this.Height = 400;

            cnt.appendChild(this.Frame);

            var btoolbar = new IHTMLDiv();

            Toolbar.ToBackground(btoolbar);
            btoolbar.style.backgroundRepeat = "repeat-x";
            btoolbar.style.backgroundColor = Color.FromGray(0xcb);

            var design = AddButton((fx)"mode.design.gif", delegate
            {
                cnt2.Hide();

                Document.body.innerHTML = this.Text.value;

                cnt.Show();
            });



            var html = AddButton((fx)"mode.html.gif", delegate
            {


                cnt.Hide();

                this.Text.value = Document.body.innerHTML;

                cnt2.Show();
            });

            btoolbar.appendChild(design, html);

            Gradient.ToBackground(Control);

            Control.style.border = "1px solid gray";
            Control.style.backgroundRepeat = "repeat-x";

            this.Control.appendChild(ttoolbar, cnt, cnt2, btoolbar);

            e.insertNextSibling(Control);

            cnt.style.overflow = IStyle.OverflowEnum.hidden;




            var d = this.Document;

            d.write("<html><body style='height: auto; border: 0; overflow: auto; background-color:transparent;'></body></html>");
            d.close();

            ttoolbar.appendChild(Spinner);

            new IXMLHttpRequest(HTTPMethodEnum.GET, "example.html",
                delegate(IXMLHttpRequest r)
                {
                    Spinner.FadeOut();

                    this.Document.body.innerHTML = r.responseText;
                }
            );

            d.DesignMode = true;


            var bold = AddButton((fx)"bold.gif", "Bold");
            var underline = AddButton((fx)"underline.gif", "Underline");
            var strike = AddButton((fx)"strikethrough.gif", "Strikethrough");
            var italic = AddButton((fx)"italic.gif", "Italic");
            var justifyleft = AddButton((fx)"justifyleft.gif", "JustifyLeft");
            var justifycenter = AddButton((fx)"justifycenter.gif", "Justifycenter");
            var justifyright = AddButton((fx)"justifyright.gif", "Justifyright");
            var justifyfull = AddButton((fx)"justifyfull.gif", "Justifyfull");
            var indent = AddButton((fx)"indent.gif", "Indent");
            var outdent = AddButton((fx)"outdent.gif", "Outdent");
            var sup = AddButton((fx)"superscript.gif", "Superscript");
            var sub = AddButton((fx)"sub.gif", "Subscript");
            var removeformat = AddButton((fx)"removeformat.gif", "Removeformat");
            var insertorderedlist = AddButton((fx)"numberedlist.gif", "InsertOrderedList");
            var insertunorderedlist = AddButton((fx)"bulletedlist.gif", "InsertUnorderedList");
            var undo = AddButton((fx)"undo.gif", "undo");
            var redo = AddButton((fx)"redo.gif", "redo");

            // create a palette
            var forecolor = AddButton((fx)"forecolor.gif",
                delegate
                {
                    Document.execCommand("ForeColor", false, "#ffff00");
                });
            
            //ColorPopup.Of(forecolor, 
            //    delegate(Color c)
            //    {
            //        Document.execCommand("ForeColor", false, "#ffff00");
            //        Document.execCommand("ForeColor", false, c.ToString());
            //    }
            //);

            var hilitecolor = CreateButton((fx)"hilitecolor.gif");

            ColorPopup.Of(hilitecolor,
                delegate(Color c)
                {
                    //var s = Document.selection;

                    
                    //Native.DebugBreak();

                    Frame.focus();

                    try
                    {
                        Document.execCommand("hilitecolor", false, c.ToString());
                    }
                    catch
                    {
                        Document.execCommand("backcolor", false, c.ToString());
                    }

                }
            );


            //var fonts = new IHTMLSelect();

            //fonts.Add("Arial", "Verdana", "Times New Roman", "Courier");
            //fonts.style.margin = "2px";
            //var fonts = CreateButton();

            //fonts.style.paddingLeft = "1em";
            //fonts.style.paddingRight = "1em";
            //fonts.style.height = "100%";

            //fonts.appendChild("font: Arial");

            ttoolbar.appendChild(
                //fonts,
                bold, italic, underline, strike, // Separator.cloneNode(false),
                justifyleft, justifycenter, justifyright, justifyfull, // Separator.cloneNode(false),
                indent, outdent, //Separator.cloneNode(false),
                 insertorderedlist, insertunorderedlist,
                sup, sub, //Separator.cloneNode(false),
                forecolor, hilitecolor,
                removeformat,

                undo, redo);



        }



        IHTMLDocument Document
        {
            get
            {
                return this.Frame.contentWindow.document;
            }
        }

        IHTMLImage Gradient = (fx)"body_back.gif";
        IHTMLImage Toolbar = (fx)"toolbar-bg.gif";
        IHTMLImage HotButton = (fx)"hot-bg.gif";
        IHTMLImage Separator = (fx)"separator.horizontal.gif";



        [Script]
        public class ToolbarButton
        {
            public IHTMLImage Image;
            public IHTMLButton Button;

            public readonly IHTMLSpan Control = new IHTMLSpan();

            public static implicit operator IHTMLElement(ToolbarButton e)
            {
                return e.Control;
            }

        }


        IHTMLButton CreateButton()
        {
            var u = new IHTMLButton();
            u.style.padding = "0";
            u.style.backgroundColor = JSColor.Transparent;

            u.style.SetSize(24, 24);

            u.style.border = "0";

            u.onmouseover += delegate
            {
                HotButton.ToBackground(u.style);

            };

            u.onmouseout += delegate
            {
                u.style.backgroundImage = "";
            };

            return u;
        }

        ToolbarButton CreateButton(IHTMLImage img)
        {
            var u = CreateButton();

            u.appendChild(img);

            var z = new ToolbarButton { Image = img, Button = u };

            z.Control.style.position = IStyle.PositionEnum.relative;
            z.Control.appendChild(u);


            return z;
        }

        ToolbarButton AddButton(IHTMLImage img, EventHandler<IHTMLButton> h)
        {
            var z = CreateButton(img);

            z.Button.onclick +=
                  delegate
                  {
                      h(z.Button);
                  };


            return z;
        }

        ToolbarButton AddButton(IHTMLImage img, string cmd)
        {
            return AddButton(img,
                delegate
                {
                    try
                    {
                        Document.execCommand(cmd, false, null);
                    }
                    catch
                    {
                        Console.Log("command failed: " + cmd);
                    }

                    Frame.contentWindow.focus();
                }
            );
        }

    }


}
