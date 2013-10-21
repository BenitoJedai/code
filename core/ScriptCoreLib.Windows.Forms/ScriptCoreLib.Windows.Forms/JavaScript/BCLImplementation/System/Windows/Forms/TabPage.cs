using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TabPage))]
    internal class __TabPage : __Panel
    {
        static internal int __tabCount = 0;
        internal string __tabId;

        internal IHTMLButton __tabButton;

        internal bool __isSelected = false;

        __TabPageControlCollection __controlCollection;

        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.TabPage class.
        public __TabPage()
        {
            this.__controlCollection = new __TabPageControlCollection((TabPage)this);
            this.__tabId = "tab" + __tabCount;
            __tabCount++;

            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;
            HTMLTarget.style.border = "1px solid gray";
            HTMLTarget.style.borderTop = "none";

            __tabButton = new IHTMLButton(this.__tabId);

            __tabButton.ApplyBorderStyle(global::System.Windows.Forms.BorderStyle.Fixed3D);

            __tabButton.style.textDecoration = "none";
            __tabButton.style.color = "#42454a";
            __tabButton.style.backgroundColor = "#dedbde";

            __tabButton.style.top = "50%";
            __tabButton.style.bottom = "50%";

            int newh = __TabControl.__TAB_BAR_HEIGHT - 2;
            __tabButton.style.height = "" + newh;

            __tabButton.style.border = "ridge";
            __tabButton.style.borderTop = "ridge";
            __tabButton.style.borderRight = "ridge";
            __tabButton.style.borderLeft = "ridge";
            __tabButton.style.borderBottom = "none";

            __tabButton.style.paddingBottom = "8px";

            setFont(DefaultFont);

            Li = new IHTMLListItem();
            Li.style.display = IStyle.DisplayEnum.inline;
            //Li.style.padding = "5px";            
            Li.style.marginRight = "0"; // "5px";

            Li.style.Float = IStyle.FloatEnum.left;

            this.__isSelected = true;

            // 2013-09-30
            // Error	5	The type 'System.Xml.Linq.XElement' is defined in 
            // an assembly that is not referenced. You must add a
            // reference to assembly 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.	X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\TabPage.cs	72	13	ScriptCoreLib.Windows.Forms

            Li.appendChild(__tabButton);
            Li.style.Float = IStyle.FloatEnum.left;

            __DeSelectTab();

            HTMLTarget.style.backgroundColor = "white";

            TextChanged += OnTextChanged;
        }

        protected void OnTextChanged(object o, EventArgs e)
        {
            this.__tabButton.innerText = Text;
        }


        public void __assignClickEvent(Action<IEvent> clickEvt)
        {
            this.__tabButton.onclick += clickEvt;
        }


        private IHTMLListItem __li;
        public IHTMLListItem Li
        {
            get { return this.__li; }
            set
            {
                this.__li = value;
            }
        }

        public Shared.Drawing.Rectangle getBounds()
        {
            return this.__tabButton.Bounds;

        }

        public void setFont(Font font)
        {
            __tabButton.style.fontSize = "4pt";
            __tabButton.style.font = font.ToCssString();
        }

        public void __DeSelectTab()
        {
            this.__isSelected = false;
            HTMLTarget.style.display = IStyle.DisplayEnum.none;

            Color bf = SystemColors.ButtonFace;
            __tabButton.style.backgroundColor = bf.ToString();
            __tabButton.style.paddingBottom = "4px";
        }

        public void SelectTab()
        {
            this.__isSelected = true;

            HTMLTarget.style.display = IStyle.DisplayEnum.inline;
            __tabButton.style.borderBottom = "none";
            __tabButton.style.backgroundColor = "white";
            __tabButton.style.paddingBottom = "8px";

            Console.WriteLine("SelectTab");
            // let datagrid know to resize?
            this.InternalVisibileChanged(new EventArgs());
        }

        public void Hide()
        {
            this.Visible = false;
        }

        public string ToolTipText { get; set; }
        public bool UseVisualStyleBackColor { get; set; }
        public bool Visible { get; set; }
        public event EventHandler AutoSizeChanged;
        public event EventHandler DockChanged;
        public event EventHandler EnabledChanged;
        public event EventHandler LocationChanged;
        public event EventHandler TabIndexChanged;
        public event EventHandler TabStopChanged;
        public event EventHandler TextChanged;
        public event EventHandler VisibleChanged;

        protected Control.ControlCollection CreateControlsInstance()
        {
            TabPage.TabPageControlCollection c = new TabPage.TabPageControlCollection((TabPage)Parent);
            return (Control.ControlCollection)c;
        }

        public static TabPage GetTabPageOfComponent(object comp)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Enter;

        protected void OnEnter(EventArgs e)
        {
            if (Enter != null)
                Enter(this, null);
        }

        public event EventHandler Leave;

        protected void OnLeave(EventArgs e)
        {
            if (Leave != null)
                Leave(this, null);
        }

        public event EventHandler PaintBackground;

        protected void OnPaintBackground(PaintEventArgs e)
        {
            if (PaintBackground != null)
                PaintBackground(this, null);
        }

        public override string ToString()
        {
            return Text;
        }


        /// <summary>
        /// IMPORTANT NOTE:
        ///     This __TabPageControlCollection class is currently not being used by BCL. Instead it is defaulting to __Control.__ControlCollection
        ///     Future work can move the implementation to this class but should be similar to what __Control.__ControlCollection
        ///     is currently doing.
        ///     This is relevant when Windows Forms adds ui controls to a TabPage the form of TabPage.Controls.Add
        ///     At first glance TabPage is using member Controls but at Runtime it assigns Controls to be of type TabPageControlCollection.
        ///     So BCL would need to do the same and assign Controls to be of type TabPageControlCollection.
        ///     So curerntly since TabPage.ControlCollection base class is Control.ControlCollection, it is allowing the base class
        ///     to do all the work.
        /// </summary>
        [Script(Implements = typeof(global::System.Windows.Forms.TabPage.TabPageControlCollection))]
        internal class __TabPageControlCollection : __Control.__ControlCollection
        {
            readonly TabPage Owner;

            public __TabPageControlCollection(TabPage owner)
                : base(owner)
            {
                this.Owner = owner;
            }

            public void Add(Control value)
            {
                throw new NotImplementedException();
            }

            public void Hide()
            {
            }

            public void Show()
            {
                for (int i = 0; i < Count; i++)
                {
                    this[i].Show();
                }
            }

            public void Remove(TabPage tp)
            {
                throw new global::System.Exception("Not implemented");
            }

            public int Count
            {
                get { return base.Count; }
            }
        }
    }
}
