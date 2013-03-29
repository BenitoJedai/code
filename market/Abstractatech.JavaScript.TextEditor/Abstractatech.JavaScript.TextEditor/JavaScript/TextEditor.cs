using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Serialized;
using ScriptCoreLib.Shared.Drawing;

//[assembly: ScriptResources("assets/Abstractatech.JavaScript.TextEditor")]

namespace ScriptCoreLib.JavaScript.Controls
{
    using StringPair = Pair<string, string>;
    using System.Linq;



    public class TextEditor
    {


        // http://download.dojotoolkit.org/release-0.2.2/dojo-0.2.2-widget/demos/widget/Editor.html
        // http://www.dynamicdrive.com/dynamicindex16/richtexteditor/index.htm
        // http://www.mozilla.org/editor/midas-spec.html
        // http://www.mozilla.org/editor/ie2midas.html
        // http://developer.mozilla.org/en/docs/Rich-Text_Editing_in_Mozilla
        // http://typetester.maratz.com/

        // http://tinymce.moxiecode.com/example_full.php?example=true

        // http://www.fckeditor.net/demo

        public const string Alias = "fx.Editor";

        public readonly IHTMLDiv Control = new IHTMLDiv();

        public IHTMLIFrame Frame = new IHTMLIFrame();
        public IHTMLTextArea TextArea = new IHTMLTextArea();

        public IHTMLDiv BottomToolbar;
        public IHTMLDiv TopToolbar;



        IHTMLImage Spinner = "assets/Abstractatech.JavaScript.TextEditor/spinner.gif";

        public int Width
        {
            set
            {
                this.Control.style.width = value + "px";
            }
        }

        private int _Height;


        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;

                this.TextArea.style.height = value + "px";
                this.Frame.style.height = value + "px";

                this.DesignerContainer.style.height = (value) + "px";
                this.SourceContainer.style.height = (value) + "px";
            }
        }

        public abstract class Popup<T>
        {
            public readonly IHTMLDiv Control = new IHTMLDiv();

            private T _Value;

            public T Value
            {
                get { return _Value; }
                set
                {
                    _Value = value;

                    if (Changed != null)
                        Changed(_Value);
                }
            }

            public System.Action<T> Changed;


            public bool IsHot;

            public Popup()
            {
                Control.style.position = IStyle.PositionEnum.absolute;
                Control.style.border = "1px solid gray";
                Control.style.backgroundColor = Color.White;
                Control.style.padding = "2px";

                Control.onmouseover +=
                   delegate
                   {
                       this.IsHot = true;
                   };

                Control.onmouseout +=
                    delegate
                    {
                        this.IsHot = false;
                    };

                Control.onclick +=
                    delegate
                    {
                        Control.Dispose();
                    };
            }

            /// <summary>
            /// Hides this popup only if it is not hot - user selected another control in the page.
            /// But if user clicks on a color, a manual action is required.
            /// </summary>
            protected void Hide()
            {
                if (this.IsHot)
                    return;

                if (this.IsFadeEnabled == null)
                {
                    Fader.FadeAndRemove(this.Control, 0, 50);
                }
                else
                {
                    if (this.IsFadeEnabled())
                    {
                        Fader.FadeAndRemove(this.Control, 0, 50);
                    }
                    else
                    {
                        this.Control.Dispose();
                    }
                }
            }

            public void Show(IHTMLElement e, int x, int y)
            {
                this.IsHot = false;

                e.appendChild(Control);

                Helper.Invoke(BeforeShow);

                Control.Show();
                Control.style.SetLocation(x, y);
                Control.style.zIndex = 1000;
                //Control.style.marginTop = "1px";

            }

            public System.Action BeforeShow;

            System.Func<bool> IsFadeEnabled;

            public Popup<T> AttachTo(ToolbarButton b, System.Func<bool> IsFadeEnabled)
            {
                this.IsFadeEnabled = IsFadeEnabled;

                b.Button.onclick +=
                    delegate
                    {
                        b.Control.style.zIndex = 1100;

                        this.Show(b.Control, 0, b.Control.Bounds.Height);
                    };


                b.Button.onblur +=
                     delegate
                     {
                         this.Hide();
                     };

                return this;
            }
        }

        public class PopupMenu : Popup<string>
        {

            public string Width
            {
                get { return Control.style.width; }
                set { Control.style.width = value; }
            }


            public string PaddingLeft
            {
                set
                {
                    foreach (INode v in Control.childNodes)
                    {
                        if (v.nodeType == INode.NodeTypeEnum.ElementNode)
                        {
                            var e = (IHTMLElement)v;

                            e.style.paddingLeft = value;
                        }
                    }
                }
            }



            public PopupMenu(params StringPair[] a)
            {

                foreach (var v in a)
                {
                    AddButton(v);
                }

                this.BeforeShow =
                    delegate
                    {
                        this.Control.style.height = "auto";
                    };
            }


            public void AddButton(StringPair e)
            {
                var x = new IHTMLButton(e.B);

                x.style.display = IStyle.DisplayEnum.block;
                x.style.border = "0";
                x.style.width = "100%";
                x.style.backgroundColor = Color.Transparent;
                //x.style.margin = "1px";
                x.style.textAlign = IStyle.TextAlignEnum.left;

                x.onclick +=
                    delegate
                    {
                        x.blur();

                        ColdStyle(x);

                        this.Value = e.A;
                    };

                x.onmouseover +=
                    delegate
                    {
                        HotButton.ToBackground(x, true);

                    };

                x.onmouseout +=
                    delegate
                    {
                        ColdStyle(x);
                    };


                ColdStyle(x);

                this.Control.appendChild(x);
            }

            static void ColdStyle(IStyle s)
            {
                s.backgroundImage = "";
            }




        }



        public class ColorPopup : Popup<Color>
        {

            public static Color[] colors = {
                0x000000, 0x993300, 0x333300, 0x003300, 0x003366, 0x000080, 0x333399, 0x333333,
                0x800000, 0xff6600, 0x7e7e00, 0x007e00, 0x007e7e, 0x0000fc, 0x666699, 0x7e7e7e,
                0xff0000, 0xff9900, 0x99cc00, 0x339966, 0x33cccc, 0x3366ff, 0x800080, 0x999999,
                0xff00ff, 0xffcc00, 0xffff00, 0x00ff00, 0x00ffff, 0x00ccff, 0x993366, 0xc0c0c0,
                0xff99cc, 0xffcc99, 0xffff99, 0xccffcc, 0xccffff, 0x99ccff, 0xcc99ff, 0xffffff
            };

            public ColorPopup()
            {
                Control.style.SetSize(128, 96);

                foreach (var v in colors)
                {
                    Control.appendChild(CreatePalette(v));
                }



                var more = new IHTMLButton();

                IHTMLImage more_img = "assets/Abstractatech.JavaScript.TextEditor/menu.more.gif";

                more.style.border = "1px solid gray";
                more.style.backgroundColor = Color.Transparent;
                more_img.ToBackground(more, false);
                more.style.backgroundPosition = "center";
                more.style.margin = "1px";
                more.style.Float = IStyle.FloatEnum.left;
                more.style.SetSize(126, 14);

                more.onclick +=
                    delegate
                    {


                    };


                //rgb selector
                //system color selector

                Control.appendChild(more);


            }


            protected IHTMLElement CreatePalette(Color e)
            {
                var c1 = new IHTMLButton();
                c1.style.border = "1px solid gray";
                c1.style.backgroundColor = e;
                c1.style.Float = IStyle.FloatEnum.left;
                c1.style.margin = "1px";
                c1.style.overflow = IStyle.OverflowEnum.hidden;
                c1.style.cursor = IStyle.CursorEnum.pointer;
                c1.style.fontSize = "1pt";
                c1.style.SetSize(14, 14);



                c1.onclick +=
                    delegate(IEvent xe)
                    {
                        c1.blur();

                        this.Control.Dispose();

                        this.Value = e;
                    };

                return c1;
            }




        }

        public void DoCommand(string cmd, object value)
        {
            // http://www.quirksmode.org/dom/execCommand.html
            InternalDocument.execCommand(cmd, false, value);
        }

        private bool _IsFadeEnabled;

        public bool IsFadeEnabled
        {
            get { return _IsFadeEnabled; }
            set { _IsFadeEnabled = value; }
        }


        public IHTMLDiv DesignerContainer = new IHTMLDiv();
        public IHTMLDiv SourceContainer = new IHTMLDiv();

        public IHTMLDiv ContainerForBorders;

        /// <summary>
        /// spawns a new text editor as a child element
        /// </summary>
        /// <param name="parent">this node must be loaded into dom</param>
        public TextEditor(IHTMLElement parent)
        {

            this.TopToolbar = new IHTMLDiv();

            //this.Control.style.border = "1px solid gray";


            //DesignerContainer.style.padding = "1px";

            SourceContainer.appendChild(this.TextArea);
            //cnt2.style.overflow = IStyle.OverflowEnum.hidden;
            //SourceContainer.Hide();
            SourceContainer.style.display = IStyle.DisplayEnum.none;

            this.TextArea.style.backgroundColor = Color.Transparent;
            this.TextArea.style.border = "0";
            this.TextArea.style.fontFamily = IStyle.FontFamilyEnum.Consolas;
            this.TextArea.style.fontSize = "10pt";
            this.TextArea.style.padding = "0";
            this.TextArea.style.margin = "0";
            this.TextArea.style.overflow = IStyle.OverflowEnum.auto;
            this.TextArea.style.display = IStyle.DisplayEnum.block;
            this.TextArea.style.width = "100%";


            //SourceContainer.style.backgroundColor = Color.Yellow;
            //DesignerContainer.style.backgroundColor = Color.Blue;
            //cnt.style.backgroundColor = Color.White;
            //cnt.style.backgroundRepeat = "repeat-x";

            this.Frame.allowTransparency = true;
            this.Frame.style.padding = "0";
            this.Frame.style.margin = "0";
            this.Frame.style.border = "0";
            this.Frame.style.overflow = IStyle.OverflowEnum.auto;
            this.Frame.style.display = IStyle.DisplayEnum.block;
            this.Frame.style.width = "100%";


            this.Height = 200;

            DesignerContainer.appendChild(this.Frame);

            this.BottomToolbar = new IHTMLDiv();

            Toolbar.InvokeOnComplete(
                delegate
                {
                    Toolbar.ToBackground(BottomToolbar);

                    BottomToolbar.style.backgroundRepeat = "repeat-x";
                    BottomToolbar.style.backgroundColor = Color.FromGray(0xcb);
                });

            ToolbarButton design = null;
            ToolbarButton html = null;



            Gradient.ToBackground(Control);


            Control.style.backgroundColor = Color.White;
            Control.style.backgroundRepeat = "repeat-x";

            this.ContainerForBorders = new IHTMLDiv(TopToolbar, DesignerContainer, SourceContainer, BottomToolbar);

            ContainerForBorders.style.border = "1px solid gray";

            this.Control.appendChild(ContainerForBorders);

            parent.appendChild(Control);

            // e.insertNextSibling(Control);

            _IsDesignMode = true;
            var d = this.InternalDocument;


            d.write("<html><body style='height: auto; border: 0; overflow: auto; background-color:transparent;'>");
            //d.write("<p><span style='font-family: verdana;'><b>Lorem</b> ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</span><p>");
            d.write("</body></html>");
            d.close();


            //ttoolbar.appendChild(Spinner);

   

            d.DesignMode = true;
            //d.body.setAttribute("contentEditable", "true");

            //d.body.contentEditable = true;

            #region ToolbarButton

            var bold = AddButton("assets/Abstractatech.JavaScript.TextEditor/bold.gif", "Bold");
            var underline = AddButton("assets/Abstractatech.JavaScript.TextEditor/underline.gif", "Underline");
            var strike = AddButton("assets/Abstractatech.JavaScript.TextEditor/strikethrough.gif", "Strikethrough");
            var italic = AddButton("assets/Abstractatech.JavaScript.TextEditor/italic.gif", "Italic");
            var justifyleft = AddButton("assets/Abstractatech.JavaScript.TextEditor/justifyleft.gif", "JustifyLeft");
            var justifycenter = AddButton("assets/Abstractatech.JavaScript.TextEditor/justifycenter.gif", "Justifycenter");
            var justifyright = AddButton("assets/Abstractatech.JavaScript.TextEditor/justifyright.gif", "Justifyright");
            var justifyfull = AddButton("assets/Abstractatech.JavaScript.TextEditor/justifyfull.gif", "Justifyfull");
            var indent = AddButton("assets/Abstractatech.JavaScript.TextEditor/indent.gif", "Indent");
            var outdent = AddButton("assets/Abstractatech.JavaScript.TextEditor/outdent.gif", "Outdent");
            var sup = AddButton("assets/Abstractatech.JavaScript.TextEditor/superscript.gif", "Superscript");
            var sub = AddButton("assets/Abstractatech.JavaScript.TextEditor/sub.gif", "Subscript");
            var removeformat = AddButton("assets/Abstractatech.JavaScript.TextEditor/removeformat.gif", "Removeformat");
            var insertorderedlist = AddButton("assets/Abstractatech.JavaScript.TextEditor/numberedlist.gif", "InsertOrderedList");
            var insertunorderedlist = AddButton("assets/Abstractatech.JavaScript.TextEditor/bulletedlist.gif", "InsertUnorderedList");
            var undo = AddButton("assets/Abstractatech.JavaScript.TextEditor/undo.gif", "undo");
            var redo = AddButton("assets/Abstractatech.JavaScript.TextEditor/redo.gif", "redo");

            var fontfamily = CreateButton("assets/Abstractatech.JavaScript.TextEditor/icon_font.gif");

            var FontMenu = new PopupMenu(
                new StringPair("consolas, courier new, courier", "Courier"),
                new StringPair("Ariel", "Ariel"),
                new StringPair("Tahoma", "Tahoma"),
                new StringPair("Times New Roman", "Times New Roman"),
                new StringPair("Verdana", "Verdana")
            )
            {
                Width = "10em",
                PaddingLeft = "24px"
            };

            FontMenu.AttachTo(fontfamily, () => this.IsFadeEnabled).Changed = value => DoCommand("fontname", value);



            var fontsize = CreateButton("assets/Abstractatech.JavaScript.TextEditor/icon_size.gif");

            var fontsize_popup = new PopupMenu(
                new StringPair("1", "Smallest"),
                new StringPair("2", "Smaller"),
                new StringPair("3", "Small"),
                new StringPair("4", "Medium"),
                new StringPair("5", "Large"),
                new StringPair("6", "Larger"),
                new StringPair("7", "Largest")
            )
            {
                Width = "10em",
                PaddingLeft = "24px"
            };

            fontsize_popup.AttachTo(fontsize, () => this.IsFadeEnabled).Changed = size => DoCommand("fontsize", size);



            var forecolor = CreateButton("assets/Abstractatech.JavaScript.TextEditor/forecolor.gif");

            var forecolor_popup = new ColorPopup();

            forecolor_popup.AttachTo(forecolor, () => this.IsFadeEnabled);
            forecolor_popup.Changed =
                delegate(Color c)
                {
                    DoCommand("ForeColor", c.ToString());
                };

            var hilitecolor = CreateButton("assets/Abstractatech.JavaScript.TextEditor/hilitecolor.gif");

            var hilitecolor_popup = new ColorPopup();

            hilitecolor_popup.AttachTo(hilitecolor, () => this.IsFadeEnabled);
            hilitecolor_popup.Changed =
                delegate(Color c)
                {
                    try
                    {
                        DoCommand("hilitecolor", c.ToString());
                    }
                    catch
                    {
                        DoCommand("backcolor", c.ToString());
                    }

                };



            ToolbarButton[] tbuttons =
                {
                    fontfamily, fontsize,

                    bold, italic, underline, strike, //,  Separator.cloneNode(false),

                    


                    justifyleft, justifycenter, justifyright, justifyfull, // Separator.cloneNode(false),
                    indent, outdent, //Separator.cloneNode(false),
                     insertorderedlist, insertunorderedlist,
                    sup, sub, //Separator.cloneNode(false),
                    //incsize, decsize,
                    forecolor, hilitecolor,
                    removeformat,

                    undo, redo
                };

            var customize = CreateButton("assets/Abstractatech.JavaScript.TextEditor/customize.gif");

            customize.Control.style.Float = IStyle.FloatEnum.right;


            TopToolbar.appendChild(customize);

            foreach (ToolbarButton v in tbuttons)
            {
                v.Button.style.SetSize(24, 24);

                TopToolbar.appendChild(v);
            }
            #endregion


            #region ToDesign
            System.Action ToDesign =
                delegate
                {

                    this.InternalSetInnerHTML(this.TextArea.value);

                    SourceContainer.style.display = IStyle.DisplayEnum.none;
                    DesignerContainer.style.display = IStyle.DisplayEnum.block;


                    design.Enabled = false;
                    html.Enabled = true;


                    foreach (var v in tbuttons)
                    {
                        v.Enabled = true;
                    }
                };
            #endregion

            #region ToHTML
            System.Action ToHTML =
                delegate
                {
                    this.TextArea.value = InternalGetInnerHTML();


                    DesignerContainer.style.display = IStyle.DisplayEnum.none;
                    SourceContainer.style.display = IStyle.DisplayEnum.block;


                    design.Enabled = true;
                    html.Enabled = false;

                    foreach (var v in tbuttons)
                    {
                        v.Enabled = false;
                    }

                };
            #endregion


            this._set_IsDesignMode =
                value =>
                {
                    if (value)
                    {
                        ToDesign();
                        return;
                    }

                    ToHTML();
                };

            design = AddButton(
                new Abstractatech.JavaScript.TextEditor.HTML.Images.FromAssets.mode_design().src,
                    delegate
                    {
                        IsDesignMode = true;
                    }
                );

            html = AddButton(
                new Abstractatech.JavaScript.TextEditor.HTML.Images.FromAssets.mode_html().src,
                    delegate
                    {
                        IsDesignMode = false;
                    }
                );

            design.Enabled = false;

            BottomToolbar.appendChild(design, html);

            TopToolbarContainer = TopToolbar;
            BottomToolbarContainer = BottomToolbar;
        }

        System.Action<bool> _set_IsDesignMode;
        bool _IsDesignMode;
        public bool IsDesignMode
        {
            get { return _IsDesignMode; }
            set { _IsDesignMode = value; _set_IsDesignMode(value); }
        }

        public IHTMLDiv TopToolbarContainer
        {
            get;
            private set;
        }

        public IHTMLDiv BottomToolbarContainer
        {
            get;
            private set;
        }

        public string InnerHTML
        {
            get
            {
                return InternalGetInnerHTML();
            }
            set
            {
                InternalSetInnerHTML(value);
            }
        }

        private void InternalSetInnerHTML(string value)
        {
            if (InternalDocument.body == null)
            {
                Native.Window.setTimeout(IFunction.OfDelegate(
                    new System.Action(
                        delegate
                        {
                            InternalDocument.body.innerHTML = value;
                        }
                )), 1);
                return;
            }
            InternalDocument.body.innerHTML = value;
        }

        private string InternalGetInnerHTML()
        {
            // Entity 'nbsp' not defined
            //var xml = InternalDocument.body.AsXElement();

            var value = InternalDocument.body.innerHTML;

            // can we tity it? 
            value = value.Replace("<br>", "<br />");
            value = value.Replace("<hr>", "<hr />");
            //value = value.Replace("&nbsp;", "<hr />");

            return value;
        }


        public ToolbarButton AddButton(IHTMLImage img, string text, System.Action h)
        {
            var x = CreateButton();

            x.Button.onclick += delegate
            {
                h();
            };

            x.Image = img;


            var u = new IHTMLTable();

            u.style.cursor = IStyle.CursorEnum.@default;
            u.cellPadding = 0;
            u.cellSpacing = 0;
            u.AddBody().AddRow(x.Image, new ITextNode(""), new ITextNode(text));

            x.Button.appendChild(u);

            return x;
        }



        IHTMLDocument InternalDocument
        {
            get
            {
                return this.Frame.contentWindow.document;
            }
        }

        public IHTMLDocument Document
        {
            get
            {
                if (!_IsDesignMode)
                {
                    InternalDocument.body.innerHTML = this.TextArea.value;
                }

                return InternalDocument;
            }
        }

        IHTMLImage Gradient = "assets/Abstractatech.JavaScript.TextEditor/body_back.gif";
        IHTMLImage Toolbar = new Abstractatech.JavaScript.TextEditor.HTML.Images.FromAssets.toolbar_bg();

        static IHTMLImage HotButton = new Abstractatech.JavaScript.TextEditor.HTML.Images.FromAssets.hot_bg();

        IHTMLImage Separator = "assets/Abstractatech.JavaScript.TextEditor/separator.horizontal.gif";



        public class ToolbarButton
        {
            public IHTMLImage Image;
            public IHTMLButton Button;

            public readonly IHTMLSpan Control = new IHTMLSpan();

            public static implicit operator IHTMLElement(ToolbarButton e)
            {
                return e.Control;
            }

            private bool _Enabled;

            public bool Enabled
            {
                get { return _Enabled; }
                set
                {
                    _Enabled = value;
                    if (this.Image != null)
                        if (value)
                            this.Image.style.Opacity = 1;
                        else
                            this.Image.style.Opacity = 0.5;

                    this.Button.disabled = !value;
                    this.Button.style.backgroundImage = "";
                }
            }

        }


        ToolbarButton CreateButton()
        {
            var u = new IHTMLButton();
            u.style.padding = "0";
            u.style.backgroundColor = Color.Transparent;

            //u.style.height = "24px";

            u.style.border = "0";

            u.onmouseover += delegate
            {
                HotButton.ToBackground(u.style);

            };

            u.onmouseout += delegate
            {
                u.style.backgroundImage = "";
            };

            var z = new ToolbarButton { Button = u };

            z.Control.style.position = IStyle.PositionEnum.relative;
            z.Control.appendChild(u);

            return z;
        }

        ToolbarButton CreateButton(IHTMLImage img)
        {
            var z = CreateButton();

            z.Button.appendChild(img);
            z.Image = img;

            return z;
        }

        public ToolbarButton AddButton(IHTMLImage img, System.Action<IHTMLButton> h)
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
                        InternalDocument.execCommand(cmd, false, null);
                    }
                    catch
                    {
                        System.Console.WriteLine("command failed: " + cmd);
                    }

                    Frame.contentWindow.focus();
                }
            );
        }

    }


}
