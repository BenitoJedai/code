using ScriptCoreLib.JavaScript.Controls;
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

                        item.InternalTableColumn.style.backgroundColor = JSColor.System.Highlight;
                        item.InternalTableColumn.style.color = JSColor.System.HighlightText;
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


                    var c1contentc = new IHTMLDiv { }.AttachTo(c.InternalTableColumn);
                    c1contentc.style.overflow = IStyle.OverflowEnum.hidden;
                    c1contentc.style.position = IStyle.PositionEnum.absolute;
                    c1contentc.style.left = "0";
                    c1contentc.style.top = "0";
                    c1contentc.style.right = "0";
                    c1contentc.style.bottom = "0";
                    c1contentc.style.padding = "3px";


                    var c1content = new IHTMLSpan { innerText = c.HeaderText }.AttachTo(c1contentc);
                    c1content.style.margin = "6px";

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

                              SourceCell.InternalTableColumn.style.borderBottom = "1px solid gray";
                              //SourceCell.InternalTableColumn.style.borderRight = "1px solid gray";
                              SourceCell.InternalTableColumn.style.backgroundColor = JSColor.White;


                              var c1contentc = new IHTMLDiv { }.AttachTo(SourceCell.InternalTableColumn);
                              SourceCell.InternalContentContainer = c1contentc;
                              SourceCell.InternalContentContainer.tabIndex = (_e.NewIndex << 16) + CellIndex;

                              c1contentc.style.overflow = IStyle.OverflowEnum.hidden;
                              c1contentc.style.position = IStyle.PositionEnum.absolute;
                              c1contentc.style.left = "0";
                              c1contentc.style.top = "0";
                              c1contentc.style.right = "0";
                              c1contentc.style.bottom = "0";
                              c1contentc.style.padding = "3px";


                              var c1content = new IHTMLSpan { }.AttachTo(c1contentc);
                              c1content.style.margin = "6px";

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

                              #region EnterEditMode
                              Action EnterEditMode =
                                  delegate
                                  {
                                      c1contentc.Orphanize();

                                      var edit = new IHTMLInput(Shared.HTMLInputTypeEnum.text);

                                      edit.style.font = this.Font.ToCssString();


                                      edit.style.border = "0";
                                      edit.style.position = IStyle.PositionEnum.absolute;
                                      edit.style.left = "0";
                                      edit.style.top = "0";
                                      edit.style.right = "0";
                                      edit.style.bottom = "0";
                                      edit.AttachTo(SourceCell.InternalTableColumn);

                                      edit.value = (string)SourceCell.Value;
                                      edit.focus();

                                      Action AtBlur = delegate
                                      {
                                          SourceCell.Value = edit.value;
                                          edit.Orphanize();
                                          c1contentc.AttachTo(SourceCell.InternalTableColumn);
                                      };

                                      edit.onblur +=
                                         delegate
                                         {
                                             if (AtBlur != null)
                                                 AtBlur();
                                         };

                                      edit.onkeyup +=
                                        _ev =>
                                        {
                                            if (_ev.IsEscape)
                                            {
                                                AtBlur = null;

                                                _ev.PreventDefault();
                                                _ev.StopPropagation();

                                                edit.Orphanize();
                                                SourceCell.InternalContentContainer.AttachTo(SourceCell.InternalTableColumn);
                                                SourceCell.InternalContentContainer.focus();
                                            }
                                        };

                                      edit.onkeypress +=
                                          _ev =>
                                          {


                                              if (_ev.IsReturn)
                                              {
                                                  AtBlur = null;

                                                  _ev.PreventDefault();
                                                  _ev.StopPropagation();

                                                  SourceCell.Value = edit.value;
                                                  edit.Orphanize();
                                                  SourceCell.InternalContentContainer.AttachTo(SourceCell.InternalTableColumn);
                                                  SourceCell.InternalContentContainer.focus();

                                              }
                                          };


                                  };
                              #endregion

                              #region ondblclick
                              c1contentc.ondblclick +=
                                  ev =>
                                  {
                                      EnterEditMode();
                                  };
                              #endregion

                              #region onmousedown
                              c1contentc.onmousedown +=
                                  ev =>
                                  {
                                      if (!ev.ctrlKey)
                                      {


                                          // clear
                                          while (this.InternalSelectedCells.Count > 0)
                                          {
                                              var item = this.InternalSelectedCells.InternalItems[0];

                                              item.InternalTableColumn.style.backgroundColor = JSColor.System.Window;
                                              item.InternalTableColumn.style.color = JSColor.System.WindowText;

                                              this.InternalSelectedCells.RemoveAt(0);
                                          }
                                      }

                                      this.InternalSelectedCells.Add(SourceCell);

                                      ev.PreventDefault();


                                  };
                              #endregion


                              #region onmousemove
                              c1contentc.onmousemove +=
                                   ev =>
                                   {
                                       if (this.MultiSelect)
                                           if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                                           {
                                               if (!this.InternalSelectedCells.Contains(SourceCell))
                                                   this.InternalSelectedCells.Add(SourceCell);

                                               ev.PreventDefault();

                                           }
                                   };
                              #endregion


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


                              #region onkeydown
                              SourceCell.InternalContentContainer.onkeydown +=
                                  ev =>
                                  {
                                      #region KeyNavigateTo
                                      Action<Keys, int, int> KeyNavigateTo =
                                        (k, x, y) =>
                                        {
                                            if (ev.KeyCode == (int)k)
                                            {
                                                // focus the cell on the right

                                                ev.PreventDefault();
                                                ev.StopPropagation();

                                                var Cell = CellAtOffset(x, y);
                                                if (Cell != null)
                                                    Cell.InternalContentContainer.focus();




                                            }
                                        };
                                      #endregion

                                      KeyNavigateTo(Keys.Right, 1, 0);
                                      KeyNavigateTo(Keys.Left, -1, 0);
                                      KeyNavigateTo(Keys.Up, 0, -1);
                                      KeyNavigateTo(Keys.Down, 0, 1);

                                  };
                              #endregion

                              #region onkeypress
                              SourceCell.InternalContentContainer.onkeypress +=
                                  ev =>
                                  {
                                      if (ev.IsReturn)
                                      {
                                          ev.PreventDefault();
                                          ev.StopPropagation();

                                          EnterEditMode();
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

                                          item.InternalTableColumn.style.backgroundColor = JSColor.System.Window;
                                          item.InternalTableColumn.style.color = JSColor.System.WindowText;

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
        public event EventHandler SelectionChanged;
    }
}
