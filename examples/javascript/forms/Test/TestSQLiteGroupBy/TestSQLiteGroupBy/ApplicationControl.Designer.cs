using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestSQLiteGroupBy
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
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.applicationWebService1 = new TestSQLiteGroupBy.ApplicationWebService();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator)).BeginInit();
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // book1MiddleAsGroupByGooWithCountBindingSourceBindingSource
            // 
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource.DataSource = typeof(TestSQLiteGroupBy.Data.Book1MiddleAsGroupByGooWithCountBindingSource);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource.Position = 0;
            // 
            // book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator
            // 
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.BindingSource = this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem,
            this.toolStripButton1});
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.Name = "book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator";
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.Size = new System.Drawing.Size(851, 25);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.TabIndex = 1;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
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
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem
            // 
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Image")));
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Name = "book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem";
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // book1MiddleAsGroupByGooWithCountBindingSourceDataGridView
            // 
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.DataSource = this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource;
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.Location = new System.Drawing.Point(0, 25);
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.Name = "book1MiddleAsGroupByGooWithCountBindingSourceDataGridView";
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.Size = new System.Drawing.Size(851, 487);
            this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "GooStateEnum";
            this.dataGridViewTextBoxColumn1.HeaderText = "GooStateEnum";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Count";
            this.dataGridViewTextBoxColumn2.HeaderText = "Count";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FirstKey";
            this.dataGridViewTextBoxColumn3.HeaderText = "FirstKey";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "FirstTitle";
            this.dataGridViewTextBoxColumn4.HeaderText = "FirstTitle";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "LastKey";
            this.dataGridViewTextBoxColumn5.HeaderText = "LastKey";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "LastTitle";
            this.dataGridViewTextBoxColumn6.HeaderText = "LastTitle";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Firstx";
            this.dataGridViewTextBoxColumn7.HeaderText = "Firstx";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Lastx";
            this.dataGridViewTextBoxColumn8.HeaderText = "Lastx";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "SumOfx";
            this.dataGridViewTextBoxColumn9.HeaderText = "SumOfx";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn10.HeaderText = "Key";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn11.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn12.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView);
            this.Controls.Add(this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(851, 512);
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator)).EndInit();
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.ResumeLayout(false);
            this.book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1MiddleAsGroupByGooWithCountBindingSourceDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ApplicationWebService applicationWebService1;
        private BindingSource book1MiddleAsGroupByGooWithCountBindingSourceBindingSource;
        private BindingNavigator book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigator;
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
        private ToolStripButton book1MiddleAsGroupByGooWithCountBindingSourceBindingNavigatorSaveItem;
        private ToolStripButton toolStripButton1;
        private DataGridView book1MiddleAsGroupByGooWithCountBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

    }
}
