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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridView))]
    internal class __DataGridView : __Control, __ISupportInitialize
    {
        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode { get; set; }

        public bool AllowUserToAddRows { get; set; }

        public IHTMLDiv InternalElement;
        public IHTMLDiv InternalScrollContainerElement;

        public override DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return this.InternalScrollContainerElement;
            }
        }

        // we do not yet use it.. needs to be tested
        public DataGridViewCellStyle DefaultCellStyle { get; set; }
        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get; set; }
        public DataGridViewCellStyle RowHeadersDefaultCellStyle { get; set; }

        public Action<int> InternalAutoResizeColumn;

        public void AutoResizeColumn(int columnIndex)
        {
            if (InternalAutoResizeColumn != null)
                InternalAutoResizeColumn(columnIndex);
        }


        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_GridColor(System.Drawing.Color)]
        public global::System.Drawing.Color GridColor { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BackgroundColor(System.Drawing.Color)]
        public global::System.Drawing.Color InternalBackgroundColor;
        public event Action InternalBackgroundColorChanged;
        public global::System.Drawing.Color BackgroundColor
        {
            get
            {
                return InternalBackgroundColor;
            }
            set
            {
                this.InternalBackgroundColor = value;
                this.InternalScrollContainerElement.style.backgroundColor = InternalBackgroundColor.ToString();
                if (InternalBackgroundColorChanged != null)
                    InternalBackgroundColorChanged();
            }
        }

        public bool InternalSkipAutoSize;


        public __DataGridView()
        {
            //Console.WriteLine("__DataGridView");

            this.DefaultCellStyle = new DataGridViewCellStyle();


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

                        item.InternalContentContainer.style.backgroundColor = SelectionBackColor.ToString();
                        //}
                        item.InternalContentContainer.style.color = JSColor.System.HighlightText;
                    }


                    if (SelectionChanged != null)
                        SelectionChanged(this, new EventArgs());
                };
            #endregion


            this.MultiSelect = true;

            this.InternalElement = new IHTMLDiv();
            this.InternalElement.style.border = "1px solid gray";
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
            IHTMLTable __ContentTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__ContentTableContainer);
            __ContentTable.style.paddingTop = "22px";

            IHTMLTableBody __ContentTableBody = __ContentTable.AddBody();

            var InternalNewRow = new __DataGridViewRow();

            InternalNewRow.InternalTableRow = __ContentTableBody.AddRow();
            InternalNewRow.InternalTableRow.style.height = "22px";

            this.InternalRows.InternalItems.Source.Add(InternalNewRow);

            var __ColumnsTableContainer = new IHTMLDiv().AttachTo(InternalScrollContainerElement);
            IHTMLTable __ColumnsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__ColumnsTableContainer);
            IHTMLTableRow __ColumnsTableRow = null;

            __ColumnsTableContainer.style.SetLocation(0, 0);
            __ColumnsTableRow = __ColumnsTable.AddBody().AddRow();
            __ColumnsTableRow.style.height = "22px";


            var __RowsTableContainer = new IHTMLDiv().AttachTo(InternalScrollContainerElement);
            __RowsTableContainer.style.SetLocation(0, 0);


            IHTMLTable __RowsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__RowsTableContainer);
            __RowsTable.style.paddingTop = "22px";
            IHTMLTableBody __RowsTableBody = __RowsTable.AddBody();





            #region Corner
            var Corner = new IHTMLDiv().AttachTo(InternalScrollContainerElement);

            Corner.style.backgroundColor = JSColor.System.ButtonFace;
            Corner.style.SetLocation(0, 0);
            Corner.style.height = "22px";

            #endregion

            #region CreateVerticalResizer |
            Func<IHTMLDiv> CreateVerticalResizer =
                () =>
                {
                    var r = new IHTMLDiv();

                    r.style.position = DOM.IStyle.PositionEnum.absolute;
                    r.style.height = "9px";
                    r.style.left = "0px";
                    r.style.width = "100%";
                    //HorizontalResizer.style.backgroundColor = JSColor.Red;
                    //r.style.cursor = DOM.IStyle.CursorEnum.move;

                    var l = new IHTMLDiv().AttachTo(r);

                    l.style.position = DOM.IStyle.PositionEnum.absolute;
                    l.style.top = "4px";
                    l.style.height = "1px";
                    l.style.left = "0px";
                    l.style.right = "0px";

                    l.style.backgroundColor = this.InternalBackgroundColor.ToString();
                    InternalBackgroundColorChanged +=
                        delegate
                        {
                            l.style.backgroundColor = this.InternalBackgroundColor.ToString();
                        };

                    return r;
                };
            #endregion


            #region CreateHorizontalResizer -
            Func<IHTMLDiv> CreateHorizontalResizer =
                () =>
                {
                    var _HorizontalResizer = new IHTMLDiv();

                    _HorizontalResizer.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizer.style.width = "9px";
                    _HorizontalResizer.style.top = "0px";
                    _HorizontalResizer.style.height = "100%";
                    //HorizontalResizer.style.backgroundColor = JSColor.Red;
                    _HorizontalResizer.style.cursor = DOM.IStyle.CursorEnum.move;

                    var _HorizontalResizerLine = new IHTMLDiv().AttachTo(_HorizontalResizer);

                    _HorizontalResizerLine.style.position = DOM.IStyle.PositionEnum.absolute;
                    _HorizontalResizerLine.style.left = "4px";
                    _HorizontalResizerLine.style.width = "1px";
                    _HorizontalResizerLine.style.top = "0px";
                    _HorizontalResizerLine.style.bottom = "0px";

                    #region InternalBackgroundColor
                    _HorizontalResizerLine.style.backgroundColor = this.InternalBackgroundColor.ToString();
                    InternalBackgroundColorChanged +=
                        delegate
                        {
                            _HorizontalResizerLine.style.backgroundColor = this.InternalBackgroundColor.ToString();
                        };
                    #endregion

                    //this.InternalColumns

                    return _HorizontalResizer;
                };
            #endregion

            var ZeroVerticalResizer = CreateVerticalResizer().AttachTo(InternalScrollContainerElement);

            ZeroVerticalResizer.style.SetLocation(0, 22 - 5);

            #region ZeroHorizontalResizer

            var ZeroHorizontalResizer = CreateHorizontalResizer().AttachTo(InternalScrollContainerElement);

            var ZeroHorizontalResizerDrag = new DragHelper(ZeroHorizontalResizer)
            {
                Position = new Point(95, 0),
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
                var value = (ZeroHorizontalResizerDrag.Position.X + 4);

                Corner.style.width = value + "px";

                __ColumnsTable.style.paddingLeft = value + "px";
                __ContentTable.style.paddingLeft = value + "px";

                __RowsTable.style.width = value + "px";
                __RowsTable.style.minWidth = value + "px";

            };

            UpdateToHorizontalResizerScroll();
            UpdateToHorizontalResizerDrag();
            #endregion

            #region Drag
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
                     ((IHTMLElement)ZeroHorizontalResizer.firstChild).style.backgroundColor = JSColor.Gray;

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

                   Corner.style.SetLocation(
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

                    //if (SourceCell.ColumnIndex == 0)
                    //    SourceCell.InternalTableColumn.style.backgroundColor = JSColor.Cyan;
                    //else
                    SourceCell.InternalTableColumn.style.backgroundColor = JSColor.System.Window;

                    // this wont work if we have multiple datagrids
                    // can we have a test for it?
                    SourceCell.InternalContentContainer = new IHTMLDiv { }.AttachTo(SourceCell.InternalTableColumn);
                    SourceCell.InternalContentContainer.tabIndex = (((SourceRow.Index + 1) << 16) + (SourceCell.ColumnIndex + 1));

                    SourceCell.InternalContentContainer.style.overflow = IStyle.OverflowEnum.hidden;
                    SourceCell.InternalContentContainer.style.position = IStyle.PositionEnum.relative;
                    SourceCell.InternalContentContainer.style.left = "0";
                    SourceCell.InternalContentContainer.style.top = "0";
                    SourceCell.InternalContentContainer.style.height = (SourceRow.Height - 1) + "px";
                    SourceCell.InternalContentContainer.style.font = DefaultFont.ToCssString();

                    SourceCell.InternalTableColumn.style.borderBottom = "1px solid gray";


                    if (SourceRow.Index % 2 == 1)
                        if (this.AlternatingRowsDefaultCellStyle != null)
                        {
                            // tested by
                            // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.cs

                            var BackColor = this.AlternatingRowsDefaultCellStyle.BackColor;
                            SourceCell.InternalStyle.InternalBackColor = BackColor;
                        }


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
                    #endregion

                    SourceCell.InternalContent = new IHTMLSpan { };
                    var InternalContent = SourceCell.InternalContent;

                    #region AtInternalValueChanged
                    Action AtInternalValueChanged = delegate
                    {
                        InternalContent.innerText = SourceCell.Value.ToString();

                        InternalRaiseCellValueChanged(SourceCell);


                    };

                    AtInternalValueChanged();
                    SourceCell.InternalValueChanged += AtInternalValueChanged;
                    #endregion

                    #region __DataGridViewButtonCell
                    if (SourceCell is __DataGridViewButtonCell)
                    {
                        var InternalButton = new IHTMLButton().AttachTo(SourceCell.InternalContentContainer);
                        InternalButton.style.font = DefaultFont.ToCssString();
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

                            ExitEditModeDone = false;

                            SourceCell.InternalContentContainer.Orphanize();

                            var EditElement = new IHTMLInput(Shared.HTMLInputTypeEnum.text);

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
                            EditElement.value = (string)SourceCell.Value;


                            #region CheckChanges
                            Action CheckChanges = delegate
                            {
                                //if (((string)SourceCell.Value) != EditElement.value)
                                //{
                                SourceCell.Value = EditElement.value;

                                //}

                            };
                            #endregion

                            #region ExitEditMode
                            Action ExitEditMode = delegate
                            {
                                if (ExitEditModeDone) return;
                                ExitEditModeDone = true;


                                EditElement.Orphanize();
                                SourceCell.InternalContentContainer.AttachTo(SourceCell.InternalTableColumn);

                                SourceCell.InternalStyle.InternalForeColorChanged +=
                                    delegate
                                    {
                                        SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                                    };

                                SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();


                                InternalRaiseCellEndEdit(SourceCell);

                                if (OriginalValue == (string)SourceCell.Value)
                                    return;

                                this.AutoResizeColumn(SourceCell.ColumnIndex);
                            };
                            #endregion



                            #region CellBeginEdit
                            EditElement.onfocus +=
                                delegate
                                {

                                    EditElement.select();
                                };
                            EditElement.focus();

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
                                    ev.PreventDefault();

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

                            if (KeyNavigateTo(Keys.Right, 1, 0)) return;
                            if (KeyNavigateTo(Keys.Left, -1, 0)) return;
                            if (KeyNavigateTo(Keys.Up, 0, -1)) return;
                            if (KeyNavigateTo(Keys.Down, 0, 1)) return;

                            if (ev.IsReturn)
                            {
                                ev.PreventDefault();
                                ev.StopPropagation();

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

                                    item.InternalContentContainer.style.backgroundColor = item.InternalStyle.InternalBackColor.ToString();
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


                             SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();
                         };

                    SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                    SourceCell.InternalContentContainer.style.backgroundColor = SourceCell.InternalStyle.InternalBackColor.ToString();


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
                                    SourceCell.Style.BackColor = SourceColumn.DefaultCellStyle.BackColor;
                                }
                            };

                        if (SourceColumn.DefaultCellStyle != null)
                        {
                            SourceCell.Style.ForeColor = SourceColumn.DefaultCellStyle.ForeColor;
                            SourceCell.Style.BackColor = SourceColumn.DefaultCellStyle.BackColor;
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

                    SourceColumn.InternalTableColumn.style.backgroundColor = JSColor.System.ButtonFace;
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

                    var c1contentclight = new IHTMLDiv { }.AttachTo(c1contentcrel);
                    c1contentclight.style.overflow = IStyle.OverflowEnum.hidden;
                    c1contentclight.style.position = IStyle.PositionEnum.absolute;
                    c1contentclight.style.left = "0";
                    c1contentclight.style.top = "0";
                    c1contentclight.style.right = "0";
                    c1contentclight.style.height = "10px";

                    c1contentclight.style.backgroundColor = JSColor.White;


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
                    c1content.style.font = DefaultFont.ToCssString();
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
                            SourceColumn.InternalTableColumn.style.width = SourceColumn.Width + "px";
                            SourceColumn.InternalTableColumn.style.minWidth = SourceColumn.Width + "px";
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

                    this.InternalScrollContainerElement.onresize +=
                        delegate
                        {
                            ColumnUpdateToHorizontalResizerScroll();
                        };

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
                            var x = ZeroHorizontalResizerDrag.Position.X;

                            for (int i = 0; i <= NewIndex; i++)
                            {
                                x += this.InternalColumns.InternalItems[i].Width;
                            }

                            ColumnHorizontalResizerDrag.Position = new Point(x, 0);

                            ColumnUpdateToHorizontalResizerScroll();
                        };

                    Reposition();

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
                                    ((IHTMLElement)SourceColumn.ColumnHorizontalResizer.firstChild).style.backgroundColor = this.InternalBackgroundColor.ToString();

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

                        if (cindex >= 0)
                        {
                            var cwidth = rows.Max(
                                rr =>
                                {
                                    __DataGridViewCell cc = rr.Cells[cindex];


                                    //Console.WriteLine("InternalAutoSize " + new { rows.Count, cindex, cc.InternalContent.offsetWidth });

                                    return cc.InternalContent.offsetWidth;
                                }
                            );

                            cwidth = Math.Max(cwidth, this.InternalColumns.InternalItems[cindex].InternalContent.offsetWidth);

                            // extra padding?
                            cwidth += 8 + 24;

                            Console.WriteLine("InternalAutoSize" + new { SourceColumn.Width, cwidth });

                            __DragStartX = ColumnHorizontalResizerDrag.Position.X + (cwidth - SourceColumn.Width);
                            SourceColumn.Width = Math.Max(20, cwidth);
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

            #region InitializeZeroColumnCell
            Action<__DataGridViewRow> InitializeZeroColumnCell =
                SourceRow =>
                {
                    var __tr = __RowsTableBody.AddRow();

                    SourceRow.InternalZeroColumnTableRow = __tr;



                    var InternalTableColumn = __tr.AddColumn();
                    InternalTableColumn.style.borderBottom = "1px solid gray";
                    InternalTableColumn.innerText = " ";
                    //c0.style.padding = "4px";
                    InternalTableColumn.style.backgroundColor = JSColor.System.ButtonFace;
                    InternalTableColumn.style.width = "100%";

                    var c1contentcrel = new IHTMLDiv { }.AttachTo(InternalTableColumn);
                    c1contentcrel.style.position = IStyle.PositionEnum.relative;
                    c1contentcrel.style.left = "0";
                    c1contentcrel.style.top = "0";
                    c1contentcrel.style.right = "0";
                    c1contentcrel.style.height = "21px";
                    c1contentcrel.style.overflow = IStyle.OverflowEnum.hidden;

                    var c1 = new IHTMLDiv().AttachTo(c1contentcrel);
                    c1.style.position = IStyle.PositionEnum.absolute;
                    c1.style.backgroundColor = JSColor.White;
                    c1.style.left = "0";
                    c1.style.top = "0";
                    c1.style.width = "6px";

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
                        c1.style.height = (SourceRow.InternalHeight - 1) + "px";
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

            Console.WriteLine("DataGridView ready");
        }


        public bool MultiSelect { get; set; }
        public bool AllowUserToOrderColumns { get; set; }
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }

        public __DataGridViewSelectedCellCollection InternalSelectedCells { get; set; }
        public DataGridViewSelectedCellCollection SelectedCells { get; set; }


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.get_SelectedRows()]

        public __DataGridViewColumnCollection InternalColumns;
        public DataGridViewColumnCollection Columns { get; set; }



        public __DataGridViewRowCollection InternalRows;
        public DataGridViewRowCollection Rows { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public Action<__DataGridViewCell> InternalRaiseCellBeginEdit;
        public Action<__DataGridViewCell> InternalRaiseCellEndEdit;
        public Action<__DataGridViewCell> InternalRaiseCellValueChanged;



        public event DataGridViewCellEventHandler CellDoubleClick;

        public event DataGridViewColumnEventHandler ColumnAdded;
        public event DataGridViewCellEventHandler CellContentClick;
        public event DataGridViewCellEventHandler CellEndEdit;
        public event DataGridViewCellCancelEventHandler CellBeginEdit;
        public event DataGridViewCellEventHandler CellValueChanged;
        public event DataGridViewRowEventHandler UserAddedRow;
        public event DataGridViewRowEventHandler UserDeletedRow;
        public event EventHandler SelectionChanged;
        public event DataGridViewCellEventHandler CellEnter;
        public event DataGridViewCellEventHandler CellLeave;


        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {
                return (DataGridViewCell)(object)this.InternalRows.InternalItems.Source[rowIndex].InternalCells.InternalItems[columnIndex];
            }
            set
            {
            }
        }

        #region DataSource
        public event EventHandler DataSourceChanged;

        public object InternalDataSource;
        public object DataSource
        {
            get
            {
                return InternalDataSource;
            }
            set
            {
                this.InternalDataSource = value;

                #region DataTable
                var DataTable = value as DataTable;
                if (DataTable != null)
                {
                    // now what?

                    // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationControl.cs
                    // http://stackoverflow.com/questions/6902269/moving-data-from-datatable-to-datagridview-in-c-sharp

                    this.Rows.Clear();

                    while (this.Columns.Count > DataTable.Columns.Count)
                        this.Columns.RemoveAt(this.Columns.Count - 1);


                    var cIndex = 0;
                    foreach (DataColumn item in DataTable.Columns)
                    {
                        if (cIndex < this.Columns.Count)
                        {
                            this.Columns[cIndex].HeaderText = item.ColumnName;
                        }
                        else
                        {
                            this.Columns.Add(
                                new DataGridViewColumn
                                {
                                    HeaderText = item.ColumnName
                                }
                            );
                        }

                        cIndex++;
                    }

                    foreach (DataRow item in DataTable.Rows)
                    {
                        var r = new DataGridViewRow();

                        foreach (DataColumn c in DataTable.Columns)
                        {
                            r.Cells.Add(
                                new DataGridViewTextBoxCell
                                {
                                    // two way binding?
                                    //ReadOnly = true,

                                    Value = item[c]
                                }
                            );
                        }

                        this.Rows.Add(r);
                    }
                }
                #endregion


                if (DataSourceChanged != null)
                    DataSourceChanged(this, new EventArgs());

            }
        }
        #endregion



        //script: error JSC1000: No implementation found for this native method, please implement 
        // [System.Windows.Forms.DataGridView.set_AlternatingRowsDefaultCellStyle(System.Windows.Forms.DataGridViewCellStyle)]

        public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
        {
            get;
            set;
        }

        public DataGridViewSelectionMode SelectionMode { get; set; }


        // tested by
        // X:\jsc.svn\examples\javascript\forms\FormsDataGridRowSelect\FormsDataGridRowSelect\ApplicationControl.Designer.cs
        public bool ReadOnly { get; set; }


        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ReadOnly(System.Boolean)]

        //        arg[0] is typeof System.Windows.Forms.BorderStyle
        ////script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]

        public BorderStyle BorderStyle { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ColumnHeadersBorderStyle(System.Windows.Forms.DataGridViewHeaderBorderStyle)]

        public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle { get; set; }
    }
}
