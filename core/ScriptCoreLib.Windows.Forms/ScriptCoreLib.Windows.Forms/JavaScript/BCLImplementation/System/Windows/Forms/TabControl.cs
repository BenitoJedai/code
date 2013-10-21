using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TabControl))]
    internal class __TabControl : __Control
    {
        IHTMLDiv InternalContainer;

        Panel __tabs;

        public static int __TAB_BAR_HEIGHT = 24;

        IHTMLUnorderedList __ul; // this is for the TABS not the content!!
        IHTMLDiv __ulContainer;

        internal static bool firstSelected = false;

        // List<__TabPage> __TabPages = new List<__TabPage>();

        int __childX = 0;
        int __childWidth = 0;

        IHTMLDiv __tabFiller; // not used currently but the idea here is that when the TABs needs scrolling due to space, will need a scroller tab

        public __TabControl()
        {
            init();
            Controls = new TabControl.ControlCollection((TabControl)this);
        }

        void init()
        {
            this.__ul = new IHTMLUnorderedList();

            this.__ul.name = "tabs";
            this.__ul.style.marginTop = "0";
            this.__ul.style.marginRight = "0";
            this.__ul.style.marginBottom = "-1px";
            this.__ul.style.marginLeft = "0";

            this.__ul.style.paddingTop = "0";
            this.__ul.style.paddingRight = "0";
            this.__ul.style.paddingBottom = "0.3em";
            this.__ul.style.paddingLeft = "0";

            this.__ul.style.width = "" + this.Size.Width;
            this.__ul.style.height = "" + __TAB_BAR_HEIGHT;

            this.__ul.style.Float = DOM.IStyle.FloatEnum.left;

            this.__ulContainer = new IHTMLDiv();
            this.__ulContainer.name = "ulcontainer";
            this.__ulContainer.style.width = "" + this.Size.Width;
            this.__ulContainer.style.height = "" + __TAB_BAR_HEIGHT;
            this.__ulContainer.style.position = DOM.IStyle.PositionEnum.relative;
            this.__ulContainer.style.top = "0";
            this.__ulContainer.style.bottom = "0";

            this.__ulContainer.appendChild(this.__ul);

            this.__tabFiller = new IHTMLDiv();
            this.__tabFiller.name = "filler";
            this.__tabFiller.style.height = "" + __TAB_BAR_HEIGHT;
            this.__tabFiller.style.borderBottom = "1px solid";
            this.__tabFiller.style.borderTop = "none";
            this.__tabFiller.style.borderRight = "none";
            this.__tabFiller.style.borderLeft = "none";
            this.__tabFiller.style.minWidth = "0";
            this.__tabFiller.style.Float = DOM.IStyle.FloatEnum.left;

            this.InternalContainer = new IHTMLDiv();
            this.InternalContainer.name = "tabcontrol";

            this.__tabs = new Panel();
            this.__tabs.Location = new Point(0, 0);
            this.__tabs.Size = new Size(this.Size.Width, __TAB_BAR_HEIGHT);

            setTabsSize();

            base.Controls.Add(this.__tabs);
            this.__tabs.GetHTMLTarget().appendChild(this.__ulContainer);

            this.__tabs.GetHTMLTarget().style.SetLocation(0, 0, this.Size.Width, __TAB_BAR_HEIGHT);

            _tabPages = new TabControl.TabPageCollection((TabControl)this);

            FontChanged += OnFontChanged;
        }

        public new event ControlEventHandler ControlAdded;

        protected override void OnControlAdded(ControlEventArgs e)
        {
            Console.WriteLine("__TabControl OnControlAdded: " + e.Control.Name);

            updateTabBar(e);

            if (ControlAdded != null)
                ControlAdded(this, e);
        }

        void setTabsSize()
        {
            int w = this.__childWidth;
            this.__tabs.GetHTMLTarget().style.SetLocation(this.__childX, 0, w, __TAB_BAR_HEIGHT);
            this.__ul.style.SetLocation(0, 0, w, __TAB_BAR_HEIGHT);
        }

        void onBackColorChanged(object o, EventArgs e)
        {
            __Control c = (__Control)o;
            int argb = 0;
            try  // use 'try' in case typecast below fails
            {
                __TabPage _tp = (__TabPage)c;
            }
            catch { }
        }


        void onForeColorChanged(object o, EventArgs e)
        {
            Console.Write("Not Implemented: onBackColorChanged invoked");
        }


        void onLocationChanged(object o, EventArgs e)
        {
            __Control c = (__Control)o;

            try  // use 'try' in case typecast below fails
            {
                __TabPage _tp = (__TabPage)c;

                int x = _tp.Location.X;
                this.__childX = x;

                this.__tabs.GetHTMLTarget().style.SetLocation(x, 0);
            }
            catch { }

        }


        void onSizeChanged(object o, EventArgs e)
        {
            __Control c = (__Control)o;

            try  // use 'try' in case typecast below fails
            {
                __TabPage _tp = (__TabPage)c;

                int w = _tp.Size.Width;
                this.__childWidth = w;
            }
            catch { }

            setTabsSize();
        }


        void updateTabBar(ControlEventArgs e)
        {
            if (e.Control is TabPage)
            {
                TabPage tp = (TabPage)e.Control;
                AddToUL(tp);

                if (this._tabPages.Count == 1)
                {
                    SelectedTab = tp; // default selection
                }

                tp.SizeChanged += onSizeChanged;
                tp.LocationChanged += onLocationChanged;
                tp.BackColorChanged += onBackColorChanged;
                tp.ForeColorChanged += onForeColorChanged;
            }
        }


        public void AddToUL(TabPage tp)
        {
            __TabPage _tp = (__TabPage)tp;

            _tp.__assignClickEvent(delegate
                                        {
                                            SelectTab(tp);
                                        }
                                    );

            __ul.appendChild(_tp.Li);
            setTabsSize();

            Shared.Drawing.Rectangle r = _tp.getBounds();

            // TODO:
            // Calculates filler space between last tab and the end of the panel
            // this same calculation can be used to add an arrow tab to allow tabs to overflow right or left.
            // Tab overflow is not currently supported.

            /* 
             int calc = this.__ul.offsetWidth - _tp.Li.offsetLeft - _tp.Li.offsetWidth;
             this.__tabFiller.style.width = ""+calc+"px";
             this.__tabFiller.style.overflow = DOM.IStyle.OverflowEnum.auto;
             __ul.appendChild(this.__tabFiller);

             int intervalId = -1;

             Action a = delegate 
             {
                 Console.WriteLine("Calculate filler width=> this.__ul.offsetWidth=" + this.__ul.offsetWidth + "   this.__ul.offsetLeft=" + this.__ul.offsetLeft + "   _tp.Li. offsetLeft=" + _tp.Li.offsetLeft + "   offsetWidth=" + _tp.Li.offsetWidth);

                 calc = this.__ul.offsetLeft - this.__ul.offsetWidth - _tp.Li.offsetLeft - _tp.Li.offsetWidth;

                 Console.WriteLine("Using width: " + calc);

                 this.__tabFiller.style.width = "" + calc + "px";
                 Native.Window.clearInterval(intervalId);
             };
            
             intervalId = Native.Window.setInterval(a, 1500);
             */
        }


        public void AddToUL(IHTMLListItem li)
        {
            __ul.Add(li);
        }

        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return this.InternalContainer;
            }
        }

        public TabAlignment Alignment { get; set; }
        public TabAppearance Appearance { get; set; }
        public Color BackColor { get; set; }
        public Image BackgroundImage { get; set; }
        public ImageLayout BackgroundImageLayout { get; set; }
        protected Size DefaultSize { get { return DefaultSize; } }
        public Rectangle DisplayRectangle { get { return DisplayRectangle; } }
        //protected bool DoubleBuffered
        //{
        //    get { throw new NotImplementedException(); }
        //    set { throw new NotImplementedException(); }
        //}

        //public TabDrawMode DrawMode
        //{
        //    get { return DrawMode; }
        //    set { DrawMode = value; }
        //}

        //public Color ForeColor
        //{
        //    get { return ForeColor; }
        //    set { ForeColor = value; }
        //}

        //public bool HotTrack
        //{
        //    get { return HotTrack; }
        //    set { HotTrack = value; }
        //}

        //public ImageList ImageList
        //{
        //    get { return ImageList; }
        //    set { ImageList = value; }
        //}

        //public Size ItemSize
        //{
        //    get { return ItemSize; }
        //    set { ItemSize = value; }
        //}

        //public bool Multiline
        //{
        //    get { return Multiline; }
        //    set { Multiline = value; }
        //}

        public Point Padding { get; set; }

        //public virtual bool RightToLeftLayout
        //{
        //    get { return RightToLeftLayout; }
        //    set { RightToLeftLayout = value; }
        //}

        public int RowCount
        {
            get { return RowCount; }
        }


        public int SelectedIndex
        {

            get
            {
                int idx = this.TabPages.IndexOf(SelectedTab);
                return idx;
            }
            set
            {
                TabPage tp = this.TabPages[value];
                SelectedTab = tp;
            }
        }

        TabPage selectedTab;
        public TabPage SelectedTab
        {
            get { return selectedTab; }
            set
            {
                if (selectedTab != value)
                {
                    __TabPage _tp = null;
                    if (selectedTab != null)
                    {
                        _tp = (__TabPage)selectedTab;
                        _tp.__DeSelectTab(); // deselect currently selected
                    }

                    selectedTab = value;
                    _tp = (__TabPage)value;
                    _tp.SelectTab(); // select newly selected
                }
            }
        }

        //public bool ShowToolTips
        //{
        //    get { return ShowToolTips; }
        //    set { ShowToolTips = value; }
        //}

        // public TabSizeMode SizeMode
        //{
        //    get { return SizeMode; }
        //    set { SizeMode = value; }
        //}


        public int TabCount { get { return this._tabPages.Count; } }

        TabControl.TabPageCollection _tabPages;
        public TabControl.TabPageCollection TabPages { get { return this._tabPages; } }

        //public string Text
        //{
        //    get { return this.Text; }
        //    set { this.Text = value; }
        //}

        public event EventHandler BackColorChanged;

        public event EventHandler BackgroundImageChanged;

        public event EventHandler BackgroundImageLayoutChanged;

        public event TabControlEventHandler Deselected;

        public event TabControlCancelEventHandler Deselecting;

        public event DrawItemEventHandler DrawItem;

        public event EventHandler ForeColorChanged;

        public event PaintEventHandler Paint;

        public event EventHandler RightToLeftLayoutChanged;

        public event TabControlEventHandler Selected;

        public event EventHandler SelectedIndexChanged;

        public event TabControlCancelEventHandler Selecting;

        public event EventHandler TextChanged;

        protected Control.ControlCollection CreateControlsInstance()
        {
            throw new NotImplementedException();
        }

        protected void CreateHandle()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Makes the tab following the tab with the specified index the current tab.
        public void DeselectTab(int index)
        {
            SelectTab(this.SelectedIndex + 1);
        }

        //
        //     Makes the tab following the tab with the specified name the current tab.        
        public void DeselectTab(string tabPageName)
        {
            throw new NotImplementedException();
        }

        //
        //     Makes the tab following the specified System.Windows.Forms.TabPage the current
        //     tab.
        public void DeselectTab(TabPage tabPage)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {

        }

        public Control GetControl(int index)
        {
            return this.Controls[index];
        }

        protected virtual object[] GetItems()
        {
            throw new NotImplementedException();
        }

        protected object[] GetItems(Type baseType)
        {
            throw new NotImplementedException();
        }

        public Rectangle GetTabRect(int index)
        {
            throw new NotImplementedException();
        }

        protected string GetToolTipText(object item)
        {
            throw new NotImplementedException();
        }

        protected bool IsInputKey(Keys keyData)
        {
            return false;
        }

        protected virtual void OnDeselected(TabControlEventArgs e)
        {
            if (Deselected != null)
                Deselected(this, e);
        }

        protected virtual void OnDeselecting(TabControlCancelEventArgs e)
        {
            if (Deselecting != null)
                Deselecting(this, e);
        }

        public event EventHandler Enter;

        protected void OnEnter(EventArgs e)
        {
            if (Enter != null)
                Enter(this, null);
        }


        protected void OnFontChanged(object o, EventArgs e)
        {
            foreach (var c in Controls)
            {
                if (c is TabPage)
                {
                    __TabPage p = (__TabPage)c;
                    p.setFont(Font);
                }
            }
        }


        public event EventHandler HandleCreated;

        protected void OnHandleCreated(EventArgs e)
        {
            if (HandleCreated != null)
                HandleCreated(this, null);
        }

        public event EventHandler HandleDestroyed;

        protected void OnHandleDestroyed(EventArgs e)
        {
            if (HandleDestroyed != null)
                HandleDestroyed(this, null);
        }

        public event EventHandler KeyDown;

        protected void OnKeyDown(KeyEventArgs key)
        {
            if (KeyDown != null)
                KeyDown(this, null);
        }

        public event EventHandler Leave;

        protected void OnLeave(EventArgs e)
        {
            if (Leave != null)
                Leave(this, null);
        }

        public event EventHandler Resize;

        protected void OnResize(EventArgs e)
        {
            if (Resize != null)
                Resize(this, null);
        }

        protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
        {
            if (RightToLeftLayoutChanged != null)
                RightToLeftLayoutChanged(this, null);
        }

        protected virtual void OnSelected(TabControlEventArgs e)
        {
            if (Selected != null)
                Selected(this, null);
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, null);

        }

        protected virtual void OnSelecting(TabControlCancelEventArgs e)
        {
            if (Selecting != null)
                Selecting(this, null);
        }

        public event EventHandler StyleChanged;

        protected void OnStyleChanged(EventArgs e)
        {
            if (StyleChanged != null)
                StyleChanged(this, null);
        }

        protected bool ProcessKeyPreview(ref Message m)
        {
            throw new NotImplementedException();
        }

        protected void RemoveAll()
        {
            throw new NotImplementedException();
        }

        protected void ScaleCore(float dx, float dy)
        {
            throw new NotImplementedException();
        }

        public void SelectTab(int index)
        {
            if (index >= _tabPages.Count) return;

            TabPage tp = _tabPages[index];
            SelectTab(tp);
        }

        public void SelectTab(string tabPageName)
        {
            int foundidx = -1;

            for (int i = 0; i < _tabPages.Count; i++)
            {
                if (_tabPages[i].Name == tabPageName)
                    foundidx = i;
            }

            if (foundidx < 0) // not found
                return;

            TabPage tp = null;

            try
            {
                tp = _tabPages[foundidx];
            }
            catch { };

            if (tp != null)
                SelectTab(tp);
        }

        public void SelectTab(TabPage tabPage)
        {
            SelectedTab = tabPage;
        }

        public string ToString()
        {
            return this.Text;
        }

        protected void UpdateTabSelection(bool updateFocus)
        {
            throw new NotImplementedException();
        }

        //protected void WndProc(ref Message m)
        //{
        //    throw new NotImplementedException();
        //}


        /// <summary>
        /// IMPORTANT NOTE:
        ///     This TabControl.ControlCollection class is currently not being used by BCL. Instead it is defaulting to __Control.__ControlCollection
        ///     Future work can move the implementation to this class but should be similar to what __Control.__ControlCollection
        ///     is currently doing.
        ///     This is relevant when Windows Forms adds a TabPage to the TabControl in the form of TabControl.Controls.Add
        ///     At first glance TabControl is using member Controls but at Runtime it assigns Controls to be of type TabControl.ControlCollection.
        ///     So BCL would need to do the same and assign Controls to be of type TabControl.ControlCollection.
        ///     So curerntly since TabControl.ControlCollection base class is Control.ControlCollection, it is allowing the base class
        ///     to do all the work.
        /// </summary>
        [Script(Implements = typeof(global::System.Windows.Forms.TabControl.ControlCollection))]
        internal class __ControlCollection : __Control.__ControlCollection
        {

            readonly TabControl Owner;
            readonly List<Control> Items = new List<Control>();


            public __ControlCollection(TabControl owner)
                : base((Control)owner)
            {
                this.Owner = owner;
            }

            public override void Add(Control e)
            {
                Console.WriteLine("__TabControl.__ControlCollection .Add invoked");

                if (!(e is TabPage))
                    throw new InvalidOperationException();

                TabPage tp = (TabPage)e;
                this.Owner.TabPages.Add(tp);

                Items.Add(e);

                var bg = this.Owner.GetHTMLTargetContainer();

                if (bg.firstChild == null)
                    bg.appendChild(e.GetHTMLTarget());
                else
                    bg.insertBefore(e.GetHTMLTarget(), bg.firstChild);

                var c = (__Control)e;

                c.InternalAssignParent(this.Owner);

                ((__TabControl)this.Owner).OnControlAdded(new ControlEventArgs(e));

                //OnControlAdded(new ControlEventArgs(e));
            }

            public override void Remove(Control value)
            {
                throw new global::System.Exception("Not implemented");
            }

            /*public override int Count
            {
                get
                {
                    return base.Count;
                }
            }*/

        }


        [Script(Implements = typeof(global::System.Windows.Forms.TabControl.TabPageCollection))]
        internal class __TabPageCollection : /*IList,*/ ICollection, IEnumerable
        {
            readonly TabControl Owner;

            readonly List<TabPage> Items = new List<TabPage>();

            public __TabPageCollection(TabControl owner)
            {
                this.Owner = owner;
            }

            public int Count
            {
                get
                {
                    return Items.Count;
                }
            }

            public bool IsReadOnly { get { return false; } }

            public TabPage this[int index]
            {
                get
                {
                    if (index < 0)
                        throw new Exception("IndexOutOfRange");

                    if (index >= this.Count)
                        throw new Exception("IndexOutOfRange");

                    return (TabPage)Items[index];
                }

                /*[Script(DefineAsStatic = true)]
                get
                {
                    return (TabPage)Expando.InternalGetMember(this, index);
                }

                [Script(DefineAsStatic = true)]
                set
                {
                    Expando.InternalSetMember(this, index, value);
                }*/
            }

            public TabPage this[string key]
            {
                get
                {
                    TabPage rtn = null;

                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        if (rtn == null)
                        {
                            if (this.Items[i].Text == key)
                                rtn = this.Items[i];
                        }
                    }

                    return rtn;
                }
            }

            public void Add(string text)
            {
                TabPage tp = new TabPage();
                tp.Text = text;
                Add(tp);
            }

            /* public int Add(object text)
             {
                 TabPage tp = new TabPage();
                 tp.Text = (string)text;
                 Add(tp);

                 return getIndex(tp);
             }*/

            public void Add(TabPage value)
            {
                this.Items.Add(value);

                __TabControl tc = (__TabControl)(Owner);
                __TabPage tp = (__TabPage)value;

                var bg = this.Owner.GetHTMLTargetContainer();

                if (bg.firstChild == null)
                    bg.appendChild(value.GetHTMLTarget());
                else
                    bg.insertBefore(value.GetHTMLTarget(), bg.firstChild);

                var c = (__TabPage)value;

                c.InternalAssignParent(this.Owner);


                ((__TabControl)this.Owner).OnControlAdded(new ControlEventArgs(value));

                if (!firstSelected)
                {
                    firstSelected = true;
                    Owner.SelectTab(value);
                }
            }

            int getIndex(TabPage tp)
            {
                for (int i = 0; i < Items.Count; i++)
                    if (Items[i] == tp)
                        return i;

                return -1;
            }


            /* public void Add(string key, string text)
             {
                 this.Items.Add(new TabPage(text) { Name = key });
             }
             */

            public void Add(string key, string text, int imageIndex)
            {
                throw new NotImplementedException();
            }


            public void Add(string key, string text, string imageKey)
            {
                throw new NotImplementedException();
            }

            public void AddRange(TabPage[] pages)
            {
                foreach (var p in pages)
                    Add(p);
            }

            public virtual void Clear()
            {
                this.Items.Clear();
            }

            public bool Contains(TabPage page)
            {
                bool rtn = false;
                foreach (var item in this.Items)
                {
                    if (!rtn)
                    {
                        if (item == page)
                            rtn = true;
                    }
                }

                return rtn;
            }

            public bool ContainsKey(string key)
            {
                bool rtn = false;
                foreach (var item in this.Items)
                {
                    if (!rtn)
                    {
                        if (item.Name == key)
                            rtn = true;
                    }
                }

                return rtn;
            }

            public bool Contains(object value)
            {
                return Contains((TabPage)value);
            }

            public IEnumerator GetEnumerator()
            {
                return this.Items.GetEnumerator();
            }

            public int IndexOf(TabPage page)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i] == page)
                        return i;
                }

                return -1;
            }

            public int IndexOfKey(string key)
            {
                for (int i = 0; i < this.Items.Count; i++)
                    if (this.Items[i].Name == key)
                        return i;

                return -1;
            }

            public int IndexOf(object value)
            {
                TabPage tp = (TabPage)value;

                return IndexOfKey(tp.Name);
            }

            /*
            public void Insert(int index, string text)
            {
                this.Items.Insert(index, new TabPage(text));
            }
             * */

            public void Insert(int index, TabPage tabPage)
            {
                this.Items.Insert(index, tabPage);
            }

            /*
            public void Insert(int index, string key, string text)
            {
                this.Items.Insert(index, new TabPage(text) { Name = key });
            }*/

            public void Insert(int index, string key, string text, int imageIndex)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, string key, string text, string imageKey)
            {
                throw new NotImplementedException();
            }

            public void Remove(TabPage value)
            {
                this.Items.Remove(value);
            }


            public void RemoveAt(int index)
            {
                this.Items.RemoveAt(index);
            }

            public void RemoveByKey(string key)
            {
                TabPage found = null;
                foreach (var item in this.Items)
                {
                    if (item.Name == key)
                    {
                        found = item;
                        break;
                    }
                }

                if (found != null)
                    this.Items.Remove(found);
            }

            public void CopyTo(Array array, int index)
            {
                throw new NotImplementedException();
            }

            public bool IsSynchronized { get { return false; } }

            public object SyncRoot { get { return null; } }

            public bool IsFixedSize { get { return false; } }

            public void Insert(int index, object tabPage)
            {
                this.Items.Insert(index, (TabPage)tabPage);
            }

            public void Remove(object tabPage)
            {
                if (this.Items.Contains((TabPage)tabPage))
                    this.Items.Remove((TabPage)tabPage);
            }
        }
    }
}
