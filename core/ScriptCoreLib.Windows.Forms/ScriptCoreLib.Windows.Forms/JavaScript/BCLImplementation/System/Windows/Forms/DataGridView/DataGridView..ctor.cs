﻿using ScriptCoreLib.JavaScript.Controls;
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

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {

        public __DataGridView()
        {
            //Console.WriteLine("__DataGridView");





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

            this.InternalElement = new IHTMLDiv();



            this.InternalElement.style.overflow = DOM.IStyle.OverflowEnum.hidden;

            this.InternalSetDefaultFont();

            this.InternalScrollContainerElement = new IHTMLDiv
            {
                // pstyle
                //className = "InternalScrollContainerElement"
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


            var __ContentTableContainer = new IHTMLDiv().AttachTo(InternalScrollContainerElement);

            this.__ContentTable = new IHTMLTable
            {
                className = __ContentTable_className,
                cellPadding = 0,
                cellSpacing = 0
            }.AttachTo(__ContentTableContainer);

            // X:\jsc.svn\examples\javascript\css\CSSOdd\CSSOdd\Application.cs

            this.__ContentTable_css_td = this.__ContentTable.css
                [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            this.__ContentTable_css_alt_td = this.__ContentTable.css
                [IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr].even[IHTMLElement.HTMLElementEnum.td];


            __ContentTable.style.paddingTop = "22px";



            var __ColumnsTableContainer = new IHTMLDiv().AttachTo(InternalScrollContainerElement);

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131213-forms-css
            this.__ColumnsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__ColumnsTableContainer);
            this.__ColumnsTable_css_td = this.__ColumnsTable.css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            IHTMLTableRow __ColumnsTableRow = null;

            __ColumnsTableContainer.style.SetLocation(0, 0);
            __ColumnsTableRow = __ColumnsTable.AddBody().AddRow();
            __ColumnsTableRow.style.height = "22px";


            var __RowsTableContainer = new IHTMLDiv().AttachTo(InternalScrollContainerElement);
            __RowsTableContainer.style.SetLocation(0, 0);


            this.__RowsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__RowsTableContainer);
            this.__RowsTable_css_td = this.__RowsTable.css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr][IHTMLElement.HTMLElementEnum.td];

            __RowsTable.style.paddingTop = "22px";
            IHTMLTableBody __RowsTableBody = __RowsTable.AddBody();


            #region Corner
            this.__Corner = new IHTMLDiv().AttachTo(InternalScrollContainerElement);



            __Corner.style.SetLocation(0, 0);
            __Corner.style.height = "22px";

            #endregion



            IHTMLTableBody __ContentTableBody = __ContentTable.AddBody();

            var InternalNewRow = new __DataGridViewRow();


            InternalNewRow.InternalTableRow = __ContentTableBody.AddRow();
            InternalNewRow.InternalTableRow.style.height = "22px";


            this.InternalRows.InternalItems.Source.Add(InternalNewRow);

            // http://www.w3schools.com/cssref/sel_last-of-type.asp
            var InternalNewRow_content_css = __ContentTableBody.css[IHTMLElement.HTMLElementEnum.tr][":last-of-type"];
            var InternalNewRow_header_css = __RowsTableBody.css[IHTMLElement.HTMLElementEnum.tr][":last-of-type"];
            var InternalNewRow_css = InternalNewRow_content_css | InternalNewRow_header_css;

            //var InternalNewRow_css = IStyleSheet.all[
            //    InternalNewRow_content_css.rule.selectorText
            //    + ", "
            //    + InternalNewRow_header_css.rule.selectorText
            //];


            this.AllowUserToAddRowsChanged +=
                delegate
                {
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

            this.InternalGridColor_css = this.InternalScrollContainerElement.css[" *[data-resizer='resizer']"];



            #region CreateVerticalResizer --
            Func<IHTMLDiv> CreateVerticalResizer =
                () =>
                {
                    var r = new IHTMLDiv();

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


            #region CreateHorizontalResizer |
            Func<IHTMLDiv> CreateHorizontalResizer =
                () =>
                {
                    var _HorizontalResizer = new IHTMLDiv();

                    _HorizontalResizer.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizer.style.width = "9px";
                    _HorizontalResizer.style.top = "0px";
                    _HorizontalResizer.style.height = "44px";
                    //HorizontalResizer.style.backgroundColor = JSColor.Red;
                    _HorizontalResizer.style.cursor = DOM.IStyle.CursorEnum.move;

                    var _HorizontalResizerLine = new IHTMLDiv().AttachTo(_HorizontalResizer);

                    _HorizontalResizerLine.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizerLine.style.left = "4px";
                    _HorizontalResizerLine.style.width = "1px";
                    _HorizontalResizerLine.style.top = "0px";
                    _HorizontalResizerLine.style.bottom = "0px";


                    _HorizontalResizerLine.setAttribute("data-resizer", "resizer");




                    this.ClientSizeChanged +=
                       delegate
                       {
                           _HorizontalResizer.style.height = "44px";
                           //_HorizontalResizer.Hide();

                           Native.window.requestAnimationFrame +=
                               //new ScriptCoreLib.JavaScript.Runtime.Timer(
                               delegate
                               {
                                   _HorizontalResizer.style.height = this.InternalScrollContainerElement.clientHeight + "px";
                                   //_HorizontalResizerLine.style.backgroundColor = "red";
                                   //_HorizontalResizer.Show();

                               }
                           ;
                           //).StartTimeout(200);
                       };



                    return _HorizontalResizer;
                };
            #endregion

            var ZeroVerticalResizer = CreateVerticalResizer().AttachTo(InternalScrollContainerElement);

            ZeroVerticalResizer.style.SetLocation(0, 22 - 5);




            #region ZeroHorizontalResizer

            var ZeroHorizontalResizer = CreateHorizontalResizer().AttachTo(InternalScrollContainerElement);

            var ZeroHorizontalResizerDrag = new DragHelper(ZeroHorizontalResizer)
            {
                //
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
                        this.InternalScrollContainerElement.scrollLeft + ZeroHorizontalResizerDrag.Position.X,
                        this.InternalScrollContainerElement.scrollTop
                    );
            };
            #endregion

            #region UpdateToHorizontalResizerDrag
            Action UpdateToHorizontalResizerDrag = delegate
            {
                //var value = (ZeroHorizontalResizerDrag.Position.X + 4);
                var value = (ZeroHorizontalResizerDrag.Position.X + 4);

                if (!this.InternalRowHeadersVisible)
                    value = 4;

                // has 2 borders
                __Corner.style.width = (value - 2) + "px";

                __ColumnsTable.style.paddingLeft = value + "px";
                __ContentTable.style.paddingLeft = value + "px";

                __RowsTable.style.width = value + "px";
                __RowsTable.style.minWidth = value + "px";

            };

            UpdateToHorizontalResizerScroll();
            UpdateToHorizontalResizerDrag();
            #endregion


            ZeroHorizontalResizer.Show(this.InternalRowHeadersVisible);
            InternalRowHeadersVisibleChanged +=
              delegate
              {
                  // tested by
                  // X:\jsc.svn\examples\javascript\Test\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\TestNoZeroColumnHeaderNoScrollbarDateDataGrid\ApplicationControl.cs


                  ZeroHorizontalResizer.Show(this.InternalRowHeadersVisible);


                  UpdateToHorizontalResizerDrag();
                  UpdateToHorizontalResizerScroll();
              };


            #region ZeroHorizontalResizerDrag Drag
            ZeroHorizontalResizerDrag.DragStart +=
                delegate
                {
                    Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.move;
                    ((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = JSColor.Blue;
                };



            ZeroHorizontalResizerDrag.DragStop +=
                 delegate
                 {
                     Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.auto;
                     //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = this.InternalBackgroundColor.ToString();
                     ((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = "";
                     //((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = "yellow";

                     UpdateToHorizontalResizerDrag();
                 };



            ZeroHorizontalResizerDrag.DragMove +=
                delegate
                {
                    UpdateToHorizontalResizerScroll();

                };
            #endregion

            #endregion

            #region onscroll
            this.InternalScrollContainerElement.onscroll +=
               e =>
               {
                   UpdateToVerticalResizerScroll();
                   UpdateToHorizontalResizerScroll();

                   __RowsTableContainer.style.SetLocation(this.InternalScrollContainerElement.scrollLeft, 0);
                   __ColumnsTableContainer.style.SetLocation(0, this.InternalScrollContainerElement.scrollTop);

                   __Corner.style.SetLocation(
                     this.InternalScrollContainerElement.scrollLeft,
                     this.InternalScrollContainerElement.scrollTop
                 );
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




                    #region AtInternalWidthChanged
                    Action AtInternalWidthChanged = delegate
                    {
                        SourceCell.InternalContentContainer.style.width = SourceColumn.Width + "px";


                        SourceCell.InternalTableColumn.style.width = SourceColumn.Width + "px";
                        SourceCell.InternalTableColumn.style.minWidth = SourceColumn.Width + "px";
                        //c1.style.backgroundColor = JSColor.Red;
                        //c1content.innerText = "@" + InternalColumn.HeaderText + ":" + InternalColumn.Width;
                    };

                    AtInternalWidthChanged();

                    SourceColumn.InternalWidthChanged += AtInternalWidthChanged;

                    SourceColumn.InternalWidthChanged +=
                        delegate
                        {
                            // tested by
                            // X:\jsc.svn\examples\javascript\forms\Test\TestGrowingGrid\TestGrowingGrid\ApplicationControl.cs

                            if (this.ColumnWidthChanged != null)
                                this.ColumnWidthChanged(this,
                                    new DataGridViewColumnEventArgs(SourceColumn)
                                   );


                        };

                    #endregion

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
                    //var SourceColumn = this.InternalColumns.InternalItems[_e.NewIndex];

                    SourceColumn.InternalContext = this;


                    //if (c is __DataGridViewButtonColumn)
                    //    Console.WriteLine("InternalColumns __DataGridViewButtonColumn ItemAdded " + new { _e.NewIndex });
                    //else
                    //    Console.WriteLine("InternalColumns ? ItemAdded " + new { _e.NewIndex });


                    SourceColumn.InternalTableColumn = __ColumnsTableRow.AddColumn();



                    SourceColumn.InternalTableColumn.style.position = IStyle.PositionEnum.relative;


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


                    // gradient effect?

                    //var c1contentclight = new IHTMLDiv { }.AttachTo(c1contentcrel);
                    //c1contentclight.style.overflow = IStyle.OverflowEnum.hidden;
                    //c1contentclight.style.position = IStyle.PositionEnum.absolute;
                    //c1contentclight.style.left = "0";
                    //c1contentclight.style.top = "0";
                    //c1contentclight.style.right = "0";
                    //c1contentclight.style.height = "10px";

                    //c1contentclight.style.backgroundColor = JSColor.White;





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

                    c1content.style.font = this.Font.ToCssString();

                    // ?
                    this.FontChanged +=
                        delegate
                        {
                            c1content.style.font = this.Font.ToCssString();
                        };

                    c1content.style.lineHeight = "22px";
                    #endregion


                    SourceColumn.InternalHeaderTextChanged +=
                        delegate
                        {
                            c1content.innerText = SourceColumn.HeaderText;
                        };

                    #region AtInternalWidthChanged
                    Action AtInternalWidthChanged =
                        delegate
                        {
                            var w = SourceColumn.Width;

                            SourceColumn.InternalTableColumn.style.width = w + "px";
                            SourceColumn.InternalTableColumn.style.minWidth = w + "px";
                        };



                    SourceColumn.InternalWidthChanged += AtInternalWidthChanged;

                    AtInternalWidthChanged();
                    #endregion

                    #region InternalHorizontalDrag
                    SourceColumn.ColumnHorizontalResizer = CreateHorizontalResizer();

                    __ColumnsTableContainer.insertNextSibling(SourceColumn.ColumnHorizontalResizer);

                    var ColumnHorizontalResizerDrag = new DragHelper(SourceColumn.ColumnHorizontalResizer)
                    {
                        Enabled = true
                    };

                    SourceColumn.InternalHorizontalDrag = ColumnHorizontalResizerDrag;
                    #endregion


                    #region ColumnUpdateToHorizontalResizerScroll
                    Action ColumnUpdateToHorizontalResizerScroll = delegate
                    {
                        var x = ColumnHorizontalResizerDrag.Position.X;

                        var scrollHeight = this.InternalScrollContainerElement.scrollHeight;
                        if (scrollHeight < 44)
                            scrollHeight = 44;



                        //Console.WriteLine("ColumnUpdateToHorizontalResizerScroll " + new { x, scrollHeight });
                        SourceColumn.ColumnHorizontalResizer.style.SetLocation(
                                    x,
                                    0
                            //this.InternalContainerElement.scrollTop
                            );


                        SourceColumn.ColumnHorizontalResizer.style.height = scrollHeight + "px";
                    };
                    #endregion

                    #region ColumnUpdateToHorizontalResizerScroll
                    this.InternalRows.InternalItems.Added +=
                        delegate
                        {
                            this.HTMLTargetRef.requestAnimationFrame +=
                                delegate
                                {
                                    ColumnUpdateToHorizontalResizerScroll();
                                };

                        };

                    //this.InternalScrollContainerElement.onresize +=
                    //    delegate
                    //    {
                    //        //SourceColumn.ColumnHorizontalResizer.Hide();
                    //        SourceColumn.ColumnHorizontalResizer.style.backgroundColor = "red";

                    //        //Native.window.requestAnimationFrame += delegate
                    //        //{
                    //        //    ColumnUpdateToHorizontalResizerScroll();

                    //        //    Native.window.requestAnimationFrame += delegate
                    //        //       {
                    //        //           SourceColumn.ColumnHorizontalResizer.Show();
                    //        //       };

                    //        //};


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

                    #region CompensateFor
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

                            if (!this.InternalRowHeadersVisible)
                                x = 0;

                            for (int i = 0; i <= NewIndex; i++)
                            {
                                //x += this.InternalColumns.InternalItems[i].Width;
                                x += this.InternalColumns.InternalItems[i].Width + 1;
                            }

                            ColumnHorizontalResizerDrag.Position = new Point(x, 0);

                            ColumnUpdateToHorizontalResizerScroll();
                        };

                    Reposition();

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

                    #region Drag
                    var __DragStartX = 0;

                    {



                        ColumnHorizontalResizerDrag.DragStart +=
                            delegate
                            {

                                Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.move;
                                ((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = JSColor.Blue;

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
                                    ((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = "";
                                    //((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = "yellow";
                                    //SourceColumn.ColumnHorizontalResizer.style.backgroundColor = "red";

                                    var cwidth = SourceColumn.Width + ColumnHorizontalResizerDrag.Position.X - __DragStartX;
                                    Console.WriteLine(new { SourceColumn.Width, cwidth });

                                    SourceColumn.Width = cwidth;
                                };
                    }


                    ColumnHorizontalResizerDrag.DragMove +=
                        delegate
                        {
                            ColumnUpdateToHorizontalResizerScroll();

                        };



                    #region InternalAutoSize
                    this.InternalAutoResizeColumn += cindex =>
                    {
                        if (cindex != SourceColumn.Index)
                            return;



                        var rows = this.InternalRows.InternalItems.Source;

                        // InternalAutoSize { Count = 33, cindex = -1 }


                        //Console.WriteLine("InternalAutoSize " + new { rows.Count, cindex });

                        if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
                            if (cindex == this.Columns.Count - 1)
                            {
                                var w = Enumerable.Range(0, cindex).Select(
                                    c => this.Columns[c].Width
                                ).Sum();

                                var value = (ZeroHorizontalResizerDrag.Position.X + 4);

                                if (!this.InternalRowHeadersVisible)
                                    value = 4;


                                // { cindex = 0, w = 0, all = 1600, WidthByFill = 1600 } 

                                var all = this.InternalScrollContainerElement.scrollWidth;

                                var WidthByFill = all - w - value - 8;

                                Console.WriteLine(
                                    new { cindex, w, value, all, WidthByFill }

                                    );

                                //{ cindex = 0, w = 0, value = 99, all = 753, WidthByFill = 654 } 

                                __DragStartX = ColumnHorizontalResizerDrag.Position.X + (WidthByFill - SourceColumn.Width);
                                SourceColumn.Width = Math.Max(20, WidthByFill);

                                return;
                            }

                        if (cindex >= 0)
                        {
                            var WidthByRowsInThisColumn = rows.Max(
                                rr =>
                                {
                                    __DataGridViewCell cc = rr.Cells[cindex];


                                    //Console.WriteLine("InternalAutoSize " + new { rows.Count, cindex, cc.InternalContent.offsetWidth });

                                    return cc.InternalContent.offsetWidth;
                                }
                            );

                            WidthByRowsInThisColumn = Math.Max(WidthByRowsInThisColumn, this.InternalColumns.InternalItems[cindex].InternalContent.offsetWidth);
                            if (WidthByRowsInThisColumn == 0)
                            {
                                // no DOM?
                                //Console.WriteLine("InternalAutoSize skipped");
                            }
                            else
                            {

                                // extra padding?
                                WidthByRowsInThisColumn += 8 + 24;

                                //Console.WriteLine("InternalAutoSize" + new { SourceColumn.Width, cwidth = WidthByRowsInThisColumn });

                                __DragStartX = ColumnHorizontalResizerDrag.Position.X + (WidthByRowsInThisColumn - SourceColumn.Width);
                                SourceColumn.Width = Math.Max(20, WidthByRowsInThisColumn);
                            }
                        }
                    };

                    SourceColumn.ColumnHorizontalResizer.onmousedown +=
                        e =>
                        {
                            if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                            {
                                e.preventDefault();
                                e.stopPropagation();

                                this.InternalAutoResizeColumn(SourceColumn.Index);


                            }
                        };

                    SourceColumn.ColumnHorizontalResizer.ondblclick +=
                        delegate
                        {
                            this.InternalAutoResizeColumn(SourceColumn.Index);
                        };
                    #endregion


                    #endregion

                    if (ColumnAdded != null)
                        ColumnAdded(this, new DataGridViewColumnEventArgs((DataGridViewColumn)(object)SourceColumn));

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

                    foreach (var item in this.InternalColumns.InternalItems)
                    {
                        this.InternalAutoResizeColumn(item.Index);
                    }
                };
            #endregion

            #region ClientSizeChanged
            // whatif we are in autosize mode?
            this.ClientSizeChanged +=
                delegate
                {

                    if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
                    {
                        Native.window.requestAnimationFrame +=
                            delegate
                            {
                                this.AutoResizeColumn(this.Columns.Count - 1);
                            };
                    }
                };
            #endregion


            #region InternalRows

            this.InternalRows.InternalItems.Added +=
                  (SourceRow, CurrentRowIndex) =>
                  {
                      if (SourceRow.InternalTableRow != null)
                          return;

                      if (InternalNewRow != null)
                      {
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
                          if (this.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None)
                          {
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

            Console.WriteLine("DataGridView ready");
        }




    }
}
