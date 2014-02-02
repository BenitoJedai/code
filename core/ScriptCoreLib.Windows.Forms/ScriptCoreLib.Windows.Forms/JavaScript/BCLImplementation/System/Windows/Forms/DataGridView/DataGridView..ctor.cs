using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        public Stopwatch DataGridViewConstructorStopwatch = Stopwatch.StartNew();


        public IHTMLTable __ContentTable;


        // typeof(__DataGridView) InternalElement 
        // name as OuterElement or InternalOuterElement?
        public readonly CSSStyleRuleMonkier css;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140124
        // X:\jsc.svn\examples\javascript\forms\Test\TestCSSButton\TestCSSButton\Application.cs
        // cant we use typeof here?
        public static string __ContentTable_className = "__DataGridViewContentTable";
        public static string __ColumnsTable_className = "__DataGridViewColumnsTable";
        public static string __RowsTable_className = "__DataGridViewRowsTable";

        public readonly CSSStyleRuleMonkier __ContentTable_css;
        public CSSStyleRuleMonkier __ContentTable_css_td;
        public CSSStyleRuleMonkier __ContentTable_css_alt_td;
        public CSSStyleRuleMonkier __ContentTable_css_odd_td;

        public IHTMLTable __ColumnsTable;
        public CSSStyleRuleMonkier __ColumnsTable_css;
        public CSSStyleRuleMonkier __ColumnsTable_css_td;

        public IHTMLTable __RowsTable;
        public CSSStyleRuleMonkier __RowsTable_css;
        public CSSStyleRuleMonkier __RowsTable_css_td;

        // lets make this the first property
        // to live in the DOM world instead
        #region AllowUserToResizeColumns
        public XAttribute AllowUserToResizeColumnsAttribute;
        public bool AllowUserToResizeColumns
        {
            get
            {
                return (bool)this.AllowUserToResizeColumnsAttribute;
            }
            set
            {
                AllowUserToResizeColumnsAttribute.Value = "" + value;

            }
        }
        #endregion

        #region RowHeadersVisible
        public event Action InternalRowHeadersVisibleChanged;
        public XAttribute RowHeadersVisibleAttribute;
        public bool RowHeadersVisible
        {
            get
            {
                return (bool)this.RowHeadersVisibleAttribute;
            }
            set
            {
                RowHeadersVisibleAttribute.Value = "" + value;

                // event managed by css instead?
                if (InternalRowHeadersVisibleChanged != null)
                    InternalRowHeadersVisibleChanged();
            }
        }
        #endregion

        public __DataGridView()
        {
            //Console.WriteLine("enter DataGridView .ctor");

            this.InternalElement = new IHTMLDiv
            {

                // do this ahead of time
                // when can we have a special type for a classname string?
                className = typeof(DataGridView).Name
            };

            // can jsc help us here and via [HTMLAttribute] redirect the data to html attribute?
            this.AllowUserToResizeColumnsAttribute = new XAttribute("AllowUserToResizeColumns", "").AttachTo(this.InternalElement);
            this.RowHeadersVisibleAttribute = new XAttribute("RowHeadersVisible", "true").AttachTo(this.InternalElement);


            // add the rule to current document.
            // what happens if we do popup?
            // wha about scoped style?
            this.css = this.InternalElement.css;

            // do we need this?
            this.InternalElement.style.overflow = DOM.IStyle.OverflowEnum.hidden;



            this.InternalColumns = new __DataGridViewColumnCollection();
            this.Columns = (DataGridViewColumnCollection)(object)this.InternalColumns;

            #region InternalRows
            this.InternalRows = new __DataGridViewRowCollection();
            this.Rows = (DataGridViewRowCollection)(object)this.InternalRows;

            this.InternalRows.InternalItems.Added +=
                (s, i) =>
                {
                    s.InternalContext = this;
                };
            #endregion


            #region SelectedCells
            this.InternalSelectedCells = new __DataGridViewSelectedCellCollection();
            this.SelectedCells = (DataGridViewSelectedCellCollection)(object)this.InternalSelectedCells;
            this.InternalSelectedCells.InternalItems.ListChanged +=
                (_s, _e) =>
                {
                    if (_e.ListChangedType == ListChangedType.ItemAdded)
                    {
                        var item = this.InternalSelectedCells.InternalItems[_e.NewIndex];

                        // when is this null?
                        //if (this.DefaultCellStyle == null)
                        //{
                        //    item.InternalContentContainer.style.backgroundColor = JSColor.System.Highlight;
                        //}
                        //else
                        //{
                        var SelectionBackColor = this.DefaultCellStyle.SelectionBackColor;
                        var SelectionForeColor = this.DefaultCellStyle.SelectionForeColor;

                        item.InternalContentContainer.style.backgroundColor = SelectionBackColor.ToString();
                        //}
                        //item.InternalContentContainer.style.color = JSColor.System.HighlightText;

                        // tested by
                        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs
                        item.InternalContentContainer.style.color = SelectionForeColor.ToString();
                    }


                    if (SelectionChanged != null)
                        SelectionChanged(this, new EventArgs());
                };
            #endregion


            this.MultiSelect = true;



            this.InternalSetDefaultFont();

            this.InternalScrollContainerElement = new IHTMLDiv
            {
                // pstyle
                className = "InternalScrollContainerElement"
            }.AttachTo(this.InternalElement);




            //this.InternalScrollContainerElement.style.backgroundColor = JSColor.Gray;
            this.BackgroundColor = global::System.Drawing.SystemColors.AppWorkspace;

            this.InternalScrollContainerElement.style.overflow = DOM.IStyle.OverflowEnum.auto;

            // tested by
            // X:\jsc.svn\examples\javascript\css\CSSPrintMediaExperiment\CSSPrintMediaExperiment\Application.cs

            // for printer we do not want to see the scollbar
            // if we change the document we will loose the style?
            // this wont work
            //IStyleSheet.Default
            //    [CSSMediaTypes.print]
            //    [this.InternalScrollContainerElement].style.overflow =
            //        IStyle.OverflowEnum.hidden;



            this.InternalScrollContainerElement.style.position = DOM.IStyle.PositionEnum.absolute;
            this.InternalScrollContainerElement.style.left = "0px";
            this.InternalScrollContainerElement.style.top = "0px";
            this.InternalScrollContainerElement.style.right = "0px";
            this.InternalScrollContainerElement.style.bottom = "0px";

            this.InternalGridColor_css = this.InternalScrollContainerElement.css[" *[data-resizer='resizer']"];

            var __ContentTableContainer = new IHTMLDiv { className = "__ContentTableContainer" }.AttachTo(InternalScrollContainerElement);

            // 116ms css.style { selectorText = table.__ContentTable[style-id="2"] > tbody > tr > td } 
            this.__ContentTable = new IHTMLTable
            {
                className = __ContentTable_className,
                cellPadding = 0,
                cellSpacing = 0
            }.AttachTo(__ContentTableContainer);

            // X:\jsc.svn\examples\javascript\css\CSSOdd\CSSOdd\Application.cs

            //this.__ContentTable_css = css.descendants[className];
            this.__ContentTable_css = css[this.__ContentTable];

            //css.adjacentSibling[]
            //css.siblings

            //92ms css.style { selectorText = div.DataGridView[style-id="2"] table.__DataGridViewContentTable > tbody > tr > td } view-source:34816

            // view-source:34816
            //92ms css.style { selectorText = table.__DataGridViewColumnsTable[style-id="3"] > tbody > tr > td } 

            // the hacky way:
            //this.__ContentTable_css = css[" table." + this.__ContentTable.className];

            this.__ContentTable_css_td = this.__ContentTable_css
                [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            this.__ContentTable_css_alt_td = this.__ContentTable_css
                [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr].even[IHTMLElement.HTMLElementEnum.td];

            this.__ContentTable_css_odd_td = this.__ContentTable_css
                [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr].odd[IHTMLElement.HTMLElementEnum.td];


            __ContentTable.style.paddingTop = "22px";



            var __ColumnsTableContainer = new IHTMLDiv
            {
                className = "__ColumnsTableContainer"
            }.AttachTo(InternalScrollContainerElement);

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131213-forms-css
            this.__ColumnsTable = new IHTMLTable
            {
                className = __ColumnsTable_className,

                cellPadding = 0,
                cellSpacing = 0
            }.AttachTo(
            __ColumnsTableContainer);

            this.__ColumnsTable_css = css[this.__ColumnsTable];
            this.__ColumnsTable_css_td = this.__ColumnsTable_css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            IHTMLTableRow __ColumnsTableRow = null;

            //__ColumnsTableContainer.style.SetLocation(0, 0);
            __ColumnsTableContainer.style.position = IStyle.PositionEnum.absolute;
            __ColumnsTableContainer.style.left = "0px";

            __ColumnsTableRow = __ColumnsTable.AddBody().AddRow();
            __ColumnsTableRow.style.height = "22px";


            var __RowsTableContainer = new IHTMLDiv
            {
                className = "__RowsTableContainer"
            }.AttachTo(InternalScrollContainerElement);

            //__RowsTableContainer.style.SetLocation(0, 0);
            __RowsTableContainer.style.position = IStyle.PositionEnum.absolute;
            __RowsTableContainer.style.top = "0px";


            this.__RowsTable = new IHTMLTable
            {
                className = __RowsTable_className,
                cellPadding = 0,
                cellSpacing = 0
            }.AttachTo(__RowsTableContainer);

            // should we make the monkier a bit lazier?
            this.__RowsTable_css = css[this.__RowsTable];

            // 139ms { __RowsTable_css = { selectorText = div.DataGridView[style-id="2"] > div:nth-of-type(1) > div:nth-of-type(3) > table:nth-of-type(1), selectorElement =  } } 
            //Console.WriteLine(new { this.__RowsTable_css });
            this.__RowsTable_css_td = this.__RowsTable_css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];
            this.__RowsTable_css_td.style.backgroundColor = "cyan";


            __RowsTable.style.paddingTop = "22px";
            IHTMLTableBody __RowsTableBody = __RowsTable.AddBody();


            #region Corner
            this.__Corner = new IHTMLDiv().AttachTo(InternalScrollContainerElement);


            __Corner.style.position = IStyle.PositionEnum.absolute;
            //__Corner.style.SetLocation(0, 0);
            __Corner.style.height = "22px";

            #endregion

            // too slow for onscroll
            //var css_fixed_left =
            //    __RowsTableContainer.css
            //    | __Corner.css;

            //var css_fixed_top =
            //   __ColumnsTableContainer.css
            //   | __Corner.css;

            Action onscroll = delegate
            {
                // perhaps we should only use .css for static styles?

                // how much faster are we if we skip .css ?
                __Corner.style.top = this.InternalScrollContainerElement.scrollTop + "px";
                __ColumnsTableContainer.style.top = this.InternalScrollContainerElement.scrollTop + "px";

                __Corner.style.left = this.InternalScrollContainerElement.scrollLeft + "px";
                __RowsTableContainer.style.left = this.InternalScrollContainerElement.scrollLeft + "px";


                //css_fixed_left.style.left = this.InternalScrollContainerElement.scrollLeft + "px";
                //css_fixed_top.style.top = this.InternalScrollContainerElement.scrollTop + "px";
            };

            IHTMLTableBody __ContentTableBody = __ContentTable.AddBody();

            var InternalNewRow = new __DataGridViewRow();


            InternalNewRow.InternalTableRow = __ContentTableBody.AddRow();
            InternalNewRow.InternalTableRow.style.height = "22px";


            this.InternalRows.InternalItems.Source.Add(InternalNewRow);

            // http://www.w3schools.com/cssref/sel_last-of-type.asp
            // dont we have lastOfType available yet?
            var InternalNewRow_content_css = __ContentTable_css
                [IHTMLElement.HTMLElementEnum.tbody].last[IHTMLElement.HTMLElementEnum.tr];

            var InternalNewRow_header_css = __RowsTable_css
                [IHTMLElement.HTMLElementEnum.tbody].last[IHTMLElement.HTMLElementEnum.tr];

            var InternalNewRow_css = InternalNewRow_content_css | InternalNewRow_header_css;


            this.AllowUserToAddRowsChanged +=
                delegate
                {
                    // conditional css?
                    if (this.AllowUserToAddRows)
                        InternalNewRow_css.style.display = IStyle.DisplayEnum.empty;
                    else
                        InternalNewRow_css.style.display = IStyle.DisplayEnum.none;
                };


            this.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = global::System.Drawing.SystemColors.Window
            };

            this.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = global::System.Drawing.SystemColors.ButtonFace
            };

            this.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = global::System.Drawing.SystemColors.ButtonFace
            };




            #region CreateVerticalResizer --
            Func<IHTMLDiv> CreateVerticalResizer =
                () =>
                {
                    var r = new IHTMLDiv { className = "VerticalResizer" };

                    r.style.position = DOM.IStyle.PositionEnum.absolute;
                    r.style.height = "9px";
                    r.style.left = "0px";
                    r.style.width = "200px";
                    //HorizontalResizer.style.backgroundColor = JSColor.Red;
                    //r.style.cursor = DOM.IStyle.CursorEnum.move;

                    var l = new IHTMLDiv().AttachTo(r);

                    l.style.position = DOM.IStyle.PositionEnum.absolute;
                    l.style.top = "4px";
                    l.style.height = "1px";
                    l.style.left = "0px";
                    l.style.right = "0px";

                    //l.style.backgroundColor = this.InternalBackgroundColor.ToString();
                    ////l.style.backgroundColor = "yellow";

                    //InternalBackgroundColorChanged +=
                    //    delegate
                    //    {
                    //        l.style.backgroundColor = this.InternalBackgroundColor.ToString();
                    //    };


                    l.setAttribute("data-resizer", "resizer");

                    //this.InternalGridColorTargets.Add(
                    //     l.css
                    // );



                    this.ClientSizeChanged +=
                       delegate
                       {
                           r.style.width = "200x";
                           //r.Hide();

                           Native.window.requestAnimationFrame +=
                               //new ScriptCoreLib.JavaScript.Runtime.Timer(
                               delegate
                               {
                                   r.style.width = this.InternalScrollContainerElement.clientWidth + "px";
                                   //l.style.backgroundColor = "red";
                                   //r.Show();

                               }
                           ;
                           //).StartTimeout(200);
                       };


                    return r;
                };
            #endregion


            //css[(dynamic x) => x.AllowUserToResizeColumns == false][" .HorizontalResizer"].style.display = IStyle.DisplayEnum.none;
            //css[x => x.getAttribute("AllowUserToResizeColumns") == false][" .HorizontalResizer"].style.display = IStyle.DisplayEnum.none;

            css[new XAttribute("AllowUserToResizeColumns", "false")][" .HorizontalResizer"].style.display = IStyle.DisplayEnum.none;


            #region CreateHorizontalResizer |
            Func<IHTMLDiv> CreateHorizontalResizer =
                () =>
                {
                    var _HorizontalResizer = new IHTMLDiv { className = "HorizontalResizer" };

                    // what about older rules?
                    // shall they stop existing once the new once is used?
                    //css_fixed_top |= _HorizontalResizer.css;

                    onscroll();

                    _HorizontalResizer.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizer.style.width = "9px";

                    _HorizontalResizer.css.style.height = "22px";
                    _HorizontalResizer.css.hover.style.height = "100%";
                    _HorizontalResizer.css.active.style.height = "100%";

                    _HorizontalResizer.style.cursor = DOM.IStyle.CursorEnum.move;

                    var _HorizontalResizerLine = new IHTMLDiv().AttachTo(_HorizontalResizer);

                    _HorizontalResizerLine.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizerLine.style.left = "4px";
                    _HorizontalResizerLine.style.width = "1px";
                    _HorizontalResizerLine.style.top = "0px";
                    _HorizontalResizerLine.style.bottom = "0px";

                    //_HorizontalResizer.css.active.first.style.color = "blue";
                    //_HorizontalResizer.css.style.backgroundColor = "yellow";
                    //_HorizontalResizer.css.active.style.backgroundColor = "cyan";

                    // debug
                    //_HorizontalResizer.css.first.style.backgroundColor = "cyan";

                    _HorizontalResizer.css.hover.first.style.backgroundColor = "black";
                    _HorizontalResizer.css.active.first.style.backgroundColor = "blue";


                    // used by?
                    _HorizontalResizerLine.setAttribute("data-resizer", "resizer");

                    return _HorizontalResizer;
                };
            #endregion

            var ZeroVerticalResizer = CreateVerticalResizer().AttachTo(InternalScrollContainerElement);

            ZeroVerticalResizer.style.SetLocation(0, 22 - 5);




            #region ZeroHorizontalResizer

            var ZeroHorizontalResizer = CreateHorizontalResizer().AttachTo(InternalScrollContainerElement);

            var ZeroHorizontalResizerDrag = new DragHelper(ZeroHorizontalResizer)
            {
                // why cant I see it?
                Position = new Point(32, 0),
                Enabled = true
            };


            Action UpdateToVerticalResizerScroll = delegate
            {
                ZeroVerticalResizer.style.SetLocation(
                    this.InternalScrollContainerElement.scrollLeft,
                    this.InternalScrollContainerElement.scrollTop + (22 - 5)
                );
            };



            #region UpdateToHorizontalResizerScroll
            Action UpdateToHorizontalResizerScroll = delegate
            {
                ZeroHorizontalResizer.style.SetLocation(
                        this.InternalScrollContainerElement.scrollLeft + ZeroHorizontalResizerDrag.Position.X - 1,
                        this.InternalScrollContainerElement.scrollTop
                    );
            };
            #endregion

            // what if the the value is changed in the inspector/
            // will our control survive the change? as we dont get any events for that.
            // almost. the Fill will not be recalculate just yet tho
            // tested by
            // X:\jsc.svn\examples\javascript\forms\Test\TestFlowDataGridPadding\TestFlowDataGridPadding\Application.cs
            css[new XAttribute("RowHeadersVisible", "false")][__ColumnsTable, __ContentTable].style.paddingLeft = "1px";
            var css_RowHeadersVisible_true = css[new XAttribute("RowHeadersVisible", "true")][__ColumnsTable, __ContentTable];




            #region UpdateToHorizontalResizerDrag
            Action UpdateToHorizontalResizerDrag = delegate
            {
                //var value = (ZeroHorizontalResizerDrag.Position.X + 4);
                var value = (ZeroHorizontalResizerDrag.Position.X + 4);

                // no we want it completly gone, not just at 4px
                if (this.RowHeadersVisible)
                {

                    __Corner.style.width = (value - 2) + "px";

                    __RowsTable.style.width = value + "px";
                    __RowsTable.style.minWidth = value + "px";

                    // has 2 borders
                }

                css_RowHeadersVisible_true.style.paddingLeft = value + "px";
            };

            #endregion

            // when this.RowHeadersVisible == false
            css[new XAttribute("RowHeadersVisible", "false")]
                [ZeroHorizontalResizer,
                __Corner,
                __RowsTable]
                .style.display = IStyle.DisplayEnum.none;

            UpdateToHorizontalResizerScroll();
            UpdateToHorizontalResizerDrag();

            InternalRowHeadersVisibleChanged +=
              delegate
              {
                  // tested by
                  // X:\jsc.svn\examples\javascript\Test\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\ApplicationControl.cs
                  UpdateToHorizontalResizerDrag();
                  UpdateToHorizontalResizerScroll();
              };


            #region ZeroHorizontalResizerDrag Drag
            ZeroHorizontalResizerDrag.DragStart +=
                delegate
                {
                    Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.move;
                    //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = JSColor.Blue;
                };



            ZeroHorizontalResizerDrag.DragStop +=
                 delegate
                 {
                     Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.auto;
                     //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = this.InternalBackgroundColor.ToString();
                     //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = "";
                     //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = "yellow";

                     UpdateToHorizontalResizerDrag();
                     InternalAutoSizeWhenFill();
                 };



            ZeroHorizontalResizerDrag.DragMove +=
                delegate
                {
                    UpdateToHorizontalResizerScroll();

                };
            #endregion

            #endregion



            onscroll();

            #region onscroll
            this.InternalScrollContainerElement.onscroll +=
               e =>
               {
                   // onscroll is high performance.
                   // using .css will slow us down 10x?

                   //var s = Stopwatch.StartNew();

                   UpdateToVerticalResizerScroll();
                   UpdateToHorizontalResizerScroll();


                   // 153209ms DataGridView onscroll { ElapsedMilliseconds = 13 }
                   // should jsc inline for performance?
                   onscroll();

                   // 35418ms DataGridView onscroll { ElapsedMilliseconds = 20 }
                   // 234208ms DataGridView onscroll { ElapsedMilliseconds = 120 } 
                   // 10468ms DataGridView onscroll { ElapsedMilliseconds = 27 } 
                   //Console.WriteLine("DataGridView onscroll " + new { s.ElapsedMilliseconds });
               };
            #endregion

            __DataGridViewCell MouseCaptureCell = null;

            InternalScrollContainerElement.onmouseup +=
                delegate
                {
                    MouseCaptureCell = null;
                };

            #region InitializeCell
            Action<__DataGridViewCell, __DataGridViewRow> InitializeMissingCell =
                (SourceCell, SourceRow) =>
                {
                    //Console.WriteLine("InitializeCell  " + new { SourceCell.ColumnIndex });

                    // is cell index equal to column index?
                    // what happens if we dont have enough columns?
                    var SourceColumn = this.InternalColumns.InternalItems[SourceCell.ColumnIndex];




                    SourceCell.InternalTableColumn = SourceRow.InternalTableRow.AddColumn();


                    SourceRow.InternalCells.InternalItemsX.Removed +=
                         (XRemovedCell, XRemovedCellIndex) =>
                         {
                             if (XRemovedCell == SourceCell)
                             {
                                 SourceCell.InternalTableColumn.Orphanize();
                             }
                         };

                    SourceCell.InternalTableColumn.style.position = IStyle.PositionEnum.relative;



                    // this wont work if we have multiple datagrids
                    // can we have a test for it?
                    SourceCell.InternalContentContainer = new IHTMLDiv { }.AttachTo(SourceCell.InternalTableColumn);
                    SourceCell.InternalContentContainer.tabIndex = (((SourceRow.Index + 1) << 16) + (SourceCell.ColumnIndex + 1));

                    // http://stackoverflow.com/questions/6601697/restore-webkits-css-outline-on-input-field
                    SourceCell.InternalContentContainer.style.outline = "none";
                    //outline-width: 0;

                    SourceCell.InternalContentContainer.style.overflow = IStyle.OverflowEnum.hidden;
                    SourceCell.InternalContentContainer.style.position = IStyle.PositionEnum.relative;
                    SourceCell.InternalContentContainer.style.left = "0";
                    SourceCell.InternalContentContainer.style.top = "0";
                    SourceCell.InternalContentContainer.style.height = (SourceRow.Height - 1) + "px";

                    ////SourceCell.InternalTableColumn.style.borderBottom = "1px solid rgba(0,0,0, 0.4)";
                    //SourceCell.InternalTableColumn.style.borderBottom = "2px solid red";



                    // should we clone? 
                    //{
                    //    var BackColor = this.DefaultCellStyle.BackColor;
                    //    SourceCell.InternalStyle.InternalBackColor = BackColor;
                    //}




                    //#region AtInternalWidthChanged
                    //Action AtInternalWidthChanged = delegate
                    //{
                    //    // !!!

                    //    //SourceCell.InternalContentContainer.style.width = SourceColumn.Width + "px";


                    //    //SourceCell.InternalTableColumn.style.width = SourceColumn.Width + "px";
                    //    //SourceCell.InternalTableColumn.style.minWidth = SourceColumn.Width + "px";

                    //    //Console.WriteLine(new { SourceColumn.Name, width = SourceColumn.Width });
                    //    //c1.style.backgroundColor = JSColor.Red;
                    //    //c1content.innerText = "@" + InternalColumn.HeaderText + ":" + InternalColumn.Width;
                    //};

                    //AtInternalWidthChanged();

                    //SourceColumn.InternalWidthChanged += AtInternalWidthChanged;

                    //SourceColumn.InternalWidthChanged +=
                    //    delegate
                    //    {



                    //    };

                    //#endregion

                    SourceCell.InternalContent = new IHTMLSpan { };
                    var InternalContent = SourceCell.InternalContent;

                    #region AtInternalValueChanged
                    Action AtInternalValueChanged = delegate
                    {
                        InternalRaiseCellFormatting(SourceCell);

                        //var innerText = SourceCell.Value.ToString();
                        var innerText = SourceCell.FormattedValue.ToString();

                        //Console.WriteLine("AtInternalValueChanged " + new { innerText });
                        InternalContent.innerText = innerText;

                        InternalRaiseCellValueChanged(SourceCell);

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140104-deploy
                        InternalContent.title = SourceCell.InternalToolTipText;
                    };

                    AtInternalValueChanged();
                    SourceCell.InternalValueChanged += AtInternalValueChanged;
                    SourceCell.InternalToolTipTextChanged += AtInternalValueChanged;
                    #endregion

                    #region __DataGridViewButtonCell
                    if (SourceCell is __DataGridViewButtonCell)
                    {
                        var InternalButton = new IHTMLButton().AttachTo(SourceCell.InternalContentContainer);

                        InternalButton.style.font = this.Font.ToCssString();

                        InternalButton.style.position = IStyle.PositionEnum.absolute;
                        InternalButton.style.left = "0px";
                        InternalButton.style.top = "0px";
                        InternalButton.style.right = "0px";
                        InternalButton.style.bottom = "0px";

                        InternalContent.AttachTo(InternalButton);

                        InternalButton.onclick +=
                            delegate
                            {
                                if (this.CellContentClick != null)
                                    this.CellContentClick(this,
                                        new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceRow.Index)
                                    );
                            };

                        return;
                    }
                    #endregion

                    //SourceCell.InternalTableColumn.style.backgroundColor = JSColor.Yellow;


                    InternalContent.AttachTo(SourceCell.InternalContentContainer);
                    InternalContent.style.marginLeft = "4px";
                    InternalContent.style.marginRight = "4px";
                    InternalContent.style.lineHeight = (SourceRow.Height - 1) + "px";
                    InternalContent.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
                    //InternalContent.style.textO;

                    //c1content.style.margin = "6px";

                    #region CellAtOffset
                    Func<int, int, __DataGridViewCell> CellAtOffset =
                        (x, y) =>
                        {
                            var value = default(__DataGridViewCell);

                            var Row = this.InternalRows.InternalItems.Source.ElementAtOrDefault(
                                SourceRow.Index + y
                            );

                            if (Row == null)
                                if (SourceRow.Index + y == this.InternalRows.Count)
                                    Row = InternalNewRow;

                            if (Row != null)
                            {
                                value = Row.InternalCells.InternalItems.ElementAtOrDefault(
                                    SourceCell.ColumnIndex + x
                                );
                            }

                            return value;
                        };
                    #endregion

                    bool ExitEditModeDone = true;

                    #region EnterEditMode
                    Action EnterEditMode =
                        delegate
                        {
                            if (this.ReadOnly)
                                return;

                            if (SourceCell.ReadOnly)
                                return;

                            if (SourceColumn.ReadOnly)
                                return;

                            if (!ExitEditModeDone)
                                return;


                            SourceCell.IsInEditMode = true;
                            ExitEditModeDone = false;

                            SourceCell.InternalContentContainer.Orphanize();

                            var EditElement = new IHTMLInput(Shared.HTMLInputTypeEnum.text);

                            EditElement.style.backgroundColor = "transparent";



                            EditElement.style.font = this.Font.ToCssString();


                            EditElement.style.borderWidth = "0";
                            EditElement.style.position = IStyle.PositionEnum.relative;
                            EditElement.style.left = "4px";
                            EditElement.style.top = "0";

                            EditElement.style.outline = "0";
                            EditElement.style.padding = "0";
                            EditElement.style.width = (SourceColumn.Width - 4) + "px";
                            EditElement.style.height = (SourceRow.Height - 1) + "px";

                            EditElement.AttachTo(SourceCell.InternalTableColumn);

                            SourceCell.InternalStyle.InternalForeColorChanged +=
                                delegate
                                {
                                    EditElement.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                                };

                            var OriginalValue = (string)SourceCell.Value;
                            EditElement.value = OriginalValue;


                            #region CheckChanges
                            Action CheckChanges = delegate
                            {
                                //if (((string)SourceCell.Value) != EditElement.value)
                                //{

                                var args = new __DataGridViewCellValidatingEventArgs(
                                    SourceCell.ColumnIndex,
                                     SourceRow.Index
                                )
                                {

                                    FormattedValue = EditElement.value
                                };

                                // tested by
                                // X:\jsc.svn\examples\javascript\forms\FormsDataGridViewDeleteRow\FormsDataGridViewDeleteRow\ApplicationControl.cs
                                if (this.CellValidating != null)
                                    this.CellValidating(this, (DataGridViewCellValidatingEventArgs)(object)args);

                                Console.WriteLine("CellValidating " + new { args.Cancel });

                                if (args.Cancel)
                                {
                                    Console.WriteLine("CellValidating Cancel " + new { OriginalValue });
                                    SourceCell.Value = OriginalValue;

                                    return;
                                }


                                SourceCell.Value = EditElement.value;

                                //}

                            };
                            #endregion

                            #region ExitEditMode
                            Action ExitEditMode = delegate
                            {
                                if (ExitEditModeDone) return;
                                ExitEditModeDone = true;
                                SourceCell.IsInEditMode = false;


                                EditElement.Orphanize();
                                SourceCell.InternalContentContainer.AttachTo(SourceCell.InternalTableColumn);

                                //SourceCell.InternalStyle.InternalForeColorChanged +=
                                //    delegate
                                //    {
                                //        SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                                //    };

                                //SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();


                                InternalRaiseCellEndEdit(SourceCell);

                                if (OriginalValue == (string)SourceCell.Value)
                                    return;

                                this.AutoResizeColumn(SourceCell.ColumnIndex);

                                //InternalRaiseCellFormatting(SourceCell);

                                Console.WriteLine("ExitEditMode AtInternalValueChanged");
                                AtInternalValueChanged();
                            };
                            #endregion



                            #region CellBeginEdit
                            EditElement.onfocus +=
                                delegate
                                {

                                    EditElement.select();
                                };
                            EditElement.focus();

                            InternalRaiseCellFormatting(SourceCell);

                            InternalRaiseCellBeginEdit(SourceCell);

                            #endregion


                            #region onblur
                            EditElement.onblur +=
                               delegate
                               {
                                   //Console.WriteLine("EditElement.onblur");

                                   if (CheckChanges != null)
                                       CheckChanges();

                                   if (ExitEditMode != null)
                                       ExitEditMode();


                               };
                            #endregion


                            var __selectionStart = -1;
                            var __selectionEnd = -1;
                            #region onkeyup
                            EditElement.onkeyup +=
                              _ev =>
                              {
                                  #region Focus
                                  Action<__DataGridViewCell> Focus =
                                      Cell =>
                                      {
                                          _ev.PreventDefault();
                                          _ev.StopPropagation();

                                          if (Cell != null)
                                          {
                                              Cell.InternalContentContainer.focus();
                                          }
                                      };
                                  #endregion

                                  if (_ev.IsEscape)
                                  {
                                      CheckChanges = null;

                                      ExitEditMode();

                                      Focus(SourceCell);
                                      return;
                                  }

                                  if (_ev.KeyCode == (int)Keys.Up)
                                  {
                                      Focus(CellAtOffset(0, -1));
                                      return;
                                  }

                                  if (_ev.KeyCode == (int)Keys.Down)
                                  {
                                      Focus(CellAtOffset(0, 1));
                                      return;
                                  }

                                  if (_ev.KeyCode == (int)Keys.Right)
                                      if (EditElement.selectionStart == __selectionStart)
                                          if (EditElement.selectionEnd == __selectionEnd)
                                              if (__selectionEnd == __selectionStart)
                                                  if (__selectionStart == EditElement.value.Length)
                                                  {
                                                      Focus(CellAtOffset(1, 0));
                                                      return;
                                                  }

                                  if (_ev.KeyCode == (int)Keys.Left)
                                      if (EditElement.selectionStart == __selectionStart)
                                          if (EditElement.selectionEnd == __selectionEnd)
                                              if (__selectionEnd == __selectionStart)
                                                  if (__selectionStart == 0)
                                                  {
                                                      Focus(CellAtOffset(-1, 0));
                                                      return;
                                                  }

                                  __selectionEnd = EditElement.selectionEnd;
                                  __selectionStart = EditElement.selectionStart;
                              };
                            #endregion


                            #region onkeypress
                            EditElement.onkeypress +=
                                _ev =>
                                {

                                    if (_ev.IsReturn)
                                    {
                                        _ev.preventDefault();
                                        _ev.stopPropagation();

                                        if (CheckChanges != null)
                                            CheckChanges();

                                        ExitEditMode();
                                        SourceCell.InternalContentContainer.focus();

                                    }

                                };
                            #endregion



                        };
                    #endregion

                    #region ondblclick
                    SourceCell.InternalContentContainer.ondblclick +=
                        ev =>
                        {

                            ev.stopPropagation();
                            ev.preventDefault();

                            if (this.CellDoubleClick != null)
                                this.CellDoubleClick(
                                    this, new DataGridViewCellEventArgs(SourceColumn.Index, SourceRow.Index)
                                );

                            EnterEditMode();


                        };
                    #endregion

                    #region  android has long taps

                    var TouchstartWatch = new Stopwatch();

                    SourceCell.InternalContentContainer.ontouchstart +=
                        delegate
                        {
                            Console.WriteLine("SourceCell.InternalContentContainer.ontouchstart");
                            TouchstartWatch.Restart();
                        };

                    SourceCell.InternalContentContainer.ontouchend +=
                        delegate
                        {
                            Console.WriteLine("SourceCell.InternalContentContainer.ontouchend");

                            if (TouchstartWatch.ElapsedMilliseconds > 300)
                            {
                                if (this.CellDoubleClick != null)
                                    this.CellDoubleClick(
                                        this, new DataGridViewCellEventArgs(SourceColumn.Index, SourceRow.Index)
                                    );

                                EnterEditMode();
                            }

                            // script: error JSC1000: No implementation found for this native method, please implement [System.Diagnostics.Stopwatch.Reset()]
                            //TouchstartWatch.Reset();

                            TouchstartWatch = new Stopwatch();
                        };
                    #endregion


                    #region onmousedown
                    SourceCell.InternalContentContainer.onmousedown +=
                        ev =>
                        {
                            MouseCaptureCell = SourceCell;

                            ev.PreventDefault();

                            if (SourceCell.InternalSelected)
                                EnterEditMode();
                            else
                                SourceCell.InternalContentContainer.focus();
                        };
                    #endregion


                    #region onmousemove
                    SourceCell.InternalContentContainer.onmousemove +=
                         ev =>
                         {
                             if (MouseCaptureCell == null) return;

                             if (!this.MultiSelect)
                             {
                                 MouseCaptureCell = SourceCell;
                                 ev.PreventDefault();
                                 SourceCell.InternalContentContainer.focus();
                                 return;
                             }


                             if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                             {
                                 if (!this.InternalSelectedCells.Contains(SourceCell))
                                     this.InternalSelectedCells.Add(SourceCell);

                                 ev.PreventDefault();
                             }
                         };
                    #endregion

                    #region onmouseup


                    SourceCell.InternalContentContainer.onmouseup +=
                        ev =>
                        {
                            if (MouseCaptureCell == null)
                                return;

                            MouseCaptureCell = null;

                            if (!ev.ctrlKey)
                                if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                                {
                                    ev.preventDefault();

                                    if (this.CellClick != null)
                                        this.CellClick(this, new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceRow.Index));



                                    SourceCell.InternalContentContainer.focus();

                                }
                        };
                    #endregion



                    #region onkeydown
                    SourceCell.InternalContentContainer.onkeydown +=
                        ev =>
                        {

                            #region KeyNavigateTo
                            Func<Keys, int, int, bool> KeyNavigateTo =
                              (k, x, y) =>
                              {
                                  if (ev.KeyCode == (int)k)
                                  {
                                      // focus the cell on the right

                                      ev.PreventDefault();
                                      ev.StopPropagation();

                                      var Cell = CellAtOffset(x, y);
                                      if (Cell != null)
                                      {
                                          Cell.InternalContentContainer.focus();
                                          return true;
                                      }



                                  }
                                  return false;
                              };
                            #endregion

                            #region FullRowSelect Delete
                            if (this.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
                            {
                                if (ev.KeyCode == (int)Keys.Delete)
                                {
                                    if (SourceRow == InternalNewRow)
                                        return;

                                    // tested by
                                    // X:\jsc.svn\examples\javascript\forms\FormsDataGridViewDeleteRow\FormsDataGridViewDeleteRow\ApplicationControl.cs


                                    // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewRowCollection.Remove(System.Windows.Forms.DataGridViewRow)]

                                    var Cell = CellAtOffset(0, 1);


                                    if (this.InternalBeforeUserDeletedRow != null)
                                        this.InternalBeforeUserDeletedRow(
                                            this,
                                            new DataGridViewRowEventArgs(SourceRow)
                                        );

                                    this.Rows.Remove(SourceRow);

                                    if (this.UserDeletedRow != null)
                                        this.UserDeletedRow(
                                            this,
                                            new DataGridViewRowEventArgs(SourceRow)
                                        );


                                    if (Cell != null)
                                    {
                                        Cell.InternalContentContainer.focus();
                                    }



                                    return;
                                }
                            }
                            #endregion



                            if (KeyNavigateTo(Keys.Right, 1, 0)) return;
                            if (KeyNavigateTo(Keys.Left, -1, 0)) return;
                            if (KeyNavigateTo(Keys.Up, 0, -1)) return;
                            if (KeyNavigateTo(Keys.Down, 0, 1)) return;

                            if (ev.IsReturn)
                            {
                                ev.preventDefault();
                                ev.stopPropagation();

                                EnterEditMode();
                                return;
                            }

                            if (ev.KeyCode == (int)Keys.Space)
                            {
                                EnterEditMode();
                                return;
                            }

                            if (char.IsLetter((char)ev.KeyCode))
                            {
                                EnterEditMode();
                                return;
                            }

                            if (char.IsNumber((char)ev.KeyCode))
                            {
                                EnterEditMode();
                                return;
                            }



                        };
                    #endregion



                    #region onblur
                    SourceCell.InternalContentContainer.onblur +=
                        ev =>
                        {
                            SourceCell.InternalSelected = false;

                            if (!ev.ctrlKey)
                            {
                                // clear
                                while (this.InternalSelectedCells.Count > 0)
                                {
                                    var item = this.InternalSelectedCells.InternalItems[0];

                                    //item.InternalContentContainer.style.backgroundColor = item.InternalStyle.InternalBackColor.ToString();
                                    item.InternalContentContainer.style.backgroundColor = "";
                                    item.InternalContentContainer.style.color = item.InternalStyle.InternalForeColor.ToString();

                                    this.InternalSelectedCells.RemoveAt(0);
                                }

                            }

                            if (this.CellLeave != null)
                                this.CellLeave(this, new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceRow.Index));

                        };
                    #endregion





                    #region onfocus
                    SourceCell.InternalContentContainer.onfocus +=
                        ev =>
                        {
                            SourceCell.InternalSelected = true;

                            ev.preventDefault();
                            ev.stopPropagation();


                            if (this.CellEnter != null)
                                this.CellEnter(
                                    this,
                                    new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceRow.Index)
                                );

                            {
                                var NewSelectedCell = SourceCell;

                                if (!this.InternalSelectedCells.Contains(NewSelectedCell))
                                    this.InternalSelectedCells.Add(NewSelectedCell);
                            }

                            if (this.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
                            {
                                // tested by
                                // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.cs

                                foreach (var NewSelectedCell in SourceRow.InternalCells.InternalItems)
                                {

                                    if (!this.InternalSelectedCells.Contains(NewSelectedCell))
                                        this.InternalSelectedCells.Add(NewSelectedCell);
                                }

                            }

                        };
                    #endregion


                    InternalBindCellMouseEnter(SourceCell);


                    #region Font
                    SourceCell.InternalContentContainer.style.font = SourceCell.InternalStyle.Font.ToCssString();
                    SourceCell.InternalStyle.InternalFontChanged +=
                        delegate
                        {
                            if (SourceCell.InternalSelected)
                                return;


                            SourceCell.InternalContentContainer.style.font = SourceCell.InternalStyle.Font.ToCssString();

                            if (SourceCell.InternalStyle.InternalFont.Underline)
                                SourceCell.InternalContentContainer.style.textDecoration = "underline";
                            else
                                SourceCell.InternalContentContainer.style.textDecoration = "";
                        };
                    #endregion

                    #region InternalForeColorChanged
                    SourceCell.InternalStyle.InternalForeColorChanged +=
                       delegate
                       {
                           if (SourceCell.InternalSelected)
                               return;


                           SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                       };

                    SourceCell.InternalStyle.InternalBackColorChanged +=
                         delegate
                         {
                             if (SourceCell.InternalSelected)
                                 return;


                             //SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();
                         };

                    SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                    //SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();

                    //SourceCell.InternalTableColumn.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();


                    if (SourceCell.InternalStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                        SourceCell.InternalContentContainer.style.textAlign = IStyle.TextAlignEnum.right;

                    #endregion




                };
            #endregion

            #region CreateMissingCells
            Action<__DataGridViewRow> CreateMissingCells =
                SourceRow =>
                {
                    //Console.WriteLine("CreateMissingCells  " + new
                    //{
                    //    SourceRow.Index,
                    //    CellsCount = SourceRow.InternalCells.InternalItems.Count,
                    //    ColumnsCount = this.InternalColumns.InternalItems.Count


                    //});

                    #region defaults
                    while (SourceRow.InternalCells.InternalItems.Count < this.InternalColumns.InternalItems.Count)
                    {
                        var ColumnIndex = SourceRow.InternalCells.Count;

                        //Console.WriteLine("CreateMissingCells  " + new { SourceRow.Index, ColumnIndex });

                        var __c = this.InternalColumns.InternalItems[ColumnIndex];

                        __DataGridViewCell SourceCell = null;


                        if (__c is __DataGridViewButtonColumn)
                        {
                            //Console.WriteLine("CreateMissingCells __DataGridViewButtonColumn " + new { SourceRow.Index, ColumnIndex });

                            SourceCell = new __DataGridViewButtonCell();
                        }
                        else
                        {
                            //Console.WriteLine("CreateMissingCells ? " + new { SourceRow.Index, ColumnIndex });

                            SourceCell = new __DataGridViewTextBoxCell();
                        }

                        SourceRow.InternalCells.InternalItems.Add(SourceCell);


                    }
                    #endregion


                    for (int i = 0; i < SourceRow.InternalCells.InternalItems.Count; i++)
                    {
                        var SourceColumn = this.InternalColumns.InternalItems[i];
                        var SourceCell = SourceRow.InternalCells.InternalItems[i];

                        if (SourceCell.InternalTableColumn == null)
                        {
                            InitializeMissingCell(
                                SourceCell,
                                SourceRow
                            );
                        }

                        SourceColumn.InternalDefaultCellStyleChanged +=
                            delegate
                            {
                                if (SourceColumn.DefaultCellStyle != null)
                                {
                                    SourceCell.Style.ForeColor = SourceColumn.DefaultCellStyle.ForeColor;
                                    //SourceCell.Style.BackColor = SourceColumn.DefaultCellStyle.BackColor;
                                }
                            };

                        ((__DataGridViewCellStyle)SourceRow.DefaultCellStyle).InternalBackColorChanged +=
                            delegate
                            {
                                // when row style is changed, who overriddes who?
                                //SourceCell.Style.BackColor = SourceRow.DefaultCellStyle.BackColor;
                            };

                        if (SourceColumn.DefaultCellStyle != null)
                        {
                            SourceCell.Style.ForeColor = SourceColumn.DefaultCellStyle.ForeColor;
                            //SourceCell.Style.BackColor = SourceColumn.DefaultCellStyle.BackColor;
                        }


                    }


                };
            #endregion

            #region InternalColumns
            this.InternalColumns.InternalItemsX.Removed +=
                (SourceColumn, NewIndex) =>
                {
                    foreach (var SourceRow in this.InternalRows.InternalItems.Source)
                    {
                        SourceRow.Cells.RemoveAt(NewIndex);
                    }

                    SourceColumn.InternalTableColumn.Orphanize();

                    SourceColumn.ColumnHorizontalResizer.Orphanize();
                };

            this.InternalColumns.InternalItemsX.Added +=
                (SourceColumn, NewIndex) =>
                {
                    // jsc why is this function slow?

                    //var SourceColumn = this.InternalColumns.InternalItems[_e.NewIndex];

                    var SourceColumnStopwatch = Stopwatch.StartNew();
                    // 1250ms { Name = dataGridView1 } InternalColumns Added { Index = 29, SourceColumnStopwatch = 41 } 

                    SourceColumn.InternalContext = this;

                    //Console.WriteLine(
                    //    new { this.Name }
                    //    + " InternalColumns Added " + new { SourceColumn.Index });


                    //if (c is __DataGridViewButtonColumn)
                    //    Console.WriteLine("InternalColumns __DataGridViewButtonColumn ItemAdded " + new { _e.NewIndex });
                    //else
                    //    Console.WriteLine("InternalColumns ? ItemAdded " + new { _e.NewIndex });


                    SourceColumn.InternalTableColumn = __ColumnsTableRow.AddColumn();
                    SourceColumn.InternalTableColumn.style.position = IStyle.PositionEnum.relative;

                    if (this.InternalRows.Count > 0)
                        foreach (var SourceRow in this.InternalRows.InternalItems.Source)
                        {
                            CreateMissingCells(SourceRow);
                        }


                    #region c1contentcrel
                    var c1contentcrel = new IHTMLDiv { }.AttachTo(SourceColumn.InternalTableColumn);
                    c1contentcrel.style.position = IStyle.PositionEnum.relative;
                    c1contentcrel.style.left = "0";
                    c1contentcrel.style.top = "0";
                    c1contentcrel.style.right = "0";
                    c1contentcrel.style.height = "22px";

                    var c1contentc = new IHTMLDiv { }.AttachTo(c1contentcrel);
                    c1contentc.style.overflow = IStyle.OverflowEnum.hidden;
                    c1contentc.style.position = IStyle.PositionEnum.absolute;
                    c1contentc.style.left = "0";
                    c1contentc.style.top = "0";
                    c1contentc.style.right = "0";
                    c1contentc.style.height = "22px";


                    var c1content = new IHTMLSpan { innerText = SourceColumn.HeaderText }.AttachTo(c1contentc);
                    SourceColumn.InternalContent = c1content;
                    c1content.style.marginLeft = "4px";

                    // tested by
                    // X:\jsc.svn\examples\javascript\forms\TTFCurrencyExperment\TTFCurrencyExperment\ApplicationControl.cs

#if FHR
                    c1content.style.font = this.Font.ToCssString();

                    // ? we should use .css here
                    this.FontChanged +=
                        delegate
                        {
                            c1content.style.font = this.Font.ToCssString();
                        };
#endif

                    c1content.style.lineHeight = "22px";
                    #endregion


                    SourceColumn.InternalHeaderTextChanged +=
                        delegate
                        {
                            // would we be better off using :after:content attr()
                            c1content.innerText = SourceColumn.HeaderText;
                        };



                    // setting the size for the headers on top

                    var SourceColumnWidth_css = default(CSSStyleRuleMonkier);

                    if (SourceColumn.Index == -1)
                    {
                        // scoped style?

                        SourceColumnWidth_css =
                            this.__RowsTable_css;


                    }
                    else
                    {
                        SourceColumnWidth_css =
                                          this.__ColumnsTable_css_td[SourceColumn.Index] |
                                          this.__ColumnsTable_css_td[SourceColumn.Index][IHTMLElement.HTMLElementEnum.div] |
                                          this.__ContentTable_css_td[SourceColumn.Index] |
                                          this.__ContentTable_css_td[SourceColumn.Index][IHTMLElement.HTMLElementEnum.div];

                    }


                    // hide Tag?
                    //var css = (gg.__ColumnsTable.css | gg.__ContentTable.css)
                    // [IHTMLElement.HTMLElementEnum.tbody]
                    // [IHTMLElement.HTMLElementEnum.tr]
                    // [IHTMLElement.HTMLElementEnum.td]
                    // [i];






                    //154572ms { Name = dataGridView1 } InternalColumns InternalWidthChanged done { ElapsedMilliseconds = 1 } 



                    #region AtInternalWidthChanged
                    Action AtInternalWidthChanged =
                        delegate
                        {
                            var SourceColumnWidth = SourceColumn.Width;

                            // tested by
                            // X:\jsc.svn\examples\javascript\forms\Test\TestGrowingGrid\TestGrowingGrid\ApplicationControl.cs
                            // X:\jsc.svn\examples\javascript\forms\Test\TestFlowDataGridPadding\TestFlowDataGridPadding\Application.cs

                            // update the designer style
                            SourceColumnWidth_css.style.width = SourceColumnWidth + "px";
                            //// table wants to squeeshe the columns, prevent it


                            if (this.ColumnWidthChanged != null)
                                this.ColumnWidthChanged(this,
                                    new DataGridViewColumnEventArgs(SourceColumn)
                                   );
                        };



                    SourceColumn.InternalWidthChanged += AtInternalWidthChanged;

                    AtInternalWidthChanged();
                    #endregion

                    //Console.WriteLine("resizeable?");

#if FHR
                    #region InternalVisibleChanged
                    var SourceColumnVisible__ColumnsTable_css = __ColumnsTable_css
                     [IHTMLElement.HTMLElementEnum.tbody]
                     [IHTMLElement.HTMLElementEnum.tr]
                     [IHTMLElement.HTMLElementEnum.td]
                     [NewIndex];

                    var SourceColumnVisible__ContentTable_css = __ContentTable_css
                     [IHTMLElement.HTMLElementEnum.tbody]
                     [IHTMLElement.HTMLElementEnum.tr]
                     [IHTMLElement.HTMLElementEnum.td]
                     [NewIndex];

                    // xattribute instead?
                    var SourceColumnVisible_css = SourceColumnVisible__ColumnsTable_css | SourceColumnVisible__ContentTable_css;

                    SourceColumn.InternalVisibleChanged +=
                        delegate
                        {
                            if (SourceColumn.Visible)
                                SourceColumnVisible_css.style.display = IStyle.DisplayEnum.empty;
                            else
                                SourceColumnVisible_css.style.display = IStyle.DisplayEnum.none;
                        };
                    #endregion

                    #region ColumnHorizontalResizer CreateHorizontalResizer
                    // should we delay this until resize is enabled?

                    SourceColumn.ColumnHorizontalResizer = CreateHorizontalResizer();
                    //SourceColumn.ColumnHorizontalResizer.AttachTo(__ColumnsTableContainer);
                    //SourceColumn.ColumnHorizontalResizer.AttachTo(InternalScrollContainerElement);
                    SourceColumn.ColumnHorizontalResizer.AttachTo(InternalElement);
                    //__ColumnsTableContainer.insertNextSibling(SourceColumn.ColumnHorizontalResizer);

                    var ColumnHorizontalResizerDrag = new DragHelper(SourceColumn.ColumnHorizontalResizer)
                    {
                        Enabled = true
                    };

                    SourceColumn.InternalHorizontalDrag = ColumnHorizontalResizerDrag;
                    #endregion


                    #region ColumnUpdateToHorizontalResizerScroll left
                    Action ColumnUpdateToHorizontalResizerScroll = delegate
                    {
                        var x = ColumnHorizontalResizerDrag.Position.X;

                        SourceColumn.ColumnHorizontalResizer.style.left = x + "px";
                    };
                    #endregion

                    #region ColumnUpdateToHorizontalResizerScroll
                    // do we need this?
                    //this.InternalRows.InternalItems.Added +=
                    //    delegate
                    //    {
                    //        this.HTMLTargetRef.requestAnimationFrame +=
                    //            delegate
                    //            {
                    //                ColumnUpdateToHorizontalResizerScroll();
                    //            };

                    //    };

                    this.InternalAtAfterVisibleChanged +=
                        delegate
                        {
                            this.HTMLTargetRef.requestAnimationFrame +=
                                delegate
                                {
                                    ColumnUpdateToHorizontalResizerScroll();
                                };

                        };
                    #endregion

                    #region CompensateFor ZeroHorizontalResizerDrag DragStop
                    Action<DragHelper> CompensateFor =
                        Target =>
                        {
                            var __X = 0;
                            Target.DragStart +=
                                delegate
                                {
                                    __X = Target.Position.X;
                                };

                            Target.DragStop +=
                                delegate
                                {
                                    ColumnHorizontalResizerDrag.Position.X +=
                                        Target.Position.X - __X;

                                    ColumnUpdateToHorizontalResizerScroll();
                                };
                        };

                    CompensateFor(ZeroHorizontalResizerDrag);
                    #endregion

                    #region Reposition
                    Action Reposition =
                        delegate
                        {
                            var x = ZeroHorizontalResizerDrag.Position.X - 1;

                            if (!this.RowHeadersVisible)
                            {
                                x = -4;
                            }

                            x -= this.InternalScrollContainerElement.scrollLeft;

                            for (int i = 0; i <= NewIndex; i++)
                            {
                                //x += this.InternalColumns.InternalItems[i].Width;

                                var CandidateColumn = this.InternalColumns.InternalItems[i];


                                // X:\jsc.svn\examples\javascript\forms\Test\TestDataGridPadding\TestDataGridPadding\ApplicationControl.cs
                                if (CandidateColumn.Visible)
                                    x += CandidateColumn.Width + 1;
                            }

                            ColumnHorizontalResizerDrag.Position = new Point(x, 0);

                            ColumnUpdateToHorizontalResizerScroll();
                        };

                    Reposition();

                    this.InternalScrollContainerElement.onscroll +=
                      e =>
                      {
                          Reposition();
                      };

                    // tested by
                    // X:\jsc.svn\examples\javascript\Test\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\ApplicationControl.cs
                    InternalRowHeadersVisibleChanged +=
                       delegate
                       {
                           Reposition();
                       };

                    for (int i = 0; i <= NewIndex; i++)
                    {
                        var item = this.InternalColumns.InternalItems[i];

                        item.InternalWidthChanged +=
                            delegate
                            {
                                Reposition();
                            };
                    }
                    #endregion

                    #region ColumnHorizontalResizerDrag
                    var __DragStartX = 0;

                    ColumnHorizontalResizerDrag.DragStart +=
                        delegate
                        {

                            Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.move;
                            //((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = JSColor.Blue;

                            __DragStartX = ColumnHorizontalResizerDrag.Position.X;
                        };

                    ColumnHorizontalResizerDrag.DragStop +=
                            delegate
                            {
                                //                                 { Width = 115, cwidth = 1045 } view-source:27892

                                // view-source:27892
                                //{ Width = 1045, cwidth = 1975 } 

                                Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.auto;
                                //((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = this.InternalBackgroundColor.ToString();
                                //((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = "";
                                //((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = "yellow";
                                //SourceColumn.ColumnHorizontalResizer.style.backgroundColor = "red";

                                var ColumnHorizontalResizerDragNewValue = SourceColumn.Width + ColumnHorizontalResizerDrag.Position.X - __DragStartX;
                                Console.WriteLine(new { SourceColumn.Width, ColumnHorizontalResizerDragNewValue });

                                SourceColumn.Width = ColumnHorizontalResizerDragNewValue;
                                InternalAutoSizeWhenFill();
                            };


                    ColumnHorizontalResizerDrag.DragMove +=
                        delegate
                        {
                            ColumnUpdateToHorizontalResizerScroll();

                        };
                    #endregion


                    #region AutoResizeColumn
                    SourceColumn.ColumnHorizontalResizer.onmousedown +=
                        e =>
                        {
                            if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                            {
                                e.preventDefault();
                                e.stopPropagation();

                                this.AutoResizeColumn(SourceColumn.Index, ObeyAutoSizeMode: false);
                                InternalAutoSizeWhenFill();

                            }
                        };

                    SourceColumn.ColumnHorizontalResizer.ondblclick +=
                        delegate
                        {
                            this.AutoResizeColumn(SourceColumn.Index, ObeyAutoSizeMode: false);
                            InternalAutoSizeWhenFill();
                        };
                    #endregion
#endif

                    bool InternalAutoResizeColumnBuzy = false;

                    #region InternalAutoResizeColumn
                    this.InternalAutoResizeColumn +=
                        (SourceColumnIndex, ObeyAutoSizeMode) =>
                        {
                            if (InternalAutoResizeColumnBuzy)
                                return;


                            if (SourceColumnIndex != SourceColumn.Index)
                                return;

                            var rows = this.InternalRows.InternalItems.Source;

                            var InternalAutoResizeColumnStopwatch = Stopwatch.StartNew();

                            // InternalAutoSize { Count = 33, cindex = -1 }
                            //Console.WriteLine(
                            //    new { this.Name }
                            //    + " InternalAutoResizeColumn "
                            //    + new
                            //    {
                            //        RowCount = rows.Count,
                            //        SourceColumnIndex,

                            //        // are we even supposed to do autoresize?
                            //        SourceColumn.AutoSizeMode
                            //    }
                            //);



                            #region Fill last column
                            if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
                            {
                                var FillColumn = InternalGetVisibleColumns().LastOrDefault();

                                if (FillColumn != null)
                                    if (SourceColumnIndex == FillColumn.Index)
                                    {
                                        //Console.WriteLine("InternalAutoResizeColumn " + new { FillColumn.Index, FillColumn.Name });

                                        //SourceColumn.le
                                        var SourceColumnLeft = InternalGetVisibleColumns()
                                            .Where(c => c.Index < FillColumn.Index)
                                            .Select(c => c.Width)
                                            .Sum();

                                        var ZeroRight = (ZeroHorizontalResizerDrag.Position.X + 4);

                                        if (!this.RowHeadersVisible)
                                            ZeroRight = 1;


                                        // { cindex = 0, w = 0, all = 1600, WidthByFill = 1600 } 

                                        var all = this.InternalScrollContainerElement.clientWidth;

                                        var WidthByFill = all - SourceColumnLeft - ZeroRight - 5;

                                        //Console.WriteLine(
                                        //    " InternalAutoResizeColumn Fill "
                                        //    + new
                                        //    {
                                        //        SourceColumnIndex,
                                        //        SourceColumnLeft,
                                        //        value = ZeroRight,
                                        //        all,
                                        //        WidthByFill
                                        //    }

                                        //    );

                                        //{ cindex = 0, w = 0, value = 99, all = 753, WidthByFill = 654 } 

#if FHR
                                        __DragStartX = ColumnHorizontalResizerDrag.Position.X + (WidthByFill - SourceColumn.Width);
#endif

                                        InternalAutoResizeColumnBuzy = true;
                                        SourceColumn.Width = Math.Max(20, WidthByFill);
                                        InternalAutoResizeColumnBuzy = false;

                                        return;
                                    }
                            }
                            #endregion



                            if (SourceColumnIndex < 0)
                                return;

                            if (SourceColumn.AutoSizeMode == DataGridViewAutoSizeColumnMode.None)
                            {
                                if (ObeyAutoSizeMode)
                                    return;
                            }



                            #region SetColumnWidth
                            var WidthByRowsInThisColumn = rows.Max(
                                rr =>
                                {
                                    __DataGridViewCell cc = rr.Cells[SourceColumnIndex];


                                    //Console.WriteLine("InternalAutoSize " + new { rows.Count, cindex, cc.InternalContent.offsetWidth });

                                    return cc.InternalContent.offsetWidth;
                                }
                            );

                            WidthByRowsInThisColumn = Math.Max(WidthByRowsInThisColumn, this.InternalColumns.InternalItems[SourceColumnIndex].InternalContent.offsetWidth);
                            if (WidthByRowsInThisColumn == 0)
                            {
                                // no DOM?
                                //Console.WriteLine("InternalAutoSize skipped");
                                return;
                            }

                            // extra padding?
                            WidthByRowsInThisColumn += 8 + 24;

                            //Console.WriteLine("InternalAutoSize" + new { SourceColumn.Width, cwidth = WidthByRowsInThisColumn });

#if FHR
                            __DragStartX = ColumnHorizontalResizerDrag.Position.X + (WidthByRowsInThisColumn - SourceColumn.Width);
#endif

                            var NewWidth = Math.Max(20, WidthByRowsInThisColumn);



                            // 15248ms { Name = dataGridView2 }InternalAutoResizeColumn { RowCount = 2, SourceColumnIndex = 0, NewWidth = 154, ElapsedMilliseconds = 2 } 
                            // 14818ms { Name = dataGridView2 } InternalAutoResizeColumn { RowCount = 2, SourceColumnIndex = 2, NewWidth = 120, ElapsedMilliseconds = 1 } 

                            //Console.WriteLine(
                            //    new { this.Name }
                            //    + " InternalAutoResizeColumn " + new
                            //    {
                            //        RowCount = rows.Count,
                            //        SourceColumnIndex,
                            //        NewWidth,
                            //        InternalAutoResizeColumnStopwatch.ElapsedMilliseconds
                            //    });

                            SourceColumn.Width = NewWidth;
                            #endregion

                        };
                    #endregion





#if FHR
                    if (ColumnAdded != null)
                        ColumnAdded(this, new DataGridViewColumnEventArgs((DataGridViewColumn)(object)SourceColumn));
#endif

                    // 1135ms { Name = dataGridView1 } InternalColumns Added { Index = 29, SourceColumnStopwatch = 7 } 

                    Console.WriteLine(
                        new { this.Name }
                        + " InternalColumns Added " + new
                        {
                            SourceColumn.Index,

                            SourceColumnStopwatch = SourceColumnStopwatch.ElapsedMilliseconds
                        });

                };

            #endregion

            #region InitializeZeroColumnCell |
            Action<__DataGridViewRow> InitializeZeroColumnCell =
                SourceRow =>
                {
                    var __tr = __RowsTableBody.AddRow();

                    SourceRow.InternalZeroColumnTableRow = __tr;



                    var InternalTableColumn = __tr.AddColumn();

                    // who controls ?
                    //InternalTableColumn.style.borderBottom = "2px solid red";

                    InternalTableColumn.innerText = " ";
                    //c0.style.padding = "4px";
                    //InternalTableColumn.style.backgroundColor = JSColor.System.ButtonFace;

                    {
                        // X:\jsc.svn\examples\javascript\forms\FormsGridCellStyle\FormsGridCellStyle\Application.cs

                        //var BackColor = this.ColumnHeadersDefaultCellStyle.BackColor;
                        //InternalTableColumn.style.backgroundColor = BackColor.ToString();
                    }



                    InternalTableColumn.style.width = "100%";

                    var c1contentcrel = new IHTMLDiv { }.AttachTo(InternalTableColumn);
                    c1contentcrel.style.position = IStyle.PositionEnum.relative;
                    c1contentcrel.style.left = "0";
                    c1contentcrel.style.top = "0";
                    c1contentcrel.style.right = "0";
                    c1contentcrel.style.height = "21px";
                    c1contentcrel.style.overflow = IStyle.OverflowEnum.hidden;

                    //var c1 = new IHTMLDiv().AttachTo(c1contentcrel);
                    //c1.style.position = IStyle.PositionEnum.absolute;

                    //c1.style.backgroundColor = JSColor.White;


                    //c1.style.left = "0";
                    //c1.style.top = "0";
                    //c1.style.width = "6px";

                    var c1img = new IHTMLDiv().AttachTo(c1contentcrel);
                    c1img.style.position = IStyle.PositionEnum.absolute;
                    c1img.style.left = "4px";
                    c1img.style.top = "0px";
                    c1img.style.right = "0";
                    c1img.style.height = "21px";

                    //new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/DataGridEditRow.png").ToBackground(c1img, false);
                    c1img.style.backgroundPosition = "left center";

                    var c2img = new IHTMLDiv().AttachTo(c1contentcrel);
                    c2img.style.position = IStyle.PositionEnum.absolute;
                    c2img.style.left = "12px";
                    c2img.style.top = "0px";
                    c2img.style.right = "0";
                    c2img.style.height = "21px";

                    //new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/DataGridEditRow.png").ToBackground(c1img, false);
                    c1img.style.backgroundPosition = "left center";
                    c2img.style.backgroundPosition = "left center";

                    #region New/Edit
                    Action AtEndEdit =
                        delegate
                        {
                            if (SourceRow.IsNewRow)
                            {
                                new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/DataGridNewRow.png").ToBackground(c2img, false);

                            }
                            else
                            {
                                c2img.style.backgroundImage = "";
                            }
                        };

                    this.CellBeginEdit +=
                        (s, e) =>
                        {
                            if (e.RowIndex == SourceRow.Index)
                            {
                                new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/DataGridEditRow.png").ToBackground(c2img, false);
                                //c1img.style.backgroundPosition = "left center";
                            }
                        };

                    this.CellEndEdit +=
                       (s, e) =>
                       {
                           if (e.RowIndex == SourceRow.Index)
                           {
                               AtEndEdit();

                           }
                       };

                    AtEndEdit();
                    #endregion

                    #region DataGridFocusRow
                    this.CellEnter +=
                        (s, e) =>
                        {
                            if (e.RowIndex == SourceRow.Index)
                            {
                                new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/DataGridFocusRow.png").ToBackground(c1img, false);
                                //c1img.style.backgroundPosition = "left center";
                            }
                        };

                    this.CellLeave +=
                        (s, e) =>
                        {
                            if (e.RowIndex == SourceRow.Index)
                            {
                                c1img.style.backgroundImage = "";
                            }
                        };
                    #endregion


                    #region AtInternalHeightChanged
                    Action AtInternalHeightChanged = delegate
                    {
                        //c1.style.height = (SourceRow.InternalHeight - 1) + "px";
                        c1img.style.height = (SourceRow.InternalHeight - 1) + "px";
                        c2img.style.height = (SourceRow.InternalHeight - 1) + "px";

                        c1contentcrel.style.height = (SourceRow.InternalHeight - 1) + "px";
                        __tr.style.height = SourceRow.InternalHeight + "px";
                    };

                    AtInternalHeightChanged();
                    SourceRow.InternalHeightChanged += AtInternalHeightChanged;
                    #endregion


                };
            #endregion

            InitializeZeroColumnCell(InternalNewRow);


            #region InternalAutoResizeColumn
            var t = new global::System.Windows.Forms.Timer();
            t.Interval = 100;
            t.Tick +=
                delegate
                {
                    t.Stop();

                    InternalAutoResizeAll();
                };
            #endregion

            this.InternalRowHeadersVisibleChanged +=
                delegate
                {
                    InternalAutoSizeWhenFill();
                };

            #region ClientSizeChanged
            // whatif we are in autosize mode?
            this.ClientSizeChanged +=
                delegate
                {

                    InternalAutoSizeWhenFill();
                };
            #endregion



            #region InternalRows.Added

            this.InternalRows.InternalItems.Added +=
                  (SourceRow, CurrentRowIndex) =>
                  {
                      if (SourceRow.InternalTableRow != null)
                          return;

                      if (InternalNewRow != null)
                      {
                          // how much time are we spending per row?
                          // what about bulk entry?
                          // could we adapt a preexisting table?

                          // how much time do we spend on moving the new row thingy?
                          InternalNewRow.InternalTableRow.Orphanize();
                          InternalNewRow.InternalZeroColumnTableRow.Orphanize();

                          this.InternalRows.InternalItems.Source.Remove(InternalNewRow);
                          this.InternalRows.InternalItems.Source.Add(InternalNewRow);
                      }

                      SourceRow.InternalTableRow = __ContentTableBody.AddRow();

                      #region AtInternalHeightChanged
                      Action AtInternalHeightChanged = delegate
                      {
                          SourceRow.InternalTableRow.style.height = SourceRow.InternalHeight + "px";
                      };

                      AtInternalHeightChanged();
                      SourceRow.InternalHeightChanged += AtInternalHeightChanged;
                      #endregion


                      CreateMissingCells(SourceRow);

                      if (!InternalSkipAutoSize)
                          if (!InternalDataSourceBusy)
                              if (this.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None)
                              {
                                  Console.WriteLine("a new row was added, auto resize?");
                                  t.Stop();
                                  t.Start();
                              }





                      InitializeZeroColumnCell(SourceRow);
                      if (InternalNewRow != null)
                      {
                          InternalNewRow.InternalTableRow.AttachTo(__ContentTableBody);
                          InternalNewRow.InternalZeroColumnTableRow.AttachTo(__RowsTableBody);
                      }
                  };
            #endregion

            #region InternalRows Removed
            this.InternalRows.InternalItems.Removed +=
                (SourceRow, i) =>
                {
                    SourceRow.InternalTableRow.Orphanize();
                    SourceRow.InternalZeroColumnTableRow.Orphanize();

                    // raise any events
                };
            #endregion


            __DataGridViewRow PendingNewRow = null;

            #region UserAddedRow
            this.InternalRaiseCellBeginEdit =
                (SourceCell) =>
                {
                    Console.WriteLine("InternalRaiseCellBeginEdit " + new { SourceCell.ColumnIndex, SourceCell.OwningRow.Index });

                    var SourceRow = SourceCell.InternalOwningRow;

                    if (SourceRow.IsNewRow)
                    {
                        var n = new __DataGridViewRow();
                        InternalNewRow = null;
                        PendingNewRow = SourceRow;

                        InternalSkipAutoSize = true;
                        this.InternalRows.InternalItems.Source.Add(n);
                        InternalSkipAutoSize = false;

                        InternalNewRow = n;

                        if (this.UserAddedRow != null)
                            this.UserAddedRow(this, new DataGridViewRowEventArgs((DataGridViewRow)(object)SourceRow));
                    }


                    if (this.CellBeginEdit != null)
                        this.CellBeginEdit(this,
                            new DataGridViewCellCancelEventArgs(SourceCell.ColumnIndex, SourceRow.Index)
                        );
                };
            #endregion

            #region CellEndEdit

            this.InternalRaiseCellValueChanged =
                (SourceCell) =>
                {
                    var SourceRow = SourceCell.InternalOwningRow;

                    if (SourceRow.IsNewRow)
                    {
                        // if there is now content on the new row then we need another new row downt we?
                        return;
                    }

                    //Console.WriteLine("InternalRaiseCellValueChanged " + new { SourceCell.ColumnIndex, SourceCell.OwningRow.Index });


                    if (PendingNewRow == SourceRow)
                        PendingNewRow = null;

                    if (this.CellValueChanged != null)
                        this.CellValueChanged(this,
                            new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceCell.OwningRow.Index)
                        );
                };

            this.InternalRaiseCellEndEdit =
                (SourceCell) =>
                {
                    //Console.WriteLine("InternalRaiseCellEndEdit " + new { SourceCell.ColumnIndex, SourceCell.OwningRow.Index });

                    var SourceRow = SourceCell.InternalOwningRow;

                    #region  PendingNewRow
                    if (PendingNewRow == SourceRow)
                    {
                        var ColumnIndex = SourceCell.ColumnIndex;
                        var RowIndex = SourceRow.Index;

                        this.InternalRows.InternalItems.Source.Remove(SourceRow);

                        SourceRow.InternalTableRow.Orphanize();
                        SourceRow.InternalZeroColumnTableRow.Orphanize();

                        //if (this.UserDeletedRow != null)
                        //    this.UserDeletedRow(this, new DataGridViewRowEventArgs((DataGridViewRow)(object)SourceRow));

                        PendingNewRow = null;

                        this.InternalRows.InternalItems.Source[RowIndex].InternalCells.InternalItems[ColumnIndex].InternalContentContainer.focus();
                        //this[SourceCell.ColumnIndex, SourceRow.Index].Selected = true;
                    }
                    #endregion


                    if (this.CellEndEdit != null)
                        this.CellEndEdit(this,
                            new DataGridViewCellEventArgs(SourceCell.ColumnIndex, SourceRow.Index)
                        );
                };
            #endregion



            this.GridColor = global::System.Drawing.Color.FromArgb(0xa0, 0xa0, 0xa0);
            this.Height = 400;


            // 2901ms exit DataGridView .ctor { ElapsedMilliseconds = 923 } 
            Console.WriteLine("event: exit DataGridView .ctor " + new { DataGridViewConstructorStopwatch.ElapsedMilliseconds });
        }

        public IEnumerable<DataGridViewColumn> InternalGetVisibleColumns()
        {
            return Enumerable.Range(0, this.Columns.Count).Select(x => this.Columns[x]).Where(x => x.Visible);
        }

        private void InternalAutoSizeWhenFill()
        {
            if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
            {
                Native.window.requestAnimationFrame +=
                    delegate
                    {
                        // tested by?

                        var x = InternalGetVisibleColumns().LastOrDefault();
                        if (x != null)
                        {
                            //Console.WriteLine("InternalAutoSizeWhenFill " + new { x.Index, x.Name });

                            this.AutoResizeColumn(x.Index);
                        }
                    };
            }
        }


        private void InternalAutoResizeAll()
        {

            // 15245ms will do autoresize? 

            var AutoResizeColumnStopwatch = Stopwatch.StartNew();

            //Console.WriteLine(new { this.Name } + " autoresize ");


            foreach (var item in this.InternalColumns.InternalItems)
            {
                this.AutoResizeColumn(item.Index, ObeyAutoSizeMode: true);
            }


            // how is it possible that
            // the code itself takes 2ms, calling handlers 400ms and all in total 3sec?
            // does the delegate calling code need to be optimized?

            //13674ms { Name = dataGridView1 } InternalAutoResizeColumn { RowCount = 3, SourceColumnIndex = 7, NewWidth = 81, ElapsedMilliseconds = 1 } view-source:35271
            //14048ms { Name = dataGridView1 } exit AutoResizeColumn { ElapsedMilliseconds = 375, columnIndex = 7 } 

            // 14049ms { Name = dataGridView1 } autoresize done { ElapsedMilliseconds = 3020 } 
            // 16697ms { Name = dataGridView2 } autoresize done { ElapsedMilliseconds = 2630 } 

            // X:\jsc.svn\examples\javascript\forms\Test\TestFlowDataGridPadding\TestFlowDataGridPadding\Application.cs

            var gg = (DataGridView)this;

            //Console.WriteLine(
            //    new { Form = gg.FindForm().Name, this.Name }
            //    + " autoresize done " + new { AutoResizeColumnStopwatch.ElapsedMilliseconds, this.InternalColumns.Count });

            // 16011ms { Name = dataGridView1 } autoresize done { ElapsedMilliseconds = 39, Count = 5 } 
        }




    }
}
