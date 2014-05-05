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
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1LeftSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.book1MiddleSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1RightSheetBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.book1MiddleSheetBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.book1MiddleViewBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.book1MiddleViewBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingNavigator)).BeginInit();
            this.book1MiddleSheetBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceDataGridView)).BeginInit();
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
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.book1LeftSheetBindingSourceDataGridView.DataSource = this.book1LeftSheetBindingSourceBindingSource;
            this.book1LeftSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1LeftSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1LeftSheetBindingSourceDataGridView.Name = "book1LeftSheetBindingSourceDataGridView";
            this.book1LeftSheetBindingSourceDataGridView.Size = new System.Drawing.Size(154, 423);
            this.book1LeftSheetBindingSourceDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "LeftContent";
            this.dataGridViewTextBoxColumn7.HeaderText = "LeftContent";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "MiddleSheet";
            this.dataGridViewTextBoxColumn8.HeaderText = "MiddleSheet";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn9.HeaderText = "Key";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn10.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn11.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
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
            // book1MiddleSheetBindingSourceBindingSource
            // 
            this.book1MiddleSheetBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1MiddleSheetBindingSource);
            this.book1MiddleSheetBindingSourceBindingSource.Position = 0;
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
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.book1RightSheetBindingSourceDataGridView.DataSource = this.book1RightSheetBindingSourceBindingSource;
            this.book1RightSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1RightSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1RightSheetBindingSourceDataGridView.Name = "book1RightSheetBindingSourceDataGridView";
            this.book1RightSheetBindingSourceDataGridView.Size = new System.Drawing.Size(160, 423);
            this.book1RightSheetBindingSourceDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "RightContent";
            this.dataGridViewTextBoxColumn12.HeaderText = "RightContent";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "MiddleSheet";
            this.dataGridViewTextBoxColumn13.HeaderText = "MiddleSheet";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn14.HeaderText = "Key";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn15.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn16.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // book1RightSheetBindingSourceBindingSource
            // 
            this.book1RightSheetBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1RightSheetBindingSource);
            this.book1RightSheetBindingSourceBindingSource.Position = 0;
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
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.book1MiddleSheetBindingSourceDataGridView.DataSource = this.book1MiddleSheetBindingSourceBindingSource;
            this.book1MiddleSheetBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1MiddleSheetBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleSheetBindingSourceDataGridView.Name = "book1MiddleSheetBindingSourceDataGridView";
            this.book1MiddleSheetBindingSourceDataGridView.Size = new System.Drawing.Size(474, 158);
            this.book1MiddleSheetBindingSourceDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Content";
            this.dataGridViewTextBoxColumn2.HeaderText = "Content";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn4.HeaderText = "Key";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn5.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn6.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // book1MiddleViewBindingSourceBindingSource
            // 
            this.book1MiddleViewBindingSourceBindingSource.DataSource = typeof(ThreeWay.Data.Book1MiddleViewBindingSource);
            this.book1MiddleViewBindingSourceBindingSource.Position = 0;
            // 
            // book1MiddleViewBindingSourceDataGridView
            // 
            this.book1MiddleViewBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1MiddleViewBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1MiddleViewBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22});
            this.book1MiddleViewBindingSourceDataGridView.DataSource = this.book1MiddleViewBindingSourceBindingSource;
            this.book1MiddleViewBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1MiddleViewBindingSourceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleViewBindingSourceDataGridView.Name = "book1MiddleViewBindingSourceDataGridView";
            this.book1MiddleViewBindingSourceDataGridView.Size = new System.Drawing.Size(474, 236);
            this.book1MiddleViewBindingSourceDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "LatestLeftContent";
            this.dataGridViewTextBoxColumn17.HeaderText = "LatestLeftContent";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "Content";
            this.dataGridViewTextBoxColumn18.HeaderText = "Content";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "LatestRightContent";
            this.dataGridViewTextBoxColumn19.HeaderText = "LatestRightContent";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn20.HeaderText = "Key";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn21.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn22.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
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
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceBindingNavigator)).EndInit();
            this.book1MiddleSheetBindingSourceBindingNavigator.ResumeLayout(false);
            this.book1MiddleSheetBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1RightSheetBindingSourceBindingSource)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleSheetBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleViewBindingSourceDataGridView)).EndInit();
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
        private DataGridView book1RightSheetBindingSourceDataGridView;
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

    }
}
