﻿using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridView))]
    internal class __DataGridView : __Control, ISupportInitialize
    {
        public IHTMLDiv InternalElement;
        public IHTMLDiv InternalContainerElement;

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
                return this.InternalContainerElement;
            }
        }

        public __DataGridView()
        {

            this.InternalColumns = new __DataGridViewColumnCollection();
            this.Columns = (DataGridViewColumnCollection)(object)this.InternalColumns;

            this.InternalRows = new __DataGridViewRowCollection();
            this.Rows = (DataGridViewRowCollection)(object)this.InternalRows;

            #region SelectedCells
            this.InternalSelectedCells = new __DataGridViewSelectedCellCollection();
            this.SelectedCells = (DataGridViewSelectedCellCollection)(object)this.InternalSelectedCells;
            this.InternalSelectedCells.InternalItems.ListChanged +=
                (_s, _e) =>
                {
                    if (_e.ListChangedType == ListChangedType.ItemAdded)
                    {
                        var item = this.InternalSelectedCells.InternalItems[_e.NewIndex];

                        item.InternalContentContainer.style.backgroundColor = JSColor.System.Highlight;
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

            this.InternalContainerElement = new IHTMLDiv().AttachTo(this.InternalElement);
            this.InternalContainerElement.style.backgroundColor = JSColor.Gray;
            this.InternalContainerElement.style.overflow = DOM.IStyle.OverflowEnum.auto;
            this.InternalContainerElement.style.position = DOM.IStyle.PositionEnum.absolute;
            this.InternalContainerElement.style.left = "0px";
            this.InternalContainerElement.style.top = "0px";
            this.InternalContainerElement.style.right = "0px";
            this.InternalContainerElement.style.bottom = "0px";


            var __ContentTableContainer = new IHTMLDiv().AttachTo(InternalContainerElement);
            IHTMLTable __ContentTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__ContentTableContainer);
            IHTMLTableBody __ContentTableBody = __ContentTable.AddBody();
            IHTMLTableRow __ContentTableNewRow = __ContentTableBody.AddRow();
            __ContentTableNewRow.style.height = "22px";
            __ContentTable.style.paddingTop = "22px";


            var __ColumnsTableContainer = new IHTMLDiv().AttachTo(InternalContainerElement);
            IHTMLTable __ColumnsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__ColumnsTableContainer);
            IHTMLTableRow __ColumnsTableRow = null;

            __ColumnsTableContainer.style.SetLocation(0, 0);
            __ColumnsTableRow = __ColumnsTable.AddBody().AddRow();
            __ColumnsTableRow.style.height = "22px";


            var __RowsTableContainer = new IHTMLDiv().AttachTo(InternalContainerElement);
            IHTMLTable __RowsTable = new IHTMLTable { cellPadding = 0, cellSpacing = 0 }.AttachTo(__RowsTableContainer);
            IHTMLTableBody __RowsTableBody = __RowsTable.AddBody();
            IHTMLTableRow __RowsTableNewRow = __RowsTableBody.AddRow();

            #region Rows table
            {
                __RowsTableContainer.style.SetLocation(0, 0);

                __RowsTableNewRow.style.height = "22px";

                var c0 = __RowsTableNewRow.AddColumn();
                c0.innerText = "*";
                c0.style.backgroundColor = JSColor.System.ButtonFace;

                __RowsTable.style.paddingTop = "22px";


            }
            #endregion

            #region Corner
            var Corner = new IHTMLDiv().AttachTo(InternalContainerElement);

            Corner.style.backgroundColor = JSColor.System.ButtonFace;
            Corner.style.SetLocation(0, 0);
            Corner.style.height = "22px";

            #endregion

            #region CreateVerticalResizer
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
                    l.style.backgroundColor = JSColor.Gray;

                    return r;
                };
            #endregion


            #region CreateHorizontalResizer
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
                    _HorizontalResizerLine.style.backgroundColor = JSColor.Gray;

                    return _HorizontalResizer;
                };
            #endregion

            var ZeroVerticalResizer = CreateVerticalResizer().AttachTo(InternalContainerElement);

            ZeroVerticalResizer.style.SetLocation(0, 22 - 5);

            #region ZeroHorizontalResizer

            var ZeroHorizontalResizer = CreateHorizontalResizer().AttachTo(InternalContainerElement);

            var ZeroHorizontalResizerDrag = new DragHelper(ZeroHorizontalResizer)
            {
                Position = new Point(95, 0),
                Enabled = true
            };


            Action UpdateToVerticalResizerScroll = delegate
            {
                ZeroVerticalResizer.style.SetLocation(
                        this.InternalContainerElement.scrollLeft,
                        this.InternalContainerElement.scrollTop + (22 - 5)
                    );
            };

            #region UpdateToHorizontalResizerScroll
            Action UpdateToHorizontalResizerScroll = delegate
            {
                ZeroHorizontalResizer.style.SetLocation(
                        this.InternalContainerElement.scrollLeft + ZeroHorizontalResizerDrag.Position.X,
                        this.InternalContainerElement.scrollTop
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
            this.InternalContainerElement.onscroll +=
               e =>
               {
                   UpdateToVerticalResizerScroll();
                   UpdateToHorizontalResizerScroll();

                   __RowsTableContainer.style.SetLocation(this.InternalContainerElement.scrollLeft, 0);
                   __ColumnsTableContainer.style.SetLocation(0, this.InternalContainerElement.scrollTop);

                   Corner.style.SetLocation(
                     this.InternalContainerElement.scrollLeft,
                     this.InternalContainerElement.scrollTop
                 );
               };
            #endregion

            #region InternalColumns
            this.InternalColumns.InternalItems.ListChanged += (_s, _e) =>
            {
                if (_e.ListChangedType == ListChangedType.ItemAdded)
                {
                    var c = this.InternalColumns.InternalItems[_e.NewIndex];

                    c.InternalTableColumn = __ColumnsTableRow.AddColumn();
                    c.InternalTableColumn.style.backgroundColor = JSColor.System.ButtonFace;
                    c.InternalTableColumn.style.position = IStyle.PositionEnum.relative;

                    var c1contentclight = new IHTMLDiv { }.AttachTo(c.InternalTableColumn);
                    c1contentclight.style.overflow = IStyle.OverflowEnum.hidden;
                    c1contentclight.style.position = IStyle.PositionEnum.absolute;
                    c1contentclight.style.left = "0";
                    c1contentclight.style.top = "0";
                    c1contentclight.style.right = "0";
                    c1contentclight.style.height = "10px";
                    c1contentclight.style.backgroundColor = JSColor.White;


                    var c1contentc = new IHTMLDiv { }.AttachTo(c.InternalTableColumn);
                    c1contentc.style.overflow = IStyle.OverflowEnum.hidden;
                    c1contentc.style.position = IStyle.PositionEnum.absolute;
                    c1contentc.style.left = "0";
                    c1contentc.style.top = "0";
                    c1contentc.style.right = "0";
                    c1contentc.style.height = "22px";


                    var c1content = new IHTMLSpan { innerText = c.HeaderText }.AttachTo(c1contentc);
                    c1content.style.marginLeft = "4px";
                    c1content.style.lineHeight = (20) + "px";

                    c.InternalHeaderTextChanged +=
                        delegate
                        {
                            c1content.innerText = c.HeaderText;
                        };




                    var ColumnHorizontalResizer = CreateHorizontalResizer();

                    __ColumnsTableContainer.insertNextSibling(ColumnHorizontalResizer);

                    var ColumnHorizontalResizerDrag = new DragHelper(ColumnHorizontalResizer)
                    {
                        Enabled = true
                    };

                    c.InternalHorizontalDrag = ColumnHorizontalResizerDrag;


                    #region ColumnUpdateToHorizontalResizerScroll
                    Action ColumnUpdateToHorizontalResizerScroll = delegate
                    {
                        ColumnHorizontalResizer.style.SetLocation(
                                 ColumnHorizontalResizerDrag.Position.X,
                                this.InternalContainerElement.scrollTop
                            );
                    };
                    #endregion



                    #region AtInternalWidthChanged
                    Action AtInternalWidthChanged =
                        delegate
                        {
                            c.InternalTableColumn.style.width = c.Width + "px";
                            c.InternalTableColumn.style.minWidth = c.Width + "px";
                            //c1content.innerText = c.HeaderText + ":" + c.Width;
                            //c.InternalTableColumn.style.backgroundColor = JSColor.Yellow;
                        };



                    c.InternalWidthChanged += AtInternalWidthChanged;

                    AtInternalWidthChanged();
                    #endregion



                    this.InternalContainerElement.onscroll +=
                      e =>
                      {
                          ColumnUpdateToHorizontalResizerScroll();
                      };

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

                            for (int i = 0; i <= _e.NewIndex; i++)
                            {
                                x += this.InternalColumns.InternalItems[i].Width;
                            }

                            ColumnHorizontalResizerDrag.Position = new Point(x, 0);

                            ColumnUpdateToHorizontalResizerScroll();
                        };

                    Reposition();

                    for (int i = 0; i <= _e.NewIndex; i++)
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

                    {
                        var __X = 0;
                        ColumnHorizontalResizerDrag.DragStart +=
                            delegate
                            {
                                Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.move;
                                ((IHTMLElement)ColumnHorizontalResizer.firstChild).style.backgroundColor = JSColor.Blue;

                                __X = ColumnHorizontalResizerDrag.Position.X;
                            };



                        ColumnHorizontalResizerDrag.DragStop +=
                             delegate
                             {
                                 Native.Document.body.style.cursor = DOM.IStyle.CursorEnum.auto;
                                 ((IHTMLElement)ColumnHorizontalResizer.firstChild).style.backgroundColor = JSColor.Gray;

                                 c.Width += ColumnHorizontalResizerDrag.Position.X - __X;
                             };
                    }


                    ColumnHorizontalResizerDrag.DragMove +=
                        delegate
                        {
                            ColumnUpdateToHorizontalResizerScroll();

                        };
                    #endregion
                }
            };
            #endregion

            #region InternalRows
            this.InternalRows.InternalItems.ListChanged +=
                  (_s, _e) =>
                  {
                      var CurrentRowIndex = _e.NewIndex;
                      var r = this.InternalRows.InternalItems[_e.NewIndex];

                      __ContentTableNewRow.Orphanize();
                      {
                          var tr = __ContentTableBody.AddRow();

                          #region AtInternalHeightChanged
                          Action AtInternalHeightChanged = delegate
                          {
                              tr.style.height = r.InternalHeight + "px";
                          };

                          AtInternalHeightChanged();
                          r.InternalHeightChanged += AtInternalHeightChanged;
                          #endregion


                          var r_Cells = (__DataGridViewCellCollection)(object)r.Cells;
                          var CellIndex = -1;
                          foreach (var SourceCell in r_Cells.InternalItems)
                          {
                              CellIndex++;
                              var CurrentCellIndex = CellIndex;

                              // is cell index equal to column index?
                              // what happens if we dont have enough columns?
                              var InternalColumn = this.InternalColumns.InternalItems[CellIndex];

                              SourceCell.InternalTableColumn = tr.AddColumn();

                              SourceCell.InternalTableColumn.style.position = IStyle.PositionEnum.relative;

                              //SourceCell.InternalTableColumn.style.borderRight = "1px solid gray";
                              SourceCell.InternalTableColumn.style.backgroundColor = JSColor.White;


                              SourceCell.InternalContentContainer = new IHTMLDiv { }.AttachTo(SourceCell.InternalTableColumn);
                              SourceCell.InternalContentContainer.tabIndex = (_e.NewIndex << 16) + CellIndex;

                              SourceCell.InternalContentContainer.style.overflow = IStyle.OverflowEnum.hidden;
                              SourceCell.InternalContentContainer.style.position = IStyle.PositionEnum.relative;
                              SourceCell.InternalContentContainer.style.left = "0";
                              SourceCell.InternalContentContainer.style.top = "0";
                              SourceCell.InternalContentContainer.style.right = "0";
                              SourceCell.InternalContentContainer.style.height = (r.Height - 1) + "px";

                              SourceCell.InternalTableColumn.style.borderBottom = "1px solid gray";


                              var c1content = new IHTMLSpan { }.AttachTo(SourceCell.InternalContentContainer);
                              c1content.style.marginLeft = "4px";
                              c1content.style.lineHeight = (r.Height - 1) + "px";

                              #region AtInternalValueChanged
                              Action AtInternalValueChanged = delegate
                              {
                                  c1content.innerText = SourceCell.Value.ToString();
                              };

                              AtInternalValueChanged();
                              SourceCell.InternalValueChanged += AtInternalValueChanged;
                              #endregion

                              //c1content.style.margin = "6px";

                              #region AtInternalWidthChanged
                              Action AtInternalWidthChanged = delegate
                              {
                                  SourceCell.InternalTableColumn.style.width = InternalColumn.Width + "px";
                                  SourceCell.InternalTableColumn.style.minWidth = InternalColumn.Width + "px";
                                  //c1.style.backgroundColor = JSColor.Red;
                                  //c1content.innerText = "@" + InternalColumn.HeaderText + ":" + InternalColumn.Width;
                              };

                              AtInternalWidthChanged();

                              InternalColumn.InternalWidthChanged += AtInternalWidthChanged;
                              #endregion


                              #region CellAtOffset
                              Func<int, int, __DataGridViewCell> CellAtOffset =
                                  (x, y) =>
                                  {
                                      var value = default(__DataGridViewCell);

                                      var Row = this.InternalRows.InternalItems.ElementAtOrDefault(
                                          CurrentRowIndex + y
                                      );

                                      if (Row != null)
                                      {
                                          value = Row.InternalCells.InternalItems.ElementAtOrDefault(
                                              CurrentCellIndex + x
                                          );
                                      }

                                      return value;
                                  };
                              #endregion

                              #region EnterEditMode
                              Action EnterEditMode =
                                  delegate
                                  {
                                      SourceCell.InternalContentContainer.Orphanize();

                                      var EditElement = new IHTMLInput(Shared.HTMLInputTypeEnum.text);

                                      EditElement.style.font = this.Font.ToCssString();


                                      EditElement.style.borderWidth = "0";
                                      EditElement.style.position = IStyle.PositionEnum.relative;
                                      EditElement.style.left = "4px";
                                      EditElement.style.top = "0";

                                      EditElement.style.outline = "0";
                                      EditElement.style.padding = "0";
                                      EditElement.style.width = (InternalColumn.Width - 4) + "px";
                                      EditElement.style.height = (r.Height - 1) + "px";

                                      EditElement.AttachTo(SourceCell.InternalTableColumn);

                                      SourceCell.InternalStyle.InternalForeColorChanged =
                                          delegate
                                          {
                                              EditElement.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                                          };

                                      EditElement.value = (string)SourceCell.Value;

                                      bool ExitEditModeDone = false;

                                      #region ExitEditMode
                                      Action ExitEditMode = delegate
                                      {
                                          if (ExitEditModeDone) return;
                                          ExitEditModeDone = true;


                                          EditElement.Orphanize();
                                          SourceCell.InternalContentContainer.AttachTo(SourceCell.InternalTableColumn);

                                          SourceCell.InternalStyle.InternalForeColorChanged =
                                              delegate
                                              {
                                                  SourceCell.InternalContentContainer.style.color = SourceCell.InternalStyle.InternalForeColor.ToString();
                                              };

                                          if (this.CellEndEdit != null)
                                              this.CellEndEdit(this,
                                                  new DataGridViewCellEventArgs(CurrentCellIndex, CurrentRowIndex)
                                              );
                                      };
                                      #endregion

                                      #region CheckChanges
                                      Action CheckChanges = delegate
                                      {
                                          if (((string)SourceCell.Value) != EditElement.value)
                                          {
                                              SourceCell.Value = EditElement.value;

                                              if (this.CellValueChanged != null)
                                                  this.CellValueChanged(this,
                                                      new DataGridViewCellEventArgs(CurrentCellIndex, CurrentRowIndex)
                                                  );
                                          }

                                      };
                                      #endregion


                                      #region CellBeginEdit
                                      EditElement.onfocus +=
                                          delegate
                                          {

                                              EditElement.select();
                                          };
                                      EditElement.focus();

                                      if (this.CellBeginEdit != null)
                                          this.CellBeginEdit(this,
                                              new DataGridViewCellCancelEventArgs(CurrentCellIndex, CurrentRowIndex)
                                          );
                                      #endregion


                                      #region onblur
                                      EditElement.onblur +=
                                         delegate
                                         {
                                             if (ExitEditMode != null)
                                                 ExitEditMode();

                                             if (CheckChanges != null)
                                                 CheckChanges();
                                         };
                                      #endregion


                                      #region onkeyup
                                      EditElement.onkeyup +=
                                        _ev =>
                                        {
                                            if (_ev.IsEscape)
                                            {
                                                CheckChanges = null;

                                                _ev.PreventDefault();
                                                _ev.StopPropagation();

                                                ExitEditMode();
                                                SourceCell.InternalContentContainer.focus();
                                            }

                                            if (_ev.KeyCode == (int)Keys.Up)
                                            {
                                                var Cell = CellAtOffset(0, -1);
                                                if (Cell != null)
                                                {
                                                    Cell.InternalContentContainer.focus();
                                                }
                                                return;
                                            }

                                            if (_ev.KeyCode == (int)Keys.Down)
                                            {
                                                var Cell = CellAtOffset(0, 1);
                                                if (Cell != null)
                                                {
                                                    Cell.InternalContentContainer.focus();
                                                }
                                                return;
                                            }

                                        };
                                      #endregion


                                      EditElement.onkeypress +=
                                          _ev =>
                                          {

                                              if (_ev.IsReturn)
                                              {
                                                  _ev.PreventDefault();
                                                  _ev.StopPropagation();


                                                  ExitEditMode();
                                                  SourceCell.InternalContentContainer.focus();

                                              }

                                          };


                                  };
                              #endregion

                              #region ondblclick
                              SourceCell.InternalContentContainer.ondblclick +=
                                  ev =>
                                  {
                                      EnterEditMode();
                                  };
                              #endregion

                              #region onmousedown
                              SourceCell.InternalContentContainer.onmousedown +=
                                  ev =>
                                  {
                                      if (!ev.ctrlKey)
                                      {


                                          // clear
                                          while (this.InternalSelectedCells.Count > 0)
                                          {
                                              var item = this.InternalSelectedCells.InternalItems[0];

                                              item.InternalContentContainer.style.backgroundColor = JSColor.System.Window;
                                              item.InternalContentContainer.style.color = item.InternalStyle.InternalForeColor.ToString();

                                              this.InternalSelectedCells.RemoveAt(0);
                                          }
                                      }

                                      this.InternalSelectedCells.Add(SourceCell);

                                      ev.PreventDefault();


                                  };
                              #endregion


                              //#region onmousemove
                              //SourceCell.InternalContentContainer.onmousemove +=
                              //     ev =>
                              //     {
                              //         if (this.MultiSelect)
                              //             if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                              //             {
                              //                 if (!this.InternalSelectedCells.Contains(SourceCell))
                              //                     this.InternalSelectedCells.Add(SourceCell);

                              //                 ev.PreventDefault();

                              //             }
                              //     };
                              //#endregion





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



                              var hasfocus = false;
                              #region onfocus
                              SourceCell.InternalContentContainer.onblur +=
                                  ev =>
                                  {
                                      hasfocus = false;
                                  };


                              SourceCell.InternalContentContainer.onfocus +=
                                  ev =>
                                  {
                                      hasfocus = true;

                                      ev.PreventDefault();
                                      ev.StopPropagation();

                                      // clear
                                      while (this.InternalSelectedCells.Count > 0)
                                      {
                                          var item = this.InternalSelectedCells.InternalItems[0];

                                          item.InternalContentContainer.style.backgroundColor = JSColor.System.Window;
                                          item.InternalContentContainer.style.color = item.InternalStyle.InternalForeColor.ToString();

                                          this.InternalSelectedCells.RemoveAt(0);
                                      }

                                      if (!this.InternalSelectedCells.Contains(SourceCell))
                                          this.InternalSelectedCells.Add(SourceCell);

                                  };
                              #endregion

                              #region onmouseup
                              SourceCell.InternalContentContainer.onmouseup +=
                                  ev =>
                                  {
                                      if (!ev.ctrlKey)
                                          if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                                          {
                                              if (hasfocus)
                                                  EnterEditMode();
                                              else
                                                  SourceCell.InternalContentContainer.focus();

                                          }
                                  };
                              #endregion


                          }

                      }
                      __ContentTableNewRow.AttachTo(__ContentTableBody);

                      #region __RowsTableNewRow
                      __RowsTableNewRow.Orphanize();
                      {
                          var __tr = __RowsTableBody.AddRow();

                          #region AtInternalHeightChanged
                          Action AtInternalHeightChanged = delegate
                          {
                              __tr.style.height = r.InternalHeight + "px";
                          };

                          AtInternalHeightChanged();
                          r.InternalHeightChanged += AtInternalHeightChanged;
                          #endregion



                          var c0 = __tr.AddColumn();
                          c0.style.borderBottom = "1px solid gray";
                          c0.innerText = " ";
                          //c0.style.padding = "4px";
                          c0.style.backgroundColor = JSColor.System.ButtonFace;
                          c0.style.width = "100%";

                      }
                      __RowsTableNewRow.AttachTo(__RowsTableBody);
                      #endregion
                  };
            #endregion



        }


        public bool MultiSelect { get; set; }
        public bool AllowUserToOrderColumns { get; set; }
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }

        public __DataGridViewSelectedCellCollection InternalSelectedCells { get; set; }
        public DataGridViewSelectedCellCollection SelectedCells { get; set; }

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


        public event DataGridViewCellEventHandler CellContentClick;
        public event DataGridViewCellEventHandler CellEndEdit;
        public event DataGridViewCellCancelEventHandler CellBeginEdit;
        public event DataGridViewCellEventHandler CellValueChanged;

        public event EventHandler SelectionChanged;



        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {
                return (DataGridViewCell)(object)this.InternalRows.InternalItems[rowIndex].InternalCells.InternalItems[columnIndex];
            }
            set
            {
            }
        }
    }
}
