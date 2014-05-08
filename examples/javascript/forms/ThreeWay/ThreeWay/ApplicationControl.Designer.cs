using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ThreeWay
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.book1LeftSheetBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.leftContentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.middleSheetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1LeftSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.book1MiddleSheetBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.contentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1MiddleSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.book1MiddleViewBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.latestLeftContentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latestRightContentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1MiddleViewBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.book1MiddleSheetBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.book1RightSheetBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1RightSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1LeftSheetBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1LeftSheetBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingNavigator)).BeginInit();
            this.book1MiddleSheetBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.book1LeftSheetBindingSourceDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(796, 423);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // book1LeftSheetBindingSourceDataGridView
            // 
            this.book1LeftSheetBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1LeftSheetBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1LeftSheetBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.leftContentDataGridViewTextBoxColumn,
            this.middleSheetDataGridViewTextBoxColumn,
            this.keyDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.book1LeftSheetBindingSourceDataGridView.DataSource = this.book1LeftSheetBindingSourceBindingSource;
            this.book1LeftSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1LeftSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1LeftSheetBindingSourceDataGridView.Name = "book1LeftSheetBindingSourceDataGridView";
            this.book1LeftSheetBindingSourceDataGridView.Size = new System.Drawing.Size(154, 423);
            this.book1LeftSheetBindingSourceDataGridView.TabIndex = 0;
            // 
            // leftContentDataGridViewTextBoxColumn
            // 
            this.leftContentDataGridViewTextBoxColumn.DataPropertyName = "LeftContent";
            this.leftContentDataGridViewTextBoxColumn.HeaderText = "LeftContent";
            this.leftContentDataGridViewTextBoxColumn.Name = "leftContentDataGridViewTextBoxColumn";
            // 
            // middleSheetDataGridViewTextBoxColumn
            // 
            this.middleSheetDataGridViewTextBoxColumn.DataPropertyName = "MiddleSheet";
            this.middleSheetDataGridViewTextBoxColumn.HeaderText = "MiddleSheet";
            this.middleSheetDataGridViewTextBoxColumn.Name = "middleSheetDataGridViewTextBoxColumn";
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            // 
            // tagDataGridViewTextBoxColumn
            // 
            this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
            this.tagDataGridViewTextBoxColumn.HeaderText = "Tag";
            this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            // 
            // book1LeftSheetBindingSourceBindingSource
            // 
            this.book1LeftSheetBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1LeftSheetBindingSource);
            this.book1LeftSheetBindingSourceBindingSource.Position = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel1.Controls.Add(this.book1MiddleSheetBindingSourceBindingNavigator);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.book1RightSheetBindingSourceDataGridView);
            this.splitContainer2.Size = new System.Drawing.Size(638, 423);
            this.splitContainer2.SplitterDistance = 474;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 25);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.book1MiddleSheetBindingSourceDataGridView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.AutoScroll = true;
            this.splitContainer3.Panel2.Controls.Add(this.book1MiddleViewBindingSourceDataGridView);
            this.splitContainer3.Size = new System.Drawing.Size(474, 398);
            this.splitContainer3.SplitterDistance = 158;
            this.splitContainer3.TabIndex = 3;
            // 
            // book1MiddleSheetBindingSourceDataGridView
            // 
            this.book1MiddleSheetBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1MiddleSheetBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1MiddleSheetBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contentDataGridViewTextBoxColumn,
            this.keyDataGridViewTextBoxColumn1,
            this.tagDataGridViewTextBoxColumn1,
            this.timestampDataGridViewTextBoxColumn1});
            this.book1MiddleSheetBindingSourceDataGridView.DataSource = this.book1MiddleSheetBindingSourceBindingSource;
            this.book1MiddleSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1MiddleSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleSheetBindingSourceDataGridView.Name = "book1MiddleSheetBindingSourceDataGridView";
            this.book1MiddleSheetBindingSourceDataGridView.Size = new System.Drawing.Size(474, 158);
            this.book1MiddleSheetBindingSourceDataGridView.TabIndex = 1;
            // 
            // contentDataGridViewTextBoxColumn
            // 
            this.contentDataGridViewTextBoxColumn.DataPropertyName = "Content";
            this.contentDataGridViewTextBoxColumn.HeaderText = "Content";
            this.contentDataGridViewTextBoxColumn.Name = "contentDataGridViewTextBoxColumn";
            // 
            // keyDataGridViewTextBoxColumn1
            // 
            this.keyDataGridViewTextBoxColumn1.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn1.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn1.Name = "keyDataGridViewTextBoxColumn1";
            // 
            // tagDataGridViewTextBoxColumn1
            // 
            this.tagDataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            this.tagDataGridViewTextBoxColumn1.HeaderText = "Tag";
            this.tagDataGridViewTextBoxColumn1.Name = "tagDataGridViewTextBoxColumn1";
            // 
            // timestampDataGridViewTextBoxColumn1
            // 
            this.timestampDataGridViewTextBoxColumn1.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn1.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn1.Name = "timestampDataGridViewTextBoxColumn1";
            // 
            // book1MiddleSheetBindingSourceBindingSource
            // 
            this.book1MiddleSheetBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1MiddleSheetBindingSource);
            this.book1MiddleSheetBindingSourceBindingSource.Position = 0;
            // 
            // book1MiddleViewBindingSourceDataGridView
            // 
            this.book1MiddleViewBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1MiddleViewBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1MiddleViewBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.latestLeftContentDataGridViewTextBoxColumn,
            this.contentDataGridViewTextBoxColumn1,
            this.latestRightContentDataGridViewTextBoxColumn,
            this.keyDataGridViewTextBoxColumn2,
            this.tagDataGridViewTextBoxColumn2,
            this.timestampDataGridViewTextBoxColumn2});
            this.book1MiddleViewBindingSourceDataGridView.DataSource = this.book1MiddleViewBindingSourceBindingSource;
            this.book1MiddleViewBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1MiddleViewBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleViewBindingSourceDataGridView.Name = "book1MiddleViewBindingSourceDataGridView";
            this.book1MiddleViewBindingSourceDataGridView.Size = new System.Drawing.Size(474, 236);
            this.book1MiddleViewBindingSourceDataGridView.TabIndex = 0;
            // 
            // latestLeftContentDataGridViewTextBoxColumn
            // 
            this.latestLeftContentDataGridViewTextBoxColumn.DataPropertyName = "LatestLeftContent";
            this.latestLeftContentDataGridViewTextBoxColumn.HeaderText = "LatestLeftContent";
            this.latestLeftContentDataGridViewTextBoxColumn.Name = "latestLeftContentDataGridViewTextBoxColumn";
            // 
            // contentDataGridViewTextBoxColumn1
            // 
            this.contentDataGridViewTextBoxColumn1.DataPropertyName = "Content";
            this.contentDataGridViewTextBoxColumn1.HeaderText = "Content";
            this.contentDataGridViewTextBoxColumn1.Name = "contentDataGridViewTextBoxColumn1";
            // 
            // latestRightContentDataGridViewTextBoxColumn
            // 
            this.latestRightContentDataGridViewTextBoxColumn.DataPropertyName = "LatestRightContent";
            this.latestRightContentDataGridViewTextBoxColumn.HeaderText = "LatestRightContent";
            this.latestRightContentDataGridViewTextBoxColumn.Name = "latestRightContentDataGridViewTextBoxColumn";
            // 
            // keyDataGridViewTextBoxColumn2
            // 
            this.keyDataGridViewTextBoxColumn2.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn2.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn2.Name = "keyDataGridViewTextBoxColumn2";
            // 
            // tagDataGridViewTextBoxColumn2
            // 
            this.tagDataGridViewTextBoxColumn2.DataPropertyName = "Tag";
            this.tagDataGridViewTextBoxColumn2.HeaderText = "Tag";
            this.tagDataGridViewTextBoxColumn2.Name = "tagDataGridViewTextBoxColumn2";
            // 
            // timestampDataGridViewTextBoxColumn2
            // 
            this.timestampDataGridViewTextBoxColumn2.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn2.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn2.Name = "timestampDataGridViewTextBoxColumn2";
            // 
            // book1MiddleViewBindingSourceBindingSource
            // 
            this.book1MiddleViewBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1MiddleViewBindingSource);
            this.book1MiddleViewBindingSourceBindingSource.Position = 0;
            // 
            // book1MiddleSheetBindingSourceBindingNavigator
            // 
            this.book1MiddleSheetBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.BindingSource = this.book1MiddleSheetBindingSourceBindingSource;
            this.book1MiddleSheetBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem,
            this.toolStripButton1});
            this.book1MiddleSheetBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleSheetBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.Name = "book1MiddleSheetBindingSourceBindingNavigator";
            this.book1MiddleSheetBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.book1MiddleSheetBindingSourceBindingNavigator.Size = new System.Drawing.Size(474, 25);
            this.book1MiddleSheetBindingSourceBindingNavigator.TabIndex = 2;
            this.book1MiddleSheetBindingSourceBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "1";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // book1MiddleSheetBindingSourceBindingNavigatorSaveItem
            // 
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Image")));
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Name = "book1MiddleSheetBindingSourceBindingNavigatorSaveItem";
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.book1MiddleSheetBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(41, 22);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // book1RightSheetBindingSourceDataGridView
            // 
            this.book1RightSheetBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1RightSheetBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1RightSheetBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28});
            this.book1RightSheetBindingSourceDataGridView.DataSource = this.book1RightSheetBindingSourceBindingSource;
            this.book1RightSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1RightSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1RightSheetBindingSourceDataGridView.Name = "book1RightSheetBindingSourceDataGridView";
            this.book1RightSheetBindingSourceDataGridView.Size = new System.Drawing.Size(160, 423);
            this.book1RightSheetBindingSourceDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "RightContent";
            this.dataGridViewTextBoxColumn23.HeaderText = "RightContent";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "MiddleSheet";
            this.dataGridViewTextBoxColumn24.HeaderText = "MiddleSheet";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "RightSheetState";
            this.dataGridViewTextBoxColumn25.HeaderText = "RightSheetState";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn26.HeaderText = "Key";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn27.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn28.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            // 
            // book1RightSheetBindingSourceBindingSource
            // 
            this.book1RightSheetBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1RightSheetBindingSource);
            this.book1RightSheetBindingSourceBindingSource.Position = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.splitContainer1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(796, 423);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.book1LeftSheetBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1LeftSheetBindingSourceBindingSource)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingNavigator)).EndInit();
            this.book1MiddleSheetBindingSourceBindingNavigator.ResumeLayout(false);
            this.book1MiddleSheetBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private BindingSource book1MiddleSheetBindingSourceBindingSource;
        private DataGridView book1LeftSheetBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private BindingSource book1LeftSheetBindingSourceBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private BindingSource book1RightSheetBindingSourceBindingSource;
        private BindingNavigator book1MiddleSheetBindingSourceBindingNavigator;
        private ToolStripButton bindingNavigatorAddNewItem;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorDeleteItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton book1MiddleSheetBindingSourceBindingNavigatorSaveItem;
        private ToolStripButton toolStripButton1;
        private SplitContainer splitContainer3;
        private DataGridView book1MiddleSheetBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridView book1MiddleViewBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private BindingSource book1MiddleViewBindingSourceBindingSource;
        private DataGridViewTextBoxColumn leftContentDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn middleSheetDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn contentDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn latestLeftContentDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn contentDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn latestRightContentDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn2;
        private DataGridView book1RightSheetBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;

    }
}
