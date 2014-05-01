namespace TestSQLJoin.Library
{
    partial class TheView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TheView));
            this.book1TheViewBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.book1TheViewBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.book1TheViewBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.book1TheViewBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dealerContactTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealerTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealerOtherTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealerContactDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealerOtherDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceBindingNavigator)).BeginInit();
            this.book1TheViewBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // book1TheViewBindingSourceBindingSource
            // 
            this.book1TheViewBindingSourceBindingSource.DataSource = typeof(TestSQLJoin.Data.Book1TheViewBindingSource);
            this.book1TheViewBindingSourceBindingSource.Position = 0;
            // 
            // book1TheViewBindingSourceBindingNavigator
            // 
            this.book1TheViewBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.book1TheViewBindingSourceBindingNavigator.BindingSource = this.book1TheViewBindingSourceBindingSource;
            this.book1TheViewBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.book1TheViewBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.book1TheViewBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.book1TheViewBindingSourceBindingNavigatorSaveItem,
            this.toolStripButton1});
            this.book1TheViewBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.book1TheViewBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.book1TheViewBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.book1TheViewBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.book1TheViewBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.book1TheViewBindingSourceBindingNavigator.Name = "book1TheViewBindingSourceBindingNavigator";
            this.book1TheViewBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.book1TheViewBindingSourceBindingNavigator.Size = new System.Drawing.Size(499, 25);
            this.book1TheViewBindingSourceBindingNavigator.TabIndex = 0;
            this.book1TheViewBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // book1TheViewBindingSourceBindingNavigatorSaveItem
            // 
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("book1TheViewBindingSourceBindingNavigatorSaveItem.Image")));
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.Name = "book1TheViewBindingSourceBindingNavigatorSaveItem";
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.book1TheViewBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.Yellow;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.toolStripButton1.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // book1TheViewBindingSourceDataGridView
            // 
            this.book1TheViewBindingSourceDataGridView.AutoGenerateColumns = false;
            this.book1TheViewBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.book1TheViewBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dealerContactTextDataGridViewTextBoxColumn,
            this.dealerTextDataGridViewTextBoxColumn,
            this.dealerOtherTextDataGridViewTextBoxColumn,
            this.dealerContactDataGridViewTextBoxColumn,
            this.dealerDataGridViewTextBoxColumn,
            this.dealerOtherDataGridViewTextBoxColumn,
            this.keyDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.book1TheViewBindingSourceDataGridView.DataSource = this.book1TheViewBindingSourceBindingSource;
            this.book1TheViewBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.book1TheViewBindingSourceDataGridView.Location = new System.Drawing.Point(0, 25);
            this.book1TheViewBindingSourceDataGridView.Name = "book1TheViewBindingSourceDataGridView";
            this.book1TheViewBindingSourceDataGridView.Size = new System.Drawing.Size(499, 111);
            this.book1TheViewBindingSourceDataGridView.TabIndex = 1;
            // 
            // dealerContactTextDataGridViewTextBoxColumn
            // 
            this.dealerContactTextDataGridViewTextBoxColumn.DataPropertyName = "DealerContactText";
            this.dealerContactTextDataGridViewTextBoxColumn.HeaderText = "DealerContactText";
            this.dealerContactTextDataGridViewTextBoxColumn.Name = "dealerContactTextDataGridViewTextBoxColumn";
            // 
            // dealerTextDataGridViewTextBoxColumn
            // 
            this.dealerTextDataGridViewTextBoxColumn.DataPropertyName = "DealerText";
            this.dealerTextDataGridViewTextBoxColumn.HeaderText = "DealerText";
            this.dealerTextDataGridViewTextBoxColumn.Name = "dealerTextDataGridViewTextBoxColumn";
            // 
            // dealerOtherTextDataGridViewTextBoxColumn
            // 
            this.dealerOtherTextDataGridViewTextBoxColumn.DataPropertyName = "DealerOtherText";
            this.dealerOtherTextDataGridViewTextBoxColumn.HeaderText = "DealerOtherText";
            this.dealerOtherTextDataGridViewTextBoxColumn.Name = "dealerOtherTextDataGridViewTextBoxColumn";
            // 
            // dealerContactDataGridViewTextBoxColumn
            // 
            this.dealerContactDataGridViewTextBoxColumn.DataPropertyName = "DealerContact";
            this.dealerContactDataGridViewTextBoxColumn.HeaderText = "DealerContact";
            this.dealerContactDataGridViewTextBoxColumn.Name = "dealerContactDataGridViewTextBoxColumn";
            // 
            // dealerDataGridViewTextBoxColumn
            // 
            this.dealerDataGridViewTextBoxColumn.DataPropertyName = "Dealer";
            this.dealerDataGridViewTextBoxColumn.HeaderText = "Dealer";
            this.dealerDataGridViewTextBoxColumn.Name = "dealerDataGridViewTextBoxColumn";
            // 
            // dealerOtherDataGridViewTextBoxColumn
            // 
            this.dealerOtherDataGridViewTextBoxColumn.DataPropertyName = "DealerOther";
            this.dealerOtherDataGridViewTextBoxColumn.HeaderText = "DealerOther";
            this.dealerOtherDataGridViewTextBoxColumn.Name = "dealerOtherDataGridViewTextBoxColumn";
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
            // TheView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.book1TheViewBindingSourceDataGridView);
            this.Controls.Add(this.book1TheViewBindingSourceBindingNavigator);
            this.Name = "TheView";
            this.Size = new System.Drawing.Size(499, 136);
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceBindingNavigator)).EndInit();
            this.book1TheViewBindingSourceBindingNavigator.ResumeLayout(false);
            this.book1TheViewBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.book1TheViewBindingSourceDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator book1TheViewBindingSourceBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton book1TheViewBindingSourceBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView book1TheViewBindingSourceDataGridView;
        public System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.BindingSource book1TheViewBindingSourceBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerContactTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerOtherTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerContactDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerOtherDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
    }
}
